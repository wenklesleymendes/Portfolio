using EM.Infra.EMConexao;
using  Acesso.Dominio;
using Acesso.Interfaces.Excecoes;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositorioSerieTurma : IRepositorioSerieTurma
    {
        public List<SerieTurma> ConsulteTodasSeriesTurmas()
        {
            var sql = $@"SELECT s.SERICODIGO, s.SERIDESCRICAO, t.TURMCODIGO, t.TURMDESCRICAO FROM TBSERIE AS s
                            INNER JOIN TBTURMASSERIE AS ts ON s.SERICODIGO = ts.TUSESERICODIGO
                            INNER JOIN TBTURMA t ON ts.TUSETURMCODIGO = t.TURMCODIGO
                            WHERE s.SERIATIVA = 'S'
                            AND ts.TUSEATIVA = 'S'
                            ORDER BY s.SERIPRIORIDADE, t.TURMDESCRICAO";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                if (dr.HasRows)
                {
                    var seriesTurmas = new List<SerieTurma>();
                    while (dr.Read())
                    {
                        var serieTurma = new SerieTurma
                        {
                            Serie = new Serie(),
                            Turma = new Turma()
                        };
                        serieTurma.Serie.Codigo = dr.GetInteger("SERICODIGO");
                        serieTurma.Serie.Descricao = dr.GetString("SERIDESCRICAO");
                        serieTurma.Turma.Codigo = dr.GetInteger("TURMCODIGO");
                        serieTurma.Turma.Descricao = dr.GetString("TURMDESCRICAO");

                        seriesTurmas.Add(serieTurma);

                    }
                    return seriesTurmas;
                }
            }
            throw new AcessoNegadoException("Séries não encontradas!");
        }
    }
}
