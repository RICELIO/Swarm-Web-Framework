using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;

namespace Swarm.Core.Web.FrontController.Common
{
    public class LoginPageFacade : PageFacade
    {
        public LoginPageFacade(HttpContext conteudo)
            : base(conteudo)
        {
        }

        public override bool ValidarAutenticacao()
        {
            return Valor.Ativo;
        }

        public override bool ValidarPermissoes()
        {
            return Valor.Ativo;
        }

        public override string GetURI()
        {
            return UrlMap.Find(Map.Seguranca.Login).Url;
        }

        #region Métodos Externos

        public static bool IsDefaultPage(HttpContext conteudo)
        {
            return UrlMap.Find(Map.FrontController.Default).Url.ToLower().Contains(PageFacade.GetFileName(conteudo));
        }

        public static bool IsTrue(int pageID)
        {
            if (UrlMap.Find(Map.FrontController.Default).ID == pageID)
                return Valor.Ativo;

            if (UrlMap.Find(Map.Seguranca.Login).ID == pageID)
                return Valor.Ativo;

            return Valor.Inativo;
        }

        #endregion
    }
}
