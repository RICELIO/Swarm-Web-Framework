using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    public abstract class UploadBase
    {
        protected UploadBase(FileUpload controle, string path, string fileName, int fileSizeKB)
        {
            this.Controle = controle;
            this.Path = !path.EndsWith(Valor.Barra) ? string.Concat(path, Valor.Barra) : path;
            this.Arquivo_Nome = fileName;
            this.Arquivo_Tamanho = fileSizeKB * Valor.Mega_1;
        }

        protected abstract List<string> GetExtensoes();

        #region Propriedades

        protected FileUpload Controle { get; set; }
        protected string Path { get; set; }
        protected string Arquivo_Nome { get; set; }
        protected int Arquivo_Tamanho { get; set; }

        #endregion

        #region Métodos

        public string GetNomedoArquivo()
        {
            return Checar.IsCampoVazio(this.Arquivo_Nome) ? this.Controle.FileName : this.Arquivo_Nome;
        }

        protected string GetFullPath()
        {
            return string.Concat(this.Path, this.GetNomedoArquivo());
        }

        public virtual bool Enviar()
        {
            try
            {
                this.ValidarItensControle();

                try
                {
                    this.Controle.PostedFile.SaveAs(this.GetFullPath());
                    return Valor.Ativo;
                }
                catch (UnauthorizedAccessException) { throw new AcessoNaoAutorizadoException(); }
            }
            catch (Exception erro) { throw erro; }
        }

        public virtual void Excluir()
        {
            try
            {
                this.Controle.PostedFile.InputStream.Dispose();
            }
            catch { throw new AcessoNaoAutorizadoException(); }
        }

        #endregion

        #region Métodos Internos

        protected virtual void ValidarItensControle()
        {
            if (Checar.IsCampoVazio(this.GetNomedoArquivo()))
                throw new NomeArquivoInvalidoException();

            if (Checar.MenorouIgual(this.Controle.PostedFile.ContentLength))
                throw new ArquivoInvalidoException();

            if (!this.GetExtensoes().Contains(this.Controle.PostedFile.ContentType))
                throw new ExtensaoInvalidaException();

            if (Checar.MaiorQue(this.Controle.PostedFile.ContentLength, this.Arquivo_Tamanho))
            {
                int valorTamanhoArquivoKB = this.Arquivo_Tamanho / Valor.Mega_1;
                throw new TamanhoInvalidoException(string.Format("O tamanho do arquivo excedeu os {0}KB permitido.", valorTamanhoArquivoKB));
            }
        }

        #endregion
    }
}
