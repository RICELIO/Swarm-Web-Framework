using System;
using System.Runtime.Serialization;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class AlterarSenhaDadosIncompletosException : Exception
    {
        public AlterarSenhaDadosIncompletosException() : base(Alertas.DadosIncompletosAlteracaodeSenha) { }
    }
}
