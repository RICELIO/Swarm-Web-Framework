using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;

namespace Swarm.Core.Web.Configuracao
{
    internal class ConfiguracoesGeraisSingleton
    {
        protected const string SESSAO_ATUAL = "_ConfiguracoesGeraisAtual_";

        #region Atributos

        private volatile static ConfiguracoesGeraisSingleton _instance;
        private static object _syncRoot = new Object();

        #endregion

        #region Propriedades

        internal ConfiguracoesGerais Configuracao { get; set; }

        public static ConfiguracoesGeraisSingleton Instance
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
                            _instance = new ConfiguracoesGeraisSingleton();
                            HttpContext.Current.Session[SESSAO_ATUAL] = _instance;
                        }
                    }
                    else
                    {
                        lock (_syncRoot)
                            _instance = (ConfiguracoesGeraisSingleton)HttpContext.Current.Session[SESSAO_ATUAL];
                    }
                }
                else if (Checar.IsNull(_instance))
                {
                    lock (_syncRoot)
                        _instance = new ConfiguracoesGeraisSingleton();
                }

                return _instance;
            }
        }

        #endregion

        private ConfiguracoesGeraisSingleton()
        {
            this.Configuracao = ConfiguracoesGeraisController.Create();
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
