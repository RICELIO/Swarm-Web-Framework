using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Ferramentas
{
    public abstract class Enumeradores
    {
        public enum TipoCriptografia
        {
            MD5 = 1,
            DES3 = 2,
            WEB = 3
        }

        public enum CampoCriptografiaEnvolvido
        {
            Origem = 1,
            Destino = 2
        }
    }
}
