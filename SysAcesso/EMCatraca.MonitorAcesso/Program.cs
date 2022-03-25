using EMCatraca.Core;
using EMCatraca.Core.Logs;
using System;
using System.Windows.Forms;

namespace EMCatraca.MonitorAcesso
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += HandlerExcecoesNaoTratadas;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMonitorNovo());
        }

        private static void HandlerExcecoesNaoTratadas(object sender, UnhandledExceptionEventArgs e)
        {
            AuditoriaLog.EscrevaErro(nameof(Program), e);
        }
    }
}
