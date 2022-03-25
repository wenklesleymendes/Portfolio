using EscolaPro.Core.Model.ControlePontoEletronico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IControlePontoRepository : IDomainRepository<PontoEletronico>
    {
        Task<IEnumerable<PontoEletronico>> BuscarPorFuncionario(int idFuncionario, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<PontoEletronico>> BuscarPorFeriasId(int feriasId);
        Task<PontoEletronico> Atualizar(PontoEletronico pontoEletronico);
        Task<PontoEletronico> BuscarPontoEletronicoPorId(int idPontoEletronico);
        Task<IEnumerable<PontoEletronico>> BuscarPorNomeArquivo(string nomeArquivo);
        Task<IEnumerable<PontoEletronico>> BuscarPorFolhaPagamentoId(int folhaPagamentoId);
    }
}
