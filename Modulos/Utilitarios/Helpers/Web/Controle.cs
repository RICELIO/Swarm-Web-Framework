using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Swarm.Utilitarios.Helpers.Web
{
    public abstract class Controle
    {
        #region Enumeradores

        public enum Estado
        {
            Ambos = Valor.EnumDefault,
            Habilitado = 1,
            Selecionado = 2
        }

        public enum Selecionado
        {
            Ambos = Valor.EnumDefault,
            Sim = 1,
            Não = 2
        }

        public enum TipoBusca
        {
            Ambos = Utilitarios.Valor.EnumDefault,
            Texto = 1,
            Valor = 2
        }

        public enum TipoItem
        {
            Todos = Valor.EnumDefault,
            Selecionado = 1,
            Especifico = 2,
        }

        #endregion

        #region Métodos - Envolvendo controles do agrupados

        /// <summary>
        /// Limpará os controles informados de modo elegante - implícito.
        /// </summary>
        public static void ReiniciarControles(params ListControl[] controles)
        {
            foreach (ListControl controle in controles)
                controle.Items.Clear();
        }

        /// <summary>
        /// Recuperar todos os valores contidos no componente envolvido - separados por vírgula.
        /// </summary>
        public static string GetAll(ListControl controle, bool isApenasSelecionados)
        {
            return Controle.GetAll(controle, Valor.Virgula, isApenasSelecionados);
        }

        /// <summary>
        /// Recuperar todos os valores contidos no componente envolvido - separados pelo separador informado.
        /// </summary>
        public static string GetAll(ListControl controle, string separador, bool isApenasSelecionados)
        {
            string retorno = string.Empty;

            foreach (ListItem item in controle.Items)
            {
                if (isApenasSelecionados && item.Selected)
                    retorno = string.Concat(retorno, string.Format("{0}{1}", item.Value, separador));
                else if (!isApenasSelecionados && !(item.Value.Equals(Valor.Default) || item.Value.Equals(Valor.Default)))
                    retorno = string.Concat(retorno, string.Format("{0}{1}", item.Value, separador));
            }

            if (Checar.IsCampoVazio(separador))
                return Checar.MenorouIgual(retorno.Length) ? Valor.Vazio : retorno.Substring(0, retorno.Length);
            else
                return Checar.MenorouIgual(retorno.Length) ? Valor.Vazio : retorno.Substring(0, retorno.Length - 1);
        }

        /// <summary>
        /// Adicionar um registro no controle envolvido.
        /// </summary>
        public static void Add(ListControl controle, string texto, object valor)
        {
            Controle.Add(controle, texto, valor, Valor.Ativo);
        }

        /// <summary>
        /// Adicionar um registro no controle envolvido.
        /// </summary>
        public static void Add(ListControl controle, string texto, object valor, bool isAtivo)
        {
            string strValor = Checar.IsNull(valor) ? Valor.Default : valor.ToString();
            ListItem item = new ListItem(texto, strValor, isAtivo);
            Controle.Add(controle, item);
        }

        /// <summary>
        /// Adicionar um registro no controle envolvido.
        /// </summary>
        /// <param name="controle">Controle envolvido.</param>
        /// <param name="item">Item envolvido.</param>
        public static void Add(ListControl controle, ListItem item)
        {
            if (!Checar.IsNull(controle.Items.FindByText(item.Text)) || !Checar.IsNull(controle.Items.FindByValue(item.Value)))
                return;

            controle.Items.Add(item);
        }

        /// <summary>
        /// Removerá um determinado registro ou um conjunto de registros do controle envolvido.
        /// </summary>
        /// <param name="controle">Controle envolvido.</param>
        /// <param name="tipoItem">Tipo de remoção a ser efetuada.</param>
        /// <param name="posicao">Informar quando na remoção de um item específico. [Opcional]</param>
        public static void Remove(ListControl controle, TipoItem tipo, int posicao)
        {
            switch (tipo)
            {
                case TipoItem.Selecionado:
                    controle.Items.Remove(controle.SelectedItem);
                    break;
                case TipoItem.Especifico:
                    controle.Items.RemoveAt(posicao);
                    break;
                default:
                case TipoItem.Todos:
                    {
                        for (int i = (controle.Items.Count - Valor.Um); i >= Valor.Zero; i--)
                            controle.Items.RemoveAt(i);

                        break;
                    }
            }
        }

        /// <summary>
        /// Selecionará um registro no controle com base no valor informado.
        /// </summary>
        /// <param name="controle">Controle envolvido.</param>
        /// <param name="conteudo">Possível conteúdo da propriedade Text ou Value que você deseja selecionar.</param>
        /// <param name="busca">Tipo de busva a ser efetuada.</param>
        public static void Select(ListControl controle, object conteudo, TipoBusca tipoBusca)
        {
            ListItem item = new ListItem();
            switch (tipoBusca)
            {
                case TipoBusca.Texto:
                    {
                        item = controle.Items.FindByText(conteudo.ToString());
                        break;
                    }
                case TipoBusca.Valor:
                    {
                        if (conteudo is System.Boolean) conteudo = Conversoes.ToBoolean(conteudo) ? Valor.Sim : Valor.Não;
                        item = controle.Items.FindByValue(conteudo.ToString());
                        break;
                    }
                default:
                case TipoBusca.Ambos:
                    {
                        item = controle.Items.FindByText(conteudo.ToString());
                        if (!Checar.IsNull(item)) break;

                        item = controle.Items.FindByValue(conteudo.ToString());
                        break;
                    }
            }

            if (Checar.IsNull(item) || (string.IsNullOrEmpty(item.Text) && string.IsNullOrEmpty(item.Value)))
                controle.SelectedIndex = Valor.MenosUm;
            else
            {
                int indexItem = controle.Items.IndexOf(item);
                controle.SelectedIndex = indexItem;
            }
        }

        /// <summary>
        /// Alterará o estado dos itens envolvidos com o controle de acordo com a característica informada.
        /// </summary>
        /// <param name="valor">Valor desejado.</param>
        /// <param name="controle">Controle envolvido.</param>
        /// <param name="estado">Tipo de estado envolvido.</param>
        public static void SetState(bool valor, ListControl controle, Estado estado)
        {
            foreach (ListItem item in controle.Items)
            {
                switch (estado)
                {
                    case Estado.Ambos:
                        {
                            item.Enabled = valor;
                            item.Selected = valor;
                            break;
                        }
                    case Estado.Habilitado:
                        item.Enabled = valor;
                        break;
                    case Estado.Selecionado:
                        item.Selected = valor;
                        break;
                }
            }
        }

        /// <summary>
        /// Recuperará todos os itens de um controle de acordo com o estado de seleção informado.
        /// </summary>
        public static List<ListItem> RecuperarItens(ListControl controle, Selecionado estadoSelecao)
        {
            List<ListItem> itensEnvolvidos = new List<ListItem>();
            
            foreach (ListItem item in controle.Items)
            {
                switch (estadoSelecao)
                {
                    case Selecionado.Ambos:
                        itensEnvolvidos.Add(item);
                        break;
                    case Selecionado.Sim:
                        {
                            if (item.Selected)
                                itensEnvolvidos.Add(item);
                            break;
                        }
                    case Selecionado.Não:
                        {
                            if (!item.Selected)
                                itensEnvolvidos.Add(item);
                            break;
                        }
                }
               
            }

            return itensEnvolvidos;
        }

        #endregion

        #region Métodos - Envolvendo controles individuais

        /// <summary>
        /// Limpará os controles informados de modo elegante - implícito.
        /// </summary>
        public static void ReiniciarControles(params ITextControl[] controles)
        {
            foreach (ITextControl controle in controles)
                controle.Text = Valor.Vazio;
        }

        public static void Set(ITextControl controle, string valor)
        {
            Controle.Set(controle, valor, Valor.Traço);
        }
        public static void Set(ITextControl controle, string valor, string valorDefault)
        {
            controle.Text = Checar.IsCampoVazio(valor) ? valorDefault : valor;
        }
        public static void Set(IButtonControl controle, string valor)
        {
            controle.Text = valor;
        }

        public static void SetBlank(params ITextControl[] listaControles)
        {
            List<ITextControl> controles = Conversoes.ToList<ITextControl>(listaControles);
            controles.ForEach(delegate(ITextControl c) { Controle.Set(c, Valor.Vazio, Valor.Vazio); });
        }

        public static void SetEnabled(bool valor, params WebControl[] listaControles)
        {
            List<WebControl> controles = Conversoes.ToList<WebControl>(listaControles);
            controles.ForEach(delegate(WebControl wc) { wc.Enabled = valor; });
        }

        public static void SetVisible(bool valor, params Control[] listaControles)
        {
            List<Control> controles = Conversoes.ToList<Control>(listaControles);
            controles.ForEach(delegate(Control c) { c.Visible = valor; });
        }

        public static void SetSelected(bool valor, params ListControl[] controles)
        {
            if (Checar.MenorouIgual(controles.Length)) return;
            foreach (ListControl controle in controles)
            {
                foreach (ListItem item in controle.Items)
                    item.Selected = valor;
            }
        }

        #endregion

        #region Métodos - Envolvendo questões de página

        /// <summary>
        /// Reiniciará os itens de sessão de modo elegante - implícito.
        /// </summary>
        public static void ReiniciarItensdeSessão(Page pagina, params string[] chaves)
        {
            if (Checar.MenorouIgual(chaves.Length)) return;
            foreach (string chave in chaves) pagina.Session.Remove(chave);
        }

        /// <summary>
        /// Removerá os itens da ViewState de modo elegante - implícito.
        /// </summary>
        public static void RemoverItensdaViewState(StateBag viewState, params string[] chaves)
        {
            if (Checar.MenorouIgual(chaves.Length)) return;
            foreach (string chave in chaves) viewState.Remove(chave);
        }

        #endregion
    }
}
