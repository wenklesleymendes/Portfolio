using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AlunoQuestionarioProvaVO
{
    public class DtoAlunoQuestionario
    {
        public int Id { get; set; }
        public DtoPergunta Pergunta { get; set; }
        public int PerguntaId { get; set; }
        public IEnumerable<DtoAlunoQuestionarioReposta> AlunoQuestionarioReposta { get; set; }
    }
}
