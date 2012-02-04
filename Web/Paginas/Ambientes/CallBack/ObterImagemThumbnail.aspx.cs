using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Swarm.Utilitarios;
using Swarm.Core.Web;

namespace Swarm.Web.CallBack
{
    public partial class ObterImagemThumbnail : PageBase
    {
        #region Propriedades

        protected string ImagemPath
        {
            get { return base.GetParametro("Imagem"); }
        }

        protected int Largura
        {
            get { return Conversoes.ToInt32(base.GetParametro("Largura")); }
        }

        protected int Altura
        {
            get { return Conversoes.ToInt32(base.GetParametro("Altura")); }
        }

        #endregion

        #region Eventos disparados pelo usuário

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RenderResultadoView();
        }

        #endregion

        #region Método

        public void RenderResultadoView()
        {
            string strFilePath = HttpContext.Current.Server.MapPath(this.ImagemPath);
            Image image = Image.FromFile(strFilePath);
            Image thumbnailImage = image.GetThumbnailImage(this.Largura, this.Altura, new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            MemoryStream imageStream = new MemoryStream();
            thumbnailImage.Save(imageStream, ImageFormat.Png);

            Response.Clear();
            Response.ContentType = "image/png";
            Response.BinaryWrite(imageStream.ToArray());

            imageStream.Close();
        }

        public bool ThumbnailCallback()
        {
            return Valor.Ativo;
        }

        #endregion
    }
}