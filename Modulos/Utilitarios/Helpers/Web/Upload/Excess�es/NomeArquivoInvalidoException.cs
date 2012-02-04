using System;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    [global::System.Serializable]
    public class NomeArquivoInvalidoException : Exception
    {        
        public NomeArquivoInvalidoException() : base(Alertas.Upload_Nome_Invalido) { }
    }
}
