using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.ControledeAcesso
{
    public abstract class AcessoController
    {
        public static void Manter(ModeloObjetoBase obj, string usuarioLOG, ColecaoPersistencia colecaoPersistencia)
        {
            ModeloControladorBase.Manter(obj, usuarioLOG, colecaoPersistencia);
        }

        public static bool Excluir(ModeloObjetoBase obj, ColecaoPersistencia colecaoPersistencia)
        {
            return ModeloControladorBase.Excluir(obj, colecaoPersistencia);
        }

        #region Métodos

        public static List<Ambiente> GetAmbientes()
        {
            List<Ambiente> ambientes = new List<Ambiente>();
            Ambiente obj = new Ambiente();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT DISTINCT {0} FROM {1}", obj.ChavePrimaria, obj.Tabela);

            LeitorFacade leitor = new LeitorFacade(sql);
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                ambientes.Add(new Ambiente(id));
            }
            leitor.Fechar();

            return ambientes;
        }

        /// <summary>
        /// Este método irá gerar um novo GUID, garantido que o mesmo ainda não tenha sido utilizado.
        /// </summary>
        public static string GerarGUID(Transacao transacao)
        {
            string guid = Valor.Vazio;
            bool jaExiste = Valor.Inativo;

            do
            {
                guid = Guid.NewGuid().ToString();
                jaExiste = AcessoController.CheckGUID(guid, transacao);
            } while (jaExiste);

            return guid;
        }

        public static bool CheckGUID(string guid, Transacao transacao)
        {
            Ambiente objAmbiente = new Ambiente();
            SuperGrupo objSuperGrupo = new SuperGrupo();
            Grupo objGrupo = new Grupo();
            Funcionalidade objFuncionalidade = new Funcionalidade();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT DISTINCT GUID FROM {0}
            SELECT DISTINCT GUID FROM {1}
            SELECT DISTINCT GUID FROM {2}
            SELECT DISTINCT GUID FROM {3}
            ", objAmbiente.Tabela, objSuperGrupo.Tabela, objGrupo.Tabela, objFuncionalidade.Tabela);

            try
            {
                LeitorFacade leitor = new LeitorFacade(sql, transacao);

                string filtro = string.Format("GUID = '{0}'", guid);
                bool jaExisteAMBIENTE = Checar.MaiorQue(leitor.GetTable(Valor.Zero, filtro, Valor.Vazio).Rows.Count);
                bool jaExisteSUPERGRUPO = Checar.MaiorQue(leitor.GetTable(Valor.Um, filtro, Valor.Vazio).Rows.Count);
                bool jaExisteGRUPO = Checar.MaiorQue(leitor.GetTable(Valor.Dois, filtro, Valor.Vazio).Rows.Count);
                bool jaExisteFUNCIONALIDADE = Checar.MaiorQue(leitor.GetTable(Valor.Três, filtro, Valor.Vazio).Rows.Count);

                return jaExisteAMBIENTE || jaExisteSUPERGRUPO || jaExisteGRUPO || jaExisteFUNCIONALIDADE;
            }
            catch { return Valor.Inativo; }
        }

        #endregion
    }
}
