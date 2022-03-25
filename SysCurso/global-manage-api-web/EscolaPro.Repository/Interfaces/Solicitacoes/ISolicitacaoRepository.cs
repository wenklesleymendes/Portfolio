using EscolaPro.Core.Model.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Solicitacoes
{
    public interface ISolicitacaoRepository : IDomainRepository<Solicitacao>
    {
        Task<IEnumerable<Solicitacao>> BuscarPorCursoId(int cursoId);
        Task InserirCursoSolicitacao(List<SolicitacaoCurso> solicitacaoCurso, int solicitacaoId);
        Task<Solicitacao> BuscarPorId(int solicitacaoId);
        Task InserirSolicitacaoFuncionarioTicket(List<SolicitacaoFuncionarioTicket> solicitacaoFuncionarioTickets, int solicitacaoId);
        Task<IEnumerable<Solicitacao>> BuscarTodos();
        Task InserirEmails(List<EmailDestinatario> emailDestinatarios, int solicitacaoId);
        Task ApagarEmailsAntigo(int solicitacaoId);
        Task InserirCertificados(List<StatusCertificado> statusCertificados, int solicitacaoId);
        Task InserirProvasSolicitacao(List<StatusProva> statusProvas, int solicitacaoId);
        Task<byte[]> UploadFoto(byte[] file, int solicitacaoId, string extensao);
        Task<Solicitacao> SelecionarFoto(int solicitacaoId);
    }
}
