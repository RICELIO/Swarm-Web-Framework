using System;
using System.Runtime.Serialization;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class SenhaInvalidaException : Exception
    {
        public SenhaInvalidaException() : base(Alertas.SenhaInvalida) { }
    }
}
