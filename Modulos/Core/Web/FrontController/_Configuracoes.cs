using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core
{
    public abstract partial class Configuracoes
    {
        public readonly static string Mapeamento_Path = Configuracao.Obter("Mapeamento_Path", Valor.Vazio, Valor.BarraInvertida);
    }
}
