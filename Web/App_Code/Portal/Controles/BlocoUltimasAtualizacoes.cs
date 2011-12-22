using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Persistencia;
using Swarm.Core.Web.Configuracao;
using Swarm.Utilitarios;

namespace Swarm.Web.Code.Portal.Controles
{
    public class BlocoUltimasAtualizacoes : ViewBase
    {
        private int RegistrosPermitidos { get; set; }

        public BlocoUltimasAtualizacoes(int maxRegistrosPermitidos)
            : base("Portal/Controles/BlocoUltimasAtualizacoes.vm")
        {
            this.RegistrosPermitidos = maxRegistrosPermitidos;
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public DataRowCollection GetVersoes()
        {
            LeitorFacade leitor = ControledeVersaoController.GetAll(this.RegistrosPermitidos);
            DataRowCollection obj = leitor.GetTable().Rows;
            leitor.Fechar();

            return obj;
        }

        public string GetDataHora(object obj)
        {
            return Valor.GetDataHoraFormatada(obj);
        }

        #endregion
    }
}