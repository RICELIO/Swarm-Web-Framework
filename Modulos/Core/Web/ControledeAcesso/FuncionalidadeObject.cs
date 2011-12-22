using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.ControledeAcesso
{
    public class Funcionalidade : ModeloObjetoBase
    {
        public Funcionalidade(long id, bool materializarClasses)
            : this()
        {
            this.Materializar(id, materializarClasses);
        }

        public Funcionalidade()
            : base("AcessoFuncionalidade", "IdAcessoFuncionalidade")
        {
        }

        #region Propriedades

        public string GUID { get; private set; }
        public string Titulo { get; set; }
        public string Descrição { get; set; }
        public bool Habilitado { get; set; }
        /// <summary>
        /// Se verdadeiro significa que este item será exibido para o usuário.
        /// </summary>
        public bool Exibir { get; set; }
        public Grupo Grupo { get; set; }

        private List<AcessoMap> ItensMap { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.GUID = Valor.Vazio;
            this.Titulo = Valor.Vazio;
            this.Descrição = Valor.Vazio;
            this.Habilitado = Valor.Ativo;
            this.Exibir = Valor.Inativo;
            this.Grupo = new Grupo();
            base.Log.Reset();

            this.ItensMap = new List<AcessoMap>();

            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (!base.Materializado && Checar.IsCampoVazio(this.GUID))
                this.GUID = AcessoController.GerarGUID(this.TransacaoEnvolvida);
            else if (Checar.IsCampoVazio(this.GUID))
                throw new Exception(Erros.ValorInvalido("Funcionalidade", "GUID"));

            if (Checar.IsCampoVazio(this.Titulo))
                throw new Exception(Erros.ValorInvalido("Funcionalidade", "Titulo"));

            if (Checar.MenorouIgual(this.Grupo.ID))
                throw new Exception(Erros.ValorInvalido("Funcionalidade", "Grupo"));

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("GUID", this.GUID);
            this.ItensPersistencia.Add("Titulo", this.Titulo);
            this.ItensPersistencia.Add("Descricao", this.Descrição, Checar.IsCampoVazio(this.Descrição));
            this.ItensPersistencia.Add("Habilitado", this.Habilitado);
            this.ItensPersistencia.Add("Exibir", this.Exibir);
            this.ItensPersistencia.Add("IdGrupo", this.Grupo.ID);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT GUID, Titulo, Descricao, Habilitado, Exibir, IdGrupo, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.GUID = leitor.GetValor("GUID").ToString();
                this.Titulo = leitor.GetValor("Titulo").ToString();
                this.Descrição = leitor.GetValor("Descricao").ToString();
                this.Habilitado = Conversoes.ToBoolean(leitor.GetValor("Habilitado"));
                this.Exibir = Conversoes.ToBoolean(leitor.GetValor("Exibir"));
                this.Grupo.ID = Conversoes.ToInt64(leitor.GetValor("IdGrupo"));
                base.Log.Materializar(leitor);

                if (materializarClasses)
                    this.Grupo.Materializar();

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos de ligação

        public List<AcessoMap> GetItens()
        {
            if (Checar.MaiorQue(this.ItensMap.Count))
                return this.ItensMap;

            AcessoMap obj = new AcessoMap();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT DISTINCT {0} FROM {1} WHERE Tipo = {2} AND IdAcesso = {3}", obj.ChavePrimaria, obj.Tabela, (int)EnumAcesso.TipodeAcesso.Funcionalidade, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql);
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                this.ItensMap.Add(new AcessoMap(id));
            }
            leitor.Fechar();

            return this.ItensMap;
        }

        #endregion
    }
}