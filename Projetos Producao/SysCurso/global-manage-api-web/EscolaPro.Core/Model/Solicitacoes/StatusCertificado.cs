using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class StatusCertificado : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public StatusCertificadoEnum StatusCertificadoEnum { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
