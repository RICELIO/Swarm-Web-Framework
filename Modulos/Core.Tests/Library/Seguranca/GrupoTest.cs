using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca;

namespace Swarm.Tests.Core.Library.Seguranca
{
    [TestFixture]
    public class GrupoTest
    {
        private Grupo Grupo { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Grupo = GrupoController.Create();
        }

        [Test]
        public void CriandoeExcluindoumGrupo()
        {
            try
            {
                ColecaoPersistencia colecaoPersistencia = new ColecaoPersistencia();

                this.Grupo.Descricao = "Grupo #1 (Homologação)";
                GrupoController.Manter(this.Grupo, "usuario.teste", colecaoPersistencia);

                colecaoPersistencia.Persistir();

                Assert.IsTrue(Valor.Ativo);
            }
            catch (Exception erro)
            {
                Assert.That(Valor.Inativo, erro.Message);
            }
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(GrupoController.Excluir(this.Grupo, null), "[Grupo] Não foi possível excluir o Cenário envolvido.");
        }
    }
}
