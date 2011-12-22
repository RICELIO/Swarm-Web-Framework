using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Core.Library.Seguranca.Autenticacao;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.Configuracao
{
    public class ConfiguracoesGerais : ModeloObjetoBase
    {
        public ConfiguracoesGerais()
            : base("ConfiguracoesGerais", "IdConfiguracoesGerais")
        {
            this.Materializar(Valor.Inativo);
        }

        #region Propriedades

        public Usuario UsuarioMaster { get; set; }
        public bool Demonstracao_Habilitar { get; set; }
        public string Demonstracao_Titulo { get; set; }
        public string Demonstracao_Mensagem { get; set; }
        public string Produto_Titulo { get; set; }
        public EnumConfiguracao.TipoApresentacao Apresentacao_Tipo { get; set; }
        public string Apresentacao_Titulo { get; set; }
        public string Apresentacao_Mensagem { get; set; }
        public string Apresentacao_Imagem { get; set; }
        public int TentativasdeAcesso { get; set; }

        private string VersaoAtual { get; set; }

        #endregion

        protected override void Reset()
        {
            this.UsuarioMaster = UsuarioController.Create();
            this.Demonstracao_Habilitar = Valor.Ativo;
            this.Demonstracao_Titulo = Valor.Vazio;
            this.Demonstracao_Mensagem = Valor.Vazio;
            this.Produto_Titulo = Valor.Vazio;
            this.Apresentacao_Tipo = EnumConfiguracao.TipoApresentacao.Indefinido;
            this.Apresentacao_Titulo = Valor.Vazio;
            this.Apresentacao_Mensagem = Valor.Vazio;
            this.Apresentacao_Imagem = Valor.Vazio;
            this.TentativasdeAcesso = Valor.Cinco; // DEFAULT
            this.Materializado = Valor.Inativo;
        }

        #region Métodos

        protected override bool Validar()
        {
            if (Checar.IsCampoVazio(this.Demonstracao_Titulo))
                throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Título de Demonstração"));

            if (Checar.IsCampoVazio(this.Demonstracao_Mensagem))
                throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Mensagem de Demonstração"));

            if (Checar.IsCampoVazio(this.Produto_Titulo))
                throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Título do Produto"));

            if (this.Apresentacao_Tipo == EnumConfiguracao.TipoApresentacao.Indefinido)
                throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Tipo de Apresentação"));

            if (this.Apresentacao_Tipo == EnumConfiguracao.TipoApresentacao.Textual)
            {
                if (Checar.IsCampoVazio(this.Apresentacao_Titulo))
                    throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Título de Apresentação"));

                if (Checar.IsCampoVazio(this.Apresentacao_Mensagem))
                    throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Mensagem de Apresentação"));
            }
            else
                if (Checar.IsCampoVazio(this.Apresentacao_Imagem))
                    throw new Exception(Erros.ValorInvalido("Configurações Gerais", "Imagem de Apresentação"));

            if (Checar.MenorQue(this.TentativasdeAcesso))
                throw new Exception("Configurações Gerais: o total de tentativas de acesso não pode ser inferior a 0.");

            base.Log.Validar();

            return Valor.Ativo;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("IdUsuarioMaster", this.UsuarioMaster.ID, Checar.MenorouIgual(this.UsuarioMaster.ID));
            this.ItensPersistencia.Add("HabilitarDemonstracao", this.Demonstracao_Habilitar);
            this.ItensPersistencia.Add("TituloDemonstracao", this.Demonstracao_Titulo);
            this.ItensPersistencia.Add("MensagemDemonstracao", this.Demonstracao_Mensagem);
            this.ItensPersistencia.Add("TituloProduto", this.Produto_Titulo);
            this.ItensPersistencia.Add("TipoApresentacao", this.Apresentacao_Tipo);
            this.ItensPersistencia.Add("TituloApresentacao", this.Apresentacao_Titulo);
            this.ItensPersistencia.Add("MensagemApresentacao", this.Apresentacao_Mensagem, Checar.IsCampoVazio(this.Apresentacao_Mensagem));
            this.ItensPersistencia.Add("ImagemApresentacao", this.Apresentacao_Imagem, Checar.IsCampoVazio(this.Apresentacao_Imagem));
            this.ItensPersistencia.Add("TentativasAcesso", this.TentativasdeAcesso);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT TOP 1 *
            FROM {0}
            ", this.Tabela);

            LeitorFacade leitor = new LeitorFacade(sql);
            if (leitor.LerLinha())
            {
                this.ID = Conversoes.ToInt64(leitor.GetValor(this.ChavePrimaria));
                this.UsuarioMaster.ID = Conversoes.ToInt64(leitor.GetValor("IdUsuarioMaster"));
                this.Demonstracao_Habilitar = Conversoes.ToBoolean(leitor.GetValor("HabilitarDemonstracao"));
                this.Demonstracao_Titulo = leitor.GetValor("TituloDemonstracao").ToString();
                this.Demonstracao_Mensagem = leitor.GetValor("MensagemDemonstracao").ToString();
                this.Produto_Titulo = leitor.GetValor("TituloProduto").ToString();
                this.Apresentacao_Tipo = (EnumConfiguracao.TipoApresentacao)Conversoes.ToInt32(leitor.GetValor("TipoApresentacao"));
                this.Apresentacao_Titulo = leitor.GetValor("TituloApresentacao").ToString();
                this.Apresentacao_Mensagem = leitor.GetValor("MensagemApresentacao").ToString();
                this.Apresentacao_Imagem = leitor.GetValor("ImagemApresentacao").ToString();
                this.TentativasdeAcesso = Conversoes.ToInt32(leitor.GetValor("TentativasAcesso"));
                base.Log.Materializar(leitor);

                if (materializarClasses)
                    this.UsuarioMaster.Materializar();

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        public string GetVersaoAtual()
        {
            if (Checar.IsCampoVazio(this.VersaoAtual))
                this.VersaoAtual = ControledeVersaoController.GetLast().Versao;

            return this.VersaoAtual;
        }

        #endregion
    }
}
