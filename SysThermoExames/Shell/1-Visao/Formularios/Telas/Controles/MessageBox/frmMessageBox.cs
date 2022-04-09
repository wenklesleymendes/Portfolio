using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formularios.Telas.Controles
{
    public partial class frmMessageBox : Form
    {
        #region Campos

        private Color primaryColor = Color.HotPink;
        private readonly int borderSize = 3;

        #endregion

        #region Propriedades

        public Color PrimaryColor
        {
            get { return primaryColor; }
            set
            {
                primaryColor = value;
                BackColor = primaryColor;//Formulário da cor da borda
                panelTitleBar.BackColor = PrimaryColor;//Cor de fundo da barra do título
            }
        }

        #endregion

        #region Construtores

        public frmMessageBox(string text)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = "";
            SetFormSize();
            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);//Definir botões padrões
        }

        public frmMessageBox(string text, string caption)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);//Definir botões padrões
        }

        public frmMessageBox(string text, string caption, MessageBoxButtons buttons)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);//Definir [Botão padrão 1]
        }

        public frmMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);//Definir [Botão padrão 1]
            SetIcon(icon);
        }

        public frmMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, defaultButton);
            SetIcon(icon);
        }

        #endregion

        #region Métodos privados
        private void InitializeItems()
        {
            FormBorderStyle = FormBorderStyle.None;
            Padding = new Padding(borderSize);//Definir o tamanho da borda
            labelMessage.MaximumSize = new Size(550, 0);
            btnClose.DialogResult = DialogResult.Cancel;
            button1.DialogResult = DialogResult.OK;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }
        private void SetFormSize()
        {
            int widht = labelMessage.Width + pictureBoxIcon.Width + panelBody.Padding.Left;
            int height = panelTitleBar.Height + labelMessage.Height + panelButtons.Height + panelBody.Padding.Top;
            Size = new Size(widht, height);
        }
        private void SetButtons(MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            int xCenter = (panelButtons.Width - button1.Width) / 2;
            int yCenter = (panelButtons.Height - button1.Height) / 2;

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    //Botão OK
                    button1.Visible = true;
                    button1.Location = new Point(xCenter, yCenter);
                    button1.Text = "Ok";
                    button1.DialogResult = DialogResult.OK;//Definir resultado do diálogo

                    //Definir Botão Padrão
                    SetDefaultButton(defaultButton);
                    break;
                case MessageBoxButtons.OKCancel:
                    //Botão OK
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                    button1.Text = "Ok";
                    button1.DialogResult = DialogResult.OK;//Definir resultado do diálogo

                    //Botão de cancelamento
                    button2.Visible = true;
                    button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                    button2.Text = "Cancelar";
                    button2.DialogResult = DialogResult.Cancel;//Definir resultado do diálogo
                    button2.BackColor = Color.DimGray;

                    //Definir botão padrão
                    if (defaultButton != MessageBoxDefaultButton.Button3)//Há apenas 2 botões, então não há como definir o botão padrão como botão 3
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);
                    break;

                case MessageBoxButtons.RetryCancel:
                    //Botão de tentar novamente
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                    button1.Text = "Tentar novamente";
                    button1.DialogResult = DialogResult.Retry;//Definir o resultado do diálogo

                    //Botão de cancelamento
                    button2.Visible = true;
                    button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                    button2.Text = "Cancelar";
                    button2.DialogResult = DialogResult.Cancel;//Definir o resultado do diálogo
                    button2.BackColor = Color.DimGray;

                    //Definir botão padrão
                    if (defaultButton != MessageBoxDefaultButton.Button3)//Há apenas 2 botões, Então o botão padrão não pode ser o botão 3
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);
                    break;

                case MessageBoxButtons.YesNo:
                    //Botão Sim
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                    button1.Text = "Sim";
                    button1.DialogResult = DialogResult.Yes;//Definir o resultado do diálogo

                    //Botão não
                    button2.Visible = true;
                    button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                    button2.Text = "Não";
                    button2.DialogResult = DialogResult.No;//Definir o resultado do diálogo
                    button2.BackColor = Color.IndianRed;

                    //Definir botão padrão
                    if (defaultButton != MessageBoxDefaultButton.Button3)//Há apenas 2 botões, Então o botão padrão não pode ser o botão 3
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    //Botão Sim
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - button1.Width - 5, yCenter);
                    button1.Text = "Sim";
                    button1.DialogResult = DialogResult.Yes;//Definir o resultado do diálogo

                    //Botão Não
                    button2.Visible = true;
                    button2.Location = new Point(xCenter, yCenter);
                    button2.Text = "Não";
                    button2.DialogResult = DialogResult.No;//Definir o resultado do diálogo
                    button2.BackColor = Color.IndianRed;

                    //Botão de cancelamento
                    button3.Visible = true;
                    button3.Location = new Point(xCenter + button2.Width + 5, yCenter);
                    button3.Text = "Cancelar";
                    button3.DialogResult = DialogResult.Cancel;//Definir o resultado do diálogo
                    button3.BackColor = Color.DimGray;

                    //Definir botão padrão
                    SetDefaultButton(defaultButton);
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    //Botão de abortar
                    button1.Visible = true;
                    button1.Location = new Point(xCenter - button1.Width - 5, yCenter);
                    button1.Text = "Abortar";
                    button1.DialogResult = DialogResult.Abort;//Definir o resultado do diálogo
                    button1.BackColor = Color.Goldenrod;

                    //Botão de tentar novamente
                    button2.Visible = true;
                    button2.Location = new Point(xCenter, yCenter);
                    button2.Text = "Tentar novamente";
                    button2.DialogResult = DialogResult.Retry;//Definir o resultado do diálogo                    

                    //Botão Ignorar
                    button3.Visible = true;
                    button3.Location = new Point(xCenter + button2.Width + 5, yCenter);
                    button3.Text = "Ignorar";
                    button3.DialogResult = DialogResult.Ignore;//Definir o resultado do diálogo
                    button3.BackColor = Color.IndianRed;

                    //Definir botão padrão
                    SetDefaultButton(defaultButton);
                    break;
            }
        }
        private void SetDefaultButton(MessageBoxDefaultButton defaultButton)
        {
            switch (defaultButton)
            {
                case MessageBoxDefaultButton.Button1://Focar botão 1
                    button1.Select();
                    button1.ForeColor = Color.White;
                    button1.Font = new Font(button1.Font, FontStyle.Underline);
                    break;
                case MessageBoxDefaultButton.Button2://Focar botão 2
                    button2.Select();
                    button2.ForeColor = Color.White;
                    button2.Font = new Font(button2.Font, FontStyle.Underline);
                    break;
                case MessageBoxDefaultButton.Button3://Focar botão 3
                    button3.Select();
                    button3.ForeColor = Color.White;
                    button3.Font = new Font(button3.Font, FontStyle.Underline);
                    break;
            }
        }
        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Error: //Erro
                    pictureBoxIcon.Image = Properties.Resources.error;
                    PrimaryColor = Color.FromArgb(224, 79, 95);
                    btnClose.FlatAppearance.MouseOverBackColor = Color.Crimson;
                    break;
                case MessageBoxIcon.Information: //Informação
                    pictureBoxIcon.Image = Properties.Resources.information;
                    PrimaryColor = Color.FromArgb(38, 191, 166);
                    break;
                case MessageBoxIcon.Question://Questionamento
                    pictureBoxIcon.Image = Properties.Resources.question;
                    PrimaryColor = Color.FromArgb(10, 119, 232);
                    break;
                case MessageBoxIcon.Exclamation://Exclamação
                    pictureBoxIcon.Image = Properties.Resources.exclamation;
                    PrimaryColor = Color.FromArgb(255, 140, 0);
                    break;
                case MessageBoxIcon.None: //Nada
                    pictureBoxIcon.Image = Properties.Resources.chat;
                    PrimaryColor = Color.CornflowerBlue;
                    break;
            }
        }

        #endregion

        #region Métodos de eventos

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void btnMsgText_Click(object sender, EventArgs e)
        //{
        //    labelDialogResult.Text = "Dialog Box Result";
        //    //It is optional to save the Dialog Result,
        //    //use it when you need to know which button or action the user selected,
        //    //and do a specific function/task according to the dialogue result.
        //    //-| For Example:
        //    DialogResult result = RJMessageBox.Show("This is an example of a Text-Only Message Box.");
        //    if (result == DialogResult.OK)
        //        labelDialogResult.Text = "You have selected the OK BUTTON";
        //    else labelDialogResult.Text = "You have selected CANCEL";

        //    //Many times we only need to display a message box, without the need to retrieve the button or action selected by the user.
        //    //-| For Example:
        //    //RJMessageBox.Show("This is an example of a Text-Only Message Box.");
        //}

        #endregion

        #region Drag Form

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        #endregion
    }
}
