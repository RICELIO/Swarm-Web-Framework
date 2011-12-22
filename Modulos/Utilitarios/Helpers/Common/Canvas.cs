using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public class Canvas
    {
        #region Propriedades

        public int X { get; private set; }
        public int Y { get; private set; }

        #endregion

        /// <summary>
        /// Objeto utilitário para definir Largura e Altura de uma imagem.
        /// </summary>
        /// <param name="x">Largura</param>
        /// <param name="y">Alturea</param>
        public Canvas(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Objeto utilitário para definir Largura e Altura de uma imagem.
        /// </summary>
        public Canvas()
        {
            this.Reset();
        }

        #region Métodos

        private void Reset()
        {
            this.X = Valor.Zero;
            this.Y = Valor.Zero;
        }

        #endregion
    }
}
