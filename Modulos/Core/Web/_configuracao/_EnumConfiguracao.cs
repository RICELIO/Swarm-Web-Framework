using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core.Web.Configuracao
{
    public abstract class EnumConfiguracao
    {
        public enum TipoApresentacao
        {
            Indefinido = Valor.EnumDefault,
            Textual = 1,
            Imagem = 2
        }
    }
}
