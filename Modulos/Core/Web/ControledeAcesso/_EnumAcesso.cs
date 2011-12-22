using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core.Web.ControledeAcesso
{
    public abstract class EnumAcesso
    {
        public enum TipodeAcesso
        {
            Indefinido = Valor.EnumDefault,
            Ambiente = 1,
            SuperGrupo = 2,
            Grupo = 3,
            Funcionalidade = 4
        }

        public enum CodigoInterno_Ambiente
        {
            Indefinido = Valor.EnumDefault,
            /// <summary>
            /// Ambiente voltado para armazenar as páginas de CallBack (Retorno de conteúdo não paginado).
            /// </summary>
            CallBack = 1,
            /// <summary>
            /// Ambiente voltado para armazenar as páginas que não trabalharão com autenticação.
            /// </summary>
            Anonimo = 2
        }

        public enum CodigoInterno_Grupo
        {
            Indefinido = Valor.EnumDefault,
            /// <summary>
            /// Itens ligados a este código não serão associados, isto é, serão exibidos de forma livre.
            /// </summary>
            Individual = 1
        }
    }
}
