using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Persistencia
{
    public class ListaItensPersistencia
    {
        #region Propriedades

        private string Tabela { get; set; }
        private string ChavePrimaria { get; set; }
        private List<ItemPersistencia> ItensEnvolvidos { get; set; }

        public string FormatoPadraoDataHora
        {
            get { return SGBDFactory.Criar().GetFormatoPadraoDateTime(); }
        }

        #endregion

        public ListaItensPersistencia(string nomeTabela, string chavePrimaria)
        {
            this.Tabela = nomeTabela;
            this.ChavePrimaria = chavePrimaria;

            this.ItensEnvolvidos = new List<ItemPersistencia>();
        }

        #region Métodos

        public void Add(string nome, object valor)
        {
            this.Add(nome, valor, valor.GetType(), false);
        }
        public void Add(string nome, object valor, Type tipo)
        {
            this.Add(nome, valor, tipo, false);
        }
        public void Add(string nome, object valor, bool isValorNulo)
        {
            this.Add(nome, valor, valor.GetType(), isValorNulo);
        }
        public void Add(string nome, object valor, Type tipo, bool isValorNulo)
        {
            ItemPersistencia item = new ItemPersistencia(nome, isValorNulo ? null : valor, tipo);

            ItemPersistencia itemEquivalente = this.ItensEnvolvidos.FirstOrDefault(obj => obj.Nome == item.Nome);
            if (Checar.IsNull(itemEquivalente))
            {
                this.ItensEnvolvidos.Add(item);
                return;
            }

            // Atualizando item já existente
            itemEquivalente.Tipo = item.Tipo;
            itemEquivalente.Valor = item.Valor;
        }

        public void Limpar()
        {
            this.ItensEnvolvidos.Clear();
        }

        public bool Possui(string nome)
        {
            return !Checar.IsNull(this.ItensEnvolvidos.Find(item => item.Nome == this.ChavePrimaria));
        }

        #endregion

        #region Métodos - SQL

        public long Inserir(Transacao transacao)
        {
            int contadorColunas = 0;
            int contadorValores = 0;
            int totaldeItensEnvolvidos = this.ItensEnvolvidos.Count;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("INSERT INTO {0}\n", this.Tabela);
            sql.AppendLine("(");
            foreach (ItemPersistencia item in this.ItensEnvolvidos)
            {
                contadorColunas++;
                if (this.IsDesconsiderarChavePrimariaNaInsercao(item)) continue;
                sql.AppendFormat("{0}{1}\n", item.Nome, Valor.InserirVirgula(contadorColunas, totaldeItensEnvolvidos));
            }
            sql.AppendLine(")");
            sql.AppendLine("VALUES");
            sql.AppendLine("(");
            foreach (ItemPersistencia item in this.ItensEnvolvidos)
            {
                contadorValores++;
                if (this.IsDesconsiderarChavePrimariaNaInsercao(item))
                    continue;
                if (item.Nome.Equals(this.ChavePrimaria) || Checar.IsNull(item.Valor))
                    sql.AppendFormat("NULL{0}\n", Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                else if (Checar.IsMesmoTipo(item.Tipo, typeof(string)) || Checar.IsMesmoTipo(item.Tipo, typeof(char)))
                    sql.AppendFormat("'{0}'{1}\n", Tratar.SqlInjection(item.Valor, Valor.Ativo), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                else if (Checar.IsMesmoTipo(item.Tipo, typeof(DateTime)))
                {
                    DateTime dataHora = Conversoes.ToDateTime(item.Valor);
                    sql.AppendFormat("'{0}'{1}\n", Tratar.SqlInjection(dataHora.ToString(this.FormatoPadraoDataHora), Valor.Ativo), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                }
                else if (Checar.IsMesmoTipo(item.Tipo, typeof(Boolean)))
                {
                    string strValor = Conversoes.ToBoolean(item.Valor) ? Valor.Um.ToString() : Valor.Zero.ToString();
                    sql.AppendFormat("{0}{1}\n", Tratar.SqlInjection(strValor), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                }
                else
                    sql.AppendFormat("{0}{1}\n", Tratar.SqlInjection(item.Valor), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
            }
            sql.AppendLine(");");
            sql.AppendLine(SGBDFactory.Criar().GetSqlChavePrimariaInserida());

            return Conversoes.ToInt64(SGBDFactory.Criar().Executar(sql, transacao, EnumPersistencia.Operacao.Incluir));
        }

        public bool Alterar(Transacao transacao)
        {
            List<ItemPersistencia> listaItensEnvolvidos = this.ItensEnvolvidos.FindAll(item => item.Nome != this.ChavePrimaria);

            int contadorValores = 0;
            int totaldeItensEnvolvidos = listaItensEnvolvidos.Count;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("UPDATE {0}\n", this.Tabela);
            sql.AppendLine("SET");
            foreach (ItemPersistencia item in listaItensEnvolvidos)
            {
                contadorValores++;

                if (item.Valor == null)
                    sql.AppendFormat("{0} = NULL{1}\n", item.Nome, Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                else if (Checar.IsMesmoTipo(item.Tipo, typeof(string)))
                    sql.AppendFormat("{0} = '{1}'{2}\n", item.Nome, Tratar.SqlInjection(item.Valor, Valor.Ativo), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                else if (Checar.IsMesmoTipo(item.Tipo, typeof(DateTime)))
                {
                    DateTime dataHora = Conversoes.ToDateTime(item.Valor);
                    sql.AppendFormat("{0} = '{1}'{2}\n", item.Nome, Tratar.SqlInjection(dataHora.ToString(this.FormatoPadraoDataHora), Valor.Ativo), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                }
                else if (Checar.IsMesmoTipo(item.Tipo, typeof(Boolean)))
                {
                    string strValor = Conversoes.ToBoolean(item.Valor) ? Valor.Um.ToString() : Valor.Zero.ToString();
                    sql.AppendFormat("{0} = {1}{2}\n", item.Nome, Tratar.SqlInjection(strValor), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
                }
                else
                    sql.AppendFormat("{0} = {1}{2}\n", item.Nome, Tratar.SqlInjection(item.Valor), Valor.InserirVirgula(contadorValores, totaldeItensEnvolvidos));
            }
            sql.AppendFormat("WHERE {0} = {1}", this.ChavePrimaria, this.ItensEnvolvidos.First(item => item.Nome == this.ChavePrimaria).Valor);

            return Conversoes.ToInt64(SGBDFactory.Criar().Executar(sql, transacao, EnumPersistencia.Operacao.Alterar)) > Valor.Zero;
        }

        public bool Remover(Transacao transacao)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("DELETE FROM {0}\n", this.Tabela);
            sql.AppendFormat("WHERE {0} = {1}", this.ChavePrimaria, this.ItensEnvolvidos.First(item => item.Nome == this.ChavePrimaria).Valor);

            return Conversoes.ToInt64(SGBDFactory.Criar().Executar(sql, transacao, EnumPersistencia.Operacao.Excluir)) > Valor.Zero;
        }

        #endregion

        #region Métodos Primários

        private bool IsDesconsiderarChavePrimariaNaInsercao(ItemPersistencia item)
        {
            return Conexão.Instance.SGBD == EnumPersistencia.SGBD.SqlServer && item.Nome.Equals(this.ChavePrimaria);
        }

        #endregion
    }
}
