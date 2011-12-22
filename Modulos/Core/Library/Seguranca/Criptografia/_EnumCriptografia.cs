using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core.Library.Seguranca.Criptografia
{
    public abstract class EnumCriptografia
    {
        public enum Tipo
        {
            Indefinido = Valor.EnumDefault,
            MD5 = 1,
            DES3 = 2
        }
    }
}