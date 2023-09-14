//using MaterialSkin.Controls;
//using Formularios.Telas._2_Operadores;
//using System.Reflection;
//using Autofac;
//using Shell.Modulos;
//using Repositorio.IoC;
//using Formularios.Telas._2_Configuracoes;
//using ModelPrincipal._3_Utilitarios;
//using Processos.Configuracoes;
//using System;
//using System.Windows.Forms;
//using System.IO;
//using System.Linq;
//using Formularios.Telas._3_Componentes;
//using System.Diagnostics;
//using ModelPrincipal._1_Entidades;
//using Formularios.Telas._4_Login;
//using MdPaciente._1_Visao;

//namespace Formularios.Telas._1_Principal
//{
//    public partial class frmPrincipal : MaterialForm
//    {
//        private static IContainer _container { get; set; }
//        private static Configuracao _configuracao { get; set; }
//        private static ProcessoConfiguracao _processoConfiguracao => new ProcessoConfiguracao();
//        private bool StatusDB { get; set; }
//        public Operador OperadorLogado { get; set; }

//        public frmPrincipal(Operador operador)
//        {
//            OperadorLogado = operador;

//            VerifiqueStatusMongo();
//            VerifiqueLogin();
//            InitializeComponent();
//            Temas.InicializeTemaPadrao(this);
//            Text = "ThermoExams";
//            CrieMenuDeModulos();
//        }

//        private void VerifiqueLogin()
//        {
//            if (OperadorLogado == null)
//            {
//                new frmLogin().ShowDialog();
//            }
//        }

//        private void VerifiqueStatusMongo(bool EhModulo = false)
//        {
//            string mensagem = EhModulo ?
//                "Não é possível abrir os módulos sem uma conexão ao banco de dados" :
//                "Não foi possível confirmar o status da conexão com o MongoDB. Verifique se o servidor está sendo executado e tente novamente." ;
//            try
//            {
//                new Repositorio.Conexao.StatusDeConexao().VerifiqueConexao();
//                StatusDB = true;
//            }
//            catch(Exception ex)
//            {
//                StatusDB = false;
//                MessageBox.Show(mensagem);
//            }
//        }

//        private void CrieMenuDeModulos()
//        {
//            //IEnumerable<EnumeradorDeModulos> modulos = Enum.GetValues<EnumeradorDeModulos>();
//            //foreach(var modulo in modulos)
//            //{
//            //    ToolStripMenuItem menuItem = new(modulo.);
//            //    menuItem.Name = modulo.Name;
//            //    //menuItem.Click += new EventHandler(CliqueDeMenu);

//            //    menuStrip1.Items.Add(modulo.Name);
//            //}

//            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Cadastro de Pacientes");
//            menuItem1.Name = "MdPaciente";
//            menuItem1.Click += new EventHandler(CliqueDeMenu);
//            menuStrip1.Items.Add(menuItem1);

//            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Cadastro de Exames");
//            menuItem2.Name = "CadastroExames";
//            menuItem2.Click += new EventHandler(CliqueDeMenu);
//            menuStrip1.Items.Add(menuItem2);

//            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("Configurações");
//            menuItem3.Name = "Configuracoes";
//            menuItem3.Click += new EventHandler(ConfiguracoesClick);
//            menuStrip1.Items.Add(menuItem3);

//            ToolStripMenuItem menuItem4 = new ToolStripMenuItem("Operadores");
//            menuItem4.Name = "Operadores";
//            menuItem4.Click += new EventHandler(btnOperadores_Click);
//            menuStrip1.Items.Add(menuItem4);
//        }

//        public void CliqueDeMenu(object sender, System.EventArgs e)
//        {
//            VerifiqueStatusMongo(true);
//            if (!StatusDB)
//            {
//                return;
//            }

//            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

//            _configuracao = _processoConfiguracao.ObtenhaConfiguracao().FirstOrDefault();

//            if (_configuracao is null || _configuracao.PathModulos is null)
//            {
//                MessageBox.Show("Não foi encontrado o módulo solicitado. Verifique a configuração da aplicação e tente novamente");
//                return;
//            }

//            var assembliesSelecionados =
//                  from arquivo in new DirectoryInfo(_configuracao.PathModulos).GetFiles()
//                  where arquivo.Name == (menuItem.Name + ".exe")
//                  select Assembly.LoadFrom(arquivo.FullName);


//            if (assembliesSelecionados is null || !assembliesSelecionados.Any())
//            {
//                MessageBox.Show("O módulo selecionado não foi encontrado.\n" + "Verfique se está instalado na pasta correta.");
//                return;
//            }


//            ContainerBuilder builder = new ContainerBuilder();

//            foreach (var assembly in assembliesSelecionados)
//            {
//                builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().FindConstructorsWith(new AllConstructorFinder());
//                builder.RegisterAssemblyModules(assembly);
//            }

//            _container = builder.Build();
//            var lista = _container.ComponentRegistry.Registrations.ToList();

//            var scope = _container.BeginLifetimeScope();

//            {
//                try
//                {
//                    var mod = scope.Resolve<IModulo>();
//                    mod.Show();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Não foi possível abrir pelo seguinte motivo:\n" + ex.Message);
//                }
//            }
//        }

//        private void btnOperadores_Click(object sender, EventArgs e)
//        {
//            VerifiqueStatusMongo(true);
//            if (!StatusDB)
//            {
//                return;
//            }
//            new frmSelecaoOperadores().Show();
//        }

//        private void ConfiguracoesClick(object sender, EventArgs e)
//        {
//            VerifiqueStatusMongo(true);
//            if (!StatusDB)
//            {
//                return;
//            }
//            new frmConfiguracoes().Show();
//        }
//    }
//}
