using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.ControledeAcesso
{
    public class Grupo : ModeloObjetoBase
    {
        public Grupo(long id, bool materializarClasses)
            : this()
        {
            this.Materializar(id, materializarClasses);
        }

        public Grupo()
            : base("AcessoGrupo", "IdAcessoGrupo")
        {
        }

        #region Propriedades

        public string GUID { get; private set; }
        public string Titulo { get; set; }
        public bool Habilitado { get; set; }
        /// <summary>
        /// Se verdadeiro significa que este item será exibido para o usuário.
        /// </summary>
        public bool Exibir { get; set; }
        public SuperGrupo SuperGrupo { get; set; }
        /// <summary>
        /// Indefinido - Os itens associados serão aninhados. | 
        /// Individual - Não será exibido o Grupo, mas as Funcionalidades associadas.
        /// </summary>
        public EnumAcesso.CodigoInterno_Grupo CodigoInterno { get; set; }

        private List<Funcionalidade> Funcionalidades { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.GUID = Valor.Vazio;
            this.Titulo = Valor.Vazio;
            this.Habilitado = Valor.Ativo;
            this.Exibir = Valor.Inativo;
            this.SuperGrupo = new SuperGrupo();
            this.CodigoInterno = EnumAcesso.CodigoInterno_Grupo.Indefinido;
            base.Log.Reset();

            this.Funcionalidades = new List<Funcionalidade>();

            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (!base.Materializado && Checar.IsCampoVazio(this.GUID))
                this.GUID = AcessoController.GerarGUID(this.TransacaoEnvolvida);
            else if (Checar.IsCampoVazio(this.GUID))
                throw new Exception(Erros.ValorInvalido("Grupo", "GUID"));

            if (Checar.IsCampoVazio(this.Titulo))
                throw new Exception(Erros.ValorInvalido("Grupo", "Titulo"));

            if (Checar.MenorouIgual(this.SuperGrupo.ID))
                throw new Exception(Erros.ValorInvalido("Grupo", "SuperGrupo"));

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("GUID", this.GUID);
            this.ItensPersistencia.Add("Titulo", this.Titulo);
            this.ItensPersistencia.Add("Habilitado", this.Habilitado);
            this.ItensPersistencia.Add("Exibir", this.Exibir);
            this.ItensPersistencia.Add("IdSuperGrupo", this.SuperGrupo.ID);
            this.ItensPersistencia.Add("CodigoInterno", (int)this.CodigoInterno, this.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Indefinido);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT GUID, Titulo, Habilitado, Exibir, IdSuperGrupo, CodigoInterno, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.GUID = leitor.GetValor("GUID").ToString();
                this.Titulo = leitor.GetValor("Titulo").ToString();
                this.Habilitado = Conversoes.ToBoolean(leitor.GetValor("Habilitado"));
                this.Exibir = Conversoes.ToBoolean(leitor.GetValor("Exibir"));
                this.SuperGrupo.ID = Conversoes.ToInt64(leitor.GetValor("IdSuperGrupo"));
                this.CodigoInterno = (EnumAcesso.CodigoInterno_Grupo)Conversoes.ToInt32(leitor.GetValor("CodigoInterno"));
                base.Log.Materializar(leitor);

                if (materializarClasses)
                    this.SuperGrupo.Materializar();

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos de ligação

        public List<Funcionalidade> GetFuncionalidades()
        {
            if (Checar.MaiorQue(this.Funcionalidades.Count))
                return this.Funcionalidades;

            Funcionalidade obj = new Funcionalidade();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT DISTINCT {0} FROM {1} WHERE IdGrupo = {2}", obj.ChavePrimaria, obj.Tabela, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql);
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                this.Funcionalidades.Add(new Funcionalidade(id, Valor.Inativo));
            }
            leitor.Fechar();

            return this.Funcionalidades;
        }

        #endregion
    }
}