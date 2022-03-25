using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class CancelamentoMatricula : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int MatriculaAlunoId { get; set; }
        public int AlunoId { get; set; }
        public DateTime DataCancelamento { get; set; }
        public MotivoCancelamento MotivoCancelamento { get; set; }
        public StatusCancelamento StatusCancelamento { get; set; }
        public string Comentario { get; set; }
        public decimal ValorMultaCancelamento { get; set; }
        public decimal ValorEmAtraso { get; set; }
        public bool IsentarCancelamento { get; set; }
        public MotivoIsencao? MotivoIsencao { get; set; }
        public int UsuarioIsencaoId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int AnexoAtestadoMedicoId { get; set; }
        public int AnexoCartaCancelamentoId { get; set; }
        public bool PagoTotal { get; set; }
        public bool DentroPrazoCancelamento { get; set; }
        public MatriculaAluno MatriculaAluno { get; set; }
        public IList<CancelamentoIsencao> CancelamentoIsencaos { get; set; }

    }
}
