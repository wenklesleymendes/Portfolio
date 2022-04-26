using EMCatraca.Simuladores.ControliD.Controller;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EMCatraca.Simuladores.ControliD.View
{
    public partial class Form1 : Form
    {
        public static TextBox txtLog;
        public static void Log(string[] value)
        {
            foreach (string line in value)
                txtLog.AppendText(line + "\r\n");
            txtLog.AppendText("-----------------------\r\n");
        }

        Device device = new Device();
        public Form1()
        {
            InitializeComponent();
            txtLog = txtLogs;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Usuario usr = new Usuario();
            usr.Show();
        }

        public void Convert_Wiegand(double numeroCartaoGravado)
        {
            double numeroAntesVirgula, numeroDepoisVirgula;
            numeroAntesVirgula = numeroDepoisVirgula = 0;
            /**
             * Para simular a conversão vamos utilizar o seguinte numero de cartão de exemplo W:9602032
             * Após aplicação da formula  96 * 2^32 + 2032 = 412316862448
             * O numero gravado será : 412316862448 agora vamos desconverter
             * A variavel numeroCartaoGravado vai receber como valor 412316862448
             */
            //Obtendo o valor antes da virgula
            //A variavel numeroAntesVirgula apos realizar a "desconversão" recebe o valor de 96
            numeroAntesVirgula = Convert.ToInt64(numeroCartaoGravado / 0x100000000);
            //Obtendo o valor depois da virgula
            //A variavel numeroDepoisVirgula apos realizar a "desconversão" recebe o valor de 2032
            numeroDepoisVirgula = Convert.ToInt64(numeroCartaoGravado % 0x100000000);
            MessageBox.Show("Numero que o Acesso lê: " + numeroCartaoGravado + "\r\nNumero Antes da Virgula: " + numeroAntesVirgula + "\r\nNumero Depois da Virgula: " + numeroDepoisVirgula);
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            double cartao = 412316862448;
            Convert_Wiegand(cartao);
        }

        public DateTime Convert_Unix(double unix)
        {
            System.DateTime dtTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtTime = dtTime.AddSeconds(unix).ToLocalTime();
            MessageBox.Show("Data" + dtTime);
            return dtTime;
        }

        private void cadastrarUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuario usr = new Usuario();
            usr.Show();
        }

        private void setarOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setarOfflineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logsDeAcessoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listarCartõesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usuariosCadastradosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exemploDeConversãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double Cartao = 412316862448;
            Convert_Wiegand(Cartao);
        }

        private void acessoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void atualizarDataEHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void catracaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void nomeExibiçãoDoAcessoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> response = new List<string>();
            response.Add("Essa função no momento não faz nada.");
            Log(response.ToArray());
        }

        private void terminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminal terminal = new Terminal();
            terminal.Show();
        }

        // Corresponde ao botão de "Limpar Logs"
        private void button1_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
    }
}
