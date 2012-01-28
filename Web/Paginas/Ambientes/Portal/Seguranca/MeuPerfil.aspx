<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/_modelos/TemplatePagina.master"
    AutoEventWireup="true" CodeFile="MeuPerfil.aspx.cs" Inherits="Swarm.Web.Portal.Seguranca.MeuPerfil" %>

<asp:Content ID="head" ContentPlaceHolderID="pagina_head" runat="Server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="pagina_body" runat="Server">
    <div class="Formulario_Titulo">
        Geral
    </div>
    <div class="Formulario_Corpo">
        <table class="Dimensao_LarguraPadrao">
            <tfoot>
                <tr>
                    <td colspan="2" class="Tabela_Informacao_Obrigatorio">
                        Campo de preenchimento obrigat&oacute;rio.
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="Tabela_Informacao Horizontal_Centro">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" />
                    </td>
                </tr>
            </tfoot>
            <tbody>
                <tr>
                    <td class="Tabela_Descricao_Obrigatorio">
                        Nome completo:
                    </td>
                    <td class="Tabela_Conteudo">
                        <asp:TextBox ID="txtNomeCompleto" runat="server" Width="70%" MaxLength="100" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
