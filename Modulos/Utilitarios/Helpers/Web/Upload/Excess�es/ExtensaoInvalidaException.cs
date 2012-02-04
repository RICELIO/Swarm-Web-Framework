using System;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    [global::System.Serializable]
    public class ExtensaoInvalidaException : Exception
    {
        public ExtensaoInvalidaException() : base(Alertas.Upload_Extensao_Invalida) { }
    }
}
