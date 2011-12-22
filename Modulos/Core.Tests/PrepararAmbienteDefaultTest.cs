using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Web.ControledeAcesso;

namespace Swarm.Tests.Core
{
    [TestFixture]
    public class PrepararAmbienteDefaultTest
    {
        [Test]
        public void PrepararAmbientes()
        {
            ColecaoPersistencia colecao = new ColecaoPersistencia();

            Ambiente portal = new Ambiente();
            portal.Titulo = "Portal Administrativo";
            portal.Habilitado = Valor.Ativo;
            portal.Restrito = Valor.Ativo;
            AcessoController.Manter(portal, "administrador", colecao);

            Ambiente anonimo = new Ambiente();
            anonimo.Titulo = "Anônimo";
            anonimo.Habilitado = Valor.Ativo;
            anonimo.Restrito = Valor.Inativo;
            anonimo.CodigoInterno = EnumAcesso.CodigoInterno_Ambiente.Anonimo;
            AcessoController.Manter(anonimo, "administrador", colecao);

            Ambiente callback = new Ambiente();
            callback.Titulo = "Callback";
            callback.Habilitado = Valor.Ativo;
            callback.Restrito = Valor.Inativo;
            AcessoController.Manter(callback, "administrador", colecao);

            SuperGrupo p_sg1 = new SuperGrupo();
            p_sg1.Titulo = "Super-Grupo de páginas que não serão exibidas para o Usuário";
            p_sg1.Habilitado = Valor.Ativo;
            p_sg1.Exibir = Valor.Inativo;
            p_sg1.Ambiente = portal;
            p_sg1.CodigoInterno = EnumAcesso.CodigoInterno_Grupo.Individual;
            AcessoController.Manter(p_sg1, "administrador", colecao);

            Grupo p_g1 = new Grupo();
            p_g1.Titulo = "Grupo de páginas que não serão exibidas para o Usuário";
            p_g1.Habilitado = Valor.Ativo;
            p_g1.Exibir = Valor.Inativo;
            p_g1.SuperGrupo = p_sg1;
            p_g1.CodigoInterno = EnumAcesso.CodigoInterno_Grupo.Individual;
            AcessoController.Manter(p_g1, "administrador", colecao);

            Funcionalidade p_func1 = new Funcionalidade();
            p_func1.Titulo = "Seleção de Funcionalidade";
            p_func1.Descrição = "Esta página é responsável pela gestão dos itens a serem exibidos para o usuário selecionar.";
            p_func1.Habilitado = Valor.Ativo;
            p_func1.Exibir = Valor.Inativo;
            p_func1.Grupo = p_g1;
            AcessoController.Manter(p_func1, "administrador", colecao);

            colecao.Persistir();

            AcessoMap ligPortal = new AcessoMap();
            ligPortal.Tipo = EnumAcesso.TipodeAcesso.Ambiente;
            ligPortal.IdAcesso = portal.ID;
            ligPortal.UrlMapID = Valor.Seis; // HOME
            ligPortal.Principal = Valor.Ativo;
            AcessoController.Manter(ligPortal, "administrador", null);

            AcessoMap ligFunc1 = new AcessoMap();
            ligFunc1.Tipo = EnumAcesso.TipodeAcesso.Funcionalidade;
            ligFunc1.IdAcesso = p_func1.ID;
            ligFunc1.UrlMapID = Valor.Sete; // VISUALIZARCONTROLESDEACESSO
            ligFunc1.Principal = Valor.Ativo;
            AcessoController.Manter(ligFunc1, "administrador", null);
        }
    }
}
