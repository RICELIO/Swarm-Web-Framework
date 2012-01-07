<%@ Register TagPrefix="controles" TagName="Autenticador" Src="~/Paginas/_controles/Autenticador.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expired.aspx.cs" Inherits="Swarm.Web.Expired"
    MasterPageFile="~/Paginas/_modelos/TemplateHome.master" %>

<asp:Content ID="head" ContentPlaceHolderID="home_head" runat="server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="home_body" runat="server">
    <div class="Bloco_LadoaLado" style="width: 67%; padding-right: 3%;">
        <div class="Horizontal_Justificado">
            <h2>
                Sess&atilde;o expirada ou tentativa de acesso sem autentica&ccedil;&atilde;o</h2>
            <div class="Dimensao_LarguraPadrao">
                Se voc&ecirc; foi redirecionado para esta p&aacute;gina significa que est&aacute;
                tentando acessar uma &aacute;rea segura do sistema sem estar devidamente autenticado.
                Caso j&aacute; tenha efetuado sua autentica&ccedil;&atilde;o anteriormente, significa
                que a mesma expirou, provavelmente devido a um per&iacute;odo de inatividade ou
                n&atilde;o raro, mas poss&iacute;vel, devido a um processo de atualiza&ccedil;&atilde;o
                do sistema.<br />
                <br />
                Para continuar trabalhando no mesmo basta que confirme seu usu&aacute;rio e senha
                ao lado.
            </div>
        </div>
    </div>
    <div class="Bloco_LadoaLado" style="width: 30%;">
        <controles:Autenticador ID="ctAutenticador" runat="server" />
    </div>
    <div class="Bloco_LadoaLado_Fechar">
        &nbsp;
    </div>
</asp:Content>
