using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Persistencia
{
    internal class ItemPersistencia
    {
        #region Propriedades

        public string Nome { get; set; }
        public object Valor { get; set; }
        public Type Tipo { get; set; }

        #endregion

        public ItemPersistencia(string nome, object valor, Type tipo)
        {
            this.Nome = nome;
            this.Valor = valor;
            this.Tipo = tipo;
        }
    }
}
