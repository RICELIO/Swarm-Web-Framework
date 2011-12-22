using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Core.Web.Configuracao;
using Swarm.Persistencia;

namespace Swarm.Tests.Core.Web.Configuracao
{
    [TestFixture]
    public class ConfiguracoesGeraisTest
    {
        private ConfiguracoesGerais Configuracao { get; set; }

        [TestFixtureSetUp]
        public void InicializarObjetosEnvolvidos()
        {
            this.Configuracao = ConfiguracoesGeraisController.Get();
        }

        [Test]
        public void ObjetoEnvolvidoNaoPodeSerNulo()
        {
            Assert.That(!Checar.IsNull(this.Configuracao), "A configuração envolvida está nula. Não foi possível instanciar o objeto solicitado.");
        }

        [Test]
        public void DeveExistirUmaConfiguracaoGeralCadastrada()
        {
            Assert.That(Checar.MaiorQue(this.Configuracao.ID), "Não foi possível localizar o registro padrão na base de dados.");
        }

        [Test]
        public void PermitirAlterarObjetoSemGerarNovoRegistro()
        {
            int tentativasPADRAO = this.Configuracao.TentativasdeAcesso;

            this.Configuracao.TentativasdeAcesso++;
            try { ConfiguracoesGeraisController.Manter(this.Configuracao, "usuario.testes", null); }
            catch { /* Prevenção */ }
            
            this.Configuracao = ConfiguracoesGeraisController.Get(); // Obtendo nova instância
            Assert.That(this.Configuracao.TentativasdeAcesso != tentativasPADRAO, "Não houve a alteração esperada no objeto.");

            this.Configuracao.TentativasdeAcesso = tentativasPADRAO;
            try { ConfiguracoesGeraisController.Manter(this.Configuracao, "usuario.testes", null); }
            catch { /* Prevenção */ }
            Assert.That(Checar.MenorouIgual(this.GetTotaldeConfiguracoesExistenteStub(), Valor.Um), "Foi detectado mais de um registo de configuração cadastro, este comportamento é indevido.");
        }

        #region Métodos Internos

        private int GetTotaldeConfiguracoesExistenteStub()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT COUNT(DISTINCT {1}) AS Total
            FROM {0}
            ", this.Configuracao.Tabela, this.Configuracao.ChavePrimaria);

            LeitorFacade leitor = new LeitorFacade(sql);
            int totaldeRegistros = leitor.LerLinha() ? Conversoes.ToInt32(leitor.GetValor(Valor.Zero)) : Valor.Zero;
            leitor.Fechar();

            return totaldeRegistros;
        }

        #endregion
    }
}
