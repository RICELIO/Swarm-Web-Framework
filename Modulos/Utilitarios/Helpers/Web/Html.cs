using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Swarm.Utilitarios
{
    public abstract class Html
    {
        public const string EspaçoemBranco = "&nbsp;";
        public const string QuebradeLinha = "<br />";

        #region Métodos Externos

        public static string Concat(string letra, string código)
        {
            string códigoHtml = código.Replace("&", Valor.Vazio);
            return string.Format("&{1}{2}", letra, códigoHtml);
        }

        public static string Encode(string value)
        {
            return HttpContext.Current.Server.UrlEncode(value);
        }

        public static string Decode(string value)
        {
            return HttpContext.Current.Server.UrlDecode(value);
        }

        #endregion
    }
}
