using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Swarm.Utilitarios
{
    internal sealed class Cultura
    {
        #region Atributos

        private volatile static Cultura instance;
        private static object syncRoot = new Object();

        #endregion

        #region Propriedades

        internal CultureInfo Provider { get; private set; }

        public static Cultura Instance
        {
            get
            {
                if (Checar.IsNull(instance))
                {
                    lock (syncRoot)
                        instance = new Cultura();
                }
                return instance;
            }
        }

        #endregion

        private Cultura()
        {
            this.Provider = new CultureInfo("pt-BR", true);

            this.Provider.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            this.Provider.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            this.Provider.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy HH:mm:ss";
        }
    }
}