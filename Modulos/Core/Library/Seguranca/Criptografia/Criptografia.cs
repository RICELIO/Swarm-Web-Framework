using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Core.Library.Seguranca.Criptografia
{
    public abstract class Criptografia
    {
        #region Propriedades

        private EnumCriptografia.Tipo Tipo { get; set; }

        protected UTF8Encoding Encoder
        {
            get { return new UTF8Encoding(); }
        }

        #endregion

        public Criptografia(EnumCriptografia.Tipo tipoCriptografia)
        {
            this.Tipo = tipoCriptografia;
        }

        #region Métodos Abstratos
        public abstract string Criptografar(string texto);
        public abstract string Descriptografar(string texto);
        public abstract bool Comparar(string texto, string hash);
        #endregion
    }
}