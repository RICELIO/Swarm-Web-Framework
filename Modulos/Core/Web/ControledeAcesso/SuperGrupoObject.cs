using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.ControledeAcesso
{
    public class SuperGrupo : ModeloObjetoBase
    {
        public SuperGrupo(long id, bool materializarClasses)
            : this()
        {
            this.Materializar(id, materializarClasses);
        }

        public SuperGrupo()
            : base("AcessoSuperGrupo", "IdAcessoSuperGrupo")
        {
        }

        #region Propriedades

        public string GUID { get; private set; }
        public string Titulo { get; set; }
        public bool Habilitado { get; set; }
        /// <summary>
        /// Se verdadeiro significa que este item será exibido para o usuário.
        /// </summary>
        public bool Exibir { get; set; }
        public Ambiente Ambiente { get; set; }
        /// <summary>
        /// Indefinido - Os itens associados serão aninhados. | 
        /// Individual - Não será exibido o Super Grupo, mas os Grupos associados.
        /// </summary>
        public EnumAcesso.CodigoInterno_Grupo CodigoInterno { get; set; }

        private List<Grupo> Grupos { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.GUID = Valor.Vazio;
            this.Titulo = Valor.Vazio;
            this.Habilitado = Valor.Ativo;
            this.Exibir = Valor.Inativo;
            this.Ambiente = new Ambiente();
            this.CodigoInterno = EnumAcesso.CodigoInterno_Grupo.Indefinido;
            base.Log.Reset();

            this.Grupos = new List<Grupo>();
            
            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (!base.Materializado && Checar.IsCampoVazio(this.GUID))
                this.GUID = AcessoController.GerarGUID(this.TransacaoEnvolvida);
            else if (Checar.IsCampoVazio(this.GUID))
                throw new Exception(Erros.ValorInvalido("Super-Grupo", "GUID"));

            if (Checar.IsCampoVazio(this.Titulo))
                throw new Exception(Erros.ValorInvalido("Super-Grupo", "Titulo"));

            if (Checar.MenorouIgual(this.Ambiente.ID))
                throw new Exception(Erros.ValorInvalido("Super-Grupo", "Ambiente"));

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("GUID", this.GUID);
            this.ItensPersistencia.Add("Titulo", this.Titulo);
            this.ItensPersistencia.Add("Habilitado", this.Habilitado);
            this.ItensPersistencia.Add("Exibir", this.Exibir);
            this.ItensPersistencia.Add("IdAmbiente", this.Ambiente.ID);
            this.ItensPersistencia.Add("CodigoInterno", (int)this.CodigoInterno, this.CodigoInterno == EnumAcesso.CodigoInterno_Grupo.Indefinido);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT GUID, Titulo, Habilitado, Exibir, IdAmbiente, CodigoInterno, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.GUID = leitor.GetValor("GUID").ToString();
                this.Titulo = leitor.GetValor("Titulo").ToString();
                this.Habilitado = Conversoes.ToBoolean(leitor.GetValor("Habilitado"));
                this.Exibir = Conversoes.ToBoolean(leitor.GetValor("Exibir"));
                this.Ambiente.ID = Conversoes.ToInt64(leitor.GetValor("IdAmbiente"));
                this.CodigoInterno = (EnumAcesso.CodigoInterno_Grupo)Conversoes.ToInt32(leitor.GetValor("CodigoInterno"));
                base.Log.Materializar(leitor);

                if (materializarClasses)
                    this.Ambiente.Materializar();

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos de ligação

        public List<Grupo> GetGrupos()
        {
            if (Checar.MaiorQue(this.Grupos.Count))
                return this.Grupos;

            Grupo obj = new Grupo();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT DISTINCT {0} FROM {1} WHERE IdSuperGrupo = {2}", obj.ChavePrimaria, obj.Tabela, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql);
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                this.Grupos.Add(new Grupo(id, Valor.Inativo));
            }
            leitor.Fechar();

            return this.Grupos;
        }

        #endregion
    }
}