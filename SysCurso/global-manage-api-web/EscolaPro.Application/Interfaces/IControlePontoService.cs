using EscolaPro.Service.Dto.ControlePontoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IControlePontoService
    {
        Task<DtoControlePonteGrid> BuscarPorCPF(string cpf, DateTime dataInicio, DateTime dataFim, bool consultaSaldoPonto = false);
        Task<DtoControlePontoHorario> Atualizar(DtoControlePontoHorario dtoControlePonto);
        Task<bool> UploadArquivo(string path, string nomeArquivo);
        Task<DtoArquivoPonto> SalvarArquivo(string fileName, DateTime now);
        Task<IEnumerable<DtoArquivoPonto>> ListaArquivosPonto();
        Task<bool> ExcluirArquivoPonto(int idArquivoPonto);
        Task<bool> ExcluirPontoEletronico(int idPontoEletronico);
        Task<DtoSaldoBancoHoras> BuscarSaldoHorasExtras(string cpf, DateTime dataInicio, DateTime dataFim);
    }
}
