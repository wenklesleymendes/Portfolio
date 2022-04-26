using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Core.Model.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoCertificadoProva
    {
        public int Id { get; set; }
        public DateTime? DataRecebimentoSuporte { get; set; }
        public DateTime? DataEntregaAluno { get; set; }
        public string GDAE { get; set; }
        public int AnexoId { get; set; }
        public int MatriculaAlunoId { get; set; }
        public StatusCertificadoEnum StatusCertificado { get; set; }
    }
}
