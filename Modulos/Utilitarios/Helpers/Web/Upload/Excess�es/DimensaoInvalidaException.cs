using System;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    [global::System.Serializable]
    public class DimensaoInvalidaException : Exception
    {
        public DimensaoInvalidaException() { }
        public DimensaoInvalidaException(string message) : base(message) { }
    }
}
