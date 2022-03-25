using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.ReguaContato;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscolaPro.Core.Model.Pagamentos
{
    public class Pagamento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        //public string Descricao { get; set; }
        public string Descricao { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public string NossoNumero { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string CodigoBarras { get; set; }
        public string NumeroLinhaDigitavel { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPago { get; set; }
        public decimal? TarifaBanco { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Acrescimo { get; set; }
        public decimal? PromocaoBolsaConvenio { get; set; }
        public TipoSituacaoEnum TipoSituacao { get; set; }
        public ICollection<EmailEnviado> EmailEnviado { get; set; }
        public ICollection<ReguaContatoFila> ReguaContatoFila { get; set; }
        public int MatriculaId { get; set; }
        public MatriculaAluno MatriculaAluno { get; set; }
        public string BoletoHTML { get; set; }
        public int? PagamentoIdOld { get; set; }
        public string NumeroRegistro { get; set; }
        
        [NotMapped]
        public bool ExisteEmail
        {
            get
            {
                if(EmailEnviado != null)
                {
                    return EmailEnviado.Count > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public DadosCartao? DadosCartao { get; set; }
        public int? DadosCartaoId { get; set; }

        [NotMapped]
        public TipoPagamentoEnum? FormaPagamento 
        {
            get 
            {
                if (DadosCartaoId.HasValue)
                {
                    TarifaBanco = 1;

                    if (DadosCartao != null)
                    {
                        return DadosCartao.TipoPagamento;
                    }
                }

                return null;
            } 
        }

        public int? SolicitacaoAlunoId { get; set; }
        public string ComprovanteCartao { get; set; }
    }
}