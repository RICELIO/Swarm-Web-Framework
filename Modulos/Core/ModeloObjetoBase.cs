using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Persistencia;
using Swarm.Utilitarios;

namespace Swarm.Core
{
    public abstract class ModeloObjetoBase : IItemPersistencia, ICloneable
    {
        public ModeloObjetoBase(string tabela, string chavePrimaria)
        {
            this.Tabela = tabela;
            this.ChavePrimaria = chavePrimaria;
            this.Log = new Log();

            this.ItensPersistencia = new ListaItensPersistencia(this.Tabela, this.ChavePrimaria);

            this.ID = Valor.MenosUm;
            this.Materializado = false;

            this.Reset();
        }

        protected abstract void Reset();
        protected abstract bool Validar();
        protected abstract void PreencherItensPersistencia();
        public abstract void Materializar(long id, bool materializarClasses);

        private void AssociarChavePrimariaAosItensdePersistencia()
        {
            this.ItensPersistencia.Add(this.ChavePrimaria, this.ID);
        }

        #region Propriedades

        public string Tabela { get; protected set; }
        public string ChavePrimaria { get; protected set; }

        protected ListaItensPersistencia ItensPersistencia { get; set; }
        protected Transacao TransacaoEnvolvida { get; private set; }

        public long ID { get; set; }
        public bool Materializado { get; protected set; }

        /// <summary>
        /// Propriedade opcional. Permite manter um registro de manipulação do registro envolvido.
        /// </summary>
        public Log Log { get; set; }

        #endregion

        #region Métodos

        public virtual void Materializar()
        {
            this.Materializar(this.ID);
        }

        public virtual void Materializar(long id)
        {
            this.Materializar(id, true);
        }

        public virtual void Materializar(bool materializarClasses)
        {
            this.Materializar(this.ID, materializarClasses);
        }

        public virtual ModeloObjetoBase LazyInstation()
        {
            if (!Checar.IsNull(this.ID) && !this.Materializado)
                this.Materializar();

            return this;
        }

        #endregion

        #region IItemPersistencia Members

        public virtual long Incluir()
        {
            if (this.Validar())
            {
                this.AssociarChavePrimariaAosItensdePersistencia();
                this.PreencherItensPersistencia();
                this.ID = this.ItensPersistencia.Inserir(this.TransacaoEnvolvida);
                
                this.Materializado = Valor.Ativo;
                return this.ID;
            }
            else
                return Valor.Zero;
        }

        public virtual bool Alterar()
        {
            if (this.Validar())
            {
                this.AssociarChavePrimariaAosItensdePersistencia();
                this.PreencherItensPersistencia();
                return this.ItensPersistencia.Alterar(this.TransacaoEnvolvida);
            }
            else
                return false;
        }

        public virtual bool Excluir()
        {
            this.AssociarChavePrimariaAosItensdePersistencia();            
            bool excluido = this.ItensPersistencia.Remover(this.TransacaoEnvolvida);
            if (!excluido && Checar.IsNull(this.TransacaoEnvolvida)) return false;

            this.ID = Valor.Zero;
            this.Materializado = Valor.Inativo;
            return true;
        }

        public void DefinirTransacaoEnvolvida(Transacao transacao)
        {
            this.TransacaoEnvolvida = transacao;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}