using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core.Web.ControledeAcesso
{
    public class AcessoMap : ModeloObjetoBase
    {
        public AcessoMap(long id)
            : this()
        {
            this.Materializar(id, false);
        }

        public AcessoMap()
            : base("AcessoMap", "IdAcessoMap")
        {
        }

        #region Propriedades

        public EnumAcesso.TipodeAcesso Tipo { get; set; }
        /// <summary>
        /// Esta propriedade não está ligada a tabelas no banco de dados, mas refere-se ao ID dos tipos de acesso disponíveis. Exemplo: ID do Ambiente, ID do Grupo etc.
        /// </summary>
        public long IdAcesso { get; set; }
        /// <summary>
        /// ID do item no arquivo de mapeamento (XML).
        /// </summary>
        public int UrlMapID { get; set; }
        /// <summary>
        /// Indica que se trata do principal item da ligação, isto é, a página principal que será chamada.
        /// </summary>
        public bool Principal { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.Tipo = EnumAcesso.TipodeAcesso.Indefinido;
            this.IdAcesso = Valor.Zero;
            this.UrlMapID = Valor.Zero;
            this.Principal = Valor.Inativo;
            base.Log.Reset();
            base.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (this.Tipo == EnumAcesso.TipodeAcesso.Indefinido)
                throw new Exception(Erros.ValorInvalido("Ligação :: Item de Acesso & UrlMap", "Tipo de Acesso"));

            if (this.Tipo == EnumAcesso.TipodeAcesso.Grupo || this.Tipo == EnumAcesso.TipodeAcesso.SuperGrupo)
                throw new Exception("Ligação :: Item de Acesso & UrlMap - Apenas Ambientes e Funcionalidades podem ser mapeadas.");

            if (this.Tipo == EnumAcesso.TipodeAcesso.Ambiente && !this.Principal)
                throw new Exception("Ligação :: Item de Acesso & UrlMap - Só pode haver um único mapeamento associado ao Ambiente e este deve ser definido como Principal.");

            if (Checar.MenorouIgual(this.IdAcesso))
                throw new Exception(Erros.ValorInvalido("Ligação :: Item de Acesso & UrlMap", "Identificador Item de Acesso"));

            if (Checar.MenorouIgual(this.UrlMapID))
                throw new Exception(Erros.ValorInvalido("Ligação :: Item de Acesso & UrlMap", "Identificador Item do UrlMap"));

            if (this.IsCausaráDuplicação())
                throw new Exception("Ligação :: Item de Acesso & UrlMap - Já existe uma vínculo para o cenário desejado. Não é permitido a existência de duplicações.");

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("Tipo", (int)this.Tipo);
            this.ItensPersistencia.Add("IdAcesso", this.IdAcesso);
            this.ItensPersistencia.Add("UrlMapID", this.UrlMapID);
            this.ItensPersistencia.Add("Principal", this.Principal);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT Tipo, IdAcesso, UrlMapID, Principal, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            while (leitor.LerLinha())
            {
                this.Tipo = (EnumAcesso.TipodeAcesso)Conversoes.ToInt32(leitor.GetValor("Tipo"));
                this.IdAcesso = Conversoes.ToInt64(leitor.GetValor("IdAcesso"));
                this.UrlMapID = Conversoes.ToInt32(leitor.GetValor("UrlMapID"));
                this.Principal = Conversoes.ToBoolean(leitor.GetValor("Principal"));
                base.Log.Materializar(leitor);

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos internos

        private bool IsCausaráDuplicação()
        {
            if (this.Tipo == EnumAcesso.TipodeAcesso.Ambiente)
            {
                string sql = string.Format("SELECT * FROM {0} WHERE Tipo = {1} AND IdAcesso = {2}", this.Tabela, (int)EnumAcesso.TipodeAcesso.Ambiente, this.IdAcesso);
                LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
                bool possuiRegistro = Checar.MaiorQue(leitor.TotaldeLinhas);
                leitor.Fechar();

                return (!this.Materializado || Checar.MenorouIgual(this.ID)) && possuiRegistro;
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"
                SELECT *
                FROM {0}
                WHERE   1=1
                    AND Tipo = {1}
                    AND IdAcesso = {2}
                    AND UrlMapID = {3}
                    {4}
                ", this.Tabela, (int)this.Tipo, this.IdAcesso, 
                this.UrlMapID, Query.MaiorQue(this.ID, "AND {0} <> {1}", this.ChavePrimaria, this.ID));

                // Previnir registros idênticos
                LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
                bool possuiRegistro = Checar.MaiorQue(leitor.TotaldeLinhas);
                leitor.Fechar();
                if (possuiRegistro || !this.Principal) return possuiRegistro;

                sql = new StringBuilder();
                sql.AppendFormat(@"
                SELECT *
                FROM {0}
                WHERE   1=1
                    AND Principal = 1
                    AND Tipo = {1}
                    AND IdAcesso = {2}
                    {3}
                ", this.Tabela, (int)this.Tipo, this.IdAcesso,
                Query.MaiorQue(this.ID, "AND {0} <> {1}", this.ChavePrimaria, this.ID));

                // Não pode haver dois itens principais.
                leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
                possuiRegistro = Checar.MaiorQue(leitor.TotaldeLinhas);
                leitor.Fechar();
                return possuiRegistro;
            }
        }

        #endregion
    }
}
