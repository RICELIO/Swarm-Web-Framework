using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Diretorio
    {
        public enum Tipo
        {
            Indefinido = Valor.EnumDefault,
            Físico = 1,
            Web = 2
        }
    }
}
