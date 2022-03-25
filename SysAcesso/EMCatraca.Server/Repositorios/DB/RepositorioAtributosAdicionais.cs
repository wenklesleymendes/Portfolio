using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioAtributosAdicionais : IRepositorioAtributosAdicionais
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioAtributosAdicionais(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public IEnumerable<AtributosAdicionais> ConsulteTodosAtributosAdcionais()
        {
            var sql = $@"SELECT ATADID, ATADNOME FROM TBATRIBUTOADICIONAL;";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                var atributos = new List<AtributosAdicionais>();
                while (dr.Read())
                {
                    var atributo = new AtributosAdicionais
                    {
                        Id = dr.GetInteger("ATADID"),
                        Descricao = dr.GetString("ATADNOME")
                    };
                    atributos.Add(atributo);
                }
                return atributos;
            }

            throw new AcessoNegadoException("Atributos Adicionais não encontrados!");
        }
    }
}
