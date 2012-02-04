using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Utilitarios;
using Swarm.Core.Web;
using Swarm.Core.Web.FrontController;

namespace Swarm.Web.Code.Portal.Controles
{
    public class MicroMenudoUsuario : ViewBase
    {
        public MicroMenudoUsuario()
            : base("Portal/Controles/MicroMenudoUsuario.vm")
        {
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public bool IsMatutino()
        {
            DateTime dataHoraAtual = DateTime.Now;
            DateTime matutinoINICIO = new DateTime(dataHoraAtual.Year, dataHoraAtual.Month, dataHoraAtual.Day, 0, 0, 0);
            DateTime matutinoFIM = new DateTime(dataHoraAtual.Year, dataHoraAtual.Month, dataHoraAtual.Day, 11, 59, 59);
            return dataHoraAtual >= matutinoINICIO && dataHoraAtual <= matutinoFIM;
        }

        public bool IsVespertino()
        {
            DateTime dataHoraAtual = DateTime.Now;
            DateTime vespertinoINICIO = new DateTime(dataHoraAtual.Year, dataHoraAtual.Month, dataHoraAtual.Day, 12, 0, 0);
            DateTime vespertinoFIM = new DateTime(dataHoraAtual.Year, dataHoraAtual.Month, dataHoraAtual.Day, 17, 59, 59);
            return dataHoraAtual >= vespertinoINICIO && dataHoraAtual <= vespertinoFIM;
        }

        public bool IsVisitante()
        {
            return !UsuarioCorrenteFacade.Instance.Autenticado;
        }

        public bool IsPossuiAmbienteDefinido()
        {
            return !Checar.IsCampoVazio(UsuarioCorrenteFacade.Environment);
        }

        public string GetAvatardoUsuarioURI()
        {
            string parametrosAvatar = string.Format("Imagem={0}&Largura=50&Altura=56", UsuarioCorrenteFacade.Instance.GetAvatar(Diretorio.Tipo.Web));
            return Navigation.GetURI(Map.Callback.ObterImagemThumbnail, parametrosAvatar);
        }

        public string GetNomedoUsuario()
        {
            return UsuarioCorrenteFacade.Instance.Login;
        }

        public string GetEnvironment()
        {
            return SecuritySettings.Ambientes.FirstOrDefault(obj => obj.GUID == UsuarioCorrenteFacade.Environment).Titulo;
        }

        public string GetMeuPerfilURI()
        {
            return Navigation.GetURI(Map.Seguranca.MeuPefil);
        }

        public string GetLogoffURI()
        {
            return Navigation.GetURI(Map.Seguranca.Logoff);
        }

        #endregion
    }
}