using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core
{
    public abstract class ModeloControladorBase
    {
        public static ModeloObjetoBase Manter(ModeloObjetoBase objeto, ColecaoPersistencia colecaoPersistencia)
        {
            return ModeloControladorBase.ManteremCascata(objeto, null, colecaoPersistencia);
        }
        public static ModeloObjetoBase Manter(ModeloObjetoBase objeto, string usuarioLOG, ColecaoPersistencia colecaoPersistencia)
        {
            Log log = Log.Create();
            Log.EnumOperacoes operacaoLOG = Checar.MaiorQue(objeto.ID) ? Log.EnumOperacoes.Atualizar : Log.EnumOperacoes.Inserir;
            if (!Checar.IsCampoVazio(usuarioLOG))
            {
                log.Usuario = usuarioLOG;
                log.DatadeCriacao = operacaoLOG == Log.EnumOperacoes.Inserir ? DateTime.Now : Valor.DataInvalida;
                log.UltimaAtualizacao = operacaoLOG == Log.EnumOperacoes.Atualizar ? DateTime.Now : Valor.DataInvalida;
            }

            return ModeloControladorBase.ManteremCascata(objeto, log, colecaoPersistencia);
        }

        public static ModeloObjetoBase ManteremCascata(ModeloObjetoBase objeto, Log log, ColecaoPersistencia colecaoPersistencia)
        {
            ColecaoPersistencia colecao = Checar.IsNull(colecaoPersistencia) ? new ColecaoPersistencia() : colecaoPersistencia;

            if (!Checar.IsNull(log))
            {
                objeto.Log.Usuario = log.Usuario;
                // A data de criação é definida apenas uma única vez na vida do registro.
                if (!Checar.IsDataValida(objeto.Log.DatadeCriacao)) objeto.Log.DatadeCriacao = log.DatadeCriacao;
                objeto.Log.UltimaAtualizacao = log.UltimaAtualizacao;
            }

            EnumPersistencia.Operacao operacao = objeto.Materializado ? EnumPersistencia.Operacao.Alterar : EnumPersistencia.Operacao.Incluir;
            colecao.AdicionarItem(objeto, operacao);

            if (Checar.IsNull(colecaoPersistencia))
                colecao.Persistir();

            return objeto;
        }

        public static bool Excluir(ModeloObjetoBase objeto, ColecaoPersistencia colecaoPersistencia)
        {
            ColecaoPersistencia colecao = Checar.IsNull(colecaoPersistencia) ? new ColecaoPersistencia() : colecaoPersistencia;
            colecao.AdicionarItem(objeto, EnumPersistencia.Operacao.Excluir);

            if (Checar.IsNull(colecaoPersistencia))
                colecao.Persistir();

            return true;
        }
    }
}
