using System;
using System.Runtime.Serialization;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class UsuarioBloqueadoException : Exception
    {
        public UsuarioBloqueadoException() : base(Alertas.UsuarioBloqueado) { }
    }
}
