using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Core
{
    public abstract partial class Alertas
    {
        public const string DadosIncompletosAlteracaodeSenha = "Não foi possível Alterar a senha, dados incompletos.";
        public const string SenhaInvalida = "A senha informada não confere com a senha atual.";
        public const string MesmaSenha = "A senha informa é a mesma que já está associada.";
        public const string UsuarioInexistente = "O usuário informado não existe.";
        public const string UsuarioBloqueado = "O usuário informado encontra-se bloqueado.";
        public const string UsuarioDesabilitado = "O usuário informado encontra-se desabilitado.";
    }
}
