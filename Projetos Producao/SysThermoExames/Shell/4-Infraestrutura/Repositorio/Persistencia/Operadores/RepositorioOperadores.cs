using Microsoft.Data.Sqlite;
using ModelPrincipal.Entidades;
using ModelPrincipal.Enumeradores;
using MongoDB.Driver;
using Repositorio.Conexao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Persistencia.Operadores
{
    public class RepositorioOperadores : IRepositorioOperadores
    {
        public RepositorioOperadores()  
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS OperadoresCadastro
                                (
                                    Codigo int, 
                                    Nome Varchar(100),
                                    Login Varchar(30), 
                                    Senha Varchar(30), 
                                    Cpf Varchar(30),
                                    Grupo int,
                                    Ativo Boolean
                                );";

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Operador> GetAll()
        {
            List<Operador> operadores = new List<Operador>();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = "SELECT * FROM OperadoresCadastro";
            SqliteDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Operador operador = new Operador()
                {
                    Codigo = dr.GetInt32("Codigo"),
                    Nome = dr.GetString("Nome"),
                    Login = dr.GetString("Login"),
                    CPF = dr.GetString("Cpf"),
                    Grupo = (EnumOperador)dr.GetInt32("Grupo"),
                    EhAtivo = dr.GetBoolean("Ativo"),
                };

                operadores.Add(operador);
            }
            return operadores;
        }

        public IEnumerable<Operador> GetPorFiltro(Func<Operador, bool> filtro)
        {
            return GetAll().ToList().Where(filtro);
        }

        public Operador ConsulteOperador(int id)
        {
            Operador operador = new Operador();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"SELECT * FROM OperadoresCadastro WHERE Codigo = {id};";
            SqliteDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                    operador.Codigo = dr.GetInt32("Codigo");
                    operador.Login = dr.GetString("Login");
                    operador.Grupo = (EnumOperador)dr.GetInt32("Grupo");
            }
            return operador;
        }

        public void Remova(int id)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"UPDATE OperadoresCadastro 
                                  SET   Ativo = $Ativo
                                 WHERE Codigo = $Codigo;";

            cmd.Parameters.AddWithValue("$Codigo", id);
            cmd.Parameters.AddWithValue("$Ativo", false);
            cmd.ExecuteNonQuery();
        }
        public bool PossuiAdministrador()
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"SELECT FIRST 1 FROM OperadoresCadastro WHERE EhAdministrador = 1;";
            SqliteDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return true;
            }
            return false;
        }

        public Operador ConsulteLogin(string login, string senha)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"SELECT * FROM OperadoresCadastro WHERE Login = '{login}' AND Senha = '{senha}'";
                SqliteDataReader dr = cmd.ExecuteReader();
            Operador operador = null;
            if (dr.Read())
            {
                operador = new Operador();
                operador.Codigo = dr.GetInt32("Codigo");
                operador.Login = dr.GetString("Login");
                operador.Grupo = (EnumOperador)dr.GetInt32("Grupo");
            }
            return operador;
        }
        public void CrieOuAtualize(Operador operador)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            if(operador.Codigo>0)
            {
                Atualize(operador);
                return;
            }
            operador.Codigo = GereCodigo();

            cmd.CommandText = $@"INSERT INTO OperadoresCadastro (Codigo, Nome, Login, Senha, Cpf, Grupo, Ativo)
                                 VALUES ($Codigo, $Nome, $Login, $Senha, $Cpf, $Grupo, $Ativo);";

            cmd.Parameters.AddWithValue("$Codigo", operador.Codigo);
            cmd.Parameters.AddWithValue("$Nome", operador.Nome);
            cmd.Parameters.AddWithValue("$Login", operador.Login);
            cmd.Parameters.AddWithValue("$Senha", operador.Senha);
            cmd.Parameters.AddWithValue("$Cpf", operador.CPF);
            cmd.Parameters.AddWithValue("$Grupo", operador.Grupo.GetHashCode());
            cmd.Parameters.AddWithValue("$Ativo", operador.EhAtivo);
            cmd.ExecuteNonQuery();
        }

        public void Atualize(Operador operador)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"UPDATE OperadoresCadastro 
                                    SET Nome = $Nome,  Login = $Login,Cpf = $Cpf ,Senha = $Senha, Grupo = $Grupo
                                 WHERE Codigo = $Codigo;";

            cmd.Parameters.AddWithValue("$Codigo", operador.Codigo);
            cmd.Parameters.AddWithValue("$Nome", operador.Nome);
            cmd.Parameters.AddWithValue("$Login", operador.Login);
            cmd.Parameters.AddWithValue("$Senha", operador.Senha);
            cmd.Parameters.AddWithValue("$Cpf", operador.CPF);
            cmd.Parameters.AddWithValue("$Grupo", operador.Grupo.GetHashCode());
            cmd.ExecuteNonQuery();
        }
        public int GereCodigo()
        {
            var todosOperadores = GetAll();
            var numeroOperadores = todosOperadores.Count();
            var codigosOperadores = GetAll().Select(o => o.Codigo).ToArray();

            if (numeroOperadores == 0)
            {
                return 1;
            }

            if (codigosOperadores.Length == (int)numeroOperadores &&
                codigosOperadores.Max() == codigosOperadores.Length)
            {
                return codigosOperadores.Max() == 0 ? 1 : codigosOperadores.Max() + 1;
            }
            
            int[] codigos = new int[codigosOperadores.Length+1];
            int f = 0;
            codigos[0] = 0;
            foreach(int i in codigosOperadores)
            {
                f++;
                codigos[f] = i;
            }
            Array.Sort(codigos);
            List<int> codigosfaltantes = new List<int>();

            for (int i = 0; i < numeroOperadores; i++)
            {
                for (int j = 0; j < codigos.Length - 1; j++)
                {
                    if (codigos[j] + 1 == codigos[j + 1])
                    {
                        continue;
                    }
                    else
                    {
                        if (!codigosfaltantes.Contains(codigos[j] + 1))
                        {
                            codigosfaltantes.Add(codigos[j] + 1);
                        }
                    }
                }

            }

            return codigosfaltantes.Min();
        }   
    }
}
