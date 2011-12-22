using System;
using System.Runtime.Serialization;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class UsuarioNaoExisteException : Exception
    {
        public UsuarioNaoExisteException(string message) : base(Alertas.UsuarioInexistente) { }
    }
}
