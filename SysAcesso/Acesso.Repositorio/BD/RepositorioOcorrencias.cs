using Acesso.Dominio;
using Acesso.Interfaces.Excecoes;
using System;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositorioOcorrencias : IRepositorioOcorrencias
    {
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

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                return dr.Read();
            }
        }

        public IEnumerable<Ocorrencia> ConsulteTodosOcorrencias()
        {
            var sql = $@"SELECT TPOCCODIGO, TPOCDESCRICAO FROM TBTIPOOCORRENCIA WHERE TPOCATIVO='S';";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
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
            }
            throw new AcessoNegadoException("Ocorrências não encontradas!");
        }
    }
}
