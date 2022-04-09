//using MaterialSkin.Controls;
//using ModelPrincipal._3_Utilitarios;
//using Processos.Configuracoes;
//using System;
//using System.Linq;

//namespace Formularios.Telas._2_Configuracoes
//{
//    public partial class frmConfiguracoes : MaterialForm
//    {
//        private static ProcessoConfiguracao processo = new ProcessoConfiguracao();
//        private Configuracao _configuracao { get; set; }

//        public frmConfiguracoes()
//        {
//            InitializeComponent();
//            AjusteCampos();
//        }

//        private void AjusteCampos()
//        {
//            var config = new ProcessoConfiguracao()
//                .ObtenhaConfiguracao().ToList();

//            if (config.Count == 0)
//            {
//                _configuracao = new Configuracao();
//                processo.CrieConfiguracao(_configuracao);
//            }

//            _configuracao = new ProcessoConfiguracao()
//                    .ObtenhaConfiguracao().FirstOrDefault();


//            txtboxDiretorioBD.Text = _configuracao.PathBase ?? "";
//            txtboxDiretorioModulos.Text = _configuracao.PathModulos ?? "";
//        }

//        private void txtboxDiretorioModulos_Click(object sender, EventArgs e)
//        {
//            folderBrowserModulos.ShowDialog();
//            txtboxDiretorioModulos.Text = folderBrowserModulos.SelectedPath;
//            _configuracao.PathModulos = folderBrowserModulos.SelectedPath;
//            processo.AtualizePathModulos(_configuracao);
//        }

//        //private void txtboxDiretorioBD_ClickAsync(object sender, EventArgs e)
//        {
//            openFileDialog1.ShowDialog();
//            txtboxDiretorioBD.Text = openFileDialog1.FileName;
//            _configuracao.PathModulos = openFileDialog1.FileName;
//            processo.AtualizePathBD(_configuracao);
//        }
//    }
//}
