using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyInnerSDK.UI;

namespace EasyInnerSDK.Entity
{
    public class OnUpdateDisplay
    {
        public int NumInner { get; set; }
        public int QtdDigitos { get; set; }
        public int TipoLeitor { get; set; }
        public int Acionamento { get; set; }
        public int PadraoCartao { get; set; }
        public int TipoConexao { get; set; }
        public int Porta { get; set; }
        public int ModuloBio { get; set; }
        public bool Teclado { get; set; }
        public bool ListaOff { get; set; }
        public bool ListaSemDigital { get; set; }
        public bool Identificacao { get; set; }
        public bool Verificacao { get; set; }
        public bool DoisLeitores { get; set; }
        public bool CatInvertida { get; set; }
        public bool Biometrico { get; set; }
        public string CartaoMaster { get; set; }

        private FrmOnline uiOnline { get; set; }
        
        public OnUpdateDisplay(FrmOnline frm)
        {
            uiOnline = frm;
        }

        public void AtualizarBilhetes(List<Bilhete> ListaBilhetes)
        {
            for (int index = 0; index < ListaBilhetes.Count; index++)
            {
                uiOnline.lstBilhetes.Items.Add(ListaBilhetes[index].ToString());
            }
        }

        public void AtualizarBilheteOnline(Bilhete RegistroOnline)
        {
            uiOnline.lstBilhetes.Items.Add(RegistroOnline.ToString());
        }

        public void AtualizarEstadoInner(int NumeroInner, Enumeradores.EstadosInner EstadoInner)
        {
            uiOnline.lblStatus.Text = "Inner: " + NumeroInner + " Estado: " + EstadoInner.ToString();
        }

        public void MensagemBox(string Mensagem, string Titulo)
        {
            uiOnline.ExibirMensagembox(Mensagem, Titulo);
        }

        public void HabilitarBotoes(bool Habilitar)
        {
            uiOnline.btnAdicionarUsuarioInnerOnline.Enabled = Habilitar;
            uiOnline.btnIniciarMaquina.Enabled = Habilitar;
            uiOnline.btnRemoverInnerLista.Enabled = Habilitar;
            uiOnline.btnPararMaquina.Enabled = !Habilitar;
        }

        public void AtualizarStatus(string Status)
        {
            uiOnline.lblStatus.Text = Status;
        }

        public void AtualizarVersaoInner(Inner InnerAtual)
        {
            bool Encontrado = false;
            for(int index = 0; index < uiOnline.lstVersaoInners.Items.Count; index++)
            {
                int NumInner = int.Parse(uiOnline.lstVersaoInners.Items[index].ToString().Substring(7, 1));
                if (NumInner == InnerAtual.Numero)
                {
                    Encontrado = true;
                }
            }
            if (Encontrado == false)
            {
                uiOnline.lstVersaoInners.Items.Add("Inner: " + InnerAtual.Numero + " " + InnerAtual.LinhaInner + ": " 
                                                    + InnerAtual.VersaoInner + " - " + InnerAtual.ModeloBioInner);
            }
        }

        public void FuncoesCatraca(bool Habilitar, bool Catraca)
        {
            if (Catraca)
            {
                uiOnline.cmdEntrada.Text = "Entrada";
                uiOnline.cmdSair.Text = "Saída";
                uiOnline.cmdEntrada.Enabled = Habilitar;
                uiOnline.cmdSair.Enabled = Habilitar;
            }
            else
            {
                uiOnline.cmdEntrada.Text = "Porta 1";
                uiOnline.cmdSair.Text = "Porta 2";
                uiOnline.cmdEntrada.Enabled = Habilitar;
                uiOnline.cmdSair.Enabled = Habilitar;
            }
        }

        public void AtulizarLabelDados(string mensagem)
        {
            uiOnline.lblDados.Text = mensagem;
        }

        public bool getInfoInner()
        {
            //Campo obrigatório
            if (uiOnline.cboTipoLeitor.SelectedIndex == -1)
            {
                MensagemBox("Favor selecionar um tipo de leitor !", "Atenção");
                return false;
            }

            //Se Catraca
            if ((uiOnline.cboEquipamento.SelectedIndex != (byte)Enumeradores.Acionamento.Acionamento_Coletor) && ((!uiOnline.optDireita.Checked) && (!uiOnline.optEsquerda.Checked)))
            {
                MensagemBox("Favor informar o lado de instalação da catraca !", "Atenção");
                return false;
            }

            NumInner = (int)uiOnline.udNumeroInner.Value;
            Porta = (int)uiOnline.txtPortaOnline.Value;
            PadraoCartao = uiOnline.cboPadraoCartao.SelectedIndex;
            ListaOff = uiOnline.chkLista.Checked;
            ListaSemDigital = uiOnline.chkListaBio.Checked;
            Acionamento = uiOnline.cboEquipamento.SelectedIndex;
            CatInvertida = uiOnline.optEsquerda.Checked;
            TipoConexao = uiOnline.cboTipoConexaoOnline.SelectedIndex;
            QtdDigitos = (int)uiOnline.udQtdDigitosCartao.Value;
            TipoLeitor = uiOnline.cboTipoLeitor.SelectedIndex;
            Teclado = uiOnline.chkHabilitaTeclado.Checked;
            Identificacao = uiOnline.chkIdentificacao.Checked;
            Verificacao = uiOnline.chkVerificacao.Checked;
            DoisLeitores = uiOnline.ckbDoisLeitores.Checked;
            CartaoMaster = uiOnline.txtCartaoMaster.Text;
            Biometrico = uiOnline.ckbBIO.Checked;
            ModuloBio = uiOnline.chkModuloLC.Checked == true ? 1 : 0; //0 LN, 0 LC
            return true;
        }

        // <summary>
        /// Remove Inner da lista
        /// </summary>
        /// <param name="UiMainOnline"></param>
        public void RemoverInner(int NumeroInner)
        {
            if (uiOnline.lstInnersCadastrados.SelectedItem != null)
            {
                foreach (Inner inner in uiOnline.lstInnersCadastrados.Items)
                {
                    if (inner.Numero == ((Inner)uiOnline.lstInnersCadastrados.SelectedItem).Numero)
                    {
                        //Remove
                        uiOnline.lstInnersCadastrados.Items.Remove(uiOnline.lstInnersCadastrados.SelectedItem);

                        //Atualiza lista
                        if (uiOnline.lstInnersCadastrados.Items.Count > 0)
                        {
                            uiOnline.lstInnersCadastrados.SelectedIndex = uiOnline.lstInnersCadastrados.Items.Count - 1;
                        }
                        MensagemBox("Inner removido da memória", "");
                        break;
                    }
                }
            }
            else
            {
                //Campo obrigatório
                MensagemBox("É necessário selecionar um Inner para remover da memória!", "");
            }
        }

        public void AdicionarInnerLista(Inner inner)
        {
            uiOnline.lstInnersCadastrados.Items.Add(inner);
        }
    }
}
