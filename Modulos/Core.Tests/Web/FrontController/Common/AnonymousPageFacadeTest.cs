using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Core.Web.FrontController.Common;

namespace Swarm.Tests.Core.Web.FrontController.Common
{
    [TestFixture]
    public class AnonymousPageFacadeTest
    {
        private Ambiente Ambiente { get; set; }
        private AcessoMap Mapeamento { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Ambiente = new Ambiente();
            this.Mapeamento = new AcessoMap();
        }

        [Test]
        public void ValidandoAcessoaUmaPaginaAnonima()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            this.Ambiente.Titulo = "Páginas Anônimas (Homologação)";
            this.Ambiente.Habilitado = Valor.Ativo;
            this.Ambiente.Restrito = Valor.Ativo;
            this.Ambiente.CodigoInterno = EnumAcesso.CodigoInterno_Ambiente.Anonimo;
            AcessoController.Manter(this.Ambiente, "usuario.testes", colecao);

            // INÍCIO: A exclusão do ambiente irá efetuar o processo de limpeza automática dos registros associados (cascata)
            SuperGrupo objSuperGrupo = new SuperGrupo();
            objSuperGrupo.Titulo = "Aninhamento Padrão - Nível 1 (Homologação)";
            objSuperGrupo.Ambiente = this.Ambiente;
            objSuperGrupo.Habilitado = Valor.Ativo;
            objSuperGrupo.CodigoInterno = EnumAcesso.CodigoInterno_Grupo.Individual;
            AcessoController.Manter(objSuperGrupo, "usuario.testes", colecao);

            Grupo objGrupo = new Grupo();
            objGrupo.Titulo = "Aninhamento Padrão - Nível 2 (Homologação)";
            objGrupo.SuperGrupo = objSuperGrupo;
            objGrupo.Habilitado = Valor.Ativo;
            objGrupo.CodigoInterno = EnumAcesso.CodigoInterno_Grupo.Individual;
            AcessoController.Manter(objGrupo, "usuario.testes", colecao);

            Funcionalidade objFuncionalidade = new Funcionalidade();
            objFuncionalidade.Titulo = "Validação de Página (Homologação)";
            objFuncionalidade.Grupo = objGrupo;
            objFuncionalidade.Habilitado = Valor.Ativo;           
            AcessoController.Manter(objFuncionalidade, "usuario.testes", colecao);
            // FIM: A exclusão do ambiente irá efetuar o processo de limpeza automática dos registros associados (cascata)

            colecao.Persistir();

            int paginaID = Valor.Dois; // Página DEFAULT (Fins de teste)

            this.Mapeamento.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            this.Mapeamento.IdAcesso = objFuncionalidade.ID;
            this.Mapeamento.UrlMapID = paginaID; // Página DEFAULT (Fins de teste)
            this.Mapeamento.Principal = Valor.Ativo;
            AcessoController.Manter(this.Mapeamento, "usuario.testes", null);

            bool isAnonymousPage = AnonymousPageFacade.IsTrue(paginaID);

            Assert.IsTrue(isAnonymousPage, "Não foi possível localizar a associação com o cenário ANONIMO.");
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(AcessoController.Excluir(this.Mapeamento, null), "[Mapeamento] Não foi possível excluir o Cenário envolvido.");
            Assert.That(AcessoController.Excluir(this.Ambiente, null), "[Ambiente] Não foi possível excluir o Cenário envolvido.");
        }
    }
}
