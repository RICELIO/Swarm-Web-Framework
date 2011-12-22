using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Core.Web.FrontController.Common
{
    public class MappedPageFacade : PageFacade
    {
        public MappedPageFacade(HttpContext conteudo)
            : base(conteudo)
        {
        }

        public override bool ValidarAutenticacao()
        {
            return UsuarioCorrenteFacade.Instance.Autenticado && !Checar.IsCampoVazio(UsuarioCorrenteFacade.Environment);
        }

        public override bool ValidarPermissoes()
        {
            if (UsuarioCorrenteFacade.Instance.IsAdministrador)
                return Valor.Ativo;

            bool possuiPermissao = Valor.Inativo;

            try
            {
                Ambiente objAmbiente = SecuritySettings.Ambientes.Find(obj => obj.GUID == UsuarioCorrenteFacade.Environment);
                string guidEnvolvido = objAmbiente.FindGUID(this.ID);

                possuiPermissao = !Checar.IsNull(UsuarioCorrenteFacade.Instance.GetPermissoes().Find(obj => obj.GUID == guidEnvolvido));
            }
            catch { possuiPermissao = Valor.Inativo; }

            return possuiPermissao;
        }

        public override bool ValidarControledeAcesso()
        {
            bool paginaDisponivel = Valor.Inativo;

            try
            {
                Ambiente objAmbiente = SecuritySettings.Ambientes.Find(obj => obj.GUID == UsuarioCorrenteFacade.Environment);
                objAmbiente.GetSuperGrupos().ForEach(sg =>
                {
                    sg.GetGrupos().ForEach(g =>
                    {
                        g.GetFuncionalidades().ForEach(f =>
                        {
                            bool paginaLocalizada = !Checar.IsNull(f.GetItens().Find(obj => obj.UrlMapID == this.ID));
                            paginaDisponivel = paginaLocalizada && f.Habilitado;
                            if (paginaDisponivel) return;
                        });
                    });
                });
            }
            catch { paginaDisponivel = Valor.Inativo; }

            return paginaDisponivel;
        }
    }
}
