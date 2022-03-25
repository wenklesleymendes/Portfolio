using System;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Helpers
{
    public static class FormHelper
    {
        private static readonly string[] MesesLongo = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
        private static readonly string[] MesesCurto = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };

        public static string ObtenhaNomeMes(int Mes)
        {
            return ObtenhaNomeMes(Mes, false);
        }

        public static string ObtenhaNomeMesAbreviado(int Mes)
        {
            return ObtenhaNomeMes(Mes, true);
        }

        public static string ObtenhaNomeMes(int Mes, bool Abrevie)
        {
            if (Mes < 1 || Mes > 12)
            {
                throw new ArgumentOutOfRangeException("Mes");
            }

            if (Abrevie)
            {
                return MesesCurto[Mes - 1];
            }

            return MesesLongo[Mes - 1];
        }

        public static string[] ObtenhaNomeMeses(bool Abrevie)
        {
            if (Abrevie)
            {
                return MesesCurto;
            }

            return MesesLongo;
        }

        public static void PreenchaComboMes(ComboBox ComboMes)
        {
            PreenchaComboMes(ComboMes, true);
            ComboMes.Items.Add("<Todos>");
        }

        public static void PreenchaComboMes(ComboBox ComboMes, bool IncluirTodos)
        {
            if (IncluirTodos)
            {
                ComboMes.Items.Add("<Todos>");
            }

            ComboMes.Items.AddRange(MesesLongo);
            ComboMes.SelectedIndex = 0;
        }

        public static void CursorProcessando()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void CursorNormal()
        {
            Cursor.Current = Cursors.Default;
        }

        public static void CursorProcessando(Form Form)
        {
            if (Form == null)
            {
                return;
            }

            Form.Cursor = Cursors.WaitCursor;
        }

        public static void CursorProcessando(Control Controle)
        {
            Form FormPai = ObtenhaFormPai(Controle);
            if (FormPai != null)
            {
                FormPai.Cursor = Cursors.WaitCursor;
            }
        }

        public static void CursorNormal(Control Controle)
        {
            Form FormPai = ObtenhaFormPai(Controle);
            if (FormPai != null)
            {
                FormPai.Cursor = Cursors.Default;
            }
        }

        public static void CursorNormal(Form Form)
        {
            if (Form == null)
            {
                return;
            }

            Form.Cursor = Cursors.Default;
        }

        private static Form ObtenhaFormPai(Control Controle)
        {
            if (Controle == null)
            {
                return null;
            }

            if (Controle is Form)
            {
                return (Form)Controle;
            }

            return ObtenhaFormPai(Controle.Parent);
        }
    }
}
