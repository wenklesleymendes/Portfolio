using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoAlunoFinanceiroGrid
    {
        public int MatriculaId { get; set; }
        public List<DtoPagamento> Pagamento { get; set; }
        public DtoPlanoPagamentoAluno PlanoPagamentoAluno { get; set; }
        public decimal Total { get; set; }
        public decimal Desconto { get; set; }
        public decimal Devido { get; set; }
        public bool TemPromocaoBolsaConvenio => Pagamento.Where(x => x.PromocaoBolsaConvenio.HasValue).Count() > 0 ? true : false;
        public bool TemDescontoPontualidade => Pagamento.Where(x => x.DescontoPontualidade.HasValue).Count() > 0 ? true : false;
        public bool TemTarifaBanco => Pagamento.Where(x => x.TarifaBanco.HasValue).Count() > 0 ? true : false;
        public bool Credito { get; set; }
        public bool TEF { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroControle { get; set; }
        public int QuantidadeParcela { get; set; }
        public bool ExistePendenciaFinanceira => Pagamento.Where(x => x.TipoSituacao == Core.Model.Pagamentos.TipoSituacaoEnum.Inadimplente || x.TipoSituacao == Core.Model.Pagamentos.TipoSituacaoEnum.InadimplenteBloqueado).Count() > 0 ? false : true;
        public bool ExistePendenciaContrato { get; set; }
        public string ComprovanteCartao { get; set; }
    }
}
