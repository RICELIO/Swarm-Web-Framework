using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Core.Library.Seguranca.Criptografia;

namespace Swarm.Core
{
    public abstract partial class Configuracoes
    {
        public readonly static string Avatar_Padrao = Configuracao.Obter("Avatar_Padrao");
        public readonly static string Avatar_Path = Configuracao.Obter("Avatar_Path");
        public readonly static string Avatar_WebPath = Configuracao.Obter("Avatar_WebPath");
        public readonly static EnumCriptografia.Tipo Usuario_TipoCriptografia = (EnumCriptografia.Tipo)Conversoes.ToInt32(Configuracao.Obter("Usuario_TipoCriptografia"));
    }
}
