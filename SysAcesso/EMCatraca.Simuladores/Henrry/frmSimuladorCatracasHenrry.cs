using EMCatraca.ClasseFirmware.Simuladores;
using EMCatraca.Core.Dominio;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EMCatraca.Simuladores.Henrry
{
    public partial class frmSimuladorCatracasHenrry : Form
    {

        CatracaHenry8x catracaHenry8x = new CatracaHenry8x();
        private Dispositivo _catraca = new Dispositivo();

        public frmSimuladorCatracasHenrry(Dispositivo catraca, int contador)
        {
            InitializeComponent();

            _catraca = catraca;

            txtServidor.Text = _catraca.IpCatraca;
            txtPorta.Text = _catraca.PortaCatraca.ToString();
            txtDecricaoDaCatraca.Text = _catraca.Descricao;

            catracaHenry8x.eventos.CatracaHenry8xStatus_RecebeStatus += CatracaHenry8xStatus_Change;
            catracaHenry8x.eventos.CatracaHenry8x_RecebeDados += CatracaHenry8xDadosRecebidos;
        }

        private void btnLigarCatraca_Click(object sender, EventArgs e)
        {
            try
            {
                catracaHenry8x.configuracao.Log = false;
                catracaHenry8x.IniciarEmuladorCatraca(_catraca);

                lbStatus.Text = "Ligado";
                lbStatus.ForeColor = Color.Green;
            }
            catch
            {
                lbStatus.Text = "Não foi Ligado";
                lbStatus.ForeColor = Color.Red;
                throw;
            }

        }

        private void btnSolicita_Click(object sender, EventArgs e)
        {
            if (lbStatus.Text.Equals("Ligado"))
            {
                SolicitarAcesso();
            }
            else
            {
                MessageBox.Show("Para Passar o cartão deve primeiro ligar o simulador");
                btnLigarCatraca.Focus();
            }

        }


        private void SolicitarAcesso()
        {
            catracaHenry8x.EnviaMensagemEmBytes(txtComando.Text);
            txtResultado.Text = "\r\nEnviou\r\n" + txtComando.Text + "\r\n" + txtResultado.Text;
        }

        delegate void EmitirStatusCallback(CatracaStatus status);
        void EmitirStatus(CatracaStatus status)
        {
            if (InvokeRequired)
            {
                EmitirStatusCallback callback = EmitirStatus;
                try { Invoke(callback, status); } catch { }
            }
            else
            {
                this.Show();
                lbComunicacao.Text = status.Status;
                Console.WriteLine(status.Mensagem);
            }
        }

        private void CatracaHenry8xStatus_Change(object sender, EventArgs e)
        {
            EmitirStatus(catracaHenry8x.status);
        }

        private void CatracaHenry8xDadosRecebidos(object sender, EventArgs e)
        {
            EmitirDadosRecebidos(catracaHenry8x.dadosRecebidos);
        }

        delegate void DadosRecebidosCallback(CatracaDadosRecebidos dadosRecebidos);
        void EmitirDadosRecebidos(CatracaDadosRecebidos dados)
        {
            if (InvokeRequired)
            {
                DadosRecebidosCallback callback = EmitirDadosRecebidos;
                Invoke(callback, dados);
            }
            else
            {
                this.Show();
                lbComunicacao.Text = "Recebeu";
                txtResultado.Text = "\r\nRecebeu\r\n" + dados.DadosString + "\r\n" + txtResultado.Text;
                string[][] str = catracaHenry8x.FiltraMensagem(dados.DadosString);
                if (str.Length >= 4)
                {
                    if (str[0].Length == 4 && str[0][1] == "REON")
                    {
                        if (str[0][3].Equals("5") || str[0][3].Equals("6") || str[0][3].Equals("1"))
                        {
                            string comando = "15+REON+000+80]{0}]{1} {2}]1]0]";
                            comando = string.Format(comando, txtMatricula.Text.Trim(), DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                            catracaHenry8x.EnviaMensagemEmBytes(comando);
                            txtResultado.Text = "\r\nEnviou\r\n" + comando + "\r\n" + txtResultado.Text;
                        }
                        if (str[0][3].Equals("30"))
                        {
                            txtMatricula.Focus();

                            string comando = "15+REON+000+80]{0}]{1} {2}]1]0]";
                            comando = string.Format(comando, txtMatricula.Text.Trim(), DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                            catracaHenry8x.EnviaMensagemEmBytes(comando);
                            txtResultado.Text = "\r\nEnviou\r\n" + comando + "\r\n" + txtResultado.Text;
                        }
                    }
                }
            }
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            string comando = "15+REON+000+0]{0}]{1} {2}]1]0]";
            comando = string.Format(comando, txtMatricula.Text.Trim(), DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            txtComando.Text = comando;
        }

        private void frmSimuladorCatracasHenrry_FormClosing(object sender, FormClosingEventArgs e)
        {
            catracaHenry8x.executarClient = false;
        }

        private void btnFinaliza_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Desconectou";
            catracaHenry8x.executarServer = false;
        }

        private void btnGiraCatraca_Click(object sender, EventArgs e)
        {
            //Girou a catraca
            string comando = "01+REON+000+81]{0}]{1} {2}]1]0]1]";
            comando = string.Format(comando, txtMatricula.Text.Trim(), DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            txtComando.Text = comando;

            catracaHenry8x.EnviaMensagemEmBytes(txtComando.Text);
            txtResultado.Text = "\r\nEnviou\r\n" + comando + "\r\n" + txtResultado.Text;

            txtMatricula.Focus();
        }

        private void btnNaoGiraCatraca_Click(object sender, EventArgs e)
        {
            //Não girou a catraca
            string comando = "01+REON+00+82]{0}]{1} {2}]1]0]1]";
            comando = string.Format(comando, txtMatricula.Text.Trim(), DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            txtComando.Text = comando;

            catracaHenry8x.EnviaMensagemEmBytes(txtComando.Text);
            txtResultado.Text = "\r\nEnviou\r\n" + comando + "\r\n" + txtResultado.Text;

            txtMatricula.Focus();
        }

        bool parar = false;
        private void btnIniciarRand_Click(object sender, EventArgs e)
        {
            var pessoa = 0;
            var val1 = 0;
            var val2 = 0;
            parar = false;
            if (!Int32.TryParse(txtPessoa.Text, out pessoa))
            {
                MessageBox.Show("Digite numeros 1, 2, 3, 5, 6!", "Atenção", MessageBoxButtons.OK);
                txtPessoa.Text = "";
                txtPessoa.Focus();
            }
            if (!Int32.TryParse(txtIniRand.Text, out val1))
            {
                MessageBox.Show("Valor inválido!", "Atenção", MessageBoxButtons.OK);
                txtIniRand.Text = "";
                txtIniRand.Focus();
            }
            if (!Int32.TryParse(txtFimRand.Text, out val2))
            {
                MessageBox.Show("Valor inválido!", "Atenção", MessageBoxButtons.OK);
                txtFimRand.Text = "";
                txtFimRand.Focus();
            }
            Random rnd = new Random();
            while (!parar)
            {
                int codigo = rnd.Next(val1, val2);
                txtMatricula.Text = pessoa.ToString() + codigo.ToString();
                SolicitarAcesso();
                Application.DoEvents();
                Thread.Sleep(200);
            }
        }

        private void btnFinalizarRand_Click(object sender, EventArgs e)
        {
            parar = true;
        }

        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SolicitarAcesso();
            }
            if (e.KeyChar == 27 || e.KeyChar == 24)
            {
                txtMatricula.Text = "";
                txtMatricula.Focus();
            }
        }

        private void txtMatricula_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                txtMatricula.Text = "";
                txtMatricula.Focus();
            }
        }

        private void btnLimparComandos_Click(object sender, EventArgs e)
        {
            txtResultado.Clear();
        }
    }
}
