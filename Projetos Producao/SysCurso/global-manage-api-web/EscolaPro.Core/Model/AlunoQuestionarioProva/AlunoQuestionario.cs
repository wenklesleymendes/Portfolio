using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.AulasOnline;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AlunoQuestionarioProva
{
    public class AlunoQuestionario : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public Pergunta Pergunta { get; set; }
        public int PerguntaId { get; set; }
        public IEnumerable<AlunoQuestionarioReposta> AlunoQuestionarioReposta { get; set; }
    }
}
