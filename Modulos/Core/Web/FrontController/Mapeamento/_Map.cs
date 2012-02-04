using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Core.Web.FrontController.Mapeamento;

namespace Swarm.Core.Web.FrontController
{
    public abstract partial class Map
    {
        public static FrontControllerMap FrontController = new FrontControllerMap();
        public static SegurancaMap Seguranca = new SegurancaMap();
        public static PortalMap Portal = new PortalMap();
        public static CallBackMap Callback = new CallBackMap();
    }
}
