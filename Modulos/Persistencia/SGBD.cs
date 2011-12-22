using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Persistencia
{
    public abstract class SGBD
    {
        internal abstract string GetStringdeConexao();
        internal abstract DbConnection GetObjConexao();

        internal abstract string Executar(StringBuilder querySql, Transacao transacao, EnumPersistencia.Operacao tipodeOperacao);
        internal abstract DataSet RecuperarDataSet(StringBuilder querySql, Transacao transacao);

        internal virtual string GetFormatoPadraoDateTime()
        {
            return "yyyy-MM-dd HH:mm:ss";
        }

        internal abstract string GetSqlChavePrimariaInserida();
        internal abstract string GetSqlLimitadordeRegistros(int registros);
    }
}
