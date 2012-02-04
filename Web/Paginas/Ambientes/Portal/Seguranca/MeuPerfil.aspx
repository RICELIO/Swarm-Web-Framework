<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/_modelos/TemplatePagina.master"
    AutoEventWireup="true" CodeFile="MeuPerfil.aspx.cs" Inherits="Swarm.Web.Portal.Seguranca.MeuPerfil" %>

<asp:Content ID="head" ContentPlaceHolderID="pagina_head" runat="Server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="pagina_body" runat="Server">
    <div>
        <div class="Formulario_Titulo">
            Autentica&ccedil;&atilde;o
        </div>
        <div class="Formulario_Corpo">
            <table class="Dimensao_LarguraPadrao">
                <tfoot>
                    <tr>
                        <td colspan="2" class="Tabela_Informacao_Alerta">
                            N&atilde;o &eacute; permitido o uso de espa&ccedil;os no &iacute;nicio ou no final
                            da sua senha.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Tabela_Informacao_Obrigatorio">
                            Campo de preenchimento obrigat&oacute;rio.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Tabela_Informacao Horizontal_Centro">
                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                        </td>
                    </tr>
                </tfoot>
                <tbody>
                    <tr>
                        <td class="Tabela_Descricao">
                            Login:
                        </td>
                        <td class="Tabela_Conteudo">
                            <asp:Label ID="lblLogin" runat="server" Text="..." />
                        </td>
                    </tr>
                    <tr>
                        <td class="Tabela_Descricao">
                            N&iacute;vel de acesso:
                        </td>
                        <td class="Tabela_Conteudo">
                            <asp:Label ID="lblNivelAcesso" runat="server" Text="..." />
                        </td>
                    </tr>
                    <tr>
                        <td class="Tabela_Descricao_Obrigatorio">
                            Senha atual:
                        </td>
                        <td class="Tabela_Conteudo">
                            <asp:TextBox ID="txtSenhaAtual" runat="server" Width="130px" MaxLength="12" TextMode="Password" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Tabela_Descricao_Obrigatorio">
                            Nova senha:
                        </td>
                        <td class="Tabela_Conteudo">
                            <asp:TextBox ID="txtNovaSenha" runat="server" Width="130px" MaxLength="12" TextMode="Password" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Tabela_Descricao_Obrigatorio">
                            Confirmar nova senha:
                        </td>
                        <td class="Tabela_Conteudo">
                            <asp:TextBox ID="txtConfirmarNovaSenha" runat="server" Width="130px" MaxLength="12"
                                TextMode="Password" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="Espacamento_Anterior_20px">
        <div class="Formulario_Titulo">
            Imagem de Exibi&ccedil;&atilde;o
        </div>
        <div class="Formulario_Corpo">
            <table class="Dimensao_LarguraPadrao">
                <thead>
                    <tr>
                        <td colspan="2" class="Tabela_Informacao Horizontal_Justificado">
                            Abaixo voc&ecirc; poder&aacute; alterar a sua imagem de exibi&ccedil;&atilde;o no
                            Sistema. Para que a opera&ccedil;&atilde;o seja conclu&iacute;da com sucesso a sua
                            imagem deve utilizar uma extens&atilde;o .jpg, .png ou .gif e possuir um tamanho
                            m&aacute;ximo de 300KB.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Tabela_Informacao Horizontal_Centro">
                            <asp:Image ID="imgAvatar" runat="server" ImageAlign="Middle" ImageUrl="~/_content/Upload/Avatar/visitante.png" />
                        </td>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <td class="Tabela_Informacao Horizontal_Centro">
                            <asp:Button ID="btnEnviarAvatar" runat="server" Text="Enviar" OnClick="btnEnviarAvatar_Click" />
                        </td>
                    </tr>
                </tfoot>
                <tbody>
                    <tr>
                        <td colspan="2" class="Tabela_Informacao Horizontal_Centro">
                            <asp:FileUpload ID="upAvatar" runat="server" Width="50%" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
