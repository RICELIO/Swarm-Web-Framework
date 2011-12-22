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
        <div style="border: dotted 2px #333333; min-height: 200px; padding: 15px;">
            <div id="blocoAutenticacao" runat="server">
                <div>
                    <div>
                        <img class="Vertical_FundoConteudo" src="./_content/Images/Icones/p_usuario.png"
                            alt="" />Usu&aacute;rio:
                    </div>
                    <div style="margin-top: 5px;">
                        <asp:TextBox ID="txtLogin" runat="server" Width="100%" />
                    </div>
                </div>
                <div style="margin-top: 15px;">
                    <div>
                        <img class="Vertical_FundoConteudo" src="./_content/Images/Icones/p_senha.png" alt="" />Senha:
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
                <asp:Literal id="ltrSelecaodeAmbiente" runat="server" />
            </div>
        </div>
    </div>
    <div class="Bloco_LadoaLado_Fechar">
        &nbsp;
    </div>
</asp:Content>
