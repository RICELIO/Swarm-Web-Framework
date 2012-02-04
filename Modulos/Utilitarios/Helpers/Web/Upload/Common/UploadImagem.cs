using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Swarm.Utilitarios.Helpers.Web.Upload;

namespace Swarm.Utilitarios.Helpers.Web
{
    public class UploadImagem : UploadBase
    {
        public UploadImagem(FileUpload controle, string path, string arquivoNome, int arquivoTamanhoKB, int larguraMaxima, int alturaMaxima, List<string> listaExtensoesSuportadas)
            : base(controle, path, arquivoNome, arquivoTamanhoKB)
        {
            this.LarguraMaxima = larguraMaxima;
            this.AlturaMaxima = alturaMaxima;
            this.ListaExtensoesPermitidas = listaExtensoesSuportadas;
            this.DefinirItensControle_Arquivo();
        }

        #region Propriedades

        private int LarguraMaxima { get; set; }
        private int AlturaMaxima { get; set; }

        private List<string> ListaExtensoesPermitidas { get; set; }

        #endregion

        #region Métodos

        public override bool Enviar()
        {
            this.ValidarItensControle();
            return base.Enviar();
        }

        #endregion

        #region Métodos Internos

        protected override List<string> GetExtensoes()
        {
            if (!Checar.IsNull(this.ListaExtensoesPermitidas) && Checar.MaiorQue(this.ListaExtensoesPermitidas.Count))
                return this.ListaExtensoesPermitidas;

            return new List<string>(new string[]
            {
                "image/pjpeg",
                "image/jpeg",
                "image/jpg",
                "image/x-png",
                "image/png",
                "image/gif"
            });
        }

        protected override void ValidarItensControle()
        {
            base.ValidarItensControle();
            this.ValidarItensControle_Imagem();
        }

        private void ValidarItensControle_Imagem()
        {
            System.Drawing.Image imagem = System.Drawing.Image.FromStream(this.Controle.PostedFile.InputStream);

            if (Checar.MaiorQue(this.LarguraMaxima) && Checar.MaiorQue(imagem.Width, this.LarguraMaxima))
                throw new DimensaoInvalidaException(String.Format("Largura do arquivo deve ser {0}cm.", this.LarguraMaxima));
            
            if (Checar.MaiorQue(this.AlturaMaxima) && Checar.MaiorQue(imagem.Height, this.AlturaMaxima))
                throw new DimensaoInvalidaException(String.Format("Altura do arquivo deve ser {0}cm.", this.AlturaMaxima));
        }

        private void DefinirItensControle_Arquivo()
        {
            bool possuiExtensaoValida = this.GetExtensoes().Exists(str => this.Arquivo_Nome.Contains(str.Split('/')[1]));
            if (Checar.IsCampoVazio(this.Arquivo_Nome) || possuiExtensaoValida) return;

            // Obtendo extensão do arquivo envolvido.
            string extensao = this.Controle.PostedFile.ContentType.Split('/')[1];
            extensao = extensao.Replace("pjpeg", "jpg");
            extensao = extensao.Replace("jpeg", "jpg");
            extensao = extensao.Replace("x-png", "png");

            this.Arquivo_Nome = string.Format("{0}.{1}", this.Arquivo_Nome, extensao);
        }

        #endregion
    }
}
