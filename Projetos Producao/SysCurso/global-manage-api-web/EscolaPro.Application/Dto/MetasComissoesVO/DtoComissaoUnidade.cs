using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MetasComissoesVO
{
    public class DtoComissaoUnidade
    {
        public int Id { get; set; }
        public int UnidadeId { get; set; }
        public int ComissaoId { get; set; }
        public string NomeUnidade { get; set; }
    }
}
