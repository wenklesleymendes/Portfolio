using EMCatraca.Core.Dominio;
using EMCatraca.MonitorAcesso.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EMCatraca.MonitorAcesso.ControlesDeUsuario
{
    public partial class ucCatraca : UserControl
    {
        public CatracaModelView CatracaModelView { get; private set; }

        private readonly Timer tempoParaExibirUltimoAcesso = new Timer();
        DateTime tempoInicial;

        public ucCatraca()
        {
            InitializeComponent();
            this.tempoParaExibirUltimoAcesso.Tick += new System.EventHandler(this.PausaParaExibirAcesso);
        }

        public void AjusteModelView(CatracaModelView catracaModelView)
        {
            CatracaModelView = catracaModelView;
            CatracaModelView.PropertyChanged += catracaModelView_PropertyChanged;
            lblCatraca.Text = CatracaModelView.DescricaoCatraca;
            imgFoto.Image = new Bitmap(Resources.semfoto);
            lblNome.Text = "";
            lblAcesso.Text = "";
            lblComentario.Text = "";
        }



        delegate void PropertyChangedCallback();

        private void catracaModelView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                var d = new PropertyChangedCallback(AtualizeControles);
                Invoke(d);
            }
            else
            {
                AtualizeControles();
            }
        }

        void AtualizeControles()
        {
            lblNome.Text = CatracaModelView.NomePessoa;

            if (CatracaModelView.Resultado.Equals(StatusLiberacaoCatraca.Negado) || CatracaModelView.Resultado.Equals(StatusLiberacaoCatraca.MudancaEstado))
            {
                lblAcesso.ForeColor = Color.Red;
            }
            else
            {
                lblAcesso.ForeColor = Color.Green;
            }

            lblAcesso.Text = CatracaModelView.Mensagem1Evento;
            lblComentario.Text = CatracaModelView.Mensagem2Evento;
            if (CatracaModelView.FotoEvento == null)
            {
                if (CatracaModelView.Mensagem1Evento.Equals("Off-line"))
                {
                    imgFoto.Image = new Bitmap(Resources.semfotooffline);
                }
                else
                {
                    imgFoto.Image = new Bitmap(Resources.semfoto);
                }
            }
            else
            {
                imgFoto.Image = (Bitmap)((new ImageConverter()).ConvertFrom(CatracaModelView.FotoEvento));
            }

            imgStatus.Visible = CatracaModelView.Mensagem1Evento.Equals("Off-line");

            tempoInicial = DateTime.Now;
            tempoParaExibirUltimoAcesso.Interval = 200;
            tempoParaExibirUltimoAcesso.Enabled = true;
        }

        private void PausaParaExibirAcesso(object sender, System.EventArgs e)
        {
            //CONFIGURAÇÃO - configurar o tempo em que o último registro continuará sendo exibido
            if (((TimeSpan)(DateTime.Now - tempoInicial)).Seconds >= 5)
            {
                AjusteModelView(CatracaModelView);
                tempoParaExibirUltimoAcesso.Enabled = false;
            }
        }

        public void DispareEvento(EventoCatraca evento)
        {
            CatracaModelView.DispareEvento(evento);
        }
    }
}
