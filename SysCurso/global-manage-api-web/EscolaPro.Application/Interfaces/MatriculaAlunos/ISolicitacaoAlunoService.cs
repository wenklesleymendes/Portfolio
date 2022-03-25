using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.MatriculaAlunos
{
    public interface ISolicitacaoAlunoService
    {
        Task<DtoSolicitacaoAluno> EfetuarSolicitacao(DtoSolicitacaoEfetuar dtoSolicitacaoEfetuar);
        Task<IEnumerable<DtoSolicitacaoAluno>> BuscarHistorico(int matriculaId);
        Task<DtoSolicitacaoAluno> Inserir(int solicitacaoId, int matriculaId, StatusPagamentoEnum statusPagamento, int UsuarioLogadoId);
        Task<DtoSolicitacaoAluno> BuscarPorId(int solicitacaoAlunoId);
        Task<bool> ConsultarPendencia(int matriculaId);
        Task<byte[]> GerarReportByte(int solicitacaoId, int usuarioLogadoId, int matriculaId);
        Task<DtoSolicitacaoAluno> AtualizarPagamentoSolicitacao(int SolicitacaoId, StatusPagamentoEnum statusPagamento);
        Task<Pagamento> GerarBoletos(DtoPagamento dtoPagamento, DtoMatriculaAlunoResponse matriculaAluno, string nossoNumero);
    }
}
