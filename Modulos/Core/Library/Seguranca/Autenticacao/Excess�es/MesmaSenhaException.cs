using System;
using System.Runtime.Serialization;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class MesmaSenhaException : Exception
    {
        public MesmaSenhaException() : base(Alertas.MesmaSenha) { }
    }
}
