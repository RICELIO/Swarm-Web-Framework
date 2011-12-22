using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;

namespace Swarm.Core
{
    public class Log
    {
        public Log(string identificacaodoUsuario, DateTime datadeCriacao, DateTime ultimaAtualizacao)
            : this()
        {
            this.Usuario = identificacaodoUsuario;
            this.DatadeCriacao = datadeCriacao;
            this.UltimaAtualizacao = ultimaAtualizacao;
        }

        public Log()
        {
            this.Reset();
        }

        public static Log Create()
        {
            return new Log();
        }

        #region Enumeradores

        public enum EnumOperacoes
        {
            Indefinido = Valor.EnumDefault,
            Atualizar = 1,
            Inserir = 2
        }

        #endregion

        #region Propriedades

        public string Usuario { get; set; }
        public DateTime DatadeCriacao { get; set; }
        public DateTime UltimaAtualizacao { get; set; }

        #endregion

        #region Métodos

        /// <summary>
        /// Este método é útil para ser utilizado em queries que necessitem das informações de LOG.
        /// </summary>
        public string GetColunasSQL()
        {
            return "Usuario, DataCriacao, UltimaAtualizacao";
        }

        public void Reset()
        {
            this.Usuario = Valor.Vazio;
            this.DatadeCriacao = DateTime.Now;
            this.UltimaAtualizacao = DateTime.Now;
        }

        public void Validar()
        {
            if (Checar.IsCampoVazio(this.Usuario))
                throw new Exception(Erros.ValorInvalido("Log", "Identificação do Usuário"));
        }

        public void PreencherItensPersistencia(ListaItensPersistencia listaItensPersistencia)
        {
            listaItensPersistencia.Add("Usuario", this.Usuario);
            listaItensPersistencia.Add("DataCriacao", this.DatadeCriacao, !Checar.IsDataValida(this.DatadeCriacao));
            listaItensPersistencia.Add("UltimaAtualizacao", this.UltimaAtualizacao, !Checar.IsDataValida(Valor.DataInvalida));
        }

        public void Materializar(LeitorFacade leitor)
        {
            this.Usuario = leitor.GetValor("Usuario").ToString();
            this.DatadeCriacao = Conversoes.ToDateTime(leitor.GetValor("DataCriacao"));
            this.UltimaAtualizacao = Conversoes.ToDateTime(leitor.GetValor("UltimaAtualizacao"));
        }

        #endregion
    }
}
