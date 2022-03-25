using EMCatraca.Core.Dominio;
using EMCatraca.WindowsForms.Configuracoes.Formularios;
using System;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Utilidades 
{ 
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InicializeSessaoDoUsuario();

            Application.Run(new frmLogin());
        }

        private static void InicializeSessaoDoUsuario()
        {
            var operador = new Operador { Codigo = 1, Nome = "Admin", EhAdministrador = true };
            SessaoDoUsuario.Instancia.OperadorLogado = operador;
        }
    }
}
