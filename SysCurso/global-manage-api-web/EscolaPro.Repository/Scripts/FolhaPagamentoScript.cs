using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class FolhaPagamentoScript
    {
        public static string Filtrar(string cpf, string nome, DateTime? inicioPeriodo, DateTime? fimPeriodo, int? idUnidade)
        {
            string sqlQueryFiltro = @"select DISTINCT fg.Id, fg.SalarioLiquido from FolhaPagamento fg
                                        inner join Funcionario as f
                                        on fg.FuncionarioId = f.Id
                                        inner join SalarioUnidade su
                                        on su.FuncionarioId = f.Id
                                        inner join Unidade u
                                        on u.Id = su.UnidadeId
                                        where fg.IsDelete = 0";

            if (inicioPeriodo.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and fg.DataCadastro >= '{inicioPeriodo.Value.Date.ToString("yyyy-MM-dd")}'";
            }

            if (fimPeriodo.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and fg.DataCadastro <= '{fimPeriodo.Value.Date.AddDays(1).ToString("yyyy-MM-dd")}'";
            }

            if (!string.IsNullOrEmpty(nome))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and f.Nome like '%{nome}%'";
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and f.CPF = '{cpf}'";
            }

            if (idUnidade.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and fg.UnidadeId = {idUnidade.Value}";
            }

            return sqlQueryFiltro;
        }
    }
}
