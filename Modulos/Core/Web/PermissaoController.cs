using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Persistencia;

namespace Swarm.Core.Web
{
    public abstract class PermissaoController
    {
        public static Permissao Manter(Permissao objeto, string usuarioLOG, ColecaoPersistencia colecaoPersistencia)
        {
            return ModeloControladorBase.Manter(objeto, usuarioLOG, colecaoPersistencia) as Permissao;
        }

        public static bool Excluir(Permissao obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Excluir(obj, colecao);
        }

        public static Permissao Create()
        {
            return new Permissao();
        }

        public static Permissao Get(long id, bool materializarClasses)
        {
            return new Permissao(id, materializarClasses);
        }

        public static LeitorFacade GetAll(long idUsuario, long idGrupo)
        {
            Permissao obj = PermissaoController.Create();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT DISTINCT *
            FROM {0}
            WHERE   1=1
                {1}
                {2}
            ",
            obj.Tabela,
            Query.MaiorQue(idUsuario, "AND IdUsuario = {0}", idUsuario),
            Query.MaiorQue(idGrupo, "AND IdGrupo = {0}", idGrupo)
            );

            return new LeitorFacade(sql);
        }

        #region Consultas

        public static LeitorFacade GetAllForUser(long idUsuario)
        {
            Permissao obj = PermissaoController.Create();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            DECLARE @IdUsuario [bigint];
            SET @IdUsuario = {0};

            SELECT DISTINCT PM.{1}, PM.[GUID]
            FROM Permissao PM
            WHERE	1=1
	            AND PM.IdUsuario = @IdUsuario
            	
            UNION

            SELECT DISTINCT PM.{1}, PM.[GUID]
            FROM Permissao PM
            INNER JOIN GrupoUsuario GU ON GU.IdGrupo = PM.IdGrupo
            INNER JOIN Usuario US ON US.IdUsuario = GU.IdUsuario
            WHERE	1=1
	            AND PM.IdUsuario = @IdUsuario
            ", idUsuario, obj.ChavePrimaria);

            return new LeitorFacade(sql);
        }

        #endregion
    }
}
