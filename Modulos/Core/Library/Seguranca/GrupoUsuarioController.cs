using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Persistencia;

namespace Swarm.Core.Library.Seguranca
{
    public abstract class GrupoUsuarioController
    {
        public static GrupoUsuario Manter(GrupoUsuario objeto, string usuarioLOG, ColecaoPersistencia colecaoPersistencia)
        {
            return ModeloControladorBase.Manter(objeto, usuarioLOG, colecaoPersistencia) as GrupoUsuario;
        }

        public static bool Excluir(GrupoUsuario obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Excluir(obj, colecao);
        }

        public static GrupoUsuario Create()
        {
            return new GrupoUsuario();
        }

        public static GrupoUsuario Get(long id, bool materializarClasses)
        {
            return new GrupoUsuario(id, materializarClasses);
        }

        public static LeitorFacade GetAll(long idGrupo, long idUsuario)
        {
            GrupoUsuario obj = GrupoUsuarioController.Create();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT DISTINCT *
            FROM {0}
            WHERE   1=1
                {1}
                {2}
            ",
            obj.Tabela,
            Query.MaiorQue(idGrupo, "AND IdGrupo = {0}", idGrupo),
            Query.MaiorQue(idUsuario, "AND IdUsuario = {0}", idUsuario)
            );

            return new LeitorFacade(sql);
        }
    }
}
