using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoUnidadeDespesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int UnidadeId { get; set; }
        public decimal? Valor { get; set; }
        public bool IsDelete { get; set; }
    }
}
