using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Persistencia.BancosdeDados
{
    public class SqlServer : SGBD
    {
        #region Métodos

        internal override string GetStringdeConexao()
        {
            return "Data Source={0};Initial Catalog={1};User Id={2};Password={3};";
        }

        internal override DbConnection GetObjConexao()
        {
            return new SqlConnection(Conexão.Instance.GetConfiguracoes());
        }

        internal override string Executar(StringBuilder querySql, Transacao transacao, EnumPersistencia.Operacao tipodeOperacao)
        {
            string retorno = Valor.Vazio;

            EnumPersistencia.Iteracao tipodePersistencia = Checar.IsNull(transacao) ?
                                                           EnumPersistencia.Iteracao.Normal :
                                                           EnumPersistencia.Iteracao.Colecao;

            SqlTransaction transacaoSql = tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) ? null : transacao.TransacaoEnvolvida as SqlTransaction;
            SqlConnection conexaoSql = tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) ? null : transacao.Conexao as SqlConnection;
            SqlCommand comandoSql = null;

            try
            {
                if (tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal))
                {
                    // Abrindo conexão
                    conexaoSql = new SqlConnection(Conexão.Instance.GetConfiguracoes());
                    conexaoSql.Open();
                }

                // recuperando instância de MySqlCommand
                comandoSql = tipodePersistencia.Equals(EnumPersistencia.Iteracao.Normal) ?
                             conexaoSql.CreateCommand() :
                             transacao.Conexao.CreateCommand() as SqlCommand;

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
                    catch (SqlException e)
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
            SqlConnection conexaoSql = null;
            SqlDataAdapter adaptadorSql = null;

            try
            {
                // abrindo conexão
                if (isTransacaoInterna)
                {
                    conexaoSql = new SqlConnection(Conexão.Instance.GetConfiguracoes());
                    conexaoSql.Open();
                }
                else
                    conexaoSql = (SqlConnection)transacao.Conexao;

                // instanciando DataAdapter
                adaptadorSql = new SqlDataAdapter(querySql.ToString(), conexaoSql);

                // definindo transação se informado
                if (!isTransacaoInterna)
                    adaptadorSql.SelectCommand.Transaction = (SqlTransaction)transacao.TransacaoEnvolvida;

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
            return "SELECT @@IDENTITY AS ID";
        }

        internal override string GetSqlLimitadordeRegistros(int registros)
        {
            return string.Format("TOP {0}", registros);
        }

        #endregion
    }
}
