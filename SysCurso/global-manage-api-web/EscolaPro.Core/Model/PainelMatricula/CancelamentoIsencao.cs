using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class CancelamentoIsencao: BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int CancelamentoMatriculaId { get; set; }
        public int MatriculaId { get; set; }
        public string Justificativa { get; set; }
        public MotivoIsencao MotivoIsencao { get; set; }
        public int UsarioId { get; set; }
        public IList<CancelamentoIsencaoPagamento> CancelamentoIsencaoPagamentos { get; set; }
    }
}
