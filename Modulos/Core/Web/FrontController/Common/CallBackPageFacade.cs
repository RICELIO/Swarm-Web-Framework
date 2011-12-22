using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca.Autenticacao;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Core.Web.FrontController.Common
{
    public class CallBackPageFacade : PageFacade
    {
        public CallBackPageFacade(HttpContext conteudo)
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
            bool isCallBackPage = Valor.Inativo;

            try
            {
                Ambiente objAmbienteCallBack = SecuritySettings.Find(EnumAcesso.CodigoInterno_Ambiente.CallBack);
                objAmbienteCallBack.GetSuperGrupos().ForEach(sg =>
                {
                    sg.GetGrupos().ForEach(g =>
                    {
                        g.GetFuncionalidades().ForEach(f =>
                        {
                            isCallBackPage = !Checar.IsNull(f.GetItens().Find(obj => obj.UrlMapID == pageID));
                            if (isCallBackPage) return;
                        });
                    });
                });
            }
            catch { isCallBackPage = Valor.Inativo; }

            return isCallBackPage;
        }

        #endregion
    }
}
