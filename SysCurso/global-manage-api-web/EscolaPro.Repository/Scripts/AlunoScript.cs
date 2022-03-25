using EscolaPro.Core.Model.CadastroAluno;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class AlunoScript
    {
        public static string Filtrar(FiltrarAluno filtrarAluno)
        {
            string sqlQueryFiltro = @"select DISTINCT aluno.ID from Aluno aluno
                                    inner join MatriculaAluno matricula
                                    on aluno.Id = matricula.AlunoId
                                    inner join Contato contato
                                    on aluno.ContatoId = contato.Id
                                    inner join Turma turma
                                    on matricula.TurmaId = turma.Id
                                    where aluno.IsDelete = 0";


            if (filtrarAluno.UnidadeId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and aluno.UnidadeId = '{filtrarAluno.UnidadeId}'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.Nome))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and aluno.Nome like '%{filtrarAluno.Nome}%'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.CPF))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and aluno.CPF like '%{filtrarAluno.CPF}%'";
            }

            if (filtrarAluno.DataNascimento.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and aluno.DataNascimento = '{filtrarAluno.DataNascimento.Value.ToString("yyyy-MM-dd")}'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.Celular))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and aluno.Email like '%{filtrarAluno.Email}%'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.Email))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and aluno.Celular like '%{filtrarAluno.Celular}%'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.NumeroMatricula))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and matricula.NumeroMatricula like '%{filtrarAluno.NumeroMatricula}%'";
            }

            //if (filtrarAluno.DataInicioMatricula.HasValue)
            //{
            //    sqlQueryFiltro = sqlQueryFiltro + $"and matricula.DataMatricula >= '{filtrarAluno.DataInicioMatricula.Value.Date.ToString("yyyy-MM-dd")}'";
            //}

            //if (filtrarAluno.DataInicioMatricula.HasValue)
            //{
            //    sqlQueryFiltro = sqlQueryFiltro + $"and matricula.DataMatricula <= '{filtrarAluno.DataFimMatricula.Value.Date.ToString("yyyy-MM-dd")}'";
            //}

            if (filtrarAluno.CursoId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and matricula.CursoId = '{filtrarAluno.CursoId}'";
            }

            if (filtrarAluno.Presencial.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Presencial = '{filtrarAluno.Presencial.Value}'";
            }

            //if (!string.IsNullOrEmpty(filtrarAluno.Ano))
            //{
            //    sqlQueryFiltro = sqlQueryFiltro + $"and turma.Ano = '{filtrarAluno.Ano}'";
            //}

            if (!string.IsNullOrEmpty(filtrarAluno.Semestre.ToString()))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Semestre = '{filtrarAluno.Semestre}'";
            }

            if (filtrarAluno.Segunda.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Segunda = '{filtrarAluno.Segunda.Value}'";
            }

            if (filtrarAluno.Terca.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Terca = '{filtrarAluno.Terca.Value}'";
            }

            if (filtrarAluno.Quarta.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Quarta = '{filtrarAluno.Quarta.Value}'";
            }

            if (filtrarAluno.Quinta.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Quinta = '{filtrarAluno.Quinta.Value}'";
            }

            if (filtrarAluno.Sabado.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Sabado = '{filtrarAluno.Sabado.Value}'";
            }

            if (filtrarAluno.Domingo.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Domingo = '{filtrarAluno.Domingo.Value}'";
            }

            if (filtrarAluno.Periodo.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.Periodo = '{(int)filtrarAluno.Periodo.Value}'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.HoraInicio))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.HorarioInicio = '{filtrarAluno.HoraInicio}'";
            }

            if (!string.IsNullOrEmpty(filtrarAluno.HoraTermino))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and turma.HorarioTermino = '{filtrarAluno.HoraTermino}'";
            }

            //if (!string.IsNullOrEmpty(filtrarAluno.Sala))
            //{
            //    sqlQueryFiltro = sqlQueryFiltro + $"and turma.Sala = '{filtrarAluno.Sala}'";
            //}

            return sqlQueryFiltro;
        }
    }
}
