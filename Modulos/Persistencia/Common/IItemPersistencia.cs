using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Persistencia
{
    public interface IItemPersistencia
    {
        long Incluir();
        bool Alterar();
        bool Excluir();

        void DefinirTransacaoEnvolvida(Transacao transacao);
    }
}
