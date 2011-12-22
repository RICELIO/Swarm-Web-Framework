using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Checar
    {
        /// <summary>
        /// Verificar se um determinado objeto é nulo.
        /// </summary>
        public static bool IsNull(object obj)
        {
            return obj == Valor.Nulo;
        }

        /// <summary>
        /// Verificar se o string informado está nulo ou vazio
        /// </summary>
        public static bool IsCampoVazio(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Verificar se a data informada é válida.
        /// </summary>
        public static bool IsDataValida(DateTime dataHora)
        {
            return dataHora != DateTime.MinValue && dataHora != DateTime.MaxValue;
        }

        /// <summary>
        /// Verificar se os tipos do parâmetro A e B são idênticos e/ou relacionados.
        /// </summary>
        public static bool IsMesmoTipo(Type comparar, Type comparado)
        {
            return comparar.Equals(comparado) || comparar.IsSubclassOf(comparado);
        }

        /// <summary>
        /// Verificar se o registro A e B (no caso IDs de um determinado objeto) são idênticos. Útil para checagem de existência.
        /// </summary>
        public static bool IsRegistroInvalido(long valorEnvolvido, long valorChecar)
        {
            return (Checar.MenorouIgual(valorEnvolvido) && Checar.MaiorQue(valorChecar)) ||
                   (Checar.MaiorQue(valorChecar) && valorEnvolvido != valorChecar);
        }

        /// <summary>
        /// Verificar se um determinado intervalo de números é inconsistente. Útil para validação de telas com campo de intervalos númericos.
        /// </summary>
        public static bool IsIntervaloInconsistente(long valorInicio, long valorFim, bool considerarValorZero)
        {
            bool isVazio = !considerarValorZero ?
                           Checar.MenorouIgual(valorInicio) || Checar.MenorouIgual(valorFim) :
                           Checar.MenorQue(valorInicio) || Checar.MenorQue(valorFim);

            bool isFinalMenorqueInicio = Checar.MenorQue(valorFim, valorInicio);
            bool isInicioMaiorqueFinal = Checar.MaiorQue(valorInicio, valorFim);

            return isVazio || isFinalMenorqueInicio || isInicioMaiorqueFinal;
        }

        /// <summary>
        /// Verificar se um determinado intervalo de datas é inconsistente. Útil para validação de telas com campo de intervalos de datas.
        /// </summary>
        public static bool IsIntervaloInconsistente(DateTime dataInicio, DateTime dataFim)
        {
            bool isDatasInvalidas = !Checar.IsDataValida(dataInicio) || !Checar.IsDataValida(dataFim);
            bool isDataFimMenorQueInicio = dataFim < dataInicio;
            bool isDataInicioMaiorQueFim = dataInicio > dataFim;

            return isDatasInvalidas || isDataFimMenorQueInicio || isDataInicioMaiorQueFim;
        }

        public static bool MenorouIgual(long valor) { return Checar.MenorouIgual(valor, Valor.Zero); }
        public static bool MenorouIgual(long valor, int comparador) { return valor <= comparador; }
        public static bool MenorouIgual(decimal valor) { return Checar.MenorouIgual(valor, Valor.Zero); }
        public static bool MenorouIgual(decimal valor, decimal comparador) { return valor <= comparador; }

        public static bool MenorQue(long valor) { return Checar.MenorQue(valor, Valor.Zero); }
        public static bool MenorQue(long valor, int comparador) { return valor < comparador; }
        public static bool MenorQue(decimal valor) { return Checar.MenorQue(valor, Valor.Zero); }
        public static bool MenorQue(decimal valor, decimal comparador) { return valor < comparador; }

        public static bool MaiorouIgual(long valor) { return Checar.MaiorouIgual(valor, Valor.Zero); }
        public static bool MaiorouIgual(long valor, int comparador) { return valor >= comparador; }
        public static bool MaiorouIgual(decimal valor) { return Checar.MaiorouIgual(valor, Valor.Zero); }
        public static bool MaiorouIgual(decimal valor, decimal comparador) { return valor >= comparador; }

        public static bool MaiorQue(long valor) { return Checar.MaiorQue(valor, Valor.Zero); }
        public static bool MaiorQue(long valor, int comparador) { return valor > comparador; }
        public static bool MaiorQue(decimal valor) { return Checar.MaiorQue(valor, Valor.Zero); }
        public static bool MaiorQue(decimal valor, decimal comparador) { return valor > comparador; }
    }
}
