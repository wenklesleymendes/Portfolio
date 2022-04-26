using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoUnidadeTurma
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DtoEndereco Endereco { get; set; }
        public string Sigla { get; set; }
        public string CNPJ { get; set; }
        public int UnidadeId { get; set; }
    }
}
