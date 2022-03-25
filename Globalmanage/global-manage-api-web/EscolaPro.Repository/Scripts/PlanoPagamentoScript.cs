using EscolaPro.Core.Model.CadastroAluno;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class PlanoPagamentoScript
    {
        public static string Filtrar(int formaPagamento, int? parcela, int cursoId, int unidadeId)
        {
            string sqlQueryFiltro = $"select DISTINCT planoPagamento.ID from PlanoPagamento planoPagamento " +
                                        "inner join PlanoPagamentoCurso planoPagamentoCurso " +
                                        "on planoPagamentoCurso.PlanoPagamentoId = planoPagamento.Id " +
                                        "inner join PlanoPagamentoUnidade planoPagamentoUnidade " +
                                        "on planoPagamentoUnidade.PlanoPagamentoId = planoPagamento.Id " +
                                        "where planoPagamento.IsDelete = 0 and planoPagamento.IsActive = 1 " +
                                        $"and planoPagamentoUnidade.UnidadeId = '{unidadeId}' " +
                                        $"and planoPagamentoCurso.CursoId = '{cursoId}' " +
                                        $"and planoPagamento.TipoPagamento = '{formaPagamento}' ";

            if (parcela.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $" and planoPagamento.QuantidadeParcela = '{parcela.Value}'";
            }

            return sqlQueryFiltro;
        }

        public static string Filtrar(int cursoId, int unidadeId)
        {
            string sqlQueryFiltro = $"select DISTINCT planoPagamento.ID from PlanoPagamento planoPagamento" +
                                                   "inner join PlanoPagamentoCurso planoPagamentoCurso" +
                                                   "on planoPagamentoCurso.PlanoPagamentoId = planoPagamento.Id" +
                                                   "inner join PlanoPagamentoUnidade planoPagamentoUnidade" +
                                                   "on planoPagamentoUnidade.PlanoPagamentoId = planoPagamento.Id" +
                                                   "where planoPagamento.IsDelete = 0" +
                                                   $"and planoPagamentoUnidade.UnidadeId = '{unidadeId}'" +
                                                   $"and planoPagamentoCurso.CursoId = '{cursoId}'";
            return sqlQueryFiltro;
        }
    }
}
