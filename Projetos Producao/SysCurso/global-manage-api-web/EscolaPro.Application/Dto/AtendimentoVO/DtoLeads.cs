using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AtendimentoVO
{
    public class DtoLeads
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string Origem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataProcessamento { get; set; }
        public int Status { get; set; }
    }
}
