using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swarm.Persistencia
{
    public class ColecaoPersistencia
    {
        #region Propriedades

        internal Transacao TransacaoEnvolvida { get; set; }
        private List<ItemTransacao> ItensEnvolvidos { get; set; }

        #endregion

        public ColecaoPersistencia()
        {
            this.TransacaoEnvolvida = new Transacao();
            this.ItensEnvolvidos = new List<ItemTransacao>();
        }

        #region Métodos

        public void AdicionarItem(object item, EnumPersistencia.Operacao tipoTransacao)
        {
            IItemPersistencia itemPersistencia = item as IItemPersistencia;

            ItemTransacao itemTransacao = new ItemTransacao(itemPersistencia, tipoTransacao);
            this.ItensEnvolvidos.Add(itemTransacao);
        }

        public void Limpar()
        {
            this.ItensEnvolvidos.Clear();
        }

        public void Persistir()
        {
            this.TransacaoEnvolvida.Iniciar();

            try
            {
                foreach (ItemTransacao item in this.ItensEnvolvidos)
                {
                    item.Objeto.DefinirTransacaoEnvolvida(this.TransacaoEnvolvida);

                    switch (item.Tipo)
                    {
                        case EnumPersistencia.Operacao.Incluir:
                            {
                                item.Objeto.Incluir();
                                break;
                            }
                        case EnumPersistencia.Operacao.Alterar:
                            {
                                item.Objeto.Alterar();
                                break;
                            }
                        case EnumPersistencia.Operacao.Excluir:
                            {
                                item.Objeto.Excluir();
                                break;
                            }
                    }
                }

                this.TransacaoEnvolvida.Commit();
            }
            catch (Exception erro)
            {
                this.TransacaoEnvolvida.Rollback();
                throw new Exception(erro.Message);
            }
        }
        #endregion
    }
}
