using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Core.Web.FrontController.Common;

namespace Swarm.Core.Web.FrontController
{
    internal abstract class PageFactory
    {
        public static PageFacade Create(HttpContext conteudo)
        {
            if (UrlMap.Find(Map.FrontController.Default).Url.ToLower().Contains(PageFactory.GetFileName(conteudo)))
                return new LoginPageFacade(conteudo);

            if (!UrlMap.Find(Map.FrontController.Controller).Url.ToLower().Contains(PageFactory.GetFileName(conteudo)))
                return new InvalidPageFacade(conteudo);

            int pageID = PageFacade.GetID(conteudo);

            if (LoginPageFacade.IsTrue(pageID))
                return new LoginPageFacade(conteudo);

            if (UrlMap.Find(Map.Seguranca.Logoff).ID == pageID)
                return new LogoffPageFacade(conteudo);

            if (UrlMap.Find(Map.Seguranca.Expired).ID == pageID)
                return new ExpiredPageFacade(conteudo);

            if (pageID == UrlMap.Find(Map.FrontController.ShowMessage).ID)
                return new ShowMessagePageFacade(conteudo);

            if (CallBackPageFacade.IsTrue(pageID))
                return new CallBackPageFacade(conteudo);

            if (HomePageFacade.IsTrue(pageID))
                return new HomePageFacade(conteudo);

            if (AnonymousPageFacade.IsTrue(pageID))
                return new AnonymousPageFacade(conteudo);

            return new MappedPageFacade(conteudo);
        }

        #region Métodos Internos

        private static string GetFileName(HttpContext conteudo)
        {
            return Path.GetFileName(conteudo.Request.Path).ToLower();
        }

        #endregion
    }
}
