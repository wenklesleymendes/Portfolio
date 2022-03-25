using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using EasyInnerSDK.Entity;
using System.Threading;

namespace EasyInnerSDK.UI
{
    public partial class FrmOnline : Form
    {
        static bool aberto = false;

        #region Propriedades

        private int ListIndex = -1;

        public bool Ativa {get;set;}

        public bool FechouMaquina { get; set; }

        public FrmOnlineController ControlOnline { get; set; }

        #endregion

        private static Form FPai;
        public FrmOnline(Form pai)
        {
            if (!aberto)
            {
                InitializeComponent();
                FPai = pai;
                MdiParent = pai;
                aberto = true;
                ControlOnline = new FrmOnlineController(this);
                Show();
            }
            else
            {
                Dispose();
            }
        }

        private bool LiberaCatraca() 
        {
            if (lstInnersCadastrados.SelectedIndex == -1)
                lstInnersCadastrados.SelectedIndex = 0;

            foreach (Inner inner in ControlOnline.ListInners.Values)
            {
                if (inner.Numero == ((Inner)lstInnersCadastrados.SelectedItem).Numero)
                {
                    inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                }
            }
            return true;
        }

        public void ExibirMensagembox(string Mensagem, string Titulo)
        {
            MessageBox.Show(Mensagem, Titulo);
        }

        #region Eventos

        #region btnAdicionarUsuarioInnerOnline_Click
        private void btnAdicionarUsuarioInnerOnline_Click(object sender, EventArgs e)
        {
            ControlOnline.AdicionarInner();
            btnIniciarMaquina.Enabled = true;
        }
        #endregion

        #region btnIniciarMaquina_Click
        private void btnIniciarMaquina_Click(object sender, EventArgs e)
        {
            btnPararMaquina.Enabled = true;
            btnIniciarMaquina.Enabled = false;
            Application.DoEvents();
            ControlOnline.IniciarMaquina();
        }
        #endregion

        #region btnPararMaquina_Click
        private void btnPararMaquina_Click(object sender, EventArgs e)
        {
            ControlOnline.PararMaquina();
            lstVersaoInners.Items.Clear();

            btnIniciarMaquina.Enabled = true;

            cmdEntrada.Enabled = false;
            btnPararMaquina.Enabled = false;
            cmdSair.Enabled = false;
        }
        #endregion

        #region MainBIO_FormClosing
        private void MainBIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            ControlOnline.PararMaquina();
        }
        #endregion

        #region btnRemoverInnerLista_Click
        private void btnRemoverInnerLista_Click(object sender, EventArgs e)
        {
            Inner InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);
            ControlOnline.UpdateDisplay.RemoverInner(InnerAtual.Numero);
            ControlOnline.RemoverInnerLista(InnerAtual);
            if (lstInnersCadastrados.Items.Count == 0)
            {
                btnIniciarMaquina.Enabled = false;
            }
        }
        #endregion

        #region FrmOnline_Load
        private void FrmOnline_Load(object sender, EventArgs e)
        {
            this.cboPadraoCartao.Items.Add("Topdata");
            this.cboPadraoCartao.Items.Add("Livre");
            this.cboPadraoCartao.SelectedIndex = 1;

            this.cboTipoConexaoOnline.Items.Add("Serial");
            this.cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
            this.cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
            this.cboTipoConexaoOnline.SelectedIndex = 2;

            this.cboTipoLeitor.Items.Clear();
            this.cboTipoLeitor.Items.Add("Código de Barras");
            this.cboTipoLeitor.Items.Add("Magnético");
            this.cboTipoLeitor.Items.Add("Proximidade Abatrack/Smart Card");
            this.cboTipoLeitor.Items.Add("Proximidade Wiegand/Smart Card");
            this.cboTipoLeitor.Items.Add("Proximidade Smart Card Serial");
            this.cboTipoLeitor.Items.Add("Código de barras serial");
            this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Sem Zero");
            //this.cboTipoLeitor.Items.Add("QR Code com letras");
            this.cboTipoLeitor.SelectedIndex = 0;

            this.cboEquipamento.Items.Clear();
            this.cboEquipamento.Items.Add("Não utilizado(Coletor)");
            this.cboEquipamento.Items.Add("Catraca Entrada/Saída");
            this.cboEquipamento.Items.Add("Catraca Entrada");
            this.cboEquipamento.Items.Add("Catraca Saída");
            this.cboEquipamento.Items.Add("Catraca Saída Liberada");
            this.cboEquipamento.Items.Add("Catraca Entrada Liberada");
            this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos");
            this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos(Sentido Giro)");
            this.cboEquipamento.Items.Add("Catraca com Urna");
            this.cboEquipamento.SelectedIndex = 0;

            btnPararMaquina.Enabled = false;
        }
        #endregion

        #region cboTipoConexaoOnline_SelectedIndexChanged
        private void cboTipoConexaoOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoConexaoOnline.SelectedIndex == 0)
            {
                txtPortaOnline.Value = 1;
            }
            else
            {
                txtPortaOnline.Value = 3570;
            }
        }
        #endregion

        #region ckbBIO_Click
        private void ckbBIO_Click(object sender, EventArgs e)
        {
            if (ckbBIO.Checked)
            {
                chkVerificacao.Enabled = true;
                chkIdentificacao.Enabled = true;
                chkListaBio.Enabled = true;
                chkModuloLC.Enabled = true;
            }
            else
            {
                chkVerificacao.Enabled = false;
                chkIdentificacao.Enabled = false;
                chkListaBio.Enabled = false;
                chkVerificacao.Checked = false;
                chkIdentificacao.Checked = false;
                chkListaBio.Checked = false;
                chkModuloLC.Enabled = false;
                chkModuloLC.Checked = false;
            }
        }
        #endregion

        #region cboPadraoCartao_SelectedIndexChanged
        private void cboPadraoCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbDoisLeitores.Enabled = (!(cboTipoLeitor.SelectedIndex == (int)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS) && !(cboTipoLeitor.SelectedIndex == (int)Enumeradores.TipoLeitor.MAGNETICO));
            ckbDoisLeitores.Checked = false;
            if (cboTipoLeitor.SelectedIndex == 2)
            {
                udQtdDigitosCartao.Value = 14;
            }
            else if (cboTipoLeitor.SelectedIndex == 3)
            {
                udQtdDigitosCartao.Value = 6;
            }

          
        }
        #endregion

        #region cmdLimpar_Click
        private void cmdLimpar_Click(object sender, EventArgs e)
        {
            lstBilhetes.Items.Clear();
        }
        #endregion

        #region cmdEntrada_Click
        private void cmdEntrada_Click(object sender, EventArgs e)
        {
            Inner inner = new Inner();
            if (inner.Catraca)
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                inner.CatInvertida = false;
                ControlOnline.HABILITA_LADO_CATRACA("Entrada", inner.CatInvertida);
                inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
            else
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                EasyInner.AcionarBipCurto(1);
                EasyInner.AcionarRele1(1);
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
        }
        #endregion

        #region lstInnersCadastrados_Click
        private void lstInnersCadastrados_Click(object sender, EventArgs e)
        {
            ListIndex = lstInnersCadastrados.SelectedIndex;
        }
        #endregion

        #region cmdSair_Click
        private void cmdSair_Click(object sender, EventArgs e)
        {
            Inner inner = new Inner();
            if (inner.Catraca)
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                inner.CatInvertida = false;
                ControlOnline.HABILITA_LADO_CATRACA("Saida", inner.CatInvertida);
                inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
            else
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                EasyInner.AcionarBipCurto(1);
                EasyInner.AcionarRele2(1);
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }   
        }
        #endregion

        private void ckbListaBio_Click(object sender, EventArgs e)
        {
            if (chkListaBio.Checked)
                chkVerificacao.Checked = true;
        }

        #endregion

        private void ckbVerificacao_Click(object sender, EventArgs e)
        {
            if (!chkVerificacao.Checked)
               chkListaBio.Checked = false;
        }

        private void FrmOnline_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        private void optEsquerda_CheckedChanged(object sender, EventArgs e)
        {
            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Esquerda_invertidaa;
        }

        private void optDireita_CheckedChanged(object sender, EventArgs e)
        {
            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
        }

        private void cboEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cboEquipamento.SelectedIndex != (byte)Enumeradores.Acionamento.Acionamento_Coletor))
            {
                optEsquerda.Enabled = true;
                optDireita.Enabled = true;
                ckbDoisLeitores.Enabled = true;
                    
                if ((cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna))
                {
                    optDireita.Checked=true;
                    optEsquerda.Enabled = false;
                    optDireita.Enabled = false;
                    imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
                    gbLadoCatraca.Enabled = true;
                    cboTipoLeitor.SelectedIndex = 4;//proximidade
                    ckbDoisLeitores.Checked = true;
               
                }
                else
                {
                    if (optDireita.Checked)
                    {
                        imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
                    }
                    else
                    {
                        if (optEsquerda.Checked)
                        {
                            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Esquerda_invertidaa;
                        }
                    }

                    gbLadoCatraca.Enabled = true;
                }

            }
            else
            {
                    optEsquerda.Enabled = false;
                    optDireita.Enabled = false;
                    gbLadoCatraca.Enabled = false;
                    imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.nenhum;
            
            }
        }

        private void chkCartaoMaster_CheckedChanged(object sender, EventArgs e)
        {
            txtCartaoMaster.Enabled = chkCartaoMaster.Checked;
        }

        private void cboPadraoCartaoOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPadraoCartao.SelectedIndex == 0)
            {
                MessageBox.Show("Este tipo é para uso exclusivo de cartões fabricado pela Topdata !");
            }
        }
    }
}