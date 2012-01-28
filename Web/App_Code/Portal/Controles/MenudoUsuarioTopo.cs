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

namespace Swarm.Web.Code.Portal.Controles
{
    public class MenudoUsuarioTopo : ViewBase
    {
        private List<AcessoMapForm> Itens { get; set; }

        public MenudoUsuarioTopo()
            : base("Portal/Controles/MenuUsuarioTopo.vm")
        {
            this.Itens = SecuritySettings.ItensdeMenu;
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public bool IsVisitante()
        {
            return !UsuarioCorrenteFacade.Instance.Autenticado;
        }

        public List<AcessoMapForm> GetItensEnvolvidos()
        {
            return this.Itens;
        }

        public int GetValorEnum(EnumAcesso.TipodeAcesso tipo)
        {
            return (int)tipo;
        }

        public bool IsUltimoItem(AcessoMapForm item)
        {
            return this.Itens.IndexOf(item) == (this.Itens.Count - Valor.Um);
        }

        public string GetURI(object objID, object objTipodeAcesso)
        {
            string uri = Valor.Vazio;
            int id = Conversoes.ToInt32(objID);
            EnumAcesso.TipodeAcesso enumTipodeAcesso = (EnumAcesso.TipodeAcesso)objTipodeAcesso;

            switch (enumTipodeAcesso)
            {
                default:
                case EnumAcesso.TipodeAcesso.Ambiente:
                case EnumAcesso.TipodeAcesso.Indefinido:
                    return Navigation.GetURI(id);
                case EnumAcesso.TipodeAcesso.SuperGrupo:
                case EnumAcesso.TipodeAcesso.Grupo:
                    string parametros = string.Format("UrlID={0}&TipoAcesso={1}", id, (int)enumTipodeAcesso);
                    return Navigation.GetURI(Map.Portal.VisualizarControlesdeAcesso, parametros);
                case EnumAcesso.TipodeAcesso.Funcionalidade:
                    return string.Format("javascript:{0}", Javascript.OpenWindow(null, Janela.Size.Normal, Navigation.GetURI(id), Janela.Altura, Janela.Largura, Valor.Ativo));
            }

            return uri;
        }

        #endregion
    }
}