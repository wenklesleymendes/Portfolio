using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class FinanceiroContratoAluno : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public bool StatusMatricula { get; set; }
        public string NumeroMatricula { get; set; }
        public DateTime? InicioContrato { get; set; }
        public DateTime? FimContrato { get; set; }
        public ICollection<Anexo> AnexoContrato { get; set; }
    }
}
