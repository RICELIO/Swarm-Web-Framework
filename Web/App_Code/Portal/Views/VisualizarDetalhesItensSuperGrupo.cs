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
    public class VisualizarDetalhesItensSuperGrupo : ViewBase
    {
        private SuperGrupo SuperGrupo { get; set; }

        public VisualizarDetalhesItensSuperGrupo(long id)
            : base("Portal/VisualizarDetalhesItensSuperGrupo.vm")
        {
            this.SuperGrupo = SecuritySettings.Ambientes.FirstOrDefault(amb => amb.GUID == UsuarioCorrenteFacade.Environment).GetSuperGrupos().FirstOrDefault(sg => sg.ID == id);
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public string GetTitulo()
        {
            return string.Format("{0} > {1}", (this.SuperGrupo.Ambiente.LazyInstation() as Ambiente).Titulo, this.SuperGrupo.Titulo);
        }

        public List<AcessoMapForm> GetGrupos()
        {
            List<AcessoMapForm> itens = new List<AcessoMapForm>();
            this.SuperGrupo.GetGrupos().ForEach(obj =>
            {
                if (obj.Habilitado && obj.Exibir && obj.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Indefinido)
                    itens.Add(new AcessoMapForm(obj.Titulo, obj.ID, EnumAcesso.TipodeAcesso.Grupo));
            });
            return itens;
        }

        public List<AcessoMapForm> GetFuncionalidades(long id)
        {
            List<AcessoMapForm> itens = new List<AcessoMapForm>();
            Grupo objGrupo = this.SuperGrupo.GetGrupos().FirstOrDefault(obj => obj.ID == id);
            if (!Checar.IsNull(objGrupo) && Checar.MaiorQue(objGrupo.ID))
                objGrupo.GetFuncionalidades().ForEach(obj =>
                    {
                        if (obj.Habilitado && obj.Exibir)
                        {
                            AcessoMap mapFuncionalidade = obj.GetItens().First(map => map.Principal);
                            itens.Add(new AcessoMapForm(obj.Titulo, mapFuncionalidade.UrlMapID, EnumAcesso.TipodeAcesso.Funcionalidade));
                        }
                    });
            return itens;
        }

        public string GetURI(long urlMapID, EnumAcesso.TipodeAcesso enumTipoAcesso)
        {
            switch (enumTipoAcesso)
            {
                default:
                case EnumAcesso.TipodeAcesso.Indefinido:
                case EnumAcesso.TipodeAcesso.Ambiente:
                case EnumAcesso.TipodeAcesso.SuperGrupo:
                    return Valor.Tralha;
                case EnumAcesso.TipodeAcesso.Grupo:
                    {
                        string parametros = string.Format("TipoAcesso={0}&UrlID={1}", (int)enumTipoAcesso, urlMapID);
                        return Navigation.GetURI(Map.Portal.VisualizarControlesdeAcesso, parametros);
                    }
                case EnumAcesso.TipodeAcesso.Funcionalidade:
                    return string.Format("javascript:{0}", Javascript.OpenWindow(null, Janela.Size.Normal, Navigation.GetURI((int)urlMapID), Janela.Altura, Janela.Largura, Valor.Ativo));
            }            
        }

        #endregion
    }
}