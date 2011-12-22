using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Core.Library.Seguranca
{
    public class GrupoUsuario : ModeloObjetoBase
    {
        public GrupoUsuario()
            : base("GrupoUsuario", "IdGrupoUsuario")
        {
        }

        public GrupoUsuario(long id, bool materializarClasses)
            : this()
        {
            this.Materializar(id, materializarClasses);
        }

        #region Propriedades

        public Grupo Grupo { get; set; }
        public Usuario Usuario { get; set; }

        #endregion

        protected override void Reset()
        {
            this.Grupo = GrupoController.Create();
            this.Usuario = UsuarioController.Create();
            base.Log.Reset();
            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (Checar.MenorouIgual(this.Grupo.ID))
                throw new Exception(Erros.ValorInvalido("Grupo & Usuário", "Identificador do Grupo"));

            if (Checar.MenorouIgual(this.Usuario.ID))
                throw new Exception(Erros.ValorInvalido("Grupo & Usuário", "Identificador do Usuário"));

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            base.ItensPersistencia.Add("IdGrupo", this.Grupo.ID);
            base.ItensPersistencia.Add("IdUsuario", this.Usuario.ID);
            base.Log.PreencherItensPersistencia(base.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT IdGrupo, IdUsuario, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.Grupo.ID = Conversoes.ToInt64(leitor.GetValor("IdGrupo"));
                this.Usuario.ID = Conversoes.ToInt64(leitor.GetValor("IdUsuario"));

                if (materializarClasses)
                {
                    this.Grupo.Materializar();
                    this.Usuario.Materializar();
                }

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }
    }
}
