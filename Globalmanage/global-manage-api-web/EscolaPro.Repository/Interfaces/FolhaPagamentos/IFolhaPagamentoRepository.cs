using EscolaPro.Core.Model.FolhaPagamentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.FolhaPagamentos
{
    public interface IFolhaPagamentoRepository : IDomainRepository<FolhaPagamento>
    {
        Task<FolhaPagamento> BuscarPorId(int idFolhaPagamento);
        Task DeletarHoraExtra(int idFolhaPagamento);
        Task AdicionarHoraExtra(List<HoraExtra> horaExtras, int idFolhaPagamento);
        Task<IEnumerable<FolhaPagamento>> BuscarPorFiltro(string cpf, string nome, DateTime? inicioPeriodo, DateTime? fimPeriodo, int? unidadeId);
        Task<bool> VerificarSeExistePagamentoPendente(int funcionarioId);
        Task<IEnumerable<FolhaPagamento>> BuscarPagamentosPendente(int funcionarioId);
    }
}
