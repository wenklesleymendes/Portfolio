using System;
using System.Drawing;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Helpers
{
    public static class DialogoHelper
    {
        public enum BotaoPadraoPergunta
        {
            Sim = 1,
            Nao = 2,
        }
        public static void AvisoDeSucesso()
        {
            AvisoDeSucesso("Operação realizada com sucesso!");
        }

        public static void AvisoDeFuncionalideDescontinuada()
        {
            Atencao("Essa funcionalidade está sendo descontinuada.\nPara mais informações, por favor, entre em contato com o suporte.");
        }

        public static DialogResult CaixaTexto(string titulo, string texto, ref string valor)
        {
            var form = new Form();
            var lblForm = new Label();
            var txtForm = new TextBox();
            var btnOk = new Button();
            var btnCancelar = new Button();

            form.Text = titulo;
            lblForm.Text = texto;
            txtForm.Text = valor;
            txtForm.MaxLength = 400;

            btnOk.Text = "OK";
            btnCancelar.Text = "Cancelar";
            btnOk.DialogResult = DialogResult.OK;
            btnCancelar.DialogResult = DialogResult.Cancel;

            lblForm.SetBounds(9, 20, 372, 13);
            txtForm.SetBounds(12, 36, 372, 20);
            btnOk.SetBounds(228, 72, 75, 23);
            btnCancelar.SetBounds(309, 72, 75, 23);

            lblForm.AutoSize = true;
            txtForm.Anchor = txtForm.Anchor | AnchorStyles.Right;
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { lblForm, txtForm, btnOk, btnCancelar });
            form.ClientSize = new Size(Math.Max(300, lblForm.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancelar;

            var dialogResult = form.ShowDialog();
            valor = txtForm.Text;
            return dialogResult;
        }

        public static void AvisoDeSucesso(string mensagem)
        {
            MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Informacao(string mensagem)
        {
            MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Informacao(string mensagem, Control controleFocus)
        {
            Informacao(mensagem);
            controleFocus.Focus();
        }

        public static void Erro(string mensagem)
        {
            MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Pergunta(string mensagem, BotaoPadraoPergunta botaoPadrao)
        {
            return MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, (botaoPadrao == BotaoPadraoPergunta.Sim ? MessageBoxDefaultButton.Button1 :
                                                                                                           MessageBoxDefaultButton.Button2));
        }
        public static DialogResult Pergunta(string mensagem)
        {
            return Pergunta(mensagem, BotaoPadraoPergunta.Sim);
        }

        public static bool Confirma(string mensagem)
        {
            return MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public static bool ConfirmaPadraoSim(string mensagem)
        {
            return MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        public static void Atencao(string mensagem)
        {
            MessageBox.Show(ObtenhaFormularioAtivo(), mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool Consistencia(bool consistencia, string mensagem, Control controleFocus)
        {
            if (consistencia)
            {
                Atencao(mensagem);
                controleFocus.Focus();
            }
            return consistencia;
        }

        public static bool Consistencia(bool consistencia, string mensagem, Func<Control> controleFocus) => Consistencia(consistencia, mensagem, controleFocus.Invoke());

        public static bool ConsistenciaObrigatorio(TextBox textBox, string mensagem)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                Atencao(mensagem);
                textBox.Focus();
                return true;
            }
            return false;
        }

        public static bool ConsistenciaObrigatorio(ComboBox comboBox, string mensagem)
        {
            if (comboBox.SelectedItem == null)
            {
                Atencao(mensagem);
                comboBox.Focus();
                return true;
            }
            return false;
        }

        public static Form ObtenhaFormularioAtivo()
        {
            return (Application.OpenForms.Count > 0) ? Application.OpenForms[0].ActiveMdiChild ?? Application.OpenForms[0] : null;
        }
    }
}
