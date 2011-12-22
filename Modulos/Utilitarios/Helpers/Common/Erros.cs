using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Erros
    {
        public static string CampoVazio(string campo)
        {
            return string.Format("Por favor, o campo '{0}' deve possuir valor preenchido.", campo);
        }

        public static string ValorInvalido(string objetoNome, string atributoNome)
        {
            return String.Format("[{0}] - [{1}] valor inválido.", objetoNome, atributoNome);
        }

        public static string ObjetoInvalido(string objetoNome)
        {
            return String.Format("Objeto [{0}] inválido ou não encontrado.");
        }

        public static string ConversaoInvalida(string objetoNome, string metodoNome)
        {
            return String.Format("[{0}] - O método [{1}] retornou um valor inválido.", objetoNome, metodoNome);
        }

        public static string NaoImplementado(string objetoNome, string metodoNome)
        {
            return String.Format("[{0}] - O método [{1}] não foi/está implementado no objeto solicitado.", objetoNome, metodoNome);
        }

        public static string NaoEncontrado(string objetoNome)
        {
            return String.Format("Não foi possível localizar os registros para [{0}].", objetoNome);
        }

        #region Erros envolvendo mais de um campo

        public static string CamposComMesmoValor(params string[] campos)
        {

            string strCampos = GetListagemCampos(campos);
            return string.Format("Por favor, os campos {0} devem possuir o mesmo valor.", strCampos);
        }

        public static string PeloMenosUm(params string[] campos)
        {
            string strCampos = GetListagemCampos(campos);
            return string.Format("Por favor, pelo menos um dos campos ({0}) devem estar preenchidos para prosseguir com a ação desejada.", strCampos);
        }

        #endregion

        #region Métodos para manipulação da classe

        private static string GetListagemCampos(string[] campos)
        {
            int count = 0;
            string strCampos = string.Empty;

            List<string> listaNomedosCampos = Conversoes.ToList<string>(campos);
            listaNomedosCampos.ForEach(delegate(string s)
            {
                count++;

                string separador = string.Empty;
                if (listaNomedosCampos.Count.Equals(1) || count.Equals(listaNomedosCampos.Count))
                    separador = string.Empty;
                else
                    separador = count.Equals(listaNomedosCampos.Count - 1) ? " e " : ", ";

                strCampos += string.Format("'{0}'{1}", s, separador);
            });

            return strCampos;
        }

        #endregion
    }
}
