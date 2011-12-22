using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios.Helpers.Web
{
    public abstract class Janela
    {
        /// <summary>
        /// Obter o tipo de janela envolvida.
        /// </summary>
        public enum Size
        {
            Normal = 1,
            Popup = 2,
            TelaCheia = 3
        }

        /// <summary>
        /// Obter de página envolvida.
        /// </summary>
        public enum Sender
        {
            Self = 1,
            Opener = 2
        }

        /// <summary>
        /// Obter largura padrão de uma janela (window) utilizada pelo sistema.
        /// </summary>
        public static int Largura
        {
            get { return 800; }
        }

        /// <summary>
        /// Obter altura padrão de uma janela (window) utilizada pelo sistema.
        /// </summary>
        public static int Altura
        {
            get { return 600; }
        }
    }
}
