using MdPaciente.Dominio;
using Microsoft.Data.Sqlite;
using Repositorio.Conexao;
using Repositorio.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MdPaciente.Infraestrutura
{
    public class RepositorioPacientes : IRepositorioPacientes
    {
        public RepositorioPacientes()
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS PacienteCadastro(Codigo Int, Nome Varchar(50), NomeMeio Varchar(50), Sobrenome Varchar(50), Sexo Blob, Etnia Integer, Nascimento Varchar(50), Whatshapp Varchar(50));";
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Paciente> GetAll()
        {
            List<Paciente> pacientes = new List<Paciente>();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = "SELECT * FROM PacienteCadastro";
            SqliteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Paciente paciente = new Paciente()
                {
                    Codigo = dr.GetInt32("Codigo"),
                    Nome = dr.GetString("Nome"),
                    NomeMeio = dr.GetString("NomeMeio"),
                    Sobrenome = dr.GetString("Sobrenome"),
                    CodigoSexo = dr.GetInt32("Sexo"),
                    CodigoEtnia = dr.GetInt32("Etnia"),
                    Nascimento = dr.GetString("Nascimento"),
                    Whatshapp = dr.GetString("Whatshapp")
                };
                pacientes.Add(paciente);
            }
            return pacientes;
        }

        public IEnumerable<Paciente> GetPorFiltro(Func<Paciente, bool> filtro)
        {
            return GetAll().ToList().Where(filtro);
        }

        public Paciente ConsultePaciente(int id)
        {
            Paciente paciente = new Paciente();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"SELECT * FROM PacienteCadastro WHERE Codigo = {id};";

            SqliteDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                paciente.Codigo = dr.GetInt32("Codigo");
                paciente.Nome = dr.GetString("Nome");
                paciente.NomeMeio = dr.GetString("NomeMeio");
                paciente.Sobrenome = dr.GetString("Sobrenome");
                paciente.CodigoSexo = dr.GetInt32("Sexo");
                paciente.CodigoEtnia = dr.GetInt32("Etnia");
                paciente.Nascimento = dr.GetString("Nascimento");
                paciente.Whatshapp = dr.GetString("Whatshapp");
            }

            return paciente;
        }

        public void Remova(int id)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"DELETE FROM PacienteCadastro WHERE Codigo = {id};";
            cmd.ExecuteNonQuery();
        }

        public void Inserir(Paciente paciente)
        {
            paciente.Codigo = GereCodigo();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"INSERT INTO PacienteCadastro 
                                  (Codigo, Nome, NomeMeio, Sobrenome, Sexo, Etnia, Nascimento, Whatshapp)
                                 VALUES 
                                  ($Codigo, $Nome, $NomeMeio, $Sobrenome, $Sexo, $Etnia, $Nascimento, $Whatshapp);";

            cmd.Parameters.AddWithValue("$Codigo", paciente.Codigo);
            cmd.Parameters.AddWithValue("$Nome", paciente.Nome);            
            cmd.Parameters.AddWithValue("$NomeMeio", paciente.NomeMeio);
            cmd.Parameters.AddWithValue("$Sobrenome", paciente.Sobrenome);
            cmd.Parameters.AddWithValue("$Sexo", paciente.CodigoSexo);
            cmd.Parameters.AddWithValue("$Etnia", paciente.CodigoEtnia);
            cmd.Parameters.AddWithValue("$Nascimento", paciente.Nascimento);
            cmd.Parameters.AddWithValue("$Whatshapp", paciente.Whatshapp);
            cmd.ExecuteNonQuery();
        }

        public void Atualize(Paciente paciente)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"UPDATE PacienteCadastro 
                                 SET Nome = $Nome, 
                                     NomeMeio = $NomeMeio, 
                                     Sobrenome = $Sobrenome,
                                     Sexo = $Sexo,
                                     Etnia = $Etnia,
                                     Nascimento = $Nascimento,
                                     Whatshapp = $Whatshapp
                                 WHERE Codigo = $Codigo;";

            cmd.Parameters.AddWithValue("$Codigo", paciente.Codigo);
            cmd.Parameters.AddWithValue("$Nome", paciente.Nome);            
            cmd.Parameters.AddWithValue("$NomeMeio", paciente.NomeMeio);
            cmd.Parameters.AddWithValue("$Sobrenome", paciente.Sobrenome);
            cmd.Parameters.AddWithValue("$Sexo", paciente.CodigoSexo);
            cmd.Parameters.AddWithValue("$Etnia", paciente.CodigoEtnia);
            cmd.Parameters.AddWithValue("$Nascimento", paciente.Nascimento);
            cmd.Parameters.AddWithValue("$Whatshapp", paciente.Whatshapp);
            cmd.ExecuteNonQuery();
        }
        public int GereCodigo()
        {
            var todosPacientees = GetAll();
            var numeroPacientees = todosPacientees.Count();
            var codigosPaciente = GetAll().Select(o => o.Codigo).ToArray();

            if (numeroPacientees == 0)
            {
                var primeiroRegistro = 1;
                return primeiroRegistro;
            }

            if (codigosPaciente.Length == (int)numeroPacientees &&
                codigosPaciente.Max() == codigosPaciente.Length)
            {
                return codigosPaciente.Max() + 1;
            }

            int[] codigos = new int[codigosPaciente.Length + 1];
            int f = 0;
            codigos[0] = 0;
            foreach (int i in codigosPaciente)
            {
                f++;
                codigos[f] = i;
            }
            Array.Sort(codigos);
            List<int> codigosfaltantes = new List<int>();

            for (int i = 0; i < numeroPacientees; i++)
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

