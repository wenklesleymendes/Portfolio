using EscolaPro.Core.Model.ContasPagar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoHistoricoDespesa
    {
        public DateTime Data { get; set; }
        public string Valor { get; set; }
        public string Descricao { get; set; }
        public string Usuario { get; set; }
        public HistoricoDespesaParcelaEnum Status { get; set; }
    }
}
