using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Core.Web.FrontController.Common
{
    public class AnonymousPageFacade : PageFacade
    {
        public AnonymousPageFacade(HttpContext conteudo)
            : base(conteudo)
        {
        }

        public override bool ValidarAutenticacao()
        {
            return Valor.Ativo;
        }

        public override bool ValidarPermissoes()
        {
            return Valor.Ativo;
        }

        #region Métodos Externos

        public static bool IsTrue(int pageID)
        {
            bool isAnonymousPage = Valor.Inativo;

            try
            {
                Ambiente objAmbienteAnonimo = SecuritySettings.Find(EnumAcesso.CodigoInterno_Ambiente.Anonimo);
                objAmbienteAnonimo.GetSuperGrupos().ForEach(sg =>
                    {
                        sg.GetGrupos().ForEach(g =>
                            {
                                g.GetFuncionalidades().ForEach(f =>
                                    {
                                        isAnonymousPage = !Checar.IsNull(f.GetItens().Find(obj => obj.UrlMapID == pageID));
                                        if (isAnonymousPage) return;
                                    });
                            });
                    });
            }
            catch { isAnonymousPage = Valor.Inativo; }

            return isAnonymousPage;
        }

        #endregion
    }
}
