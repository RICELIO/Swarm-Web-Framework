using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    internal abstract partial class Alertas
    {
        public const string Upload_Nome_Invalido = "O nome do arquivo informado é inválido.";
        public const string Upload_Extensao_Invalida = "A extensão do arquivo inválida.";
        public const string Upload_Arquivo_Invalido = "O nome do arquivo é inválido.";
        public const string Upload_Acesso_Negado = "A permissão para operar com o arquivo foi negada.";
    }
}
