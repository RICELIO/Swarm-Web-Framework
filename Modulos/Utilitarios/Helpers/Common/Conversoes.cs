using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Conversoes
    {
        #region Coleções

        public static List<Tipo> ToList<Tipo>(IEnumerable enumerado)
        {
            List<Tipo> listaRetorno = new List<Tipo>();
            IEnumerator enumerator = enumerado.GetEnumerator();
            while (enumerator.MoveNext()) listaRetorno.Add((Tipo)enumerator.Current);
            return listaRetorno;
        }

        public static List<Tipo> ToList<Tipo>(IEnumerable<Tipo> enumerado)
        {
            List<Tipo> listaRetorno = new List<Tipo>();
            listaRetorno.AddRange(enumerado);
            return listaRetorno;
        }

        public static List<Tipo> ToList<Tipo>(Tipo[] array)
        {
            List<Tipo> listaRetorno = new List<Tipo>(array.Length);
            listaRetorno.AddRange(array);
            return listaRetorno;
        }

        #endregion

        #region Int16

        public static Int16 ToInt16(object valor)
        {
            try
            {
                return Convert.ToInt16(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return Int16.MinValue; }
        }

        #endregion

        #region Int32

        public static Int32 ToInt32(object valor)
        {
            try
            {
                return Convert.ToInt32(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return Int32.MinValue; }
        }

        #endregion

        #region Int64

        public static Int64 ToInt64(object valor)
        {
            try
            {
                return Convert.ToInt64(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return Int64.MinValue; }
        }

        #endregion

        #region Double

        public static Double ToDouble(object valor)
        {
            try
            {
                return Convert.ToDouble(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return double.MinValue; }
        }

        #endregion

        #region Decimal

        public static Decimal ToDecimal(object valor)
        {
            try
            {
                return Convert.ToDecimal(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return decimal.MinValue; }
        }

        #endregion

        #region Boolean

        private const string VALORES_BOOLEANOS_VERDADEIRO = "SIM,VERDADEIRO,TRUE";
        private const string VALORES_BOOLEANOS_FALSO = "NÃO,FALSO,FALSE";

        /// <summary>
        /// Converter objeto para booleano.
        /// </summary>
        /// <param name="obj">Valores aceitáveis: 'sim', 'verdadeiro', 'não', 'falso', 'true', 'false' ou qualquer valor numérico inteiro. *Não Considerar as aspas.</param>
        /// <returns>Booleano.</returns>
        public static bool ToBoolean(object obj)
        {
            try
            {
                if (Checar.IsNull(obj) || string.IsNullOrEmpty(obj.ToString()))
                    return false;

                if (VALORES_BOOLEANOS_VERDADEIRO.Contains(obj.ToString().ToUpper()))
                    return true;

                if (VALORES_BOOLEANOS_FALSO.Contains(obj.ToString().ToUpper()))
                    return false;

                int objValor = Conversoes.ToInt32(obj);
                return Convert.ToBoolean(objValor, Cultura.Instance.Provider);
            }
            catch (Exception) { return false; }
        }

        #endregion

        #region DateTime

        public static DateTime ToDateTime(object valor)
        {
            try
            {
                return Convert.ToDateTime(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return DateTime.MinValue; }
        }

        #endregion

        #region Char

        public static char ToChar(object valor)
        {
            try
            {
                return Convert.ToChar(valor.ToString(), Cultura.Instance.Provider);
            }
            catch { return char.MinValue; }
        }

        #endregion

        #region Base64String

        public static string ToBase64String(byte[] array)
        {
            try
            {
                return Convert.ToBase64String(array);
            }
            catch { throw new Exception(Erros.ConversaoInvalida("Conversoes", "Base64String")); }
        }

        public static byte[] FromBase64String(string valor)
        {
            try
            {
                return Convert.FromBase64String(valor);
            }
            catch { throw new Exception(Erros.ConversaoInvalida("Conversoes", "DeBase64String")); }
        }

        #endregion
    }
}