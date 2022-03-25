using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using ExemploOnline.Entity;

namespace EasyInnerSDK.DAO
{
    class DAOUsuarios
    {
        public DAOUsuarios()
        {
            DAOConexao.ConectarBase();
        }
        public List<Usuarios> ConsultarUsuarios(int QtdUsuariosLenght)
        {
            try
            {
                DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
                Command.CommandText = "SELECT * FROM ListaOffLine";
                List<Usuarios> ListUsers = new List<Usuarios>();
                DbDataReader reader = Command.ExecuteReader();
                int Count = 0;
                while (reader.Read())
                {
                    Usuarios user = new Usuarios();
                    user.CodigoUsuario = int.Parse(reader["Codigo"].ToString());
                    user.Usuario = reader["Cartao"].ToString();
                    user.Faixa = reader["Faixa"].ToString() == "" ? 0 : (int)reader["Faixa"];
                    ListUsers.Add(user);
                    Count++;
                    if (QtdUsuariosLenght != 0)
                    {
                        if (Count == QtdUsuariosLenght)
                        {
                            DAOConexao.Conn.Close();
                            break;
                        }
                    }
                }
                DAOConexao.Conn.Close();
                return ListUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
