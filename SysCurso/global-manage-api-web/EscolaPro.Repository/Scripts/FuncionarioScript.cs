using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public static class FuncionarioScript
    {
        public static string Filtrar(int materiaId)
        {
            string sqlQueryFiltro = @"select DISTINCT funcionario.Id from Funcionario funcionario
                                        inner join CursoProfessor cursoProfessor
                                        on funcionario.Id = cursoProfessor.FuncionarioId
                                        inner join MateriaCursoProfessor materiaCursoProfessor
                                        on  cursoProfessor.Id = materiaCursoProfessor.CursoProfessorId
                                        where materiaCursoProfessor.IdMateria = " + materiaId.ToString();

            return sqlQueryFiltro;
        }

        public static string FiltrarFuncionario(int? idUnidade, string nome, bool? ativo, string cpf, DateTime? dataInicioTerminoContrato, DateTime? dataFimTerminoContrato)
        {
            string sqlQueryFiltro = @"select funcionario.Id from Funcionario funcionario
                                        inner join DadosContratacao dadosContratacao
                                        on funcionario.DadosContratacaoId = dadosContratacao.Id
                                        inner join SalarioUnidade salarioUnidade
                                        on funcionario.Id = salarioUnidade.FuncionarioId
                                        where funcionario.IsDelete = 0 ";

            if (ativo.HasValue)
            {
                if (ativo.Value)
                {
                    sqlQueryFiltro = $"{sqlQueryFiltro} and funcionario.IsActive = '{ativo.Value}'";
                }
            }

            if (!string.IsNullOrEmpty(nome))
            {
                sqlQueryFiltro = $"{sqlQueryFiltro} and funcionario.Nome like '%{nome}%'";
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                sqlQueryFiltro = $"{sqlQueryFiltro} and funcionario.CPF like '%{cpf}%'";
            }

            if (dataInicioTerminoContrato.HasValue)
            {
                sqlQueryFiltro = $"{sqlQueryFiltro} and dadosContratacao.DataAtestadoDemissao >= '{dataInicioTerminoContrato.Value.AddDays(-1).ToString("yyyy-MM-dd")}'" +
                    $" and dadosContratacao.TipoRegimeContratacao != 1 and dadosContratacao.TipoRegimeContratacao != 4 and dadosContratacao.TipoRegimeContratacao != 6 ";
            }

            if (dataFimTerminoContrato.HasValue)
            {
                sqlQueryFiltro = $"{sqlQueryFiltro} and dadosContratacao.DataAtestadoDemissao <= '{dataFimTerminoContrato.Value.AddDays(1).ToString("yyyy-MM-dd")}'" +
                    $" and dadosContratacao.TipoRegimeContratacao != 1 and dadosContratacao.TipoRegimeContratacao != 4 and dadosContratacao.TipoRegimeContratacao != 6 ";
            }

            if (idUnidade.HasValue)
            {
                sqlQueryFiltro = $"{sqlQueryFiltro} and salarioUnidade.UnidadeId = '{idUnidade.Value}'";
            }

            return sqlQueryFiltro;
        }
    }
}
