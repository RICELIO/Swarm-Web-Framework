using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;

namespace Swarm.Core.Web.FrontController.Common
{
    public class InvalidPageFacade : PageFacade
    {
        public InvalidPageFacade(HttpContext conteudo)
            : base(conteudo)
        {
            Navigation.ShowMessage("Não foi possível identificar a página solicitada. Volte para a página inicial do sistema.");
        }

        public override bool ValidarAutenticacao()
        {
            return Valor.Ativo;
        }

        public override bool ValidarPermissoes()
        {
            return Valor.Ativo;
        }

        public override string GetURI()
        {
            return Valor.Vazio;
        }
    }
}
