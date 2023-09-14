using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using EasyInnerSDK.Entity;

namespace EasyInnerSDK.DAO
{
    class DAOHorarios
    {
        public DAOHorarios()
        {
            DAOConexao.ConectarBase();
        }
        public List<Horarios> ConsultarHorarios()
        {
            try
            {
                DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
                Command.CommandText = "SELECT * FROM ListaHorarios";
                List<Horarios> ListHorarios = new List<Horarios>();
                DbDataReader reader = Command.ExecuteReader();
                int Count = 0;
                while (reader.Read())
                {
                    Horarios Hor = new Horarios();
                    Hor.Codigo = (int)reader["Codigo"];
                    Hor.Horario = reader["Horario"].ToString();
                    Hor.Faixa = (int)reader["Faixa"];
                    Hor.Dia = (int)reader["Dia"];
                    Hor.Hora = (int)reader["Hora"];
                    Hor.Minuto = (int)reader["Minuto"];
                    ListHorarios.Add(Hor);
                    Count++;
                    if (ListHorarios.Count != 0)
                    {
                        DAOConexao.Conn.Close();
                        break;
                    }
                }
                DAOConexao.Conn.Close();
                return ListHorarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
