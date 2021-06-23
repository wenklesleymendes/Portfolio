using SysCar.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SysCar.Repositorio
{
    public class CarroDAO
    {
        private readonly string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private readonly IDbConnection conexao;

        public CarroDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<CarroDTO> ObtenhaCarros(int? id = null)
        {
            var carros = new List<CarroDTO>();

            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                selectCmd.CommandText = Equals(id, null) 
                                         ? "select * from Carro" 
                                         : $"select * from Carro where Id = {id}";

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    carros.Add(new CarroDTO
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Marca = Convert.ToString(resultado["Marca"]),
                        Cor = Convert.ToString(resultado["Cor"]),
                        Placa = Convert.ToString(resultado["Placa"]),
                    });
                }

                return carros;
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

        public List<CarroDTO> ObtenhaCarrosMarcaEhCor(string cor, string marca)
        {
            var carros = new List<CarroDTO>();

            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                selectCmd.CommandText = $"select * from Carro where  Cor = '{cor}' and  Marca = '{marca}'";

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    carros.Add(new CarroDTO
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Marca = Convert.ToString(resultado["Marca"]),
                        Cor = Convert.ToString(resultado["Cor"]),
                        Placa = Convert.ToString(resultado["Placa"]),
                    });
                }

                return carros;
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

        public void Inserir(CarroDTO carro)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                IDbDataParameter Marca = new SqlParameter("Marca", carro.Marca);
                IDbDataParameter Cor = new SqlParameter("Cor", carro.Cor);
                IDbDataParameter Placa = new SqlParameter("Placa", carro.Placa);

                insertCmd.CommandText = "insert into Carro (Marca, Cor, Placa) values (@Marca, @Cor, @Placa)";
                insertCmd.Parameters.Add(Marca);
                insertCmd.Parameters.Add(Cor);
                insertCmd.Parameters.Add(Placa);

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

        public void Atualizar(CarroDTO carro)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Carro set Marca = @Marca, Cor = @Cor, Placa = @Placa where Id = @Id";

                IDbDataParameter Marca = new SqlParameter("Marca", carro.Marca);
                IDbDataParameter Cor = new SqlParameter("Cor", carro.Cor);
                IDbDataParameter Placa = new SqlParameter("Placa", carro.Placa);

                updateCmd.Parameters.Add(Marca);
                updateCmd.Parameters.Add(Cor);
                updateCmd.Parameters.Add(Placa);

                IDbDataParameter Id = new SqlParameter("Id", carro.Id);
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
                DeleteCmd.CommandText = "delete from Carro where Id = @Id";

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