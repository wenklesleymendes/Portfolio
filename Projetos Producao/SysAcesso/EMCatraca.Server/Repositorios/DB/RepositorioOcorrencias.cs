using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositorioOcorrencias : IRepositorioOcorrencias
    {
        private readonly IDBHelper _dbhelper;

        public RepositorioOcorrencias(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public bool ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias)
        {
            if (string.IsNullOrEmpty(ocorrencias))
            {
                return false;
            }

            var sql = $@"SELECT 1
                            FROM TBOCORPROFPROFISSIONAIS
                            WHERE OCORDATA = {DateTime.Now.DataSql()}
                            AND OCORTIPO in ({ocorrencias})
                            AND(OCORPROFCODIGO = {idPessoa} OR OCORPROFISSCODIGO = {idPessoa})";

            if (((TipoPessoa)tipoPessoa) == TipoPessoa.Aluno)
            {
                sql = $@"SELECT 1
                    FROM TBOCORRALUNO
                    WHERE OCALDATAFIMOCORR >= {DateTime.Now.DataSql()}
                    AND OCALTIPOOCORR in ({ocorrencias})
                    AND OCALMATALUNO = {idPessoa}";
            }

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            return dr.Read();

        }

        public IEnumerable<Ocorrencia> ConsulteTodosOcorrencias()
        {
            var sql = $@"SELECT TPOCCODIGO, TPOCDESCRICAO FROM TBTIPOOCORRENCIA WHERE TPOCATIVO='S';";

            using DbConnection cn = _dbhelper.CrieConexao();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            using var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                var retorno = new List<Ocorrencia>();
                while (dr.Read())
                {
                    var ocorrencia = new Ocorrencia
                    {
                        Id = dr.GetInteger("TPOCCODIGO"),
                        Descricao = dr.GetString("TPOCDESCRICAO")
                    };
                    retorno.Add(ocorrencia);
                }
                return retorno;
            }

            throw new AcessoNegadoException("Ocorrências não encontradas!");
        }
    }
}
