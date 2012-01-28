using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Valor
    {
        public const int MenosDois = -2;
        public const int MenosUm = -1;
        public const int Zero = 0;
        public const int Um = 1;
        public const int Dois = 2;
        public const int Três = 3;
        public const int Quatro = 4;
        public const int Cinco = 5;
        public const int Seis = 6;
        public const int Sete = 7;
        public const int Oito = 8;
        public const int Nove = 9;
        public const int Dez = 10;
        public const int Onze = 11;
        public const int Doze = 12;
        public const int Vinte = 20;
        public const int Trinta = 30;
        public const int Cinquenta = 50;
        public const int Cem = 100;

        public const Int32 EnumDefault = Int32.MinValue;
        public const object Nulo = null;
        public const bool Ativo = true;
        public const bool Inativo = false;

        public const string Sim = "1";
        public const string Não = "0";
        public const string Ponto = ".";
        public const string Traço = "-";
        public const string Exclamação = "?";
        public const string Virgula = ",";
        public const string PontoeVirgula = ";";
        public const string EspacoemBranco = " ";
        public const string DoisPontos = ":";
        public const string Barra = "/";
        public const string BarraInvertida = @"\";
        public const string BarraReta = "|";
        public const string Sublinhado = "_";
        public const string Tralha = "#";

        public static readonly DateTime DataInvalida = DateTime.MinValue;
        public static readonly string Default = Valor.Zero.ToString();
        public static readonly string Vazio = string.Empty;

        #region Métodos

        /// <summary>
        /// Retorna uma string caso a posição atual informada seja menor ao tamanho informado
        /// </summary>
        public static string InserirVirgula(int posicaoAtual, int tamanhodaString)
        {
            return posicaoAtual >= tamanhodaString ? Valor.Vazio : Valor.Virgula;
        }

        /// <summary>
        /// Recupera uma lista com todas as datas que estiverem dentro do período informado.
        /// </summary>
        public static List<DateTime> GetDatas(DateTime datadeInicio, DateTime datadeTermino)
        {
            List<DateTime> datas = new List<DateTime>();

            if (datadeInicio > datadeTermino)
                return datas;

            DateTime dataIntervalo = datadeInicio.AddDays(Valor.Um);
            while (dataIntervalo.ToShortDateString() != datadeTermino.ToShortDateString())
            {
                datas.Add(dataIntervalo);
                dataIntervalo = dataIntervalo.AddDays(Valor.Um);
            }
            datas.Add(datadeTermino);

            return datas;
        }

        /// <summary>
        /// Preenche uma string a direita ou a esquerda com o valor desejado - de acordo com a quantidade de caracteres desejáveis.
        /// </summary>
        public static string Preencher(Direcao direcao, string valor, string caracterDESEJAVEL, int tamanho)
        {
            string retorno = valor;
            
            while (Checar.MenorQue(retorno.Length, tamanho))
                retorno = direcao == Direcao.Esquerda ? 
                          string.Concat(caracterDESEJAVEL, retorno) : 
                          string.Concat(retorno, caracterDESEJAVEL);
            
            return retorno;
        }

        public static string GetDataHoraFormatada(object dataHora)
        {
            try
            {
                DateTime obj = Conversoes.ToDateTime(dataHora);
                return string.Format("{0} &agrave;s {1}", obj.ToShortDateString(), obj.ToShortTimeString());
            }
            catch { return Valor.Vazio; }
        }

        #endregion
    }
}
