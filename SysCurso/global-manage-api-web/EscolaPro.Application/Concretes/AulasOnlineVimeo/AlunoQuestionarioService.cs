using AutoMapper;
using EscolaPro.Core.Model.AlunoQuestionarioProva;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Service.Dto.AlunoQuestionarioProvaVO;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.AulasOnlineVimeo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.AulasOnlineVimeo
{
    public class AlunoQuestionarioService : IAlunoQuestionarioService
    {
        private readonly IMapper _mapper;
        private readonly IAlunoQuestionarioRepository _alunoQuestionarioRepository;
        private readonly IPerguntaRepository _perguntaRepository;
        private readonly IRespostaService _respostaService;

        public AlunoQuestionarioService(IMapper mapper,
            IAlunoQuestionarioRepository alunoQuestionarioRepository,
            IRespostaService respostaService,
            IPerguntaRepository perguntaRepository)
        {
            _alunoQuestionarioRepository = alunoQuestionarioRepository;
            _perguntaRepository = perguntaRepository;
            _respostaService = respostaService;
            _mapper = mapper;
        }

        public async Task<DtoAlunoQuestionario> BuscarPorPerguntaId(int perguntaId)
        {
            try
            {
                var alunoQuestionario = await _alunoQuestionarioRepository.BuscarPorPerguntaId(perguntaId);

                return _mapper.Map<DtoAlunoQuestionario>(alunoQuestionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoRespostaCorreta> Inserir(List<DtoAlunoQuestionario> dtoAlunoQuestionarioLista)
        {
            try
            {
                DtoRespostaCorreta respostaCorretas = new DtoRespostaCorreta();

                respostaCorretas.Resposta = new List<DtoPerguntaResposta>();

                foreach (var questionario in dtoAlunoQuestionarioLista)
                {
                    var alunoQuestionario = await _alunoQuestionarioRepository.AddAsync(_mapper.Map<AlunoQuestionario>(questionario));

                    var resposta = await _respostaService.BuscarPorPergunta(questionario.PerguntaId);

                    respostaCorretas.Resposta.Add(new DtoPerguntaResposta
                    {
                        PerguntaId = questionario.PerguntaId,
                        Resposta = resposta.Where(x => x.Correta).ToList()
                    });
                }

                return respostaCorretas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
