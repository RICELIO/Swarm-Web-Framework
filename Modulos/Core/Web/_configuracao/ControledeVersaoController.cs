using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.Configuracao
{
    public abstract class ControledeVersaoController
    {
        public static ControledeVersao Manter(ControledeVersao obj, string usuarioLOG, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Manter(obj, usuarioLOG, colecao) as ControledeVersao;
        }

        public static bool Excluir(ControledeVersao obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Excluir(obj, colecao);
        }

        public static ControledeVersao Create()
        {
            return new ControledeVersao();
        }

        public static ControledeVersao Get(long id)
        {
            return new ControledeVersao(id);
        }

        #region Consultas

        /// <summary>
        /// Recuperar todos os registros de Versão cadastrados no sistema.
        /// </summary>
        /// <param name="registros">Se maior que 0 será limitado o número de registros solicitados.</param>
        public static LeitorFacade GetAll(int registros)
        {
            ControledeVersao obj = ControledeVersaoController.Create();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT {2} *
            FROM {0}
            ORDER BY {1} DESC
            {3}
            ",
            obj.Tabela, obj.ChavePrimaria,
            Query.IsSGBD(Query.MaiorQue(registros, Conexão.Instance.GetLimitadordeRegistrosSQL(registros)), EnumPersistencia.SGBD.SqlServer),
            Query.IsSGBD(Query.MaiorQue(registros, Conexão.Instance.GetLimitadordeRegistrosSQL(registros)), EnumPersistencia.SGBD.MySQL)
            );

            return new LeitorFacade(sql);
        }

        public static ControledeVersao GetLast()
        {
            ControledeVersao obj = new ControledeVersao();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT MAX(DISTINCT {1}) AS {1}
            FROM {0}
            ", obj.Tabela, obj.ChavePrimaria);

            LeitorFacade leitor = new LeitorFacade(sql);
            if (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(Valor.Zero));
                obj.Materializar(id);
            }
            leitor.Fechar();

            return obj;
        }

        #endregion
    }
}
