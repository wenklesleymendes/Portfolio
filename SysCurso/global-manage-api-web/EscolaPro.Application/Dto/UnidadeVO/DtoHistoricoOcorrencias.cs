using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoHistoricoOcorrencias
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro => DateTime.Now;
        public int? UnidadeId { get; set; }
        public bool IsDelete { get; set; }
    }
}
