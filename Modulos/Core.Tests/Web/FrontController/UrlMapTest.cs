using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Core.Web;
using Swarm.Core.Web.FrontController;

namespace Swarm.Tests.Core.Web.FrontController
{
    [TestFixture]
    public class UrlMapTest
    {
        [Test]
        public void CarregarArquivocomSucesso()
        {
            Assert.IsNotNull(UrlMap.Instance);
        }

        [Test]
        public void PossuiItensMapeados()
        {
            Assert.That(Checar.MaiorQue(UrlMap.Instance.Itens.Count), "Não há itens de URL mapeados.");
        }

        [Test]
        public void PaginaControladoraDeveEstarMapeada()
        {
            UrlMap obj = UrlMap.Instance;
            UrlMapItem objITEM = obj.Itens.Find(i => i.Key == Map.FrontController.Controller);
            Assert.IsNotNull(objITEM);

            Assert.IsTrue(Checar.MaiorouIgual(objITEM.ID));
            Assert.IsNotNullOrEmpty(objITEM.Key);
            Assert.IsNotNullOrEmpty(objITEM.Url);
            Assert.IsNotNullOrEmpty(objITEM.Titulo);
            Assert.IsTrue(objITEM.Ocultar);
        }
    }
}