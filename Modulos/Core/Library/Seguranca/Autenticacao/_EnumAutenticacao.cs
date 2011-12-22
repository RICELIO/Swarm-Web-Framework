using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    public abstract class EnumAutenticacao
    {
        public enum TipodeUsuario
        {
            Indefinido = Valor.EnumDefault,
            Administrador = 1,
            Usuario = 2
        }
    }
}
