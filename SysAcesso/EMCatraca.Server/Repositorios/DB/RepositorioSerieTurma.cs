using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioSerieTurma : IRepositorioSerieTurma
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioSerieTurma(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public List<SerieTurma> ConsulteTodasSeriesTurmas()
        {
            var sql = $@"SELECT s.SERICODIGO, s.SERIDESCRICAO, t.TURMCODIGO, t.TURMDESCRICAO FROM TBSERIE AS s
                            INNER JOIN TBTURMASSERIE AS ts ON s.SERICODIGO = ts.TUSESERICODIGO
                            INNER JOIN TBTURMA t ON ts.TUSETURMCODIGO = t.TURMCODIGO
                            WHERE s.SERIATIVA = 'S'
                            AND ts.TUSEATIVA = 'S'
                            ORDER BY s.SERIPRIORIDADE, t.TURMDESCRICAO";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
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

            throw new AcessoNegadoException("Séries não encontradas!");
        }
    }
}
