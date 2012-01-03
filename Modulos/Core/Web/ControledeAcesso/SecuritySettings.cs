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

        private static object syncRoot = new Object();

        private volatile static List<Ambiente> _instance;
        private volatile static List<AcessoMapForm> _itensdeMenu;

        #endregion

        #region Propriedades

        public static List<Ambiente> Ambientes
        {
            get
            {
                if (Checar.IsNull(_instance) || Configuracoes.EmDesenvolvimento)
                {
                    lock (syncRoot)
                        _instance = AcessoController.GetAmbientes();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Recupera a lista dos itens que serão exibidos no menu principal do sistema.
        /// </summary>
        public static List<AcessoMapForm> ItensdeMenu
        {
            get
            {
                if (Checar.IsNull(_itensdeMenu) || Configuracoes.EmDesenvolvimento)
                {
                    lock (syncRoot)
                        _itensdeMenu = SecuritySettings.GetItensdeMenu();
                }
                return _itensdeMenu;
            }
        }

        #endregion

        private SecuritySettings() { }

        #region Métodos Externos

        public static void Destroy()
        {
            _instance = null;
            _itensdeMenu = null;
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

        #region Métodos Internos

        private static List<AcessoMapForm> GetItensdeMenu()
        {
            List<AcessoMapForm> itens = new List<AcessoMapForm>();

            try
            {
                Ambiente objAmbiente = SecuritySettings.Ambientes.FirstOrDefault(obj => obj.GUID == UsuarioCorrenteFacade.Environment);

                // PÁGINA INICIAL DO SISTEMA
                AcessoMap mapAmbiente = objAmbiente.GetItemBase();
                itens.Add(new AcessoMapForm("Início", mapAmbiente.IdAcesso, EnumAcesso.TipodeAcesso.Ambiente));

                // SUPER-GRUPOS QUE SERÃO EXIBIDOS NO MENU
                objAmbiente.GetSuperGrupos().ForEach(mapSuperGrupo =>
                    {
                        if (mapSuperGrupo.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Indefinido && mapSuperGrupo.Habilitado && mapSuperGrupo.Exibir)
                            itens.Add(new AcessoMapForm(mapSuperGrupo.Titulo, mapSuperGrupo.ID, EnumAcesso.TipodeAcesso.SuperGrupo));
                    });

                // GRUPOS QUE SERÃO EXIBIDOS NO MENU
                List<SuperGrupo> listaSuperGrupos = objAmbiente.GetSuperGrupos().FindAll(objSuperGrupo => objSuperGrupo.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Individual);
                listaSuperGrupos.ForEach(objSuperGrupo =>
                    {
                        objSuperGrupo.GetGrupos().ForEach(mapGrupo =>
                            {
                                if (mapGrupo.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Indefinido && mapGrupo.Habilitado && mapGrupo.Exibir)
                                    itens.Add(new AcessoMapForm(mapGrupo.Titulo, mapGrupo.ID, EnumAcesso.TipodeAcesso.Grupo));
                            });
                    });

                // FUNCIONALIDADES QUE SERÃO EXIBIDAS NO MENU
                listaSuperGrupos.ForEach(objSuperGrupo =>
                    {
                        List<Grupo> listaGrupos = objSuperGrupo.GetGrupos().FindAll(objGrupo => objGrupo.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Individual);
                        listaGrupos.ForEach(objGrupo =>
                            {
                                objGrupo.GetFuncionalidades().ForEach(objFuncionalidade =>
                                    {
                                        if (objFuncionalidade.Habilitado && objFuncionalidade.Exibir)
                                        {
                                            AcessoMap mapFuncionalidade = objFuncionalidade.GetItens().First(map => map.Principal);
                                            itens.Add(new AcessoMapForm(objFuncionalidade.Titulo, mapFuncionalidade.UrlMapID, EnumAcesso.TipodeAcesso.Funcionalidade));
                                        }
                                    });
                            });
                    });
            }
            catch { }

            // Ordenando itens de acordo com a Prioridade e Título
            var itensOrdenados = from item in itens
                                 orderby item.Prioridade ascending, item.Titulo ascending
                                 select item;

            return itensOrdenados.ToList<AcessoMapForm>();
        }

        #endregion
    }
}
