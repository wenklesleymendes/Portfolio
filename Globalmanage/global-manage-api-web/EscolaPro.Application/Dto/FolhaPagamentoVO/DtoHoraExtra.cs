using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FolhaPagamentoVO
{
    public class DtoHoraExtra
    {
        public int Id { get; set; }
        public float Porcentagem { get; set; }
        public string QuantidadeHoras { get; set; }
        public decimal Valor { get; set; }
        public int FolhaPagamentoId { get; set; }
    }
}
