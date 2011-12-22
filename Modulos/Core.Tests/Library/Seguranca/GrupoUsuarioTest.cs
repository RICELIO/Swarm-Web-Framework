using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Tests.Core.Library.Seguranca
{
    [TestFixture]
    public class GrupoUsuarioTest
    {
        private Usuario Usuario { get; set; }
        private Grupo Grupo { get; set; }
        private GrupoUsuario Ligacao { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Usuario = UsuarioController.Create();
            this.Grupo = GrupoController.Create();
            this.Ligacao = GrupoUsuarioController.Create();
        }

        [Test]
        public void CriandoUmaLigacao()
        {
            try
            {
                ColecaoPersistencia colecaoPersistencia = new ColecaoPersistencia();

                this.Usuario.Tipo = EnumAutenticacao.TipodeUsuario.Usuario;
                this.Usuario.Login = "usuario.teste";
                this.Usuario.Senha = "bnkGt/s/jhxjZsCRAFDllQ=="; // testes
                UsuarioController.Manter(this.Usuario, colecaoPersistencia);

                this.Grupo.Descricao = "Grupo #1 (Homologação)";
                GrupoController.Manter(this.Grupo, this.Usuario.Login, colecaoPersistencia);

                this.Ligacao.Usuario = this.Usuario;
                this.Ligacao.Grupo = this.Grupo;
                GrupoUsuarioController.Manter(this.Ligacao, this.Usuario.Login, colecaoPersistencia);

                colecaoPersistencia.Persistir();

                Assert.That(Checar.MaiorQue(this.Usuario.GetGrupos().Count), "Não há grupos associados ao usuário 'usuario.teste'");
                Assert.That(Checar.MaiorQue(this.Grupo.GetUsuarios().Count), "Não há usuários associados ao grupo 'Grupo #1 (Homologação)'");
            }
            catch (Exception erro) { Assert.That(Valor.Inativo, erro.Message); }
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(GrupoUsuarioController.Excluir(this.Ligacao, null), "[Ligação Grupo & Usuário] Não foi possível excluir o Cenário envolvido.");
            Assert.That(GrupoController.Excluir(this.Grupo, null), "[Grupo] Não foi possível excluir o Cenário envolvido.");
            Assert.That(UsuarioController.Excluir(this.Usuario, null), "[Usuário] Não foi possível excluir o Cenário envolvido.");
        }
    }
}
