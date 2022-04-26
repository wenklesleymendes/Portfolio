using EscolaPro.Service.Dto.MatriculaAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AgendaProvaVO
{
    public class DtoUnidadeTransporteProva
    {
        public int Id { get; set; }
        public int NumeroOnibus { get; set; }
        public int TotalVagas { get; set; }
        public int? VagasRestantes { get; set; }
        public int UnidadeParticipanteProvaId { get; set; }
        public DtoUnidadeParticipanteProva UnidadeParticipanteProva { get; set; }
        public ICollection<DtoProvaAluno> provaAlunos { get; set; }
    }
}
