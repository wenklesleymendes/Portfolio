using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;

namespace EasyInnerSDK.DAO
{
    class DAOConexao
    {
        private static string CaminhoBD = "";
        public static DbConnection Conn = null;
        public static DbConnection ConectarBase()
        {
            try
            {
                if (Conn == null)
                {
                    if (File.Exists(CaminhoBD = Environment.SpecialFolder.ProgramFiles + "\\SDK EasyInner\\Exemplos\\BaseExemplos\\SDK_Exemplos.mdb") == false)
                    {
                        OpenFileDialog CaminhoBase = new OpenFileDialog();
                        CaminhoBase.Filter = "Arquivos mdb (*.mdb)|*.mdb";
                        DialogResult ds = CaminhoBase.ShowDialog();
                        if (ds == DialogResult.OK)
                        {
                            CaminhoBD = CaminhoBase.FileName;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    string BD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + CaminhoBD;
                    Conn = new OleDbConnection(BD);
                }
                if (Conn.State == System.Data.ConnectionState.Closed)
                {
                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Conn;
        }

        public static OleDbParameter ReturnParametro(object valor)
        {
            OleDbParameter Parametro = new OleDbParameter();
            switch (valor.GetType().FullName)
            {
                case "System.Int32":
                    Parametro.DbType = System.Data.DbType.Int32;
                    break;
                case "System.String":
                    Parametro.DbType = System.Data.DbType.String;
                    break;
                case "System.Boolean":
                    Parametro.DbType = System.Data.DbType.Boolean;
                    break;
            }
            Parametro.Value = valor;
            return Parametro;
        }
    }
}
