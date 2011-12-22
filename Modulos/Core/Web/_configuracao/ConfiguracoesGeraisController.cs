using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Persistencia;

namespace Swarm.Core.Web.Configuracao
{
    public abstract class ConfiguracoesGeraisController
    {
        public static ConfiguracoesGerais Manter(ConfiguracoesGerais obj, string usuarioLOG, ColecaoPersistencia colecao)
        {
            ConfiguracoesGerais objAtualizado = ModeloControladorBase.Manter(obj, usuarioLOG, colecao) as ConfiguracoesGerais;
            ConfiguracoesGeraisSingleton.Destroy(); // LIMPANDO AS CONFIGURAÇÕES ANTIGAS
            return objAtualizado;
        }

        public static bool Excluir(ConfiguracoesGerais obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Excluir(obj, colecao);
        }

        public static ConfiguracoesGerais Create()
        {
            return new ConfiguracoesGerais();
        }

        public static ConfiguracoesGerais Get()
        {
            return ConfiguracoesGeraisSingleton.Instance.Configuracao;
        }
    }
}
