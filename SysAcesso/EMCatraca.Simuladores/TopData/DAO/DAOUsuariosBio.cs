using EMCatraca.Simuladores.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;

namespace EMCatraca.Simuladores.DAO
{
    class DAOUsuariosBio
    {
        #region Sem digital
        public List<UsuarioSemDigital> ConsultarUsuariosSD()
        {
            try
            {
                DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
                Command.CommandText = "SELECT * FROM ListaSemDigital";
                List<UsuarioSemDigital> ListUsers = new List<UsuarioSemDigital>();
                DbDataReader reader = Command.ExecuteReader();
                int Count = 0;
                while (reader.Read())
                {
                    UsuarioSemDigital user = new UsuarioSemDigital();
                    user.CodigoSemDigital = int.Parse(reader["Codigo"].ToString());
                    user.Usuario = reader["Cartao"].ToString();
                    ListUsers.Add(user);
                    Count++;
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
        public bool ExcluirUsuarioSemDigital(string UsuarioSD)
        {
            DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
            Command.CommandText = "DELETE FROM ListaSemDigital Where Cartao = '" + UsuarioSD + "'";
            if (Command.ExecuteNonQuery() > 0)
            {
                DAOConexao.Conn.Close();
                return true;
            }
            DAOConexao.Conn.Close();
            return false;
        }
        public bool InserirUsuarioSemDigital(UsuarioSemDigital user)
        {
            DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
            Command.CommandText = "INSERT INTO ListaSemDigital (Cartao) VALUES (?)";
            OleDbParameter parametro = DAOConexao.ReturnParametro(user.Usuario);
            Command.Parameters.Add(parametro);
            if (Command.ExecuteNonQuery() > 0)
            {
                DAOConexao.Conn.Close();
                return true;
            }
            DAOConexao.Conn.Close();
            return false;
        }
        #endregion

        #region Templates
        public List<UsuarioBio> ConsultarUsuariosBio(int TipoComBio)
        {
            try
            {
                string ComBio = TipoComBio == 0 ? "UsuariosBio" : "UsuariosBioLC";
                DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
                //Command.CommandText = "SELECT * FROM " + ComBio + " ORDER BY CDbl(Cartao)";    -- V4.0.7
                Command.CommandText = "SELECT * FROM " + ComBio + " ORDER BY Cartao";
                List<UsuarioBio> ListUsers = new List<UsuarioBio>();
                DbDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    UsuarioBio user = new UsuarioBio();
                    user.CodigoUsuario = int.Parse(reader["Codigo"].ToString());
                    user.Usuario = reader["Cartao"].ToString();
                    user.TemplateA = reader["Template1"].ToString();
                    user.TemplateB = reader["Template2"].ToString();
                    ListUsers.Add(user);
                }
                reader.Close();
                DAOConexao.Conn.Close();
                return ListUsers;
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public bool ExisteUsuarioBio(string usuario, int TipoComBio)
        {
            string tabelaBio = TipoComBio == 0 ? "UsuariosBio" : "UsuariosBioLC";
            DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
            Command.CommandText = "SELECT * FROM " + tabelaBio + " WHERE Cartao = '" + Utils.RemZeroEsquerda(usuario) + "'";
            DbDataReader reader = Command.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            return false;
        }
        public bool InserirTemplateBD(UsuarioBio user, int TipoComBio)
        {
            string tabelaBio = TipoComBio == 0 ? "UsuariosBio" : "UsuariosBioLC";
            DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
            Command.CommandText = "INSERT INTO " + tabelaBio + " (Cartao, Template1, Template2) VALUES (?, ?, ?)";
            OleDbParameter PmtUsuario = DAOConexao.ReturnParametro(Utils.RemZeroEsquerda(user.Usuario));
            OleDbParameter PmtTemplate1 = DAOConexao.ReturnParametro(user.TemplateA);
            OleDbParameter PmtTemplate2 = DAOConexao.ReturnParametro(user.TemplateB);
            Command.Parameters.Add(PmtUsuario);
            Command.Parameters.Add(PmtTemplate1);
            Command.Parameters.Add(PmtTemplate2);
            if (Command.ExecuteNonQuery() > 0)
            {
                DAOConexao.Conn.Close();
                return true;
            }
            DAOConexao.Conn.Close();
            return false;
        }
        public bool ExcluirTemplateBD(string Usuario, int TipoComBio)
        {
            string tabelaBio = TipoComBio == 0 ? "UsuariosBio" : "UsuariosBioLC";
            DbCommand Command = DAOConexao.ConectarBase().CreateCommand();
            Command.CommandText = "DELETE FROM " + tabelaBio + " Where Cartao = '" + Usuario + "'";
            if (Command.ExecuteNonQuery() > 0)
            {
                DAOConexao.Conn.Close();
                return true;
            }
            DAOConexao.Conn.Close();
            return false;
        }
        #endregion
    }
}
