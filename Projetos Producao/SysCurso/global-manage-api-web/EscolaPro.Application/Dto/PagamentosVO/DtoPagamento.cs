using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoPagamento
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public string Descricao { get; set; }
        public string NossoNumero { get; set; }
        public string NumeroRegistro { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string CodigoBarras { get; set; }
        public string NumeroLinhaDigitavel { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorVencimento
        {
            get
            {
                decimal valorDesconto = 0;

                if (Desconto.HasValue)
                {
                    var valorX = (Math.Round((Valor * Desconto.Value) / 100, 2, MidpointRounding.ToEven));

                    valorDesconto = Valor - valorX;
                }
                return valorDesconto;
            }
        }
        public decimal? Desconto { get; set; }
        public decimal? Acrescimo { get; set; }
        public decimal? PromocaoBolsaConvenio { get; set; }
        public decimal? DescontoPontualidade { get; set; }
        public TipoSituacaoEnum TipoSituacao { get; set; }
        //{
        //    get
        //    {
        //        if (DataVencimento < DateTime.Now)
        //        {
        //            return TipoSituacaoEnum.Inadimplente;
        //        }

        //        return TipoSituacao;
        //    }
        //    set { }
        //}
        //public ICollection<EmailEnviado> EmailEnviado { get; set; }
        public int MatriculaId { get; set; }
        public string BoletoHTML { get; set; }
        public int? PagamentoIdOld { get; set; }
        public List<DtoPagamento> Pagamento { get; set; }
        public bool ExisteEmail { get; set; }
        public decimal? TarifaBanco { get; set; }
        public TipoPagamentoEnum? FormaPagamento { get; set; }
        public int? SolicitacaoAlunoId { get; set; }
        public string ComprovanteCartao { get; set; }
    }
}
