using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core.Web.ControledeAcesso
{
    /// <summary>
    /// Referência ao controle de Itens que serão exibidos no menu principal do sistema.
    /// </summary>
    public class AcessoMapForm
    {
        public AcessoMapForm(string titulo, long id, EnumAcesso.TipodeAcesso tipodeAcesso)
        {
            this.Titulo = titulo;
            this.ID = id;
            this.TipodeAcesso = tipodeAcesso;
            this.Prioridade = this.TipodeAcesso == EnumAcesso.TipodeAcesso.Ambiente ? Valor.Um : Valor.Dois;
        }

        #region Propriedades

        public string Titulo { get; private set; }
        public long ID { get; private set; }
        public EnumAcesso.TipodeAcesso TipodeAcesso { get; private set; }
        public int Prioridade { get; private set; }

        #endregion
    }
}
