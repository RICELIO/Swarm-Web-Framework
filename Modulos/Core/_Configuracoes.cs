using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core
{
    public abstract partial class Configuracoes
    {
        public readonly static bool EmDesenvolvimento = Conversoes.ToBoolean(Configuracao.Obter("Desenvolvimento"));
        public readonly static string Logomarca_WebPath = Configuracao.Obter("Logomarca_WebPath");
    }
}
