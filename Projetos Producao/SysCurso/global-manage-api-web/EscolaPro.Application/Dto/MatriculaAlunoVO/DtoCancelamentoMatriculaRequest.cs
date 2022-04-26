using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoCancelamentoMatriculaRequest
    {
        public int Id { get; set; }
        public int MatriculaAlunoId { get; set; }
        public int AlunoId { get; set; }
        public DateTime DataCancelamento { get; set; }
        public MotivoCancelamento? MotivoCancelamento { get; set; }
        public StatusCancelamento? StatusCancelamento { get; set; }
        public string Comentario { get; set; }
        public decimal ValorMultaCancelamento { get; set; }
        public decimal ValorEmAtraso { get; set; }
        public bool IsentarCancelamento { get; set; }
        public MotivoIsencao? MotivoIsencao { get; set; }
        public int UsuarioIsencaoId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int AnexoAtestadoMedicoId { get; set; }
        public bool Validar { get; set; }
        public int AnexoCartaCancelamentoId { get; set; }
        public bool PagoTotal { get; set; }
        public virtual bool DentroPrazoCancelamento { get; set; }
    }
}
