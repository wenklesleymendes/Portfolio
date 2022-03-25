using EMCatraca.Configuracao.Helpers;
using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.Configuracao.ControlesDeUsuario
{
    public partial class ControleDispositivo : UserControl
    {
        public List<Dispositivo> Catracas = new List<Dispositivo>();
        private bool _ehNovaCatraca = false;
        private bool _senhaInvalida;
        private bool _ehDebug = true;
        private readonly IRepositorioAuditoria repositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();

        public ControleDispositivo()
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
            txtSenha.Enabled = desbloquear;
            txtPorta.Enabled = desbloquear;
            txtLogin.Enabled = desbloquear;
            txtSenha.Enabled = desbloquear;
            txtStatusSenha.Enabled = desbloquear;
            txtConfirmaSenha.Enabled = desbloquear;

            chkGiroInvertido.Enabled = desbloquear;
            chkComunicacaoWebApi.Enabled = desbloquear;
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

            if (chkComunicacaoWebApi.Checked)
            {
                helperCatraca.AddColumn("Login", "Login", 60);
            }

            gridDispositivo.Size = chkComunicacaoWebApi.Checked
                                           ? new Size(507, 368)
                                           : new Size(393, 400);

            Catracas = MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("emcatraca.catracas.cfg");
            gridDispositivo.Exiba(Catracas);
        }

        private void chkComunicacaoWebApi_CheckedChanged(object sender, EventArgs e)
        {
            pnCatracaLoginSenha.Visible = chkComunicacaoWebApi.Checked;

            pnStatusSenha.Visible = true;

            AjustaGrid();
        }

        private void txtConfimaSenha_Leave(object sender, EventArgs e)
        {
            ValideSenha();
        }

        private void ValideSenha()
        {
            var senhaValida = txtSenha.Text.Equals(txtConfirmaSenha.Text);

            lbStatusSenha.ForeColor = senhaValida ? Color.Green : Color.Red;
            txtStatusSenha.ForeColor = senhaValida ? Color.Green : Color.Red;
            txtSenha.BackColor = senhaValida ? Color.Green : Color.Red;
            txtConfirmaSenha.BackColor = senhaValida ? Color.Green : Color.Red;
            txtStatusSenha.Text = senhaValida
                                   ? "Senha validada com sucesso!"
                                   : "Divergências de senha tente novamente!";

            _senhaInvalida = senhaValida;
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
                chkComunicacaoWebApi.Checked = catraca.EhComunicacaoWebApi;
                txtLogin.Text = catraca.Login;
                txtSenha.Text = catraca.Senha;
                txtConfirmaSenha.Text = catraca.Senha;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkComunicacaoWebApi.Checked && !_senhaInvalida)
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
                LogGeral.WriteError(ex);
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
                Login = txtSenha.Text.Trim(),
                Senha = txtSenha.Text.Trim(),
                EhComunicacaoWebApi = chkComunicacaoWebApi.Checked,
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
                    catraca.EhComunicacaoWebApi = chkComunicacaoWebApi.Checked;
                    catraca.Login = txtLogin.Text.Trim();
                    catraca.Senha = txtSenha.Text.Trim();
                }

                catracas.Add(catraca);
            }

            return catracas;
        }

        private static Auditoria MonteAuditoria()
        {
            var log = $"O Operador: {SessaoDoUsuario.Instancia.OperadorLogado.Codigo} - {SessaoDoUsuario.Instancia.OperadorLogado.Nome}, realizou alterações nas configurações de acesso.";
            return new Auditoria(EnumFuncaoAcesso.ConfiguracaoDeAcesso, "Salvar", log);
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
            txtSenha.Clear();
            txtPorta.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            txtStatusSenha.Clear();
            txtConfirmaSenha.Clear();

            chkGiroInvertido.Checked = false;
            chkComunicacaoWebApi.Checked = false;

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

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

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
