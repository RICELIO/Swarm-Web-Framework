<%@ Register TagPrefix="controles" TagName="Autenticador" Src="~/Paginas/_controles/Autenticador.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expired.aspx.cs" Inherits="Swarm.Web.Expired"
    MasterPageFile="~/Paginas/_modelos/TemplateHome.master" %>

<asp:Content ID="head" ContentPlaceHolderID="home_head" runat="server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="home_body" runat="server">
    <div class="Horizontal_Centro" style="width: 30%;">
        <controles:Autenticador ID="ctAutenticador" runat="server" />
    </div>
</asp:Content>
