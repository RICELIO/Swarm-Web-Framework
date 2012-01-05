<%@ Register TagPrefix="controles" TagName="Autenticador" Src="~/Paginas/_controles/Autenticador.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Swarm.Web.Login"
    MasterPageFile="~/Paginas/_modelos/TemplateHome.master" %>

<asp:Content ID="head" ContentPlaceHolderID="home_head" runat="server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="home_body" runat="server">
    <div class="Bloco_LadoaLado" style="width: 67%; padding-right: 3%;">
        <div id="blocodeDemonstracao" runat="server" class="Horizontal_Justificado">
            <asp:Literal ID="ltrBlocodeDemonstracao" runat="server" />
        </div>
        <div class="Horizontal_Justificado">
            <asp:Literal ID="ltrBlocodeApresentacao" runat="server" />
        </div>
        <div class="Horizontal_Justificado">
            <asp:Literal ID="ltrBlocoUltimasAtualizacoes" runat="server" />
        </div>
    </div>
    <div class="Bloco_LadoaLado" style="width: 30%;">
        <controles:Autenticador ID="ctAutenticador" runat="server" />
    </div>
    <div class="Bloco_LadoaLado_Fechar">
        &nbsp;
    </div>
</asp:Content>
