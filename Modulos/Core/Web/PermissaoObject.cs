using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Core.Web
{
    public class Permissao : ModeloObjetoBase
    {
        public Permissao(long id, bool materializarClasses)
            : this()
        {
            this.Materializar(id, materializarClasses);
        }

        public Permissao()
            : base("Permissao", "IdPermissao")
        {
        }

        #region Propriedades

        public Usuario Usuario { get; set; }
        public GrupoUsuario Grupo { get; set; }
        public string GUID { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.Usuario = UsuarioController.Create();
            this.Grupo = GrupoUsuarioController.Create();
            this.GUID = Valor.Vazio;
            base.Log.Reset();
            base.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (Checar.MenorouIgual(this.Usuario.ID) && Checar.MenorouIgual(this.Grupo.ID))
                throw new Exception(Erros.ValorInvalido("Permissão", "Usuário / Grupo"));

            if (Checar.IsCampoVazio(this.GUID))
                throw new Exception(Erros.ValorInvalido("Permissão", "GUID"));

            if (this.IsCausaráDuplicação())
                throw new Exception("Permissão - A permissão informada já existe. Não é permitido a existência de duplicações.");

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("IdUsuario", this.Usuario.ID, Checar.MenorouIgual(this.Usuario.ID));
            this.ItensPersistencia.Add("IdGrupo", this.Grupo.ID, Checar.MenorouIgual(this.Grupo.ID));
            this.ItensPersistencia.Add("GUID", this.GUID);
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
                this.Usuario.ID = Conversoes.ToInt64(leitor.GetValor("IdUsuario"));
                this.Grupo.ID = Conversoes.ToInt64(leitor.GetValor("IdGrupo"));
                this.GUID = leitor.GetValor("GUID").ToString();
                base.Log.Materializar(leitor);

                if (materializarClasses)
                {
                    this.Usuario.Materializar();
                    this.Grupo.Materializar();
                }

                this.Materializado = Valor.Ativo;
            }
        }

        #endregion

        #region Métodos internos

        private bool IsCausaráDuplicação()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT *
            FROM {0}
            WHERE   1=1
                AND GUID = '{1}'
                {2}
                {3}
                {4}
            ", this.Tabela, this.GUID,
            Query.MaiorQue(this.Usuario.ID, "AND IdUsuario = {0}", this.Usuario.ID),
            Query.MaiorQue(this.Grupo.ID, "AND IdGrupo = {0}", this.Grupo.ID),
            Query.MaiorQue(this.ID, "AND {0} <> {1}", this.ChavePrimaria, this.ID));

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            bool possuiRegistro = Checar.MaiorQue(leitor.TotaldeLinhas);
            leitor.Fechar();

            return possuiRegistro;
        }

        #endregion
    }
}
