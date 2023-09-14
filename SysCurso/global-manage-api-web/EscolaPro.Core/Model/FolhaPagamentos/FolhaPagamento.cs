using System;
using System.Collections.Generic;
using System.Text;
using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.MetasComissoes;

namespace EscolaPro.Core.Model.FolhaPagamentos
{
    public class FolhaPagamento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataPagamento { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public decimal? SalarioBruto { get; set; }
        public decimal? SalarioLiquido { get; set; }
        public decimal? Alimentacao { get; set; }
        public decimal? Transporte { get; set; }
        //public decimal? ValorPorDiaAula { get; set; }
        //public int QuantidadeDiaAulas { get; set; }
        public int? QuantidadeDias { get; set; }
        public decimal? MonitoriaProva { get; set; }
        public decimal? ComissaoPrimeiraParcelaPaga { get; set; }
        public decimal? BonusMetaMes { get; set; }
        public decimal? BonusMetaPeriodo { get; set; }
        public decimal? ValorAdicional { get; set; }
        public string JustificativaValorAdicional { get; set; }
        public decimal? ValorDiasDSR { get; set; }
        public string JustificativaDSR { get; set; }
        public decimal? ValorFerias { get; set; }
        public string JustificativaFerias { get; set; }
        public decimal? ValorDecimoTerceiro { get; set; }
        public string JustificativaDecimoTerceiro { get; set; }
        public decimal? ValorTotalDesconto { get; set; }
        public string JustificativaDesconto { get; set; }
        public decimal? ValorTotalPagamento { get; set; }
        public IEnumerable<HoraExtra> HoraExtra { get; set; }
        public DadosFuncionario.Funcionario Funcionario { get; set; }
        public IEnumerable<Anexo> ReciboComprovanteBancario { get; set; }
        public int? FuncionarioId { get; set; }
        public int UnidadeId { get; set; }
        public string NomeUsuario { get; set; }
        public string BancoPagamento { get; set; }
        public DateTime? Competencia { get; set; }
        public DateTime? InicioHoraExtraPaga { get; set; }
        public DateTime? TerminoHoraExtraPaga { get; set; }
    }
}
