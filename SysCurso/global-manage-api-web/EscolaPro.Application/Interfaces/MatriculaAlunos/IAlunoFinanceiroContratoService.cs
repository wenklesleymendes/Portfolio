using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Service.Dto.EmailVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.MatriculaAlunos
{
    public interface IAlunoFinanceiroContratoService
    {
        Task<List<DtoPagamento>> GerarPagamentos(DtoAlunoFinanceiroGrid dtoAlunoFinanceiro, int usuarioLogadoId = 0);
        Task<DtoAlunoFinanceiroGrid> ConsultarPainelFinanceiro(int matriculaId);
        Task<DtoAlunoFinanceiroGrid> ContratarPlano(DtoContratarPlano dtoContratarPlano);
        Task<bool> GerarBoletoEnviarPorEmail(int alunoId, List<int> pagamentoIds, TipoAcaoBoletoEnum tipoAcaoBoleto);
        Task<List<string>> EnviarBoletoPorEmailOuRecalcular(DtoGerarBoletoRequest dtoGerarBoleto);
        Task<IEnumerable<DtoEmailEnviado>> ConsultarEmail(int pagamentoId);
        Task<List<DtoPagamento>> GerarPagamentoResidual(DtoGerarBoletoRequest dtoGerarBoletoRequest);
        Task<DtoPagamentoCreditoResponse> EfetuarPagamentoAPIAdquirente(DtoPagamentoCartaoCredito dtoPagamentoCartaoCredito);
        Task<DtoDadosCartao> BuscarDetalhePagamento(int pagamentoId);
        Task<DtoPagamento> BuscarPorId(int pagamentoId);
        Task<string> ConsultarComprovante(int pagamentoId);
        Task TicketEnviar(int matriculaId, int UsuarioLogadoId, int[] pagamentoIds);
        Task<DtoPagamento> GerarMultaCancelamento(DtoPagamento pagamento, bool isento);
        Task<DtoPagamento> IsentarPagemento(DtoPagamento pagamento);
    }
}
