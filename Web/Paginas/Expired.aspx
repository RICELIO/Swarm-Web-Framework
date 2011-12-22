<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expired.aspx.cs" Inherits="Swarm.Web.Expired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Swarm Web Framework ::</title>
</head>
<body>
    <form id="frmExpired" runat="server" style="width: 100%;">
    <div style="margin-top: 50px; width: 300px; padding: 5px; text-align: center; margin-left: auto;
        margin-right: auto; border: solid 1px #CCCCCC;">
        <div style="margin-bottom: 15px;">
            Sua sess&atilde;o expirou. Efetue novamente o processo de autentica&ccedil;&atilde;o
            no sistema.
        </div>
        <div style="text-align: left;">
            <div style="font-weight: bold;">
                Usu&aacute;rio
            </div>
            <div>
                <asp:TextBox ID="txtLogin" runat="server" Width="98%" />
            </div>
        </div>
        <div style="text-align: left; margin-top: 10px;">
            <div style="font-weight: bold;">
                Senha
            </div>
            <div>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="98%" />
            </div>
        </div>
        <div style="margin-top: 10px;">
            <asp:Button ID="btnAutenticar" runat="server" Text="Entrar" OnClick="btnAutenticar_Click" />
        </div>
    </div>
    </form>
</body>
</html>
