using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AtendimentoVO
{
    public class DtoLeadJson
    {
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Unidade { get; set; }
        public string ComoConheceu { get; set; }
        public string NomeFluxo { get; set; }
        public int UsuarioUnidade { get; set; }
        public int CanaldeAtendimentoLeads { get; set; }
        public int ComoNosConheceu { get; set; }
        public int UnidadeCadastro { get; set; }
    }
}
