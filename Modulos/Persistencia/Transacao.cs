using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Swarm.Persistencia
{
    public class Transacao
    {
        #region Propriedades

        internal DbConnection Conexao { get; set; }
        internal DbTransaction TransacaoEnvolvida { get; set; }
        public bool ConexaoFechada { get; private set; }
        
        #endregion

        public Transacao()
        {
            this.ConexaoFechada = false;
        }

        #region Métodos
        
        public void Iniciar()
        {
            this.Conexao = SGBDFactory.Criar().GetObjConexao();
            this.Conexao.Open();

            this.TransacaoEnvolvida = this.Conexao.BeginTransaction();
        }

        private void Fechar()
        {
            if (!this.ConexaoFechada)
            {
                this.Conexao.Close();
                this.TransacaoEnvolvida.Dispose();
                this.ConexaoFechada = true;
            }
        }
        
        #endregion

        #region Métodos para Controle da Transação
        
        public void Commit()
        {
            try
            {
                this.TransacaoEnvolvida.Commit();
            }
            catch { this.Rollback(); }
            finally { this.Fechar(); }
        }

        public void Rollback()
        {
            try
            {
                this.TransacaoEnvolvida.Rollback();
            }
            finally { this.Fechar(); }
        }
        
        #endregion
    }
}