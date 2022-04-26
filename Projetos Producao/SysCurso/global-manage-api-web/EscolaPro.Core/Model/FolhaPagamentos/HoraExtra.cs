using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.FolhaPagamentos
{
    public class HoraExtra : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public float Porcentagem { get; set; }
        public string QuantidadeHoras { get; set; }
        public decimal Valor { get; set; }
        public int FolhaPagamentoId { get; set; }
    }
}
