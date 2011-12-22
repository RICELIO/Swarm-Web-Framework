using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;
using Swarm.Utilitarios;

namespace Swarm.Persistencia.BancosdeDados
{
    public class Mysql : SGBD
    {
        public Mysql() { }

        #region Métodos

        internal override string GetStringdeConexao()
        {
            return "Server={0};Database={1};User ID={2};Password={3};";
        }

        internal override DbConnection GetObjConexao()
        {
            return new MySqlConnection(Conexão.Instance.GetConfiguracoes());
        }

        internal override string Executar(StringBuilder querySql, Transacao transacao, EnumPersistencia.Operacao tipodeOperacao)
        {
            string retorno = Valor.Vazio;

            EnumPersistencia.Iteracao tipodePersistencia = Checar.IsNull(transacao) ?
                                                           EnumPersistencia.Iteracao.Normal :
                                                           EnumPersistencia.Iteracao.Colecao;

            MySqlTransaction transacaoSql = tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) ? null : transacao.TransacaoEnvolvida as MySqlTransaction;
            MySqlConnection conexaoSql = tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) ? null : transacao.Conexao as MySqlConnection;
            MySqlCommand comandoSql = null;

            try
            {
                if (tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal))
                {
                    // Abrindo conexão
                    conexaoSql = new MySqlConnection(Conexão.Instance.GetConfiguracoes());
                    conexaoSql.Open();
                }

                // recuperando instância de MySqlCommand
                comandoSql = tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) ?
                             conexaoSql.CreateCommand() :
                             transacao.Conexao.CreateCommand() as MySqlCommand;

                // Iniciando a transação
                if (tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal))
                    transacaoSql = conexaoSql.BeginTransaction();

                // Definindo conexao e transação para MySqlCommand
                comandoSql.Connection = conexaoSql;
                comandoSql.Transaction = transacaoSql;

                // Definindo query para comandoSql
                comandoSql.CommandText = querySql.ToString();

                // executando query
                if (tipodeOperacao == EnumPersistencia.Operacao.Incluir)
                {
                    object resultado = comandoSql.ExecuteScalar();
                    if (!Checar.IsNull(resultado)) retorno = resultado.ToString();
                }
                else
                    retorno = comandoSql.ExecuteNonQuery().ToString();

                // integridade referencial, confirmando execução.
                if (tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal))
                    transacaoSql.Commit();
            }
            catch (Exception ex)
            {
                if (tipodePersistencia == EnumPersistencia.Iteracao.Normal)
                {
                    try
                    {
                        transacaoSql.Rollback(); // integridade referencial, desfazendo execução.
                    }
                    catch (MySqlException e)
                    {
                        if (!Checar.IsNull(transacaoSql.Connection))
                            throw new Exception("Uma exceção do tipo " + e.GetType() + " foi encontrado ao tentar desfazer a transação. Erro: " + e.Message);
                    }
                }

                throw new Exception("Uma exceção do tipo " + ex.GetType() + " foi encontrado enquanto a query estava sendo processada. Erro: " + ex.Message);
            }
            finally
            {
                if (tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) && !Checar.IsNull(conexaoSql)) conexaoSql.Close();
                if (!Checar.IsNull(comandoSql)) comandoSql.Dispose();
                if (tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) && !Checar.IsNull(transacaoSql)) transacaoSql.Dispose();
            }

            return retorno;
        }

        internal override DataSet RecuperarDataSet(StringBuilder querySql, Transacao transacao)
        {
            bool isTransacaoInterna = Checar.IsNull(transacao) || transacao.ConexaoFechada;

            DataSet dataSet = new DataSet();
            MySqlConnection conexaoSql = null;
            MySqlDataAdapter adaptadorSql = null;

            try
            {
                // Abrindo conexão
                if (isTransacaoInterna)
                {
                    conexaoSql = new MySqlConnection(Conexão.Instance.GetConfiguracoes());
                    conexaoSql.Open();
                }
                else
                    conexaoSql = (MySqlConnection)transacao.Conexao;

                // instanciando DataAdapter
                adaptadorSql = new MySqlDataAdapter(querySql.ToString(), conexaoSql);

                // definindo transação se informado
                if (!isTransacaoInterna)
                    adaptadorSql.SelectCommand.Transaction = (MySqlTransaction)transacao.TransacaoEnvolvida;

                // executando query e preenchendo dataSet.
                adaptadorSql.Fill(dataSet);
            }
            catch (Exception ex)
            {
                throw new Exception("Uma exceção do tipo " + ex.GetType() + " foi encontrado enquanto a query estava sendo processada. Erro: " + ex.Message);
            }
            finally
            {
                if (!Checar.IsNull(conexaoSql) && isTransacaoInterna) conexaoSql.Close();
                if (!Checar.IsNull(adaptadorSql)) adaptadorSql.Dispose();
            }

            return dataSet;
        }

        #endregion

        #region Métodos - Sintaxe

        internal override string GetSqlChavePrimariaInserida()
        {
            return "SELECT LAST_INSERT_ID() AS ID";
        }

        internal override string GetSqlLimitadordeRegistros(int registros)
        {
            return string.Format("LIMIT {0}", registros);
        }

        #endregion
    }
}
