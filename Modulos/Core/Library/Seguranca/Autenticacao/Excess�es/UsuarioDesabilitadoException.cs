using System;
using System.Runtime.Serialization;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class UsuarioDesabilitadoException : Exception
    {
        public UsuarioDesabilitadoException() : base(Alertas.UsuarioDesabilitado) { }
    }
}
