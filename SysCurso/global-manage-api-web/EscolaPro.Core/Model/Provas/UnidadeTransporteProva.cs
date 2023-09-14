using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Provas
{
    public class UnidadeTransporteProva : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int NumeroOnibus { get; set; }
        public int TotalVagas { get; set; }
        public int AgendaProvaId { get; set; }
        public AgendaProva AgendaProva { get; set; }
        public int UnidadeParticipanteProvaId { get; set; }
        public UnidadeParticipanteProva UnidadeParticipanteProva { get; set; }
        public ICollection<ProvaAluno> ProvaAlunos { get; set; }
        public ICollection<HistoricoProvas> HistoricoProvas { get; set; }
    }
}
