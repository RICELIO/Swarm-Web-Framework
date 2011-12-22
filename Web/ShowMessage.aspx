<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMessage.aspx.cs" Inherits="Swarm.Web.ShowMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Swarm Web Framework ::</title>
</head>
<body>
    <form id="frmShowMessage" runat="server">
    <div>
        Mensagem:&nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Text="..." />
    </div>
    </form>
</body>
</html>
