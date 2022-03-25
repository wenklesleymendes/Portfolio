using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyInnerSDK.UI;
using EasyInnerSDK.Entity;


namespace EasyInnerSDK.UI
{
    public partial class FrmOffLine : Form
    {
        static bool aberto = false;
        public FrmOffLineController OffLineController { get; set; }
        public FrmOffLine(Form pai)
        {
            if (!aberto)
            {
                InitializeComponent();
                OffLineController = new FrmOffLineController(this);
                MdiParent = pai;
                aberto = true;
                Show();
            }
            else
            {
                Dispose();
            }

        }

        //************************************************
        //Habilita/Desabilita campos
        //************************************************
        private void chkHorarios_CheckedChanged(object sender, EventArgs e)
        {
            chkLista.Enabled = chkHorarios.Checked;
            chkLista.Checked = false;
            chkSirene.Enabled = chkHorarios.Checked;
            chkSirene.Checked = false;
        }

        //************************************************
        //Habilita/Desabilita campos Bio
        //************************************************
        private void chkBio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBio.Checked)
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
                chkModuloLC.Enabled = false;
            }

            chkVerificacao.Checked = false;
            chkIdentificacao.Checked = false;
            chkListaBio.Checked = false;
            chkModuloLC.Checked = false;
        }

        //************************************************
        //Habilita/Desabilita campos
        //************************************************
        private void chkListaBio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkListaBio.Checked)
                chkVerificacao.Checked = true;
        }

        //************************************************
        //Habilita/Desabilita campos
        //************************************************
        private void chkVerificacao_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVerificacao.Checked)
                chkListaBio.Checked = false;
        }

        //************************************************
        //Botão Enviar
        //************************************************
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            OffLineController.Enviar();
        }

        //************************************************
        //Botão Receber
        //************************************************
        private void btnReceber_Click(object sender, EventArgs e)
        {
            OffLineController.Receber();
        }

        //************************************************
        //Abertura do Formulário
        //Carregamento das combos
        //************************************************
        private void FrmOffLine_Load(object sender, EventArgs e)
        {
            this.cboTipoLeitor.Items.Add("Código de Barras");
            this.cboTipoLeitor.Items.Add("Magnético");
            this.cboTipoLeitor.Items.Add("Proximidade Abatrack/Smart Card");
            this.cboTipoLeitor.Items.Add("Proximidade Wiegand/Smart Card");
            this.cboTipoLeitor.Items.Add("Proximidade Smart Card Serial");
            this.cboTipoLeitor.Items.Add("Código de barras serial");
            this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Sem Zero");
            this.cboTipoLeitor.SelectedIndex = 0;

            this.cboTipoConexao.Items.Add("Serial");
            this.cboTipoConexao.Items.Add("TCP/IP porta variável");
            this.cboTipoConexao.Items.Add("TCP/IP porta fixa");
            this.cboTipoConexao.SelectedIndex = 2;

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
        }

        //************************************************
        //Habilita opção 2 leitores se for proximidade
        //************************************************
        private void cboPadraoCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkDoisLeitores.Enabled = (!(cboTipoLeitor.SelectedIndex == (int)EasyInnerSDK.Entity.Enumeradores.TipoLeitor.CODIGO_DE_BARRAS) && !(cboTipoLeitor.SelectedIndex == (int)EasyInnerSDK.Entity.Enumeradores.TipoLeitor.MAGNETICO));
            chkDoisLeitores.Checked = false;

            if (cboTipoLeitor.SelectedIndex == 2)
            {
                txtDigitos.Text = "14";
            }
            else if (cboTipoLeitor.SelectedIndex == 3)
            {
                txtDigitos.Text = "6";
            }
            
        }

        //************************************************
        //Fechamento Formulário
        //************************************************
        private void FrmOffLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        //************************************************
        //Seleção combo Equipamento
        //Habilita/Desabilita campos conforme seleção
        //************************************************
        private void cboEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se catraca
            if ((cboEquipamento.SelectedIndex != (byte)Enumeradores.Acionamento.Acionamento_Coletor))
            {
                optEsquerda.Enabled = true;
                optDireita.Enabled = true;
                chkDoisLeitores.Enabled = true;

                //Se Urna
                if ((cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna))
                {
                    optDireita.Checked = true;
                    optEsquerda.Enabled = false;
                    optDireita.Enabled = false;
                    imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;
                    lblCatraca.Enabled = true;
                    cboTipoLeitor.SelectedIndex = 3;//proximidade
                    chkDoisLeitores.Checked = true;
                }
                else //Não é Urna
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
                    lblCatraca.Enabled = true;
                }
            }
            else //Coletor
            {
                optEsquerda.Enabled = false;
                optDireita.Enabled = false;
                lblCatraca.Enabled = false;
                imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.nenhum;
            }
        }

        private void optEsquerda_CheckedChanged(object sender, EventArgs e)
        {
            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Esquerda_invertidaa;       
        }

        private void optDireita_CheckedChanged(object sender, EventArgs e)
        {
            imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.Direita_normall;       
        }

        private void chkCartaoMaster_CheckedChanged(object sender, EventArgs e)
        {
            txtCartaoMaster.Enabled = chkCartaoMaster.Checked;
        }

        private void rdbPadraoTopdata_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPadraoTopdata.Checked)
            {
                MessageBox.Show("Este tipo é para uso exclusivo de cartões fabricado pela Topdata !");
            }
        }       
    }
}
