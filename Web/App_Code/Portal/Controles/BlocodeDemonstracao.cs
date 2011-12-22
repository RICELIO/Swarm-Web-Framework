using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Core.Web.Configuracao;

namespace Swarm.Web.Code.Portal.Controles
{
    public class BlocodeDemonstracao : ViewBase
    {
        public BlocodeDemonstracao()
            : base("Portal/Controles/BlocodeDemonstracao.vm")
        {
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public string GetTitulo()
        {
            return ConfiguracoesGeraisController.Get().Demonstracao_Titulo;
        }

        public string GetMensagem()
        {
            return ConfiguracoesGeraisController.Get().Demonstracao_Mensagem;
        }

        #endregion
    }
}