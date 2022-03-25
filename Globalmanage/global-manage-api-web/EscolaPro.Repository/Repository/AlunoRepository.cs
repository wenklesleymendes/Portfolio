using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
namespace EscolaPro.Repository.Repository
{
    public class AlunoRepository : DomainRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Aluno> BuscarPorCPF(string cpf)
        {
            IQueryable<Aluno> query = await Task.FromResult(GenerateQuery((x => x.CPF == cpf), null)
                .Include(x => x.Endereco)
                .Include(x => x.Contato)
                .Include(x => x.Unidade)
                .Include(x => x.Nacionalidade)
                .Include(x => x.Naturalidade));

            return query.FirstOrDefault();
        }

        public async Task<Aluno> BuscarPorId(int idAluno)
        {
            IQueryable<Aluno> query = await Task.FromResult(GenerateQuery((x => x.Id == idAluno), null)
                .Include(x => x.Endereco)
                .Include(x => x.Contato)
                .Include(x => x.Unidade)
                .Include(x => x.Matriculas)
                .ThenInclude(y => y.Curso)
                .Include(x => x.Nacionalidade)
                .Include(x => x.Naturalidade));

            return query.AsNoTracking().FirstOrDefault();
        }

        public async Task<List<Aluno>> BuscarPorEmail(string[] emails)
        {
            IQueryable<Aluno> query = await Task.FromResult(GenerateQuery((x => emails.Contains(x.Contato.Email)), null)
                .Include(x => x.Contato));

            return query.AsNoTracking().ToList();
        }

        public Aluno BuscarPorMatriculaId(int matriculaId)
        {
            return dbContext.Set<Core.Model.PainelMatricula.MatriculaAluno>()
                .Include(x => x.Aluno)
                .Where(x => x.Id == matriculaId)
                .Select(x => x.Aluno)
                .AsNoTracking()
                .FirstOrDefault();
        }

        [Obsolete]
        public async Task<List<Aluno>> FiltrarAluno(FiltrarAluno filtrarAluno)
        {
            try
            {
                DateTime? dataInicio = null;
                DateTime? dataFim = null;

                if (!string.IsNullOrEmpty(filtrarAluno.DataInicioMatricula)) dataInicio = DateTime.Parse(filtrarAluno.DataInicioMatricula);

                if (!string.IsNullOrEmpty(filtrarAluno.DataFimMatricula)) dataFim = DateTime.Parse(filtrarAluno.DataFimMatricula);

                var alunos = dbSet.Include(x => x.Contato)
                    .Include(x => x.Matriculas)
                        .ThenInclude(y => y.Turma)
                    .Include(x => x.Matriculas)
                        .ThenInclude(y => y.Unidade)
                    .Include(x => x.Unidade)
                    .Where(x => x.IsDelete == false &&
                                (string.IsNullOrEmpty(filtrarAluno.Nome) || x.Nome.Contains(filtrarAluno.Nome)) &&
                                (string.IsNullOrEmpty(filtrarAluno.CPF) || x.CPF.Contains(filtrarAluno.CPF)) &&
                                (!filtrarAluno.DataNascimento.HasValue || x.DataNascimento.Date.Equals(filtrarAluno.DataNascimento.Value.Date)) &&
                                (!filtrarAluno.ComoConheceuEnum.HasValue || x.Contato.ComoConheceuEnum.Equals(filtrarAluno.ComoConheceuEnum)) &&
                                (string.IsNullOrEmpty(filtrarAluno.Email) || x.Contato.Email.Contains(filtrarAluno.Email)) &&
                                (string.IsNullOrEmpty(filtrarAluno.Celular) || x.Contato.Celular.Contains(filtrarAluno.Celular)) &&
                                x.Matriculas.Any(y => y.IsDelete == false &&
                                                        ((filtrarAluno.UnidadeId != null) ? (y.UnidadeId == filtrarAluno.UnidadeId.Value) : (filtrarAluno.VerTodosUnidades || y.UnidadeId == filtrarAluno.UnidadeId.Value)) &&
                                                        (string.IsNullOrEmpty(filtrarAluno.NumeroMatricula) || y.NumeroMatricula == filtrarAluno.NumeroMatricula) &&
                                                        (!dataInicio.HasValue || y.DataMatricula.Date >= dataInicio.Value.Date) &&
                                                        (!dataFim.HasValue || y.DataMatricula.Date <= dataFim.Value.Date) &&
                                                        //(filtrarAluno.DataFimMatricula.HasValue ? filtrarAluno.DataFimMatricula.Value.Date <= y.DataMatricula.Date.AddMonths(11).AddDays(1) : true) &&
                                                        (!filtrarAluno.CursoId.HasValue || y.CursoId == filtrarAluno.CursoId) &&
                                                        (string.IsNullOrEmpty(filtrarAluno.Ano) || y.Turma.Ano == filtrarAluno.Ano) &&
                                                        (!filtrarAluno.Presencial.HasValue || y.Turma.Presencial == filtrarAluno.Presencial) &&
                                                        (string.IsNullOrEmpty(filtrarAluno.Semestre.ToString()) || y.Turma.Semestre == filtrarAluno.Semestre.Value.ToString()) &&
                                                        (!filtrarAluno.Segunda.HasValue || y.Turma.Segunda == filtrarAluno.Segunda) &&
                                                        (!filtrarAluno.Terca.HasValue || y.Turma.Terca == filtrarAluno.Terca) &&
                                                        (!filtrarAluno.Quarta.HasValue || y.Turma.Quarta == filtrarAluno.Quarta) &&
                                                        (!filtrarAluno.Quinta.HasValue || y.Turma.Quinta == filtrarAluno.Quinta) &&
                                                        (!filtrarAluno.Sexta.HasValue || y.Turma.Sexta == filtrarAluno.Sexta) &&
                                                        (!filtrarAluno.Sabado.HasValue || y.Turma.Sabado == filtrarAluno.Sabado) &&
                                                        (!filtrarAluno.Domingo.HasValue || y.Turma.Domingo == filtrarAluno.Domingo) &&
                                                        (!filtrarAluno.Periodo.HasValue || y.Turma.Periodo == (PeriodoEnum)filtrarAluno.Periodo) &&
                                                        (!filtrarAluno.StatusMatricula.HasValue || y.Status == filtrarAluno.StatusMatricula) &&
                                                        (string.IsNullOrEmpty(filtrarAluno.HoraInicio) || y.Turma.HorarioInicio == filtrarAluno.HoraInicio) &&
                                                        (string.IsNullOrEmpty(filtrarAluno.HoraTermino) || y.Turma.HorarioTermino == filtrarAluno.HoraTermino) &&
                                                        (!filtrarAluno.Sala.HasValue || y.Turma.Sala == (SalaEnum)filtrarAluno.Sala.Value)
                                ))
                    .Distinct()
                    .AsNoTracking()
                    .ToList();

                return alunos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Aluno> SelecionarFoto(int alunoId)
        {
            try
            {
                IQueryable<Aluno> query = await Task.FromResult(GenerateQuery((x => x.Id == alunoId), null));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> UploadFoto(byte[] file, int alunoId, string extensao)
        {
            var aluno = await BuscarPorId(alunoId);
            aluno.Foto = file;
            aluno.Extensao = extensao;

            await UpdateAsync(aluno);
            return aluno.Foto;
        }


    }
}
