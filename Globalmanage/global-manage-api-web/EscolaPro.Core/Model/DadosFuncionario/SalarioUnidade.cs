using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.DadosFuncionario
{
    public class SalarioUnidade : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string DescricaoCargo { get; set; }
        public int FuncionarioId { get; set; }
        public int UnidadeId { get; set; }
        public decimal ValorSalario { get; set; }
    }
}
