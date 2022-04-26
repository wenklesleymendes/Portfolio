using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IUnidadeRepository : IDomainRepository<Unidade>
    {
        Task<Unidade> BuscarPorId(int idUnidade);
        Task<byte[]> UploadFoto(byte[] file, int alunoId, string extensao);
        Task<Unidade> SelecionarFoto(int alunoId);
        Task<IEnumerable<Unidade>> BuscarTodos();
        Task<IEnumerable<Unidade>> BuscarPorTipo(TipoUnidade tipoUnidade);

    }
}
