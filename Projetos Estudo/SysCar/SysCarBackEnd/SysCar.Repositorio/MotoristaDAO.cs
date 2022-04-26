using SysCar.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SysCar.Models
{
    public class MotoristaDAO
    {
        private readonly string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private readonly IDbConnection conexao;

        public MotoristaDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<MotoristaDTO> ObtenhaMotoristas()
        {
            var Motoristas = new List<MotoristaDTO>();

            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                selectCmd.CommandText = $"select * from Motorista";
                                       

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    Motoristas.Add(new MotoristaDTO
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["Nome"])
                    });
                }

                return Motoristas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public List<MotoristaDTO> ObtenhaMotoristaPorNome(string Nome)
        {
            var Motoristas = new List<MotoristaDTO>();

            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                selectCmd.CommandText = $"select * from Motorista where  Nome = '{Nome}'";

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    Motoristas.Add(new MotoristaDTO
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["Nome"])
                    });
                }

                return Motoristas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Inserir(MotoristaDTO Motorista)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                IDbDataParameter Nome = new SqlParameter("Nome", Motorista.Nome);

                insertCmd.CommandText = "insert into Motorista (Nome, Cor, Placa) values (@Nome, @Cor, @Placa)";
                insertCmd.Parameters.Add(Nome);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Atualizar(MotoristaDTO Motorista)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Motorista set Nome = @Nome, Cor = @Cor, Placa = @Placa where Id = @Id";

                IDbDataParameter Nome = new SqlParameter("Nome", Motorista.Nome);

                updateCmd.Parameters.Add(Nome);

                IDbDataParameter Id = new SqlParameter("Id", Motorista.Id);
                updateCmd.Parameters.Add(Id);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Deletar(int id)
        {
            try
            {
                IDbCommand DeleteCmd = conexao.CreateCommand();
                DeleteCmd.CommandText = "delete from Motorista where Id = @Id";

                IDbDataParameter Id = new SqlParameter("Id", id);
                DeleteCmd.Parameters.Add(Id);

                DeleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}