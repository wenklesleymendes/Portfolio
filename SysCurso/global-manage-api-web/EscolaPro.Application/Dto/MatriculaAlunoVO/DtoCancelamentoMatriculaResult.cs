using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoCancelamentoMatriculaResult : DtoCancelamentoMatriculaRequest
    {

        public List<DtoPagamento> ParcelasEmAtraso { get; set; }

        public override bool DentroPrazoCancelamento
        {
            get { return (MatriculaAluno == null || DataCancelamento == new DateTime()) ? false : MatriculaAluno.DataMatricula.AddDays(7).Date > DataCancelamento.Date; }
        }
        public DtoCancelamentoIsencao CancelamentoIsencao { get; set; }
        public DtoMatriculaAluno MatriculaAluno { get; set; }
        public ICollection<DtoPagamento> PagamentosIsentos { get; set; }
    }
}
