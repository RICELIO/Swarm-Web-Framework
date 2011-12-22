using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Persistencia
{
    internal class ItemTransacao
    {
        #region Propriedades
        public IItemPersistencia Objeto { get; private set; }
        public EnumPersistencia.Operacao Tipo { get; private set; }
        #endregion

        public ItemTransacao(IItemPersistencia objeto, EnumPersistencia.Operacao tipoTransacao)
        {
            this.Objeto = objeto;
            this.Tipo = tipoTransacao;
        }
    }
}
