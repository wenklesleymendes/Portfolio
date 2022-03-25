using EscolaPro.Service.Dto.MatriculaAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.ProvasCertificados
{
    public interface ICertificadoProvaService
    {
        Task<DtoCertificadoProva> Inserir(DtoCertificadoProva dtoCertificadoProva);
        Task<DtoCertificadoProva> BuscarSolicitacaoAtual(int matriculaId);
        Task<IEnumerable<DtoCertificadoProva>> BuscarPorMatriculaId(int matriculaId);
        Task<DtoCertificadoProva> BuscarPorId(int certificadoProvaId);
    }
}
