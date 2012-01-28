using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Helpers.Web;
using Swarm.Core.Web;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Core.Web.FrontController;

namespace Swarm.Web.Code.Portal.Views
{
    public class VisualizarDetalhesItensGrupo : ViewBase
    {
        private Grupo Grupo { get; set; }

        public VisualizarDetalhesItensGrupo(long id)
            : base("Portal/VisualizarDetalhesItensGrupo.vm")
        {
            SecuritySettings.Ambientes.FirstOrDefault(amb => amb.GUID == UsuarioCorrenteFacade.Environment).GetSuperGrupos().ForEach(supG =>
            {
                Grupo obj = supG.GetGrupos().Find(grupo => grupo.ID == id);
                if (!Checar.IsNull(obj))
                {
                    this.Grupo = obj;
                    return;
                }
            });
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public string GetTitulo()
        {
            string tituloSuperGrupo = (this.Grupo.SuperGrupo.LazyInstation() as SuperGrupo).Titulo;
            string tituloAmbiente = (this.Grupo.SuperGrupo.Ambiente.LazyInstation() as Ambiente).Titulo;
            return string.Format("{0} > {1} > {2}", tituloAmbiente, tituloSuperGrupo, this.Grupo.Titulo);
        }

        public List<AcessoMapForm> GetFuncionalidades()
        {
            List<AcessoMapForm> itens = new List<AcessoMapForm>();
            this.Grupo.GetFuncionalidades().ForEach(obj =>
                {
                    if (obj.Habilitado && obj.Exibir)
                    {
                        AcessoMap mapFuncionalidade = obj.GetItens().First(map => map.Principal);
                        itens.Add(new AcessoMapForm(obj.Titulo, obj.Descrição, mapFuncionalidade.UrlMapID, EnumAcesso.TipodeAcesso.Funcionalidade));
                    }
                });
            return itens;
        }

        public string GetURI(long urlMapID)
        {
            return string.Format("javascript:{0}", Javascript.OpenWindow(null, Janela.Size.Normal, Navigation.GetURI((int)urlMapID), Janela.Altura, Janela.Largura, Valor.Ativo));
        }

        #endregion
    }
}