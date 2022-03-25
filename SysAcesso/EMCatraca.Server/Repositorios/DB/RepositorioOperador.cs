using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioOperador : IRepositorioOperador
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioOperador(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

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

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

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

            throw new AcessoNegadoException("Operador não encontrado!");
        }

        public bool ValideOperador(int codigo, string senha)
        {
            var sql = $@"SELECT 1 FROM TBOPERADOR WHERE OPERATIVO = 'S' AND OPERCODIGO = {codigo} AND OPERSENHA = '{senha}'";

            using var cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();

            return dr.Read();

        }
    }
}
