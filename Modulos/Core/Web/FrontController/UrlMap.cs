using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Swarm.Utilitarios;
using Swarm.Core;
using Swarm.Core.Web.FrontController;

namespace Swarm.Core.Web
{
    public class UrlMap
    {
        private UrlMap()
        {
            this.Itens = new List<UrlMapItem>();
        }

        #region Constantes

        private const string NOMEDOCUMENTO_MAPEAMENTO = "UrlMap.xml";

        #endregion

        #region Atributos

        private volatile static UrlMap instance;
        private static object syncRoot = new Object();

        #endregion

        #region Propriedades

        public static UrlMap Instance
        {
            get
            {
                if (Checar.IsNull(instance) || Configuracoes.EmDesenvolvimento)
                {
                    lock (syncRoot)
                        instance = UrlMap.Deserialize();
                }
                return instance;
            }
        }

        [XmlArray("ItensMapeados"), XmlArrayItem(ElementName = "item")]
        public List<UrlMapItem> Itens
        {
            get;
            set;
        }

        #endregion

        #region Métodos Internos

        private static UrlMap Deserialize()
        {
            try
            {
                UrlMap obj = new UrlMap();
                XmlSerializer serializer = new XmlSerializer(typeof(UrlMap));

                string caminhoCompletoXML = string.Concat(Configuracoes.Mapeamento_Path, NOMEDOCUMENTO_MAPEAMENTO);
                TextReader reader = new StreamReader(caminhoCompletoXML);

                obj = (UrlMap)serializer.Deserialize(reader);
                reader.Close();

                return obj;
            }
            catch (Exception erro) { throw erro; }
        }

        #endregion

        #region Métodos Externos

        public static void Destroy()
        {
            instance = null;
        }

        public static UrlMapItem Find(int id)
        {
            try
            {
                UrlMapItem obj = UrlMap.Instance.Itens.Find(item => item.ID == id);
                if (Checar.IsNull(obj)) throw new Exception();
                return obj;
            }
            catch { throw new Exception(string.Format("Não foi possível localizar o item solicitado. [{0}]", id)); }
        }

        public static UrlMapItem Find(string key)
        {
            try
            {
                UrlMapItem obj = UrlMap.Instance.Itens.Find(item => item.Key == key);
                if (Checar.IsNull(obj)) throw new Exception();
                return obj;
            }
            catch { throw new Exception(string.Format("Não foi possível localizar o item solicitado. [{0}]", key)); }
        }

        #endregion
    }
}
