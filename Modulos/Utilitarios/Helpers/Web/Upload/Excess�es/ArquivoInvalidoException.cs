using System;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    [global::System.Serializable]
    public class ArquivoInvalidoException : Exception
    {
        public ArquivoInvalidoException() : base(Alertas.Upload_Arquivo_Invalido) { }
    }
}
