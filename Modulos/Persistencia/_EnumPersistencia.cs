using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Persistencia
{
    public abstract class EnumPersistencia
    {
        public enum SGBD
        {
            Indefinido = Valor.EnumDefault,
            MySQL = 1,
            SqlServer = 2
        }

        public enum Operacao
        {
            Indefinido = Valor.EnumDefault,
            Incluir = 1,
            Alterar = 2,
            Excluir = 3
        }

        public enum Iteracao
        {
            Indefinido = Valor.EnumDefault,
            Normal = 1,
            Colecao = 2
        }
    }
}
