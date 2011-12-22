using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Swarm.Utilitarios;

namespace Swarm.Persistencia
{
    public class LeitorFacade
    {
        #region Propriedades

        private Transacao TransacaoEnvolvida { get; set; }
        private DataSet DataSetEnvolvido { get; set; }
        private int TabelaCorrente { get; set; }
        public int LinhaCorrente { get; private set; }

        private int TotaldeTabelasEnvolvidas
        {
            get { return this.DataSetEnvolvido.Tables.Count; }
        }

        private int TotaldeLinhasEnvolvidas
        {
            get { return this.DataSetEnvolvido.Tables[this.TabelaCorrente].Rows.Count; }
        }

        public int TotaldeLinhas
        {
            get
            {
                if (this.TotaldeTabelasEnvolvidas <= Valor.Zero)
                    return Valor.Zero;

                int indexTabelaEnvolvida = this.TabelaCorrente < Valor.Zero ? Valor.Zero : this.TabelaCorrente;
                return this.DataSetEnvolvido.Tables[indexTabelaEnvolvida].Rows.Count;
            }
        }

        #endregion

        public LeitorFacade(string query)
            : this(new StringBuilder(query))
        {
        }
        public LeitorFacade(string query, Transacao transacao)
            : this(new StringBuilder(query), transacao)
        {
        }

        public LeitorFacade(StringBuilder query)
            : this(query, null)
        {
        }
        public LeitorFacade(StringBuilder query, Transacao transacao)
            : this()
        {
            this.GetDados(query, transacao);
        }

        public LeitorFacade()
        {
            this.Reset();
            this.DataSetEnvolvido = null;
        }

        #region Métodos - Validações

        private bool IsDataSetIndisponivel()
        {
            return Checar.IsNull(this.DataSetEnvolvido);
        }

        private bool IsTabelaIndisponivel(string nomeTabela)
        {
            if (this.TotaldeTabelasEnvolvidas == Valor.Zero)
                return true;

            if (!Checar.IsCampoVazio(nomeTabela) && !this.DataSetEnvolvido.Tables.Contains(nomeTabela))
                return true;

            return false;
        }

        private bool IsLinhaIndisponivel()
        {
            return this.LinhaCorrente >= this.TotaldeLinhasEnvolvidas;
        }

        #endregion

        #region Métodos - Operações comuns

        private void Reset()
        {
            this.LinhaCorrente = Valor.MenosUm;
            this.TabelaCorrente = Valor.MenosUm;
        }

        public void Fechar()
        {
            if (!this.IsDataSetIndisponivel())
                this.DataSetEnvolvido.Dispose();
        }

        #endregion

        #region Métodos - Leitura/Iteração
        public bool LerLinha()
        {
            return this.LerLinha(Valor.Vazio);
        }
        public bool LerLinha(string nomeTabela)
        {
            if (this.IsDataSetIndisponivel())
                return false;

            if (this.IsTabelaIndisponivel(nomeTabela))
                return false;

            this.OperacaoRedefinirTabelaEnvolvida(nomeTabela); // apenas se necessário.
            this.LinhaCorrente++; // definindo próxima linha

            if (this.IsLinhaIndisponivel())
                return false;

            return true;
        }

        private void OperacaoRedefinirTabelaEnvolvida(string nomeTabela)
        {
            if (Checar.IsCampoVazio(nomeTabela))
            {
                this.TabelaCorrente = Valor.Zero;
                return;
            }

            int posicaoTabelaInformada = this.DataSetEnvolvido.Tables.IndexOf(nomeTabela);
            if (this.TabelaCorrente == posicaoTabelaInformada) return;

            this.TabelaCorrente = posicaoTabelaInformada;
            this.LinhaCorrente = Valor.MenosUm;
        }

        #endregion

        #region Métodos - Recuperação de valores disponíveis

        private void GetDados(StringBuilder query, Transacao transacao)
        {
            this.DataSetEnvolvido = SGBDFactory.Criar().RecuperarDataSet(query, transacao);
        }

        public DataSet GetDataSet()
        {
            if (this.IsDataSetIndisponivel())
                return null;

            return this.DataSetEnvolvido;
        }

        public DataTable GetTable()
        {
            return this.GetTable(Valor.Zero, Valor.Vazio, Valor.Vazio);
        }
        public DataTable GetTable(int index)
        {
            return this.GetTable(index, Valor.Vazio, Valor.Vazio);
        }
        public DataTable GetTable(string filterExpression, string sortExpression)
        {
            return this.GetTable(Valor.Zero, filterExpression, sortExpression);
        }
        public DataTable GetTable(int index, string filterExpression, string sortExpression)
        {
            if (this.IsDataSetIndisponivel())
                return null;

            if (Checar.IsCampoVazio(filterExpression) && Checar.IsCampoVazio(sortExpression))
                return this.DataSetEnvolvido.Tables[index];

            DataView data = new DataView(this.DataSetEnvolvido.Tables[index], filterExpression, sortExpression, DataViewRowState.CurrentRows);
            return data.ToTable();
        }

        public object GetValor(string coluna)
        {
            if (this.IsDataSetIndisponivel())
                return null;

            return this.DataSetEnvolvido.Tables[this.TabelaCorrente].Rows[this.LinhaCorrente][coluna];
        }

        public object GetValor(int coluna)
        {
            if (this.IsDataSetIndisponivel())
                return null;

            return this.DataSetEnvolvido.Tables[this.TabelaCorrente].Rows[this.LinhaCorrente][coluna];
        }

        #endregion
    }
}
