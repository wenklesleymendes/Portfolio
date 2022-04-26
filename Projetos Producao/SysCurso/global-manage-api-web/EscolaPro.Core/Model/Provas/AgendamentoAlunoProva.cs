using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Provas
{
    public class AgendamentoAlunoProva : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public Aluno Aluno { get; set; }
        public AgendaProva AgendaProva { get; set; }
    }
}
