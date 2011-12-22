using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Core.Web.Configuracao;

namespace Swarm.Web.Code.Portal.Controles
{
    public class BlocodeApresentacao : ViewBase
    {
        public BlocodeApresentacao()
            : base("Portal/Controles/BlocodeApresentacao.vm")
        {
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public string GetTitulo()
        {
            return ConfiguracoesGeraisController.Get().Apresentacao_Titulo;
        }

        public string GetMensagem()
        {
            return ConfiguracoesGeraisController.Get().Apresentacao_Mensagem;
        }

        #endregion
    }
}