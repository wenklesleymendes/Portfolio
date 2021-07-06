using Acesso.Dominio;
using Acesso.Interfaces.Excecoes;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositorioAtributosAdicionais : IRepositorioAtributosAdicionais
    {
        public IEnumerable<AtributosAdicionais> ConsulteTodosAtributosAdcionais()
        {
            var sql = $@"SELECT ATADID, ATADNOME FROM TBATRIBUTOADICIONAL;";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
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
            }

            throw new AcessoNegadoException("Atributos Adicionais não encontrados!");
        }
    }
}
