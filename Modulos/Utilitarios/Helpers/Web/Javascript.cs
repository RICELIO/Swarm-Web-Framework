using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Swarm.Utilitarios.Helpers.Web
{
    public abstract class Javascript
    {
        /// <summary>
        /// Inserir um script na página.
        /// </summary>
        /// <param name="pagina">Página envolvida.</param>
        /// <param name="identificador">Identificação do script.</param>
        /// <param name="script">Código-fonte.</param>
        /// <param name="insercaoPostergada">Inserir o script após o processamento da página - pelo navegador. Indicado quando se deseja trabalhar com os componentes ASP.NET.</param>
        public static void Add(Page pagina, string strID, StringBuilder script, bool insercaoPostergada)
        {
            Javascript.Add(pagina, strID, script.ToString(), insercaoPostergada);
        }

        /// <summary>
        /// Inserir um script na página.
        /// </summary>
        /// <param name="pagina">Página envolvida.</param>
        /// <param name="identificador">Identificação do script.</param>
        /// <param name="script">Código-fonte.</param>
        /// <param name="insercaoPostergada">Inserir o script após o processamento da página - pelo navegador. Indicado quando se deseja trabalhar com os componentes ASP.NET.</param>
        public static void Add(Page pagina, string strID, string script, bool insercaoPostergada)
        {
            if (insercaoPostergada)
                pagina.ClientScript.RegisterStartupScript(pagina.GetType(), strID, script, true);
            else
                pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType(), strID, script, true);
        }

        /// <summary>
        /// Fechar uma página.
        /// </summary>
        /// <param name="pagina">Página envolvida.</param>
        /// <param name="sender">Página requisitante ou Página envolvida</param>
        public static void CloseWindow(Page pagina, Janela.Sender sender)
        {
            string strID = string.Format("fecharJanela_{0}", pagina.ClientID);
            string script = string.Format("{0}.close();", sender.ToString().ToLower());
            Javascript.Add(pagina, strID, script, Valor.Ativo);
        }

        /// <summary>
        /// Recarregar uma página.
        /// </summary>
        /// <param name="pagina">Página envolvida.</param>
        /// <param name="sender">Página requisitante ou Página envolvida</param>
        /// <param name="fecharPaginaAtual">Caso queira que a página envolvida seja fechada após a recarga solicitada.</param>
        public static void ReloadPage(Page pagina, Janela.Sender sender, bool isFecharPaginaEnvolvida)
        {
            string invocadorEnvolvido = sender.ToString().ToLower();

            StringBuilder script = new StringBuilder();
            script.AppendFormat("{0}.location.href = {0}.location.href; ", invocadorEnvolvido);
            if (isFecharPaginaEnvolvida) script.Append("window.close();");

            string strID = string.Format("recarregarPagina_{0}", pagina.ClientID);
            Javascript.Add(pagina, strID, script, Valor.Ativo);
        }

        /// <summary>
        /// Exibir uma mensagem de alerta.
        /// </summary>
        /// <param name="pagina">Página envolvida.</param>
        /// <param name="mensagem">Mensagem que será exibida no alerta.</param>
        public static void Alert(Page pagina, string mensagem)
        {
            Javascript.Alert(pagina, mensagem, Valor.Vazio);
        }

        /// <summary>
        /// Exibir uma mensagem de alerta.
        /// </summary>
        /// <param name="pagina">Página envolvida.</param>
        /// <param name="mensagem">Mensagem que será exibida no alerta.</param>
        /// <param name="uri">Redirecionamento: URI de destino caso queira utilizar a função pós ALERTA.</param>
        public static void Alert(Page pagina, string mensagem, string uri)
        {
            mensagem = mensagem.Replace(Environment.NewLine, @"\n");
            mensagem = mensagem.Replace(@"\", @"\\");
            mensagem = mensagem.Replace(@"'", @"\'");
            mensagem = mensagem.Replace(@"""", @"\""");
            mensagem = mensagem.Replace(@"\\n", @"\n");
            mensagem = mensagem.Replace(@"\\r", "");

            StringBuilder script = new StringBuilder();
            script.AppendFormat(@"
            alert('{0}');
            {1}
            ", mensagem, !Checar.IsCampoVazio(uri) ? string.Format("self.location.href = '{0}';", uri) : Valor.Vazio);

            string strID = string.Format("alert_{0}", pagina.ClientID);
            Javascript.Add(pagina, strID, script, Valor.Ativo);
        }

        /// <summary>
        /// Abrir uma janela.
        /// </summary>
        /// <param name="pagina">A página envolvida.</param>
        /// <param name="size">Tipo de janela que será aberta.</param>
        /// <param name="uri">URI da página que será carregada na janela envolvida.</param>
        public static string OpenWindow(Page pagina, Janela.Size size, string uri)
        {
            return Javascript.OpenWindow(pagina, size, uri, Valor.Zero, Valor.Zero);
        }

        /// <summary>
        /// Abrir uma janela.
        /// </summary>
        /// <param name="pagina">A página envolvida.</param>
        /// <param name="size">Tipo de janela que será aberta.</param>
        /// <param name="uri">URI da página que será carregada na janela envolvida.</param>
        /// <param name="altura">Altura da janela envolvida.</param>
        /// <param name="largura">Largura da janela envolvida.</param>
        public static string OpenWindow(Page pagina, Janela.Size size, string uri, int altura, int largura)
        {
            return Javascript.OpenWindow(pagina, size, uri, Valor.Zero, Valor.Zero, Valor.Inativo);
        }

        /// <summary>
        /// Abrir uma janela.
        /// </summary>
        /// <param name="pagina">A página envolvida.</param>
        /// <param name="size">Tipo de janela que será aberta.</param>
        /// <param name="uri">URI da página que será carregada na janela envolvida.</param>
        /// <param name="altura">Altura da janela envolvida.</param>
        /// <param name="largura">Largura da janela envolvida.</param>
        /// <param name="apenasLeitura">Quando TRUE indica que o script não será executado, apenas retornado em string.</param>
        public static string OpenWindow(Page pagina, Janela.Size size, string uri, int altura, int largura, bool apenasLeitura)
        {
            string strClientID = Checar.IsNull(pagina) ? Guid.NewGuid().ToString() : pagina.ClientID;
            
            string strWindow = string.Format("window_{0}", Guid.NewGuid().ToString().Replace(Valor.Traço, Valor.Vazio));
            altura = Checar.MenorouIgual(altura) ? Janela.Altura : altura;
            largura = Checar.MenorouIgual(largura) ? Janela.Largura : largura;

            StringBuilder script = new StringBuilder();

            switch (size)
            {
                default:
                case Janela.Size.Normal:
                    {
                        script.AppendFormat("AbrirJanela('{0}','{1}',{2},{3});", uri, strWindow, altura, largura);
                        break;
                    }
                case Janela.Size.Popup:
                    {
                        script.AppendFormat("AbrirJanelaPopup('{0}','{1}',{2},{3});", uri, strWindow, altura, largura);
                        break;
                    }
                case Janela.Size.TelaCheia:
                    {
                        script.AppendFormat("AbrirJanelaTelaCheia('{0}','{1}');", uri, strWindow);
                        break;
                    }
            }

            string strID = string.Format("newWindow_{0}", strClientID);
            if (!apenasLeitura) Javascript.Add(pagina, strID, script, Valor.Ativo);
            
            return script.ToString();
        }
    }
}
