using System;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    [global::System.Serializable]
    public class AlterarSenhaDadosIncompletosException : Exception
    {
        public AlterarSenhaDadosIncompletosException() : base(Alertas.DadosIncompletosAlteracaodeSenha) { }
    }
}
