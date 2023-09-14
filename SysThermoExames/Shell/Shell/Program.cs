using Autofac;
using Formularios.IoC;
using Formularios.Telas.Principal;
using Formularios.Telas.Login;
using MaterialSkin.Controls;
using Repositorio.Conexao;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Shell
{
    internal static class Program
    {
        private static IContainer Container { get; set; }

        public static MaterialForm MaterialForm { get; private set; }

        public static Form Form { get; private set; }

        private static bool StatusDB { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var builder = new ContainerBuilder();
            IocVisao.Registre(builder);
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                Form = scope.Resolve<frmLoginOperador>();
                Application.Run(Form);
            }
        }

        private static void InicieMongoDB()
        {
            try
            {
                Process.Start(new ProcessStartInfo("mongod.exe") { UseShellExecute = false});
                new StatusDeConexao().VerifiqueConexao();
                StatusDB = true;
            }
            catch(Exception ex)
            {    
                MessageBox.Show($"{ex.Message}: Banco de Dados mongod.exe");
                StatusDB = false; 
            }
        }
    }
}