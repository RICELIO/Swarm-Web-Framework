using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.ControledeAcesso
{
    public class Ambiente : ModeloObjetoBase
    {
        public Ambiente(long id)
            : this()
        {
            this.Materializar(id, false);
        }

        public Ambiente()
            : base("AcessoAmbiente", "IdAcessoAmbiente")
        {
        }

        #region Propriedades

        public string GUID { get; private set; }
        public string Titulo { get; set; }
        public bool Habilitado { get; set; }
        /// <summary>
        /// Se verdadeiro significa que apenas usuários autenticados poderão ter acesso ao ambiente.
        /// </summary>
        public bool Restrito { get; set; }
        /// <summary>
        /// Indefinido - Itens que não possuírão manipulação especial no core. | 
        /// CallBack - Todas as páginas de CallBack serão aninhadas neste ambiente. | 
        /// Anonimo - Todas as páginas que não serão controladas por autenticação serão aninhadas neste ambiente.
        /// </summary>
        public EnumAcesso.CodigoInterno_Ambiente CodigoInterno { get; set; }

        private AcessoMap ItemMap { get; set; }
        private List<SuperGrupo> SuperGrupos { get; set; }
        private List<Funcionalidade> Funcionalidades { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.GUID = Valor.Vazio;
            this.Titulo = Valor.Vazio;
            this.Habilitado = Valor.Ativo;
            this.Restrito = Valor.Inativo;
            this.CodigoInterno = EnumAcesso.CodigoInterno_Ambiente.Indefinido;
            base.Log.Reset();

            this.ItemMap = new AcessoMap();
            this.SuperGrupos = new List<SuperGrupo>();
            this.Funcionalidades = new List<Funcionalidade>();

            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (!base.Materializado && Checar.IsCampoVazio(this.GUID))
                this.GUID = AcessoController.GerarGUID(this.TransacaoEnvolvida);
            else if (Checar.IsCampoVazio(this.GUID))
                throw new Exception(Erros.ValorInvalido("Ambiente", "GUID"));

            if (Checar.IsCampoVazio(this.Titulo))
                throw new Exception(Erros.ValorInvalido("Ambiente", "Titulo"));

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("GUID", this.GUID);
            this.ItensPersistencia.Add("Titulo", this.Titulo);
            this.ItensPersistencia.Add("Habilitado", this.Habilitado);
            this.ItensPersistencia.Add("Restrito", this.Restrito);
            this.ItensPersistencia.Add("CodigoInterno", (int)this.CodigoInterno, this.CodigoInterno == EnumAcesso.CodigoInterno_Ambiente.Indefinido);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT GUID, Titulo, Habilitado, Restrito, CodigoInterno, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.GUID = leitor.GetValor("GUID").ToString();
                this.Titulo = leitor.GetValor("Titulo").ToString();
                this.Habilitado = Conversoes.ToBoolean(leitor.GetValor("Habilitado"));
                this.Restrito = Conversoes.ToBoolean(leitor.GetValor("Restrito"));
                this.CodigoInterno = (EnumAcesso.CodigoInterno_Ambiente)Conversoes.ToInt32(leitor.GetValor("CodigoInterno"));
                base.Log.Materializar(leitor);

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos de ligação

        public AcessoMap GetItemBase()
        {
            if (Checar.MaiorQue(this.ItemMap.ID))
                return this.ItemMap;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT DISTINCT {0} FROM {1} WHERE Tipo = {2} AND IdAcesso = {3} AND Principal = 1", this.ItemMap.ChavePrimaria, this.ItemMap.Tabela, (int)EnumAcesso.TipodeAcesso.Ambiente, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql);
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(this.ItemMap.ChavePrimaria));
                this.ItemMap = new AcessoMap(id);
            }
            leitor.Fechar();

            return this.ItemMap;
        }

        public List<SuperGrupo> GetSuperGrupos()
        {
            if (Checar.MaiorQue(this.Funcionalidades.Count))
                return this.SuperGrupos;

            SuperGrupo obj = new SuperGrupo();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT DISTINCT {0} FROM {1} WHERE IdAmbiente = {2}", obj.ChavePrimaria, obj.Tabela, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql);
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                this.SuperGrupos.Add(new SuperGrupo(id, Valor.Inativo));
            }
            leitor.Fechar();

            return this.SuperGrupos;
        }

        #endregion

        #region Métodos Auxiliares

        public string FindGUID(int pageID)
        {
            string guidEnvolvido = Valor.Vazio;

            if (this.GetItemBase().UrlMapID == pageID)
                guidEnvolvido = this.GUID;
            else
                this.GetSuperGrupos().ForEach(sg =>
                    {
                        sg.GetGrupos().ForEach(g =>
                            {
                                g.GetFuncionalidades().ForEach(f =>
                                    {
                                        bool paginaEnvolvida = !Checar.IsNull(f.GetItens().Find(obj => obj.UrlMapID == pageID));
                                        if (paginaEnvolvida)
                                        {
                                            guidEnvolvido = f.GUID;
                                            return;
                                        }
                                    });
                            });
                    });

            return guidEnvolvido;
        }

        #endregion
    }
}
