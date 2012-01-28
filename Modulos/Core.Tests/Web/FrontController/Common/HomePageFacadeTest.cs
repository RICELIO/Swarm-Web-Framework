using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca.Autenticacao;
using Swarm.Core.Web;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Core.Web.FrontController.Common;

namespace Swarm.Tests.Core.Web.FrontController.Common
{
    [TestFixture]
    public class HomePageFacadeTest
    {
        private const int PAGINA_ID = Valor.Seis; // Página HOME (fins de teste)

        private Usuario Usuario { get; set; }
        private Ambiente Ambiente { get; set; }
        private AcessoMap Mapeamento { get; set; }
        private Permissao Permissao { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Usuario = UsuarioController.Create();
            this.Ambiente = new Ambiente();
            this.Mapeamento = new AcessoMap();
            this.Permissao = PermissaoController.Create();
        }

        [Test]
        public void ValidandoAcessoaUmaHomePage()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            this.Usuario.Tipo = EnumAutenticacao.TipodeUsuario.Usuario;
            this.Usuario.Login = "usuario.teste";
            this.Usuario.Senha = "bnkGt/s/jhxjZsCRAFDllQ=="; // testes
            UsuarioController.Manter(this.Usuario, colecao);

            this.Ambiente.Titulo = "Portal (Homologação)";
            this.Ambiente.Habilitado = Valor.Ativo;
            this.Ambiente.Restrito = Valor.Ativo;
            AcessoController.Manter(this.Ambiente, "usuario.testes", colecao);

            colecao.Persistir();

            this.Mapeamento.Tipo = EnumAcesso.TipodeAcesso.Ambiente;
            this.Mapeamento.IdAcesso = this.Ambiente.ID;
            this.Mapeamento.UrlMapID = PAGINA_ID;
            this.Mapeamento.Principal = Valor.Ativo;
            AcessoController.Manter(this.Mapeamento, "usuario.testes", null);

            bool isHomePage = HomePageFacade.IsTrue(PAGINA_ID);

            Assert.IsTrue(isHomePage, "Não foi possível localizar a associação com o cenário HOME.");

            try { UsuarioCorrenteFacade.Desconectar(); }
            catch { /* Prevenção */ }
        }

        [Test]
        public void ValidandoAutenticacaoePermissoes()
        {
            Assert.IsFalse(this.ValidarAutenticacaoStub(), "O usuário está autenticado quando não deveria estar.");
            Assert.IsFalse(this.ValidarPermissoesStub(), "O usuário está autenticado quando não deveria estar.");

            UsuarioCorrenteFacade.Autenticar("usuario.teste", "testes");
            Assert.IsTrue(this.ValidarAutenticacaoStub(), "O usuário não está autenticado no sistema.");
            Assert.IsFalse(this.ValidarPermissoesStub(), "O usuário não deveria possui permissão de acesso a HOME.");
                        
            this.Permissao.Usuario = this.Usuario;
            this.Permissao.GUID = this.Ambiente.FindGUID(PAGINA_ID);
            PermissaoController.Manter(this.Permissao, "usuario.teste", null);

            Assert.IsTrue(this.ValidarPermissoesStub(), "O usuário deveria possui permissão de acesso a HOME.");
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(PermissaoController.Excluir(this.Permissao, null), "[Permissão] Não foi possível excluir o Cenário envolvido.");
            Assert.That(AcessoController.Excluir(this.Mapeamento, null), "[Mapeamento] Não foi possível excluir o Cenário envolvido.");
            Assert.That(AcessoController.Excluir(this.Ambiente, null), "[Ambiente] Não foi possível excluir o Cenário envolvido.");
            Assert.That(UsuarioController.Excluir(this.Usuario, null), "[Usuário] Não foi possível excluir o Cenário envolvido.");
        }

        #region Métodos Internos

        private bool ValidarAutenticacaoStub()
        {
            return UsuarioCorrenteFacade.Instance.Autenticado;
        }

        private bool ValidarPermissoesStub()
        {
            if (UsuarioCorrenteFacade.Instance.IsAdministrador)
                return Valor.Ativo;

            bool possuiPermissao = Valor.Inativo;

            try
            {
                List<Ambiente> objAmbientes = SecuritySettings.Ambientes.Where(obj => obj.CodigoInterno == EnumAcesso.CodigoInterno_Ambiente.Indefinido && obj.Restrito).ToList();
                string guidEnvolvido = objAmbientes.Find(obj => obj.GetItemBase().UrlMapID == PAGINA_ID).GUID;

                possuiPermissao = !Checar.IsNull(UsuarioCorrenteFacade.Instance.GetPermissoes().Find(obj => obj.GUID == guidEnvolvido));
            }
            catch { possuiPermissao = Valor.Inativo; }

            return possuiPermissao;
        }

        #endregion
    }
}
