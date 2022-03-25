using EscolaPro.Core.Model.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO
{
    public class DtoGerarBoletoRequest
    {
        public List<int> PagamentoIds { get; set; }
        public int AlunoId { get; set; }
        public TipoAcaoBoletoEnum TipoAcao { get; set; }
        public bool PDF { get; set; }
    }
}
