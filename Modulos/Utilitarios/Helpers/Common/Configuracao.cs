using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Configuracao
    {
        /// <summary>
        /// Retorna a chave de configuração solicitada.
        /// </summary>
        /// <param name="chave">Nome da chave envolvida.</param>
        public static string Obter(string chave)
        {
            return Configuracao.Obter(chave, Valor.Vazio);
        }

        /// <summary>
        /// Retorna a chave de configuração solicitada.
        /// </summary>
        /// <param name="chave">Nome da chave envolvida.</param>
        /// <param name="valorPadrao">Valor padrão caso não encontre a chave solicitada.</param>
        public static string Obter(string chave, string valorPadrao)
        {
            return Configuracao.Obter(chave, valorPadrao, Valor.Vazio);
        }

        /// <summary>
        /// Retorna a chave de configuração solicitada.
        /// </summary>
        /// <param name="chave">Nome da chave envolvida.</param>
        /// <param name="valorPadrao">Valor padrão caso não encontre a chave solicitada.</param>
        /// <param name="caracterFinal">Caso queira incluir um valor no final do valor obtido que não tenha sido informado, por exemplo, uma barra "\".</param>
        public static string Obter(string chave, string valorPadrao, string caracterFinal)
        {
            try
            {
                string retorno = ConfigurationManager.AppSettings[chave].ToString();
                if (Checar.IsCampoVazio(caracterFinal)) return retorno;

                bool isPossuiCaracterInformado = retorno.EndsWith(caracterFinal);
                return isPossuiCaracterInformado ? retorno : string.Concat(retorno, caracterFinal);
            }
            catch (ConfigurationException)
            {
                throw new ConfigurationErrorsException("A chave de configuração [" + chave + "] não foi encontrada.");
            }
            catch (NullReferenceException)
            {
                return valorPadrao;
            }
        }
    }
}
