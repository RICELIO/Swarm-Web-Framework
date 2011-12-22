using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Tests.Core.Web.ControledeAcesso
{
    [TestFixture]
    public class AcessoTest
    {
        private Ambiente AmbienteEnvolvido { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.AmbienteEnvolvido = new Ambiente();
            this.AmbienteEnvolvido.Titulo = "Portal Administrativo (Homologação)";
            this.AmbienteEnvolvido.Habilitado = Valor.Ativo;
            this.AmbienteEnvolvido.Restrito = Valor.Ativo; // Acesso não autenticado.
            AcessoController.Manter(this.AmbienteEnvolvido, "usuario.teste", null);
        }

        [Test]
        public void CriandoCenariodeAcessoAnonimo()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            // INÍCIO: A exclusão do ambiente irá efetuar o processo de limpeza automática dos registros associados (cascata)
            SuperGrupo objSuperGrupo = new SuperGrupo();
            objSuperGrupo.Titulo = "Administrativo (Homologação)";
            objSuperGrupo.Habilitado = Valor.Ativo;
            objSuperGrupo.Ambiente = this.AmbienteEnvolvido;
            AcessoController.Manter(objSuperGrupo, "usuario.teste", colecao);

            Grupo objGrupo = new Grupo();
            objGrupo.Titulo = "Segurança (Homologação)";
            objGrupo.Habilitado = Valor.Ativo;
            objGrupo.SuperGrupo = objSuperGrupo;
            AcessoController.Manter(objGrupo, "usuario.teste", colecao);

            Funcionalidade objFuncionalidade = new Funcionalidade();
            objFuncionalidade.Titulo = "Manutenção de Usuários (Homologação)";
            objFuncionalidade.Habilitado = Valor.Ativo;
            objFuncionalidade.Grupo = objGrupo;
            AcessoController.Manter(objFuncionalidade, "usuario.teste", colecao);
            // FIM: A exclusão do ambiente irá efetuar o processo de limpeza automática dos registros associados (cascata)

            colecao.Persistir();
            Assert.IsTrue(Checar.MaiorQue(this.AmbienteEnvolvido.ID));
        }

        [Test]
        public void FoiGeradoCorretamenteGUID()
        {
            Assert.That(!Checar.IsCampoVazio(this.AmbienteEnvolvido.GUID), "Não foi gerado o GUID do ambiente.");
        }

        [Test]
        public void ValidarMapeamentos()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            AcessoMap objAmbienteMAP = new AcessoMap();
            objAmbienteMAP.Tipo = EnumAcesso.TipodeAcesso.Ambiente;
            objAmbienteMAP.IdAcesso = this.AmbienteEnvolvido.ID;
            objAmbienteMAP.UrlMapID = Valor.Um;
            objAmbienteMAP.Principal = Valor.Ativo;
            AcessoController.Manter(objAmbienteMAP, "usuario.teste", colecao);
            AcessoController.Excluir(objAmbienteMAP, colecao);            
            colecao.Persistir();

            Assert.That(Checar.MenorouIgual(objAmbienteMAP.ID), "Não foi possível concluir a operação de criação e exclusão de mapeamento.");
        }

        [Test]
        [ExpectedException]
        public void NaoPermiteoMapeamentoDeGruposOuSuperGrupos()
        {
            AcessoMap objGrupoMAP = new AcessoMap();
            objGrupoMAP.Tipo = EnumAcesso.TipodeAcesso.Grupo; // *ERRO*
            objGrupoMAP.IdAcesso = Valor.Um; // DEFAULT
            objGrupoMAP.UrlMapID = Valor.Um;
            objGrupoMAP.Principal = Valor.Ativo;
            AcessoController.Manter(objGrupoMAP, "usuario.teste", null);

            Assert.IsFalse(Checar.MaiorQue(objGrupoMAP.ID), "Não deveria permitir o mapeamento de Grupo ou SuperGrupo.");
        }

        [Test]
        [ExpectedException]
        public void NaoPermitirMaisDeUmItemMapeadoNoMesmoAmbiente()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            AcessoMap objAmbienteMAP = new AcessoMap();
            objAmbienteMAP.Tipo = EnumAcesso.TipodeAcesso.Ambiente;
            objAmbienteMAP.IdAcesso = Valor.Um; // DEFAULT (ID Ambiente)
            objAmbienteMAP.UrlMapID = Valor.Um;
            objAmbienteMAP.Principal = Valor.Ativo;
            AcessoController.Manter(objAmbienteMAP, "usuario.teste", colecao);

            AcessoMap objAmbienteMAP_DUPLICADO = new AcessoMap();
            objAmbienteMAP.Tipo = EnumAcesso.TipodeAcesso.Ambiente;
            objAmbienteMAP.IdAcesso = Valor.Um; // DEFAULT (ID Ambiente)
            objAmbienteMAP.UrlMapID = Valor.Dois;
            objAmbienteMAP.Principal = Valor.Ativo;
            AcessoController.Manter(objAmbienteMAP_DUPLICADO, "usuario.teste", colecao); // *ERRO*

            colecao.Persistir();
            Assert.IsFalse(Checar.MaiorQue(objAmbienteMAP_DUPLICADO.ID), "Não deveria permitir a duplicação de ligações quando Ambiente.");
        }

        [Test]
        public void PermitirMultiplosItensMapeadosQuandoFuncionalidade()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            AcessoMap objFuncionalidadeMAP = new AcessoMap();
            objFuncionalidadeMAP.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            objFuncionalidadeMAP.IdAcesso = Valor.Um; // DEFAULT (ID SuperGrupo ou Grupo)
            objFuncionalidadeMAP.UrlMapID = Valor.Um;
            objFuncionalidadeMAP.Principal = Valor.Ativo;
            AcessoController.Manter(objFuncionalidadeMAP, "usuario.teste", colecao);

            AcessoMap objFuncionalidadeMAP_2 = new AcessoMap();
            objFuncionalidadeMAP_2.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            objFuncionalidadeMAP_2.IdAcesso = Valor.Um; // DEFAULT (ID SuperGrupo ou Grupo)
            objFuncionalidadeMAP_2.UrlMapID = Valor.Dois;
            objFuncionalidadeMAP_2.Principal = Valor.Inativo;
            AcessoController.Manter(objFuncionalidadeMAP_2, "usuario.teste", colecao);

            AcessoController.Excluir(objFuncionalidadeMAP, colecao);
            AcessoController.Excluir(objFuncionalidadeMAP_2, colecao);

            colecao.Persistir();
            Assert.IsFalse(Checar.MaiorQue(objFuncionalidadeMAP.ID), "O registro 1 deveria ter sido removido.");
            Assert.IsFalse(Checar.MaiorQue(objFuncionalidadeMAP_2.ID), "O registro 2 deveria ter sido removido.");
        }

        [Test]
        [ExpectedException]
        public void NaoPodeHaverDoisItensMapeadosComoPrincipaisQuandoFuncionalidadeMesmoSendoDistintos()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            AcessoMap objFuncionalidadeMAP = new AcessoMap();
            objFuncionalidadeMAP.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            objFuncionalidadeMAP.IdAcesso = Valor.Um; // DEFAULT (ID SuperGrupo ou Grupo)
            objFuncionalidadeMAP.UrlMapID = Valor.Um;
            objFuncionalidadeMAP.Principal = Valor.Ativo;
            AcessoController.Manter(objFuncionalidadeMAP, "usuario.teste", colecao);

            AcessoMap objFuncionalidadeMAP_2 = new AcessoMap();
            objFuncionalidadeMAP_2.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            objFuncionalidadeMAP_2.IdAcesso = Valor.Um; // DEFAULT (ID SuperGrupo ou Grupo)
            objFuncionalidadeMAP_2.UrlMapID = Valor.Dois;
            objFuncionalidadeMAP_2.Principal = Valor.Ativo; // *ERRO*
            AcessoController.Manter(objFuncionalidadeMAP_2, "usuario.teste", colecao);

            colecao.Persistir();
        }

        [TestFixtureTearDown]
        public void ExcluirCenariodeAcessoAnonimoGerado()
        {
            Assert.That(AcessoController.Excluir(this.AmbienteEnvolvido, null), "Não foi possível excluir o Cenário envolvido.");
        }
    }
}
