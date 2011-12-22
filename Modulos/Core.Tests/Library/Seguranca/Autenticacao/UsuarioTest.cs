using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Tests.Core.Library.Seguranca.Autenticacao
{
    [TestFixture]
    public class UsuarioTest
    {
        private Usuario Usuario { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Usuario = UsuarioController.Create();
        }

        [Test]
        public void CriandoeExcluindoumUsuario()
        {
            try
            {
                ColecaoPersistencia colecaoPersistencia = new ColecaoPersistencia();

                this.Usuario.Tipo = EnumAutenticacao.TipodeUsuario.Usuario;
                this.Usuario.Login = "usuario.teste";
                this.Usuario.Senha = "bnkGt/s/jhxjZsCRAFDllQ=="; // testes
                this.Usuario.Avatar = Valor.Vazio;
                UsuarioController.Manter(this.Usuario, colecaoPersistencia);

                colecaoPersistencia.Persistir();

                Assert.IsTrue(Valor.Ativo);
            }
            catch (Exception erro) { Assert.That(Valor.Inativo, erro.Message); }
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(UsuarioController.Excluir(this.Usuario, null), "[Usuário] Não foi possível excluir o Cenário envolvido.");
        }
    }
}
