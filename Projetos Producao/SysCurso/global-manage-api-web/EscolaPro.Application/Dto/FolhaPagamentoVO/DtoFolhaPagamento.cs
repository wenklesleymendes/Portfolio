using EscolaPro.Core.Model;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.Funcionario;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Service.Dto.FuncionarioVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FolhaPagamentoVO
{
    public class DtoFolhaPagamento
    {
        public int Id { get; set; }
        public DateTime? DataCadastro => DateTime.Now;
        public DateTime? DataPagamento { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public decimal? SalarioBruto { get; set; }
        public decimal? SalarioLiquido { get; set; }
        public decimal? Alimentacao { get; set; }
        public decimal? Transporte { get; set; }
        //public decimal? ValorPorDiaAula { get; set; }
        public int? QuantidadeDias { get; set; }
        public decimal? ComissaoPrimeiraParcelaPaga { get; set; }
        public decimal? BonusMetaMes { get; set; }
        public decimal? BonusMetaPeriodo { get; set; }
        public decimal? ValorAdicional { get; set; }
        public decimal? MonitoriaProva { get; set; }
        public string JustificativaValorAdicional { get; set; }
        public decimal? ValorDiasDSR { get; set; }
        public string JustificativaDSR { get; set; }
        public decimal? ValorFerias { get; set; }
        public string JustificativaFerias { get; set; }
        public decimal? ValorDecimoTerceiro { get; set; }
        public string JustificativaDecimoTerceiro { get; set; }
        public decimal? ValorTotalDesconto { get; set; }
        public string JustificativaDesconto { get; set; }
        public IEnumerable<DtoHoraExtra> HoraExtra { get; set; }
        public DtoFuncionario Funcionario { get; set; }
        public IEnumerable<DtoAnexo> ReciboComprovanteBancario { get; set; }
        public int? FuncionarioId { get; set; }
        public int? UnidadeId { get; set; }
        public string NomeUsuario { get; set; }
        public string BancoPagamento { get; set; }
        public DateTime? Competencia { get; set; }
        public DateTime? InicioHoraExtraPaga { get; set; }
        public DateTime? TerminoHoraExtraPaga { get; set; }

        public decimal ValorTotalPagamento
        {
            get
            {
                int quantidadeDias = 0;

                if (QuantidadeDias.HasValue)
                    quantidadeDias = QuantidadeDias.Value;

                decimal salarioLiquido = 0;
                if (SalarioLiquido.HasValue)
                    salarioLiquido = SalarioLiquido.Value;

                decimal alimentacao = 0;
                if (Alimentacao.HasValue)
                    alimentacao = Alimentacao.Value;

                decimal transporte = 0;
                if (Transporte.HasValue)
                    transporte = Transporte.Value;

                //decimal valorPorDiaAula = 0;
                //if (ValorPorDiaAula.HasValue)
                //    valorPorDiaAula = ValorPorDiaAula.Value;

                decimal comissaoPrimeiraParcelaPaga = 0;
                if (ComissaoPrimeiraParcelaPaga.HasValue)
                    comissaoPrimeiraParcelaPaga = ComissaoPrimeiraParcelaPaga.Value;

                decimal bonusMetaPeriodo = 0;
                if (BonusMetaPeriodo.HasValue)
                    bonusMetaPeriodo = BonusMetaPeriodo.Value;

                decimal valorFerias = 0;
                if (ValorFerias.HasValue)
                    valorFerias = ValorFerias.Value;

                decimal valorDecimoTerceiro = 0;
                if (ValorDecimoTerceiro.HasValue)
                    valorDecimoTerceiro = ValorDecimoTerceiro.Value;

                decimal valorDiasDSR = 0;
                if (ValorDiasDSR.HasValue)
                    valorDiasDSR = ValorDiasDSR.Value;

                decimal valorAdicional = 0;
                if (ValorAdicional.HasValue)
                    valorAdicional = ValorAdicional.Value;

                decimal monitoriaProva = 0;
                if (MonitoriaProva.HasValue)
                    monitoriaProva = MonitoriaProva.Value;

                decimal horasExtras = 0;

                if(HoraExtra != null)
                {
                    foreach (var item in HoraExtra)
                    {
                        horasExtras = horasExtras + item.Valor;
                    }
                }

                var saldoPositivo = salarioLiquido + alimentacao + transporte + comissaoPrimeiraParcelaPaga + bonusMetaPeriodo + valorFerias + valorDecimoTerceiro + valorDiasDSR + horasExtras + valorAdicional + monitoriaProva;

                return ValorTotalDesconto.HasValue ? saldoPositivo - ValorTotalDesconto.Value : saldoPositivo;
            }
        }
    }
}
