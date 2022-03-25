using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ValidacaoExterna;

namespace EMCatraca.Simuladores.Neokoros
{
    public partial class frmSimuladorNeokoros : Form
    {
        private readonly IValidador _validador = new ValidacaoExterna.Validador();

        public frmSimuladorNeokoros()
        {
            InitializeComponent();
            lblNomeDoAluno.Text = string.Empty;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegistrarEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                LimpeResultadosAnteriores();
                _validador.RegistrarGiro(txtMatricula.Text, DateTime.Now, 1, 1);

                lblRegistrou.Text = "Acesso registrado";
            }
            catch (Exception exception)
            {
                lbIMensagem1.Text = exception.Message;
            }
        }

        private void btnRegistrarSaida_Click(object sender, EventArgs e)
        {
            try
            {
                LimpeResultadosAnteriores();
                _validador.RegistrarGiro(txtMatricula.Text, DateTime.Now, 1, 2);

                lblRegistrou.Text = "Acesso registrado";
            }
            catch (Exception exception)
            {
                lbIMensagem1.Text = exception.Message;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            LimpeResultadosAnteriores();
            lblNomeDoAluno.Text = Consultar();
        }

        private void btnValidarAcesso_Click(object sender, EventArgs e)
        {
            LimpeResultadosAnteriores();
            try
            {
                _validador.ValidarAcesso(txtMatricula.Text, DateTime.Now, 1, out var mensagem1, out var mensagem2, out var acessoEsperado);

                lbIMensagem1.Text = mensagem1;
                lblMensagem2.Text = mensagem2;
                lblAcessoEsperado.Text = acessoEsperado.ToString();
            }
            catch (Exception exception)
            {
                lbIMensagem1.Text = exception.Message;
            }
        }

        private void TxtMatricula_Enter(object sender, EventArgs e)
        {
            LimpeResultadosAnteriores();
        }

        private void btnRegistraDll_Click(object sender, EventArgs e)
        {
            RegistraDll(true);
        }

        private void btnRemoverRegistro_Click(object sender, EventArgs e)
        {
            RegistraDll(false);
        }

        private void LimpeResultadosAnteriores()
        {
            lbIMensagem1.Text = string.Empty;
            lblMensagem2.Text = string.Empty;
            lblAcessoEsperado.Text = string.Empty;
            lblRegistrou.Text = string.Empty;
            lblNomeDoAluno.Text = string.Empty;
        }

        private string Consultar()
        {
            _validador.ConsultarCodigo(txtMatricula.Text, out var nome);
            return nome;
        }

        private void RegistraDll(bool registrar)
        {
            var localRegistro = 0;
            var localRegasm = 0;
            var pathNeokoros = new string[3];
            pathNeokoros[0] = Environment.CurrentDirectory;
            pathNeokoros[1] = @"C:\Neokoros\exe";
            pathNeokoros[2] = @"C:\Program Files(x86)\Neokoros\exe";
            var pathRegasm = new string[2];
            pathRegasm[0] = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe";
            pathRegasm[1] = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe";
            var pathLocal = Environment.CurrentDirectory;
            var complemento = registrar ? "/tlb" : "/u";

            if (Directory.Exists(pathNeokoros[1]))
                localRegistro = 1;
            else if (Directory.Exists(pathNeokoros[2]))
                localRegistro = 2;

            if (localRegistro > 0)
            {
                File.Copy(Path.Combine(pathLocal, "ValidacaoExterna.dll"), pathNeokoros[localRegistro]);
                File.Copy(Path.Combine(pathLocal, "FirebirdSql.Data.FirebirdClient.dll"), pathNeokoros[localRegistro]);
            }

            if (File.Exists(pathRegasm[0]))
                localRegasm = 0;
            else if (File.Exists(pathRegasm[1]))
                localRegasm = 1;

            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = pathRegasm[localRegasm],
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = Path.Combine(pathNeokoros[localRegistro], $"ValidacaoExterna.dll {complemento}")
            };

            using (var exeProcess = Process.Start(startInfo))
            {
                if (exeProcess != null)
                {
                    exeProcess.WaitForExit();
                }
            }
        }
    }
}
