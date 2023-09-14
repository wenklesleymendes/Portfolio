using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoSalarioUnidade
    {
        public int FuncionarioId { get; set; }
        public int UnidadeId { get; set; }
        public decimal ValorSalario { get; set; }
        public string NomeUnidade { get; set; }
        public string DescricaoCargo { get; set; }
    }
}
