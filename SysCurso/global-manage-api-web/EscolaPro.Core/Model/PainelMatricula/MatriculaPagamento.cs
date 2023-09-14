using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class MatriculaPagamento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public bool EmailEnviado { get; set; }
        public decimal DescontoBolsaConvenio { get; set; }
        public decimal Valor { get; set; }
    }
}
