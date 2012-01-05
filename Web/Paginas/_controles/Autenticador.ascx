<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Autenticador.ascx.cs"
    Inherits="Swarm.Web.Controles.Autenticador" %>
<div class="Horizontal_Esquerda" style="border: dotted 2px #333333; min-height: 200px;
    padding: 15px;">
    <div id="blocoAutenticacao" runat="server">
        <div>
            <div>
                <img class="Vertical_FundoConteudo" src="./_content/Images/Icones/p_usuario.png"
                    alt="" />&nbsp;Usu&aacute;rio:
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="txtLogin" runat="server" Width="100%" />
            </div>
        </div>
        <div style="margin-top: 15px;">
            <div>
                <img class="Vertical_FundoConteudo" src="./_content/Images/Icones/p_senha.png" alt="" />&nbsp;Senha:
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="100%" />
            </div>
        </div>
        <div class="Horizontal_Centro" style="margin-top: 25px;">
            <asp:Button ID="btnAutenticar" runat="server" Text="Entrar" OnClick="btnAutenticar_Click" />
        </div>
    </div>
    <div id="blocoSelecaoAmbiente" runat="server">
        <asp:Literal ID="ltrSelecaodeAmbiente" runat="server" />
    </div>
</div>
