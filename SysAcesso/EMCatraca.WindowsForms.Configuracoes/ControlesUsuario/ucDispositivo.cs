using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using EMCatraca.WindowsForms.Configuracoes.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.ControlesUsuario
{
    public partial class ucDispositivo : UserControl
    {
        public List<Dispositivo> Catracas = new List<Dispositivo>();
        private bool _ehNovaCatraca = false;
        private bool _senhaInvalida;
        private bool _ehDebug = true;
        private readonly IRepositorioAuditoria repositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();

        public ucDispositivo()
        {
            InitializeComponent();

            AjustaGrid();
            gridDispositivo.DataGridView.Click += gridDispositivo_Click;
            AjustaCamposDaTela(false);
        }

        private void AjustaCamposDaTela(bool desbloquear)
        {
            txtDescricao.Enabled = desbloquear;
            txtIP.Enabled = desbloquear;
            txtPorta.Enabled = desbloquear;

            chkGiroInvertido.Enabled = desbloquear;
            chkTipoGiroCatraca.Enabled = desbloquear;
        }

        private void ControleDispositivo_Load(object sender, EventArgs e)
        {
            ObtenhaInforacoesCatracaSelecionado();
        }

        private void AjustaGrid()
        {
            gridDispositivo.Limpe();

            var helperCatraca = new DataGridViewHelper(gridDispositivo.DataGridView);
            helperCatraca.RemovaColunas();
            helperCatraca.AddColumn("Código", "Codigo", 50);
            helperCatraca.AddColumn("Descrição", "Descricao", 90);
            helperCatraca.AddColumn("IP", "IpCatraca", 90);
            helperCatraca.AddColumn("Porta", "PortaCatraca", 50);

            if (chkGiroInvertido.Checked)
            {
                helperCatraca.AddColumnCheckBox("Giro Invertido", "EhGiroInvertido", 80);
            }

            if (chkTipoGiroCatraca.Checked)
            {
                helperCatraca.AddColumn("Tipo Acesso", "TipoAcesso", 60);
            }

            gridDispositivo.Size = chkTipoGiroCatraca.Checked
                                           ? new Size(507, 368)
                                           : new Size(393, 400);

            Catracas = MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("emcatraca.catracas.cfg");
            gridDispositivo.Exiba(Catracas);
        }

        private void chkComunicacaoWebApi_CheckedChanged(object sender, EventArgs e)
        {
            pnCatracaLoginSenha.Visible = chkTipoGiroCatraca.Checked;

            AjustaGrid();
        }

        private void gridDispositivo_Click(object sender, EventArgs e)
        {
            ObtenhaInforacoesCatracaSelecionado();
            AjustaCamposDaTela(true);

            btnIncluir.Enabled = false;

            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void ObtenhaInforacoesCatracaSelecionado()
        {
            _ehNovaCatraca = false;

            var catracaSelecionada = gridDispositivo.ObtenhaObjetosSelecionados<Dispositivo>();
            foreach (var catraca in catracaSelecionada)
            {
                txtCodigo.Value = catraca.Codigo;
                txtDescricao.Text = catraca.Descricao;
                txtIP.Text = catraca.IpCatraca;
                txtPorta.Text = catraca.PortaCatraca;
                chkGiroInvertido.Checked = catraca.EhGiroInvertido;
              
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTipoGiroCatraca.Checked && !_senhaInvalida)
                {
                    MessageBox.Show($"Senha invalida!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (!_ehNovaCatraca)
                {
                    Catracas = ObtenhaCatracasAlterada();
                }
                else
                {
                    Catracas.Add(ObtenhaNovaCatraca());
                }

                MapeadorArquivoJson.BackUpConfiguracao();
                MapeadorArquivoJson.Gravar<List<Dispositivo>>("emcatraca.catracas.cfg", Catracas);

                if (!_ehDebug)
                {
                    repositorioAuditoria.RegistreAuditoria(MonteAuditoria());
                }

                var messangem = _ehNovaCatraca
                                 ? "Configurações da  nova catraca registrada com sucesso!"
                                 : "Configurações da catraca alterada com sucesso!";

                MessageBox.Show(messangem, "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                AjustaGrid();
                AtivacaoTodosBtns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um ao tentar salvar configuração:\n\r" + ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditoriaLog.EscrevaErro(nameof(ucDispositivo), ex);
            }
        }

        private Dispositivo ObtenhaNovaCatraca()
        {
            return new Dispositivo()
            {
                Codigo = Convert.ToInt32(txtCodigo.Value),
                Descricao = txtDescricao.Text.Trim(),
                IpCatraca = txtIP.Text.Trim(),
                PortaCatraca = txtPorta.Text.Trim(),
                EhGiroInvertido = chkGiroInvertido.Checked
            };
        }

        private List<Dispositivo> ObtenhaCatracasAlterada()
        {
            var codigoCatracaSelecionada = gridDispositivo.ObtenhaObjetosSelecionados<Dispositivo>().Select(c => c.Codigo).FirstOrDefault();
            var catracas = new List<Dispositivo>();
            foreach (var catraca in Catracas)
            {
                if (catraca.Codigo == codigoCatracaSelecionada)
                {
                    catraca.Codigo = Convert.ToInt32(txtCodigo.Value);
                    catraca.Descricao = txtDescricao.Text.Trim();
                    catraca.IpCatraca = txtIP.Text.Trim();
                    catraca.PortaCatraca = txtPorta.Text.Trim();
                    catraca.EhGiroInvertido = chkGiroInvertido.Checked;
                    
                }

                catracas.Add(catraca);
            }

            return catracas;
        }

        private static Auditoria MonteAuditoria()
        {
            var log = $"O Operador: {SessaoDoUsuario.Instancia.OperadorLogado.Codigo} - {SessaoDoUsuario.Instancia.OperadorLogado.Nome}, realizou alterações nas configurações de acesso.";
            return new Auditoria(FuncaoAcesso.ConfiguracaoDeAcesso, "Salvar", log);
        }

        private void chkGiroInvertido_CheckedChanged(object sender, EventArgs e)
        {
            AjustaGrid();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            _ehNovaCatraca = true;

            AjustaCamposDaTela(true);

            btnIncluir.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;

            var proximoCodigo = Catracas.Any()
                                 ? Catracas.Last().Codigo + 1
                                 : 1;

            txtCodigo.Value = proximoCodigo;

            LimpaTela();

            txtDescricao.Focus();
            txtDescricao.Select();
        }

        private void AtivacaoTodosBtns()
        {
            btnIncluir.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void LimpaTela()
        {
            txtDescricao.Clear();
            txtIP.Clear();

            chkGiroInvertido.Checked = false;
            chkTipoGiroCatraca.Checked = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var catracaSelecionada = gridDispositivo.ObtenhaObjetosSelecionados<Dispositivo>().First();

            if (MessageBox.Show($"Tem que deseja excluir {catracaSelecionada.Descricao} ", "Excluir", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Catracas.Remove(catracaSelecionada);
                MapeadorArquivoJson.Gravar<List<Dispositivo>>("emcatraca.catracas.cfg", Catracas);
                AjustaGrid();
                LimpaTela();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AtivacaoTodosBtns();

            btnSalvar.Enabled = false;
            LimpaTela();
            AjustaCamposDaTela(false);
        }

        private void txtDescricao_Leave(object sender, EventArgs e)
        {
            txtDescricao.Text = txtDescricao.Text.Trim().ToUpper();
        }
    }
}
