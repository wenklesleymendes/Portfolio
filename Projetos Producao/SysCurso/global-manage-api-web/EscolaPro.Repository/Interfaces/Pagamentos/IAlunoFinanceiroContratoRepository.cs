using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Pagamentos
{
    public interface IAlunoFinanceiroContratoRepository : IDomainRepository<Pagamento>
    {
        Task<string> UltimoNossoNumeroGerado();
        Task<IEnumerable<Pagamento>> ParcelasAVencer(int quantidoDias, int reguaContatoRegrasId);
        Task<IEnumerable<Pagamento>> ConsultarPainelFinanceiro(int matriculaId);
        Task<Pagamento> BuscarPorId(int pagamentoId);
        Task<List<Pagamento>> BuscarPorId(List<int> pagamentoIds);
        Task<Pagamento> BuscarPorNossoNumero(string nossoNumero);
        Task<DadosCartao> InserirDetalheCartao(DadosCartao dadosCartao);
        Task AtualizarPagamento(List<Pagamento> lists, PlanoPagamento planoPagamento = null, SolicitacaoEfetuar solicitacaoEfetuar = null, bool contrato = false);
        Task InserirPagamentoSolicitacao(SolicitacaoAluno solicitacao, SolicitacaoEfetuar solicitacaoEfetuar);
        Task<Pagamento> Inserir(Pagamento pagamento);
        Task RemoverPorMatricula(int id);
    }
}
