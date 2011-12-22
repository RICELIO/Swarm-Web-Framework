using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Persistencia
{
    public abstract class Query
    {
        #region Validações

        public static string IsSGBD(string condicao, params EnumPersistencia.SGBD[] tiposBD)
        {
            bool isEnvolvido = Valor.Inativo;
            Conversoes.ToList<EnumPersistencia.SGBD>(tiposBD).ForEach(tipoBD =>
                {
                    if (Conexão.Instance.SGBD == tipoBD)
                        isEnvolvido = Valor.Ativo;
                    return;
                });
            return isEnvolvido ? condicao : Valor.Vazio;
        }

        public static string MaiorQue(long valor, string condicao)
        {
            return Checar.MaiorQue(valor) ? condicao : Valor.Vazio;
        }
        public static string MaiorQue(long valor, string condicao, params object[] parametros)
        {
            return Checar.MaiorQue(valor) ? string.Format(condicao, parametros) : Valor.Vazio;
        }

        public static string IsPreenchido(string valor, string condicao)
        {
            return !Checar.IsCampoVazio(valor) ? condicao : Valor.Vazio;
        }
        public static string IsPreenchido(string valor, string condicao, params object[] parametros)
        {
            return !Checar.IsCampoVazio(valor) ? string.Format(condicao, parametros) : Valor.Vazio;
        }

        public static string IsValido(bool valor, string condicao)
        {
            return valor ? condicao : Valor.Vazio;
        }
        public static string IsValido(bool valor, string condicao, params object[] parametros)
        {
            return valor ? string.Format(condicao, parametros) : Valor.Vazio;
        }

        public static string IsValido(DateTime dataHora, string condicao)
        {
            return Checar.IsDataValida(dataHora) ? condicao : Valor.Vazio;
        }
        public static string IsValido(DateTime dataHora, string condicao, params object[] parametros)
        {
            return Checar.IsDataValida(dataHora) ? string.Format(condicao, parametros) : Valor.Vazio;
        }

        public static string IsIntervalosValido(DateTime dataHoraINICIO, DateTime dataHoraFIM, string condicao)
        {
            return !Checar.IsIntervaloInconsistente(dataHoraINICIO, dataHoraFIM) ? condicao : Valor.Vazio;
        }
        public static string IsIntervalosValido(DateTime dataHoraINICIO, DateTime dataHoraFIM, string condicao, params object[] parametros)
        {
            return !Checar.IsIntervaloInconsistente(dataHoraINICIO, dataHoraFIM) ? string.Format(condicao, parametros) : Valor.Vazio;
        }

        #endregion

        #region Valores

        public static string ValorString(string valor)
        {
            return Checar.IsNull(valor) || Checar.IsCampoVazio(valor) ? "NULL" : valor;
        }

        public static string ValorNumerico(long valor)
        {
            return Checar.MaiorQue(valor) ? valor.ToString() : "NULL";
        }

        public static string ValorBooleano(bool valor)
        {
            return valor ? Valor.Um.ToString() : Valor.Zero.ToString();
        }

        #endregion
    }
}
