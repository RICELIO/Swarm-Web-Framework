using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Core.Web.FrontController.Common
{
    public class HomePageFacade : PageFacade
    {
        public HomePageFacade(HttpContext conteudo)
            : base(conteudo)
        {
        }

        public override bool ValidarAutenticacao()
        {
            return UsuarioCorrenteFacade.Instance.Autenticado;
        }

        public override bool ValidarPermissoes()
        {
            if (UsuarioCorrenteFacade.Instance.IsAdministrador)
                return Valor.Ativo;

            bool possuiPermissao = Valor.Inativo;

            try
            {
                List<Ambiente> objAmbientes = SecuritySettings.Ambientes.Where(obj => obj.CodigoInterno == EnumAcesso.CodigoInterno_Ambiente.Indefinido && obj.Restrito).ToList();
                string guidEnvolvido = objAmbientes.Find(obj => obj.GetItemBase().UrlMapID == this.ID).GUID;

                possuiPermissao = !Checar.IsNull(UsuarioCorrenteFacade.Instance.GetPermissoes().Find(obj => obj.GUID == guidEnvolvido));
            }
            catch { possuiPermissao = Valor.Inativo; }

            return possuiPermissao;
        }

        #region Métodos Externos

        public static bool IsTrue(int pageID)
        {
            bool isHomePage = Valor.Inativo;

            try
            {
                List<Ambiente> objAmbientes = SecuritySettings.Ambientes.Where(obj => obj.CodigoInterno == EnumAcesso.CodigoInterno_Ambiente.Indefinido && obj.Restrito).ToList();
                isHomePage = !Checar.IsNull(objAmbientes.Find(obj => obj.GetItemBase().UrlMapID == pageID));
            }
            catch { isHomePage = Valor.Inativo; }

            return isHomePage;
        }

        #endregion
    }
}
