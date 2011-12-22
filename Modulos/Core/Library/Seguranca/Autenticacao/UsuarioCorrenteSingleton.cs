using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Swarm.Utilitarios;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    internal class UsuarioCorrenteSingleton
    {
        protected const string SESSAO_ATUAL_USUARIO = "_UsuarioAtual_";

        #region Atributos

        private volatile static UsuarioCorrenteSingleton _instance;
        private static object _syncRoot = new Object();

        #endregion

        #region Propriedades

        public Usuario Usuario { get; set; }

        public static UsuarioCorrenteSingleton Instance
        {
            get
            {
                if (!Checar.IsNull(HttpContext.Current) && !Checar.IsNull(HttpContext.Current.Session))
                {
                    // Se a sessão é nula é porque não esta autenticado.
                    if (Checar.IsNull(HttpContext.Current.Session[SESSAO_ATUAL_USUARIO]))
                    {
                        lock (_syncRoot)
                        {
                            _instance = new UsuarioCorrenteSingleton();
                            HttpContext.Current.Session[SESSAO_ATUAL_USUARIO] = _instance;
                        }
                    }
                    else
                    {
                        lock (_syncRoot)
                            _instance = (UsuarioCorrenteSingleton)HttpContext.Current.Session[SESSAO_ATUAL_USUARIO];
                    }
                }
                else if (Checar.IsNull(_instance))
                {
                    lock (_syncRoot)
                        _instance = new UsuarioCorrenteSingleton();
                }

                return _instance;
            }
        }

        #endregion

        private UsuarioCorrenteSingleton()
        {
            this.Usuario = UsuarioController.Create();
        }

        #region Métodos

        public static void Destroy()
        {
            _instance = null;
            HttpContext.Current.Session[SESSAO_ATUAL_USUARIO] = null;
        }

        #endregion
    }
}
