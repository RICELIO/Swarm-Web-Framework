using System;
using System.Collections.Generic;
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
            if (LoginPageFacade.IsDefaultPage(conteudo))
                return new LoginPageFacade(conteudo);

            if (InvalidPageFacade.IsNotControllerPage(conteudo))
                return new InvalidPageFacade(conteudo);

            int pageID = PageFacade.GetID(conteudo);

            if (LoginPageFacade.IsTrue(pageID))
                return new LoginPageFacade(conteudo);

            if (LogoffPageFacade.IsTrue(pageID))
                return new LogoffPageFacade(conteudo);

            if (ExpiredPageFacade.IsTrue(pageID))
                return new ExpiredPageFacade(conteudo);

            if (ShowMessagePageFacade.IsTrue(pageID))
                return new ShowMessagePageFacade(conteudo);

            if (CallBackPageFacade.IsTrue(pageID))
                return new CallBackPageFacade(conteudo);

            if (HomePageFacade.IsTrue(pageID))
                return new HomePageFacade(conteudo);

            if (AnonymousPageFacade.IsTrue(pageID))
                return new AnonymousPageFacade(conteudo);

            return new MappedPageFacade(conteudo);
        }
    }
}
