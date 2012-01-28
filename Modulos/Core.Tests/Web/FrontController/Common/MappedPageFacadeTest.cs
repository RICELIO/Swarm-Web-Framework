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
    public class MappedPageFacadeTest
    {
        private const int PAGINA_ID = Valor.Sete; // Página mapeada (fins de teste)

        private Usuario Usuario { get; set; }
        private Ambiente Ambiente { get; set; }
        private Funcionalidade Funcionalidade { get; set; }
        private AcessoMap Mapeamento { get; set; }
        private Permissao Permissao { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            this.Usuario = UsuarioController.Create();
            this.Usuario.Tipo = EnumAutenticacao.TipodeUsuario.Usuario;
            this.Usuario.Login = "usuario.teste";
            this.Usuario.Senha = "bnkGt/s/jhxjZsCRAFDllQ=="; // testes
            UsuarioController.Manter(this.Usuario, colecao);

            this.Ambiente = new Ambiente();
            this.Ambiente.Titulo = "Portal (Homologação)";
            this.Ambiente.Habilitado = Valor.Ativo;
            this.Ambiente.Restrito = Valor.Ativo;
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
            // FIM: A exclusão do ambiente irá efetuar o processo de limpeza automática dos registros associados (cascata)

            this.Funcionalidade = new Funcionalidade();
            this.Funcionalidade.Titulo = "Validação de Página Mapeada (Homologação)";
            this.Funcionalidade.Grupo = objGrupo;
            this.Funcionalidade.Habilitado = Valor.Ativo;
            AcessoController.Manter(this.Funcionalidade, "usuario.testes", colecao);

            colecao.Persistir();

            this.Mapeamento = new AcessoMap();
            this.Mapeamento.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            this.Mapeamento.IdAcesso = this.Funcionalidade.ID;
            this.Mapeamento.UrlMapID = PAGINA_ID;
            this.Mapeamento.Principal = Valor.Ativo;
            AcessoController.Manter(this.Mapeamento, "usuario.testes", null);

            this.Permissao = PermissaoController.Create();

            try { UsuarioCorrenteFacade.Desconectar(); }
            catch { /* Prevenção */ }
        }

        [Test]
        public void ValidandoAutenticacaoePermissoes()
        {
            // VALIDAÇÃO INICIAL: NEGAR TODAS
            this.Funcionalidade.Habilitado = Valor.Inativo;
            AcessoController.Manter(this.Funcionalidade, "usuario.testes", null);
            Assert.IsFalse(this.ValidarControledeAcessoStub(), "A funcionalidade está habilitada para uso quando não deveria estar.");
            Assert.IsFalse(this.ValidarAutenticacaoStub(), "O usuário está autenticado quando não deveria estar.");
            Assert.IsFalse(this.ValidarPermissoesStub(), "O usuário possui permissão de acesso quando não deveria possuir.");

            // APESAR DE AUTENTICADO É NECESSÁRIO INFORMAR O ENVIRONMENT ENVOLVIDO
            UsuarioCorrenteFacade.Autenticar("usuario.teste", "testes");
            Assert.IsFalse(this.ValidarAutenticacaoStub(), "O Environment foi informado quando não deveria está preenchido.");
            Assert.IsFalse(this.ValidarPermissoesStub(), "O Environment foi informado quando não deveria está preenchido.");
            UsuarioCorrenteFacade.Environment = this.Ambiente.GUID;

            // UMA VEZ AUTENTICADO, SETADO O ENVIRONMENT E DEFINIDO A FUNCIONALIDADE ENVOLVIDA COMO ATIVA DEVE APROVAR DUAS DAS TRÊS CHECAGENS,
            // MAS DEVE NEGAR A CHECAGEM DE PERMISSÃO, POIS A MESMA AINDA NÃO FOI GERADA
            this.Funcionalidade.Habilitado = Valor.Ativo;
            AcessoController.Manter(this.Funcionalidade, "usuario.testes", null);
            Assert.IsTrue(this.ValidarControledeAcessoStub(), "A funcionalidade não está habilitada para uso quando deveria estar.");
            Assert.IsTrue(this.ValidarAutenticacaoStub(), "O usuário não está autenticado no sistema.");
            Assert.IsFalse(this.ValidarPermissoesStub(), "O usuário não deveria possui permissão de acesso a PÁGINA MAPEADA.");

            // COM A PERMISSÃO GERADA, A ÚLTIMA VALIDAÇÃO TAMBÉM DEVE SER APROVADA
            this.Permissao.Usuario = this.Usuario;
            this.Permissao.GUID = this.Funcionalidade.GUID;
            PermissaoController.Manter(this.Permissao, "usuario.teste", null);
            Assert.IsTrue(this.ValidarPermissoesStub(), "O usuário deveria possui permissão de acesso a PÁGINA MAPEADA.");
        }

        [TestFixtureTearDown]
        public void ExcluindoObjetosEnvolvidos()
        {
            Assert.That(PermissaoController.Excluir(this.Permissao, null), "[Permissão] Não foi possível excluir o Cenário envolvido.");
            Assert.That(AcessoController.Excluir(this.Mapeamento, null), "[Mapeamento] Não foi possível excluir o Cenário envolvido.");
            Assert.That(AcessoController.Excluir(this.Funcionalidade, null), "[Funcionalidade] Não foi possível excluir o Cenário envolvido.");
            Assert.That(AcessoController.Excluir(this.Ambiente, null), "[Ambiente] Não foi possível excluir o Cenário envolvido.");
            Assert.That(UsuarioController.Excluir(this.Usuario, null), "[Usuário] Não foi possível excluir o Cenário envolvido.");
        }

        #region Métodos Internos

        private bool ValidarAutenticacaoStub()
        {
            return UsuarioCorrenteFacade.Instance.Autenticado && !Checar.IsCampoVazio(UsuarioCorrenteFacade.Environment);
        }

        private bool ValidarPermissoesStub()
        {
            if (UsuarioCorrenteFacade.Instance.IsAdministrador)
                return Valor.Ativo;

            bool possuiPermissao = Valor.Inativo;

            try
            {
                Ambiente objAmbiente = SecuritySettings.Ambientes.Find(obj => obj.GUID == UsuarioCorrenteFacade.Environment);
                string guidEnvolvido = objAmbiente.FindGUID(PAGINA_ID);

                possuiPermissao = !Checar.IsNull(UsuarioCorrenteFacade.Instance.GetPermissoes().Find(obj => obj.GUID == guidEnvolvido));
            }
            catch { possuiPermissao = Valor.Inativo; }

            return possuiPermissao;
        }

        private bool ValidarControledeAcessoStub()
        {
            bool paginaDisponivel = Valor.Inativo;

            try
            {
                Ambiente objAmbiente = SecuritySettings.Ambientes.Find(obj => obj.GUID == UsuarioCorrenteFacade.Environment);
                objAmbiente.GetSuperGrupos().ForEach(sg =>
                {
                    if (paginaDisponivel) return;
                    sg.GetGrupos().ForEach(g =>
                    {
                        if (paginaDisponivel) return;
                        g.GetFuncionalidades().ForEach(f =>
                        {
                            bool paginaLocalizada = !Checar.IsNull(f.GetItens().Find(obj => obj.UrlMapID == PAGINA_ID));
                            paginaDisponivel = paginaLocalizada && f.Habilitado;
                            if (paginaDisponivel) return;
                        });
                    });
                });
            }
            catch { paginaDisponivel = Valor.Inativo; }

            return paginaDisponivel;
        }

        #endregion
    }
}
