using EM.Infra.EMConexao;
using  Acesso.Dominio;
using Acesso.Interfaces.Excecoes;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositorioOperador : IRepositorioOperador
    {
        public IEnumerable<Operador> ConsulteTodosOperadorAtivos()
        {
            var sql = $@"SELECT * FROM (
 	                        SELECT DISTINCT OPERCODIGO, OPERNOME, OPERADM
                                FROM TBOPERADOR WHERE OPERCODIGO = 1
                        )
                        UNION ALL
                        SELECT * FROM (
	                        SELECT DISTINCT OPERCODIGO, OPERNOME, OPERADM
                                FROM TBOPERADOR
                                INNER JOIN TBEMPRESAAUTORIZADAOPERADOR 
                                    ON EMAOOPERCODIGO = OPERCODIGO
                                WHERE OPERATIVO = 'S' AND OPERCODIGO > 1
                                ORDER BY OPERADM DESC, OPERNOME
                        )";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                if (dr.HasRows)
                {
                    var operadores = new List<Operador>();
                    while (dr.Read())
                    {
                        var operador = new Operador
                        {
                            Codigo = dr.GetInteger("OPERCODIGO"),
                            Nome = dr.GetString("OPERNOME"),
                            EhAdministrador = dr.GetString("OPERADM") == "S"
                        };

                        operadores.Add(operador);
                    }
                    return operadores;
                }
            }
            throw new AcessoNegadoException("Operador não encontrado!");
        }

        public bool ValideOperador(int codigo, string senha)
        {
            var sql = $@"SELECT 1 FROM TBOPERADOR WHERE OPERATIVO = 'S' AND OPERCODIGO = {codigo} AND OPERSENHA = '{senha}'";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                return dr.Read();
            }
        }
    }
}
