<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManutencaoControleAcesso_Selecao.aspx.cs"
    Inherits="Swarm.Web.Portal.ManutencaoControleAcesso_Selecao" MasterPageFile="~/Paginas/_modelos/TemplateJanela.master" %>

<asp:Content ID="head" ContentPlaceHolderID="janela_head" runat="server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="janela_body" runat="server">
    <div class="Formulario_Topo">
        <asp:ImageButton ID="btnNovo" runat="server" ImageAlign="Middle" ImageUrl="~/_content/Images/Botoes/p_novo.png" />
    </div>
    <div class="Formulario_Corpo">
        <div class="Formulario_Bloco_SubTitulo">
            Cadastro Geral
        </div>
        <div class="Formulario_Bloco_Conteudo">
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
    </div>
</asp:Content>
