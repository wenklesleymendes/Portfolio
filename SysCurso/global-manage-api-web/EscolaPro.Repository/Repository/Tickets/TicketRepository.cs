using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Tickets
{
    public class TicketRepository : DomainRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<Ticket> BuscarPorId(int idTicket)
        {
            IQueryable<Ticket> query = await Task.FromResult(GenerateQuery((x => x.Id == idTicket), null)
                                        .Include(x => x.DestinatarioTicket).ThenInclude(y => y.UsuarioDestinarioTicket)
                                        .Include(x => x.AssuntoTicket));
            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<Ticket>> BuscarPorMatriculaId(int matriculaId, int assuntoTicketId)
        {
            IQueryable<Ticket> query = await Task.FromResult(GenerateQuery((x => x.MatriculaId == matriculaId &&
                                                                                (assuntoTicketId == 0 || x.AssuntoTicketId == assuntoTicketId)), null)
                                                                        .Include(x => x.DestinatarioTicket).ThenInclude(y => y.UsuarioDestinarioTicket)
                                                                        .Include(x => x.AssuntoTicket)
                                                                        .Include(x => x.Matricula).ThenInclude(x => x.Unidade)
                                                                        .Include(x => x.Matricula).ThenInclude(x => x.Aluno));
            return query.ToList();
        }

        public async Task<IEnumerable<Ticket>> BuscarPorMatriculaId(int? matriculaId)
        {
            try
            {
                IQueryable<Ticket> query = await Task.FromResult(GenerateQuery((x => x.MatriculaId == matriculaId), null)
                                            .Include(x => x.DestinatarioTicket).ThenInclude(y => y.UsuarioDestinarioTicket)
                                            .Include(x => x.AssuntoTicket)
                                            .Include(x => x.Matricula).ThenInclude(x => x.Aluno)
                                            .Include(x => x.Matricula).ThenInclude(x => x.Unidade)
                                            );
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Obsolete]
        public async Task<IEnumerable<Ticket>> Filtrar(FiltrarTicket filtrarTicket, bool encaminhados = false)
        {
            try
            {
                IQueryable<Ticket> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete &&
                               ((filtrarTicket.StatusTickets == StatusTicketEnum.Todos) ||
                               (filtrarTicket.StatusTickets == StatusTicketEnum.Devolvido && x.Status == StatusTicketEnum.Devolvido) ||
                               (filtrarTicket.StatusTickets == StatusTicketEnum.EmAtendimento && x.Status == StatusTicketEnum.EmAtendimento) ||
                               (filtrarTicket.StatusTickets == StatusTicketEnum.Finalizado && x.Status == StatusTicketEnum.Finalizado) ||
                               (filtrarTicket.StatusTickets == StatusTicketEnum.Atrasados && x.Status != StatusTicketEnum.Finalizado && x.DataAbertura.Value.AddDays(x.AssuntoTicket.TempoEmDias).Date < DateTime.Now.Date) ||
                               (filtrarTicket.StatusTickets == StatusTicketEnum.Aberto && x.Status != StatusTicketEnum.Finalizado)) &&
                               (!string.IsNullOrEmpty(filtrarTicket.NumeroProtocolo) ? x.NumeroProtocolo.Contains(filtrarTicket.NumeroProtocolo) : true) &&
                               (string.IsNullOrEmpty(filtrarTicket.NumeroMatricula) || x.Matricula.NumeroMatricula == filtrarTicket.NumeroMatricula) &&
                               (string.IsNullOrEmpty(filtrarTicket.NomeAluno) || x.Matricula.Aluno.Nome.ToUpper().Contains(filtrarTicket.NomeAluno.ToUpper())) &&
                               (filtrarTicket.PeriodoAberturaInicio.HasValue ? x.DataAbertura.Value.Date >= filtrarTicket.PeriodoAberturaInicio.Value.Date : true) &&
                               (filtrarTicket.PeriodoAberturaFim.HasValue ? x.DataAbertura.Value.Date <= filtrarTicket.PeriodoAberturaFim.Value.Date : true) &&
                               (filtrarTicket.AssuntoTicketId.HasValue ? x.AssuntoTicketId == filtrarTicket.AssuntoTicketId.Value : true) &&
                               (filtrarTicket.UnidadeId.HasValue ? x.Matricula.UnidadeId == filtrarTicket.UnidadeId : true) &&
                               (filtrarTicket.DepartamentoId.HasValue ? x.DestinatarioTicket.Any(d => d.DepartamentoId == filtrarTicket.DepartamentoId) : true) &&
                               (!string.IsNullOrEmpty(filtrarTicket.NomeResponsavel) ?
                                     (x.UsuarioLogadoId == filtrarTicket.FiltroUsuario.Id ||
                                         x.DestinatarioTicket.Any(y =>
                                             y.UsuarioDestinarioTicket.Any(a => a.FuncionarioId == filtrarTicket.FiltroUsuario.Id) ||
                                             ((
                                                      filtrarTicket.FiltroUsuario.UnidadeId == y.UnidadeId) &&
                                                     (
                                                         filtrarTicket.FiltroUsuario.DepartamentoId == y.DepartamentoId) &&
                                                         y.UsuarioDestinarioTicket.Count() == 0
                                             ))
                                    ) : true
                               )), null)
                                            .Include(x => x.Matricula)
                                            .ThenInclude(x => x.Aluno)
                                            .Include(x => x.DestinatarioTicket)
                                            .ThenInclude(y => y.UsuarioDestinarioTicket)
                                            .Select(x => new Ticket
                                            {
                                                Id = x.Id,
                                                AssuntoTicket = x.AssuntoTicket,
                                                AssuntoTicketId = x.AssuntoTicketId,
                                                DataAbertura = x.DataAbertura,
                                                DataAtendimento = x.DataAtendimento,
                                                IdFuncionarioAtendente = x.IdFuncionarioAtendente,
                                                MatriculaId = x.MatriculaId,
                                                NumeroProtocolo = x.NumeroProtocolo,
                                                UsuarioLogadoId = x.UsuarioLogadoId,
                                                Status = x.Status,
                                                IsDelete = x.IsDelete,
                                                Matricula = x.MatriculaId == null ? null : new Core.Model.PainelMatricula.MatriculaAluno
                                                {
                                                    Aluno = x.Matricula.Aluno,
                                                    AlunoId = x.Matricula.AlunoId,
                                                    CursoId = x.Matricula.CursoId,
                                                    DataMatricula = x.Matricula.DataMatricula,
                                                    Id = x.Matricula.Id,
                                                    NumeroMatricula = x.Matricula.NumeroMatricula,
                                                    PlanoPagamentoAlunoId = x.Matricula.PlanoPagamentoAlunoId,
                                                    Status = x.Matricula.Status,
                                                    Unidade = x.Matricula.Unidade
                                                },
                                                DestinatarioTicket = x.DestinatarioTicket.Select(y => new DestinatarioTicket
                                                {
                                                    DataAtendimento = y.DataAtendimento,
                                                    Departamento = y.Departamento,
                                                    DepartamentoId = y.DepartamentoId,
                                                    Mensagem = y.Mensagem,
                                                    StatusTicket = y.StatusTicket,
                                                    TicketId = y.TicketId,
                                                    UnidadeId = y.UnidadeId,
                                                    Id = y.Id,
                                                    Unidade = y.Unidade,
                                                    UsuarioLogadoId = y.UsuarioLogadoId,
                                                    UsuarioDestinarioTicket = y.UsuarioDestinarioTicket.Select(a => new UsuarioDestinarioTicket
                                                    {
                                                        DestinatarioTicketId = a.DestinatarioTicketId,
                                                        FuncionarioId = a.FuncionarioId,
                                                        Id = a.Id,
                                                        IsDelete = a.IsDelete
                                                    }).ToList()
                                                }).ToList()
                                            })
                    .Distinct()
                    .AsNoTracking());
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ticket>> TickestsUsuario(Usuario usuario, StatusTicketEnum statusTicket = StatusTicketEnum.Todos)
        {
            IQueryable<Ticket> query = await Task.FromResult(GenerateQuery(x =>
            x.DestinatarioTicket.Any(y => !x.IsDelete &&
                              ((statusTicket == StatusTicketEnum.Todos) ||
                               (statusTicket == StatusTicketEnum.Devolvido && x.Status == StatusTicketEnum.Devolvido) ||
                               (statusTicket == StatusTicketEnum.EmAtendimento && x.Status == StatusTicketEnum.EmAtendimento) ||
                               (statusTicket == StatusTicketEnum.Finalizado && x.Status == StatusTicketEnum.Finalizado) ||
                               (statusTicket == StatusTicketEnum.Atrasados && x.Status != StatusTicketEnum.Finalizado && x.DataAbertura.Value.AddDays(x.AssuntoTicket.TempoEmDias).Date < DateTime.Now.Date) ||
                               (statusTicket == StatusTicketEnum.Aberto && x.Status != StatusTicketEnum.Finalizado)) &&

                                    (x.UsuarioLogadoId == usuario.Id ||

                                         x.DestinatarioTicket.Any(y =>
                                             y.UsuarioDestinarioTicket.Any(a => a.FuncionarioId == usuario.Id) ||
                                             (
                                                 (
                                                      usuario.UnidadeId == y.UnidadeId) &&
                                                     (
                                                         usuario.DepartamentoId == y.DepartamentoId) &&
                                                         y.UsuarioDestinarioTicket.Count() == 0
                                             ))
                                    )
                               )).Include(x => x.Matricula)
                                 .ThenInclude(x => x.Aluno)
                                 .Include(x => x.Matricula)
                                 .ThenInclude(x => x.Unidade)
                                 .Include(x => x.AssuntoTicket)
                                 .Include(x => x.DestinatarioTicket)
                                 .ThenInclude(y => y.UsuarioDestinarioTicket));
            return query.ToList();
        }
    }
}