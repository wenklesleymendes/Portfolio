using Microsoft.Data.Sqlite;
using System;
namespace Repositorio.Conexao
{
    public class DBHelper
    {
        public DBHelper() { }

        public static SqliteConnection ConexaoDb()
        {

            var teste = System.Environment.CurrentDirectory;
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

            SqliteConnection sqliteConnection = 
                new SqliteConnection($"Data Source={path}BD\\ThermoExams.db;");

            try
            {
                sqliteConnection.Open();
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
            return sqliteConnection;
        }
    }
}
