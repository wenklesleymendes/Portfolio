using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class PerguntaService : IPerguntaService
    {
        private readonly IPerguntaRepository _perguntaRepository;
        private readonly IRespostaRepository _respostaRepository;
        private readonly IAnexoService _anexoService;
        private readonly IVideoAulaRepository _videoAulaRepository;
        private readonly IMapper _mapper;

        public PerguntaService
        (IPerguntaRepository perguntaRepository,
            IAnexoService anexoService,
            IVideoAulaRepository videoAulaRepository,
            IRespostaRepository respostaRepository,
            IMapper mapper)
        {
            _perguntaRepository = perguntaRepository;
            _anexoService = anexoService;
            _videoAulaRepository = videoAulaRepository;
            _respostaRepository = respostaRepository;
            _mapper = mapper;
        }


        public async Task<DtoPergunta> BuscarPorId(int perguntaId)
        {
            try
            {
                var pergunta = await _perguntaRepository.BuscarPorId(perguntaId);

                DtoPergunta dtoPergunta = _mapper.Map<DtoPergunta>(pergunta);

                var anexoPergunta = await _anexoService.BuscarPorFiltro(new Core.Model.Anexos.AnexoFiltrar { PerguntaId = pergunta.Id });

                if (anexoPergunta.Any())    
                {
                    dtoPergunta.AnexoId = anexoPergunta.FirstOrDefault().Id.Value;
                    dtoPergunta.Extensao = anexoPergunta.FirstOrDefault().Extensao;
                    dtoPergunta.ArquivoString = anexoPergunta.FirstOrDefault().ArquivoString;
                }

                var respostas = await _respostaRepository.BuscarPorPergunta(pergunta.Id);

                List<DtoResposta> dtoRespostas = new List<DtoResposta>();

                foreach (var item in respostas)
                {
                    DtoResposta dtoResposta = _mapper.Map<DtoResposta>(item);
                 
                    var anexoResposta = await _anexoService.BuscarPorFiltro(new Core.Model.Anexos.AnexoFiltrar { RespostaId = dtoResposta.Id });

                    if (anexoResposta.Any())
                    {
                        dtoResposta.AnexoId = anexoResposta.FirstOrDefault().Id.Value;
                        dtoResposta.Extensao = anexoResposta.FirstOrDefault().Extensao;
                        dtoResposta.ArquivoString = anexoResposta.FirstOrDefault().ArquivoString;
                    }

                    dtoRespostas.Add(_mapper.Map<DtoResposta>(dtoResposta));
                }

                dtoPergunta.Resposta = dtoRespostas;

                return dtoPergunta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoGridGeneric<DtoPergunta>> BuscarPorVideoAula(int videoAulaId)
        {
            try
            {
                var videoAula = await _videoAulaRepository.GetByIdAsync(videoAulaId);

                var perguntas = await _perguntaRepository.BuscarPorVideoAula(videoAulaId);

                List<DtoPergunta> perguntaLista = new List<DtoPergunta>();

                foreach (var item in perguntas)
                {
                    //var pergunta = await BuscarPorId(item.Id);

                    var pergunta = new DtoPergunta();

                    pergunta.Id = item.Id;
                    pergunta.IsDelete = item.IsDelete;
                    pergunta.DescricaoPergunta = item.DescricaoPergunta;
                    pergunta.VideoAulaId = item.VideoAulaId;
                    
                    var resposta = new List<DtoResposta>();
                    foreach (var item2 in item.Resposta)
                    {
                        resposta.Add(_mapper.Map<DtoResposta>(item2));
                    }
                    pergunta.Resposta = resposta;

                    perguntaLista.Add(pergunta);
                }

                DtoGridGeneric<DtoPergunta> gridGeneric = new DtoGridGeneric<DtoPergunta> 
                {
                    Titulo = videoAula.TituloAula,
                    Lista = perguntaLista
                };

                return gridGeneric;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int perguntaId)
        {
            try
            {
                var pergunta = await _perguntaRepository.GetByIdAsync(perguntaId);
                pergunta.IsDelete = true;
                var id = await _perguntaRepository.UpdateAsync(pergunta);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoPergunta> Inserir(DtoPergunta dtoPergunta)
        {
            try
            {
                if(dtoPergunta.Id == 0)
                {
                    var pergunta = await _perguntaRepository.AddAsync(_mapper.Map<Pergunta>(dtoPergunta));

                    return await BuscarPorId(pergunta.Id);
                }
                else
                {
                    var respostas = _mapper.Map<List<Resposta>>(dtoPergunta.Resposta);
                    var pergunta = _mapper.Map<Pergunta>(dtoPergunta);

                    await _perguntaRepository.Atualizar(pergunta, respostas);

                    return await BuscarPorId(dtoPergunta.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
