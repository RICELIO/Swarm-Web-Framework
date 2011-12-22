using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public class Mes
    {
        #region Propriedades

        public string Descricao { get; private set; }
        public int Valor { get; private set; }

        #endregion

        public Mes(string descricao, int valor)
        {
            this.Descricao = descricao;
            this.Valor = valor;
        }

        public override string ToString()
        {
            return this.Descricao;
        }

        #region Métodos Externos

        /// <summary>
        /// Lista com todos os meses do ano.
        /// </summary>
        public static List<Mes> GetAll()
        {
            return new List<Mes>()
            {
                new Mes("Janeiro", 1),
                new Mes("Fevereiro",2),
                new Mes("Março",3),
                new Mes("Abril",4),
                new Mes("Maio",5),
                new Mes("Junho",6),
                new Mes("Julho",7),
                new Mes("Agosto",8),
                new Mes("Setembro",9),
                new Mes("Outubro",10),
                new Mes("Novembro",11),
                new Mes("Dezembro",12)
            };
        }

        #endregion
    }
}
