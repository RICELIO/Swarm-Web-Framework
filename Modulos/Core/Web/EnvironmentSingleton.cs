using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;

namespace Swarm.Core.Web
{
    internal class EnvironmentSingleton
    {
        protected const string SESSAO_ATUAL = "_EnvironmentAtual_";

        #region Atributos

        private volatile static EnvironmentSingleton _instance;
        private static object _syncRoot = new Object();

        #endregion

        #region Propriedades

        internal string Token { get; set; }

        public static EnvironmentSingleton Instance
        {
            get
            {
                if (!Checar.IsNull(HttpContext.Current) && !Checar.IsNull(HttpContext.Current.Session))
                {
                    // Se a sessão é nula é porque não esta autenticado.
                    if (Checar.IsNull(HttpContext.Current.Session[SESSAO_ATUAL]))
                    {
                        lock (_syncRoot)
                        {
                            _instance = new EnvironmentSingleton();
                            HttpContext.Current.Session[SESSAO_ATUAL] = _instance;
                        }
                    }
                    else
                    {
                        lock (_syncRoot)
                            _instance = (EnvironmentSingleton)HttpContext.Current.Session[SESSAO_ATUAL];
                    }
                }
                else if (Checar.IsNull(_instance))
                {
                    lock (_syncRoot)
                        _instance = new EnvironmentSingleton();
                }

                return _instance;
            }
        }

        #endregion

        private EnvironmentSingleton()
        {
            this.Token = Valor.Vazio;
        }

        #region Métodos

        public static void Destroy()
        {
            _instance = null;
            HttpContext.Current.Session[SESSAO_ATUAL] = null;
        }

        #endregion
    }
}
