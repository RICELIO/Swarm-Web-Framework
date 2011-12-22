using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Tratar
    {
        /// <summary>
        /// Previnindo caracteres que podem causar SQL Injection.
        /// </summary>
        public static string SqlInjection(object valor)
        {
            return Tratar.SqlInjection(valor, Valor.Inativo);
        }

        /// <summary>
        /// Previnindo caracteres que podem causar SQL Injection.
        /// </summary>
        public static string SqlInjection(object valor, bool ehValidacaocomASPAS)
        {
            string parametro = valor.ToString();
            Tratar.SqlInjection(ref parametro, ehValidacaocomASPAS);
            return parametro;
        }

        /// <summary>
        /// Previnindo caracteres que podem causar SQL Injection.
        /// </summary>
        public static void SqlInjection(ref string parametro)
        {
            Tratar.SqlInjection(ref parametro, Valor.Inativo);
        }

        /// <summary>
        /// Previnindo caracteres que podem causar SQL Injection.
        /// </summary>
        public static void SqlInjection(ref string parametro, bool ehValidacaocomASPAS)
        {
            parametro = parametro.Replace("'", "''");
            if (ehValidacaocomASPAS) return;

            parametro = parametro.Replace(";", Valor.Vazio);
            parametro = parametro.Replace("#", Valor.Vazio);
            parametro = parametro.Replace("-- ", Valor.Vazio);
            parametro = parametro.Replace("--", Valor.Vazio);
            parametro = parametro.Replace("/*", Valor.Vazio);
            parametro = parametro.Replace("*/", Valor.Vazio);
        }
    }
}
