using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class StatusProva : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public StatusProvaEnum StatusProvaEnum { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
