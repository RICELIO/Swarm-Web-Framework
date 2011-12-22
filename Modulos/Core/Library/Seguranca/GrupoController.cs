using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Persistencia;

namespace Swarm.Core.Library.Seguranca
{
    public abstract class GrupoController
    {
        public static Grupo Manter(Grupo objeto, string usuarioLOG, ColecaoPersistencia colecaoPersistencia)
        {
            return ModeloControladorBase.Manter(objeto, usuarioLOG, colecaoPersistencia) as Grupo;
        }

        public static bool Excluir(Grupo obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Excluir(obj, colecao);
        }

        public static Grupo Create()
        {
            return new Grupo();
        }

        public static Grupo Get(long id)
        {
            return new Grupo(id);
        }

        public static LeitorFacade GetAll(string descricao)
        {
            Grupo obj = GrupoController.Create();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT DISTINCT *
            FROM {0}
            WHERE   1=1
                {1}
            ",
            obj.Tabela,
            Query.IsPreenchido(descricao, "AND Descricao = '{0}'", descricao)
            );

            return new LeitorFacade(sql);
        }
    }
}
