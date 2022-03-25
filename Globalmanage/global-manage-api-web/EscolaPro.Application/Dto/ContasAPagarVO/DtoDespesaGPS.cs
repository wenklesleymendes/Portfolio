using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoDespesaGPS
    {
        public int Id { get; set; }
        public string NomeContribuinte { get; set; }
        public string CodigoPagamento { get; set; }
        public DateTime Competencia { get; set; }
        public string Identificador { get; set; }
        public decimal? ValorINSS { get; set; }
        public decimal? ValorOutrasEntidades { get; set; }
        public decimal? AtualizacaoMonetariaJuroMora { get; set; }
        public decimal? ValorTotalRecolher { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string IdentificaçãoComprovante { get; set; }
        public string ReferenciaEmpresa { get; set; }
    }
}
