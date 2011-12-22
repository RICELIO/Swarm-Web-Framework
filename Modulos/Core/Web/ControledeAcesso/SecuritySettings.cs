using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Core.Web
{
    public class SecuritySettings
    {
        #region Atributos

        private volatile static List<Ambiente> instance;
        private static object syncRoot = new Object();

        #endregion

        #region Propriedades

        public static List<Ambiente> Ambientes
        {
            get
            {
                if (Checar.IsNull(instance) || Configuracoes.EmDesenvolvimento)
                {
                    lock (syncRoot)
                        instance = AcessoController.GetAmbientes();
                }
                return instance;
            }
        }

        #endregion

        private SecuritySettings() { }

        #region Métodos Externos

        public static void Destroy()
        {
            instance = null;
        }

        public static Ambiente Find(string titulo)
        {
            try
            {
                Ambiente obj = SecuritySettings.Ambientes.Find(a => a.Titulo == titulo);
                if (Checar.IsNull(obj)) throw new Exception();
                return obj;
            }
            catch { throw new Exception(string.Format("Não foi possível localizar o ambiente solicitado. [{0}]", titulo)); }
        }

        public static Ambiente Find(EnumAcesso.CodigoInterno_Ambiente codigoInterno)
        {
            try
            {
                Ambiente obj = SecuritySettings.Ambientes.Find(a => a.CodigoInterno == codigoInterno);
                if (Checar.IsNull(obj)) throw new Exception();
                return obj;
            }
            catch { throw new Exception(string.Format("Não foi possível localizar o ambiente solicitado. [{0}]", codigoInterno)); }
        }

        #endregion
    }
}
