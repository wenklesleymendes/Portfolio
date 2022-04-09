using Formularios.Telas.Principal;
using Formularios.Telas._3_Componentes;
using Formularios.Telas.Login;
using MaterialSkin.Controls;
using ModelPrincipal.Utilitarios;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Formularios
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}