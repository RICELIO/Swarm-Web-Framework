using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Swarm.Core.Web.FrontController
{
    public class UrlMapItem
    {
        [XmlAttribute(AttributeName = "id", DataType = "int")]
        public int ID
        {
            get;
            set;
        }

        [XmlAttribute(AttributeName = "key")]
        public string Key
        {
            get;
            set;
        }

        [XmlAttribute(AttributeName = "url")]
        public string Url
        {
            get;
            set;
        }

        [XmlAttribute(AttributeName = "titulo")]
        public string Titulo
        {
            get;
            set;
        }

        [XmlAttribute(AttributeName = "ocultar", DataType = "boolean")]
        public bool Ocultar
        {
            get;
            set;
        }
    }
}
