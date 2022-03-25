using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Pagamentos
{
    public class EmailEnviado : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime DataEnvio { get; set; }
        public string EmailPara { get; set; }
        public string CorpoEmail { get; set; }
        public int PagamentoId { get; set; }
    }
}
