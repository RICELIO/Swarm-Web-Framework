using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.Configuracao
{
    public class ControledeVersao : ModeloObjetoBase
    {
        public ControledeVersao(long id)
            : this()
        {
            this.Materializar(id);
        }

        public ControledeVersao()
            : base("ControleVersao", "IdControleVersao")
        {
        }

        #region Propriedades

        public string Versao { get; set; }
        public string Notas { get; set; }
        public DateTime LogdeInstalacao { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.Versao = Valor.Vazio;
            this.Notas = Valor.Vazio;
            this.LogdeInstalacao = DateTime.Now;
            base.Log.Reset();
            base.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (Checar.IsCampoVazio(this.Versao))
                throw new Exception(Erros.ValorInvalido("Controle de Versão", "Versão"));

            if (Checar.IsCampoVazio(this.Notas))
                throw new Exception(Erros.ValorInvalido("Controle de Versão", "Notas"));

            if (!Checar.IsDataValida(this.LogdeInstalacao))
                throw new Exception(Erros.ValorInvalido("Controle de Versão", "Log de Instalação"));

            base.Log.Validar();

            return Valor.Ativo;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("Versao", this.Versao);
            this.ItensPersistencia.Add("Notas", this.Notas);
            this.ItensPersistencia.Add("Log", this.LogdeInstalacao);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT *
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql);
            if (leitor.LerLinha())
            {
                this.Versao = leitor.GetValor("Versao").ToString();
                this.Notas = leitor.GetValor("Notas").ToString();
                this.LogdeInstalacao = Conversoes.ToDateTime(leitor.GetValor("Log"));
                base.Log.Materializar(leitor);

                base.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion
    }
}
