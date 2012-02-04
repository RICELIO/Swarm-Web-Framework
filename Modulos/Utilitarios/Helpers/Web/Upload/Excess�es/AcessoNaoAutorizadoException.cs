using System;

namespace Swarm.Utilitarios.Helpers.Web.Upload
{
    [global::System.Serializable]
    public class AcessoNaoAutorizadoException : Exception
    {        
        public AcessoNaoAutorizadoException() : base(Alertas.Upload_Acesso_Negado) { }
    }
}
