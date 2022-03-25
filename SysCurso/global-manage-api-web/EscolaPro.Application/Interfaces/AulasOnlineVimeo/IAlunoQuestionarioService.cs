using EscolaPro.Service.Dto.AlunoQuestionarioProvaVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.AulasOnlineVimeo
{
    public interface IAlunoQuestionarioService
    {
        Task<DtoRespostaCorreta> Inserir(List<DtoAlunoQuestionario> dtoAlunoQuestionario);
        Task<DtoAlunoQuestionario> BuscarPorPerguntaId(int perguntaId);
    }
}
