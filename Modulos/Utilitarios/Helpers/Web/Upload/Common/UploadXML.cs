using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Swarm.Utilitarios.Helpers.Web.Upload;

namespace Swarm.Utilitarios.Helpers.Web
{
    public class UploadXML : UploadBase
    {
        public UploadXML(FileUpload arquivo, string path, string arquivoNome, int arquivoTamanhoKB)
            : base(arquivo, path, arquivoNome, arquivoTamanhoKB)
        {
            this.DefinirItensControle_Arquivo();
        }

        #region Métodos Internos

        protected override List<string> GetExtensoes()
        {
            return new List<string>() { "text/xml" };
        }

        private void DefinirItensControle_Arquivo()
        {
            bool possuiExtensaoValida = this.Arquivo_Nome.EndsWith(".xml");
            if (Checar.IsCampoVazio(this.Arquivo_Nome) || possuiExtensaoValida) return;

            this.Arquivo_Nome = string.Format("{0}.xml", this.Arquivo_Nome);
        }

        #endregion
    }
}