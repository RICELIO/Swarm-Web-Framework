using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca;
using Swarm.Core.Library.Seguranca.Autenticacao;
using Swarm.Core.Web;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Tests.Core.Web
{
    [TestFixture]
    public class PermissaoTest
    {
        private Usuario Usuario { get; set; }
        private Ambiente Ambiente { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Usuario = UsuarioController.Create();
            this.Usuario.Tipo = EnumAutenticacao.TipodeUsuario.Usuario;
            this.Usuario.Login = "usuario.teste";
            this.Usuario.Senha = "bnkGt/s/jhxjZsCRAFDllQ=="; // testes
            this.Usuario.Avatar = Valor.Vazio;
            UsuarioController.Manter(this.Usuario, null);

            this.Ambiente = new Ambiente();
            this.Ambiente.Titulo = "Portal Administrativo (Homologação)";
            this.Ambiente.Habilitado = Valor.Ativo;
            this.Ambiente.Restrito = Valor.Ativo;
            AcessoController.Manter(this.Ambiente, this.Usuario.Login, null);
        }

        [Test]
        public void CriandoeExcluindoPermissaoParaUmUsuario()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            Permissao objPermissao = PermissaoController.Create();
            objPermissao.Usuario = this.Usuario;
            objPermissao.GUID = this.Ambiente.GUID;
            PermissaoController.Manter(objPermissao, this.Usuario.Login, colecao);
            PermissaoController.Excluir(objPermissao, colecao);

            colecao.Persistir();
            Assert.IsTrue(Checar.MenorouIgual(objPermissao.ID));
        }

        [Test]
        public void CriandoeExcluindoPermissaoParaUmGrupo()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            Swarm.Core.Library.Seguranca.Grupo objGrupo = GrupoController.Create();
            objGrupo.Descricao = "Grupo de teste #1";
            GrupoController.Manter(objGrupo, this.Usuario.Login, colecao);

            GrupoUsuario objGrupoUsuario = GrupoUsuarioController.Create();
            objGrupoUsuario.Usuario = this.Usuario;
            objGrupoUsuario.Grupo = objGrupo;
            GrupoUsuarioController.Manter(objGrupoUsuario, this.Usuario.Login, colecao);

            Permissao objPermissao = PermissaoController.Create();
            objPermissao.Grupo = objGrupoUsuario;
            objPermissao.GUID = this.Ambiente.GUID;
            PermissaoController.Manter(objPermissao, this.Usuario.Login, colecao);

            PermissaoController.Excluir(objPermissao, colecao);
            GrupoUsuarioController.Excluir(objGrupoUsuario, colecao);
            GrupoController.Excluir(objGrupo, colecao);          

            colecao.Persistir();
            Assert.IsTrue(Checar.MenorouIgual(objPermissao.ID));
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(AcessoController.Excluir(this.Ambiente, null), "[Ambiente] Não foi possível excluir o Cenário envolvido.");
            Assert.That(UsuarioController.Excluir(this.Usuario, null), "[Usuário] Não foi possível excluir o Cenário envolvido.");
        }
    }
}
