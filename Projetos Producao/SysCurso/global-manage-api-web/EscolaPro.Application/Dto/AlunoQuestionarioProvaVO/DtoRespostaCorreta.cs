using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AlunoQuestionarioProvaVO
{
    public class DtoRespostaCorreta
    {
        public List<DtoPerguntaResposta> Resposta { get; set; }
        public string Porcentagem { get; set; }
    }

    public class DtoPerguntaResposta 
    {
        public int PerguntaId { get; set; }
        public List<DtoResposta> Resposta { get; set; }
    }
}
