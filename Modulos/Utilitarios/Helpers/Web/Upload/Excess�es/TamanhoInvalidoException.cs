using System;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    [global::System.Serializable]
    public class TamanhoInvalidoException : Exception
    {
        public TamanhoInvalidoException() { }
        public TamanhoInvalidoException(string message) : base(message) { }
    }
}
