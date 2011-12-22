using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Persistencia.BancosdeDados;

namespace Swarm.Persistencia
{
    public abstract class SGBDFactory
    {
        public static Swarm.Persistencia.SGBD Criar()
        {
            switch (Conexão.Instance.SGBD)
            {
                default:
                case EnumPersistencia.SGBD.Indefinido:
                case EnumPersistencia.SGBD.MySQL:
                    return new Mysql();
                case EnumPersistencia.SGBD.SqlServer:
                    return new SqlServer();
            }
        }
    }
}
