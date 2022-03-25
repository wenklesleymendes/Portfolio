using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.MatriculaAlunos
{
    public interface ICancelamentoMatriculaService
    {
        Task<DtoCancelamentoMatriculaResult> BuscarPorMatricula(int matriculaId);
        Task<DtoCancelamentoMatriculaResult> EfetuarCancelamento(DtoCancelamentoMatriculaRequest EfetuarCancelamento);
        Task<string> ValidarCancelamento(DtoCancelamentoMatriculaRequest EfetuarCancelamento);
        Task<bool> SalvarAutorizacaoIsencao(DtoCancelamentoAutorizacaoIsencao autorizacaoIsencao);
        Task<DtoCancelamentoMatriculaResult> GerarMultaCancelamento(DtoCancelamentoMatriculaRequest dtoCancelamentoMatricula);
        Task<byte[]> GerarReportByte(int matriculaId, int usuarioLogadoId, MotivoCancelamento motivoCancelamento);
    }
}
