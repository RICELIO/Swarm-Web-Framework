using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Core.Web.Configuracao;

namespace Swarm.Tests.Core.Web.Configuracao
{
    [TestFixture]
    public class ControledeVersaoTest
    {
        private ControledeVersao ControledeVersao { get; set; }

        [TestFixtureSetUp]
        public void InicializandoObjetosEnvolvidos()
        {
            this.ControledeVersao = ControledeVersaoController.Create();
        }

        [Test]
        public void RegistrandoeAlterandoUmaVersaoNoSistema()
        {
            this.ControledeVersao.Versao = "1.0.1b";
            this.ControledeVersao.Notas = "Registro de versão para fins de testes unitários.";
            this.ControledeVersao.LogdeInstalacao = DateTime.Now;
            ControledeVersaoController.Manter(this.ControledeVersao, "usuario.testes", null);
            Assert.That(Checar.MaiorQue(this.ControledeVersao.ID), "Não foi possível criar o registro solicitado.");

            string versaoAlterada = "1.0.2b";
            this.ControledeVersao.Versao = versaoAlterada;
            ControledeVersaoController.Manter(this.ControledeVersao, "usuario.testes", null);
            Assert.That(ControledeVersaoController.Get(this.ControledeVersao.ID).Versao == versaoAlterada, "Não foi possível efetuar a alteração solicitada.");
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.IsTrue(ControledeVersaoController.Excluir(this.ControledeVersao, null), "[Controle de Versão] Não foi possível excluir o registro envolvido.");
        }
    }
}
