using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    public partial class frmLiberacaoAcesso : FrmBase
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private readonly IRepositorioAuditoria repositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();
        private readonly IRepositorioAluno repositorioAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAluno();
        private readonly IRepositorioSerieTurma repositorioSerieTurma = FabricaDeRepositorios.Instancia.CrieRepositorioSerieTurma();

        private List<SerieTurma> serieTurma;
        private List<Liberacao> liberacoes = new List<Liberacao>();
        private List<Aluno> alunos;

        public frmLiberacaoAcesso()
        {
            InitializeComponent();
            NomeFuncao = "Liberação de Acesso";

            var tm = new Timer { Interval = 1000 };
            tm.Tick += (s, ev) => VerificaAcessos();
            tm.Start();
        }

        private void FrmLiberacaoAcesso_Load(object sender, EventArgs e)
        {
            radAlunos.Checked = true;
            AtualizaListaDeAutorizacao();
            foreach (DataGridViewColumn column in dgvLiberados.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void RadAlunos_CheckedChanged(object sender, EventArgs e)
        {
            if (radAlunos.Checked)
            {
                CarregarAlunos();
                cboAlunos.Enabled = true;
            }
            else
            {
                cboAlunos.SelectedIndex = -1;
                cboAlunos.Enabled = false;
            }
        }

        private void RadSerieTurma_CheckedChanged(object sender, EventArgs e)
        {
            if (radSerieTurma.Checked)
            {
                CarregarSeriesTurmas();
                cboSerie.Enabled = true;
                cboTurma.Enabled = true;
            }
            else
            {
                cboSerie.SelectedIndex = -1;
                cboTurma.SelectedIndex = -1;
                cboSerie.Enabled = false;
                cboTurma.Enabled = false;
            }
        }

        private void CboAlunos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void CboSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void CboTurma_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void CarregarAlunos()
        {
            alunos = repositorioAluno.ConsulteAlunosAtivos().Select(x => new Aluno { Id = x.Id, Nome = x.Id + " - " + x.Nome }).ToList();
            cboAlunos.ValueMember = "Id";
            cboAlunos.DisplayMember = "Nome";
            cboAlunos.DataSource = alunos;
            cboAlunos.SelectedIndex = -1;
        }

        private void CarregarSeriesTurmas()
        {
            cboSerie.ValueMember = "Codigo";
            cboSerie.DisplayMember = "Descricao";
            serieTurma = repositorioSerieTurma.ConsulteTodasSeriesTurmas();
            cboSerie.DataSource = (List<Serie>)serieTurma.GroupBy(s => s.Serie.Descricao).ToList().Select(s => s.First().Serie).ToList();
        }

        private void CarregarTurmas()
        {
            if (cboSerie.SelectedIndex == -1)
                return;
            cboTurma.ValueMember = "Codigo";
            cboTurma.DisplayMember = "Descricao";
            int codigoSerie = ((Serie)cboSerie.SelectedItem).Codigo;
            cboTurma.DataSource = (List<Turma>)serieTurma.Where(t => t.Serie.Codigo == codigoSerie).Select(t => t.Turma).ToList();
        }

        private void CboSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSerie.SelectedIndex != -1)
            {
                CarregarTurmas();
            }
        }

        private void BtnLiberar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMotivo.Text) || txtMotivo.Text.Length < 5)
            {
                MessageBox.Show("É necessário informar o Motivo da liberação!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMotivo.Select();
                return;
            }

            if (radAlunos.Checked && cboAlunos.SelectedIndex != -1)
            {
                if (liberacoes.Any(l => l.Aluno.Id == ((Aluno)cboAlunos.SelectedItem).Id && !l.Acessou))
                {
                    MessageBox.Show("Aluno já possui liberação!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                LibereAluno((Aluno)cboAlunos.SelectedItem);
                AtualizaListaDeAutorizacao();
                txtMotivo.Text = "";
            }
            else if (radSerieTurma.Checked && cboSerie.SelectedIndex != -1)
            {
                if (cboTurma.Items.Count > 0 && cboTurma.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione uma Turma para ser liberada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboTurma.Focus();
                }
                else
                {
                    var serieTurma = new SerieTurma
                    {
                        Serie = (Serie)cboSerie.SelectedItem,
                        Turma = (Turma)cboTurma.SelectedItem
                    };

                    foreach (Aluno aluno in repositorioAluno.ConsulteAlunosPorTurmaMontada(serieTurma.Serie.Codigo, serieTurma.Turma.Codigo))
                    {
                        if (liberacoes.Any(l => l.Aluno.Id == aluno.Id && !l.Acessou))
                            continue;
                        else
                            LibereAluno(aluno);
                    }

                    AtualizaListaDeAutorizacao();
                    txtMotivo.Text = "";
                }
            }
        }

        private void TxtMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void AtualizaListaDeAutorizacao()
        {
            liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");
            var liberacoesValidas = new List<Liberacao>();
            dgvLiberados.Rows.Clear();
            foreach (Liberacao liberacao in liberacoes)
            {
                if (!liberacao.Acessou && liberacao.DataHoraLiberou >= DateTime.Now.AddSeconds((double)(-60 * liberacao.TempoParaAcessso)))
                {
                    var x = dgvLiberados.Rows.Add(liberacao.Aluno.Id, liberacao.Aluno.Nome, (liberacao.DataHoraLiberou.AddMinutes((double)liberacao.TempoParaAcessso) - DateTime.Now).ToString("mm\\:ss"));
                    liberacoesValidas.Add(liberacao);
                }
            }
            MapeadorArquivoJson.Gravar<List<Liberacao>>("emcatraca.liberacao.cfg", liberacoesValidas);
            VerificaAcessos();
        }

        private void VerificaAcessos()
        {
            liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");
            if (dgvLiberados.Rows.Count == 0) return;
            for (int i = 0; i < liberacoes.Count(); i++)
            {
                var tempo = (double)(60 * liberacoes[i].TempoParaAcessso);
                var tempoDecorrido = (DateTime.Now - liberacoes[i].DataHoraLiberou).TotalSeconds;
                if (tempoDecorrido < tempo)
                {
                    dgvLiberados.Rows[i].Cells[2].Value = (liberacoes[i].DataHoraLiberou.AddMinutes((double)liberacoes[i].TempoParaAcessso) - DateTime.Now).ToString("mm\\:ss");
                    AjustarLinhasDaGrid(i, Color.Black);
                    if (tempoDecorrido > (tempo - 5))
                    {
                        AjustarLinhasDaGrid(i, Color.Brown);
                    }
                    else if (tempoDecorrido > (tempo - 15))
                    {
                        AjustarLinhasDaGrid(i, Color.FromArgb(80, 75, 0));
                    }
                }
                else
                {
                    AtualizaListaDeAutorizacao();
                    return;
                }

                if (liberacoes[i].Acessou)
                {
                    AjustarLinhasDaGrid(i, Color.Blue);
                    if (liberacoes[i].DataHoraAcessou != null && (DateTime.Now - liberacoes[i].DataHoraAcessou).TotalSeconds > 5)
                        AtualizaListaDeAutorizacao();
                }
            }
        }

        private void AjustarLinhasDaGrid(int linha, Color cor)
        {
            foreach (DataGridViewCell celula in dgvLiberados.Rows[linha].Cells)
            {
                celula.Style.ForeColor = cor;
            }
        }

        private void LibereAluno(Aluno aluno)
        {
            var liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");
            var liberacao = new Liberacao
            {
                Aluno = aluno,
                Operador = SessaoDoUsuario.Instancia.OperadorLogado,
                Motivo = txtMotivo.Text,
                DataHoraLiberou = DateTime.Now,
                Acessou = false,
                TempoParaAcessso = numTempoAcesso.Value
            };

            liberacoes.Add(liberacao);

            MapeadorArquivoJson.Gravar<List<Liberacao>>("emcatraca.liberacao.cfg", liberacoes);

            repositorioAuditoria.RegistreAuditoria(MonteAuditoria(aluno, txtMotivo.Text));

        }

        private static Auditoria MonteAuditoria(Aluno aluno, string motivo)
        {
            var log = $"Liberou o acesso do Aluno: {aluno.Id} - {aluno.Nome}, Motivo: {motivo}";
            return new Auditoria(FuncaoAcesso.AcessoAluno, "Liberação", log);
        }

        private void cboAlunos_TextUpdate(object sender, EventArgs e)
        {
            string filtro = cboAlunos.Text;
            List<Aluno> filtrados = alunos.FindAll(x => x.Nome.Contains(filtro));
            cboAlunos.DataSource = filtrados;
            if (String.IsNullOrWhiteSpace(filtro))
            {
                cboAlunos.DataSource = alunos;
            }
            cboAlunos.DroppedDown = true;
            cboAlunos.IntegralHeight = true;
            cboAlunos.SelectedIndex = -1;
            cboAlunos.Text = filtro;
            cboAlunos.SelectionStart = filtro.Length;
            cboAlunos.SelectionLength = 0;
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            if (dgvLiberados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Não há liberações selecionadas", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var id = Convert.ToInt32(dgvLiberados.SelectedRows[0].Cells["Id"].Value.ToString());
            var aluno = dgvLiberados.SelectedRows[0].Cells["Nome"].Value.ToString();
            if (MessageBox.Show($"Deseja realmente remover o acesso do Aluno(a):\n\n{aluno}", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                liberacoes = liberacoes.Where(l => l.Aluno.Id != id).ToList();
                MapeadorArquivoJson.Gravar<List<Liberacao>>("emcatraca.liberacao.cfg", liberacoes);
                AtualizaListaDeAutorizacao();
            }

        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoverFormulario(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void pnlConteudo_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void dgvLiberados_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            MoverFormulario(e);
        }
    }
}
