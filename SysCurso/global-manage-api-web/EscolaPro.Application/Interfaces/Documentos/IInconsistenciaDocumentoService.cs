using EscolaPro.Service.Dto.DocumentosAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Documentos
{
    public interface IInconsistenciaDocumentoService
    {
        Task<int[]> BuscarTodos(int matriculaId);
        Task<IEnumerable<DtoInconsistenciaDocumento>> BuscarPorMatriculaId(int matriculaId);
        Task<bool> Excluir(int inconsistenciaDocumentoId);
        Task<IEnumerable<DtoInconsistenciaDocumento>> Inserir(DtoInconsistenciaDocumentoRequest dtoInconsistencia);
    }
}
