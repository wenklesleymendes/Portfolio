using AutoMapper;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class OcorrenciaService : IOcorrenciaService
    {
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public OcorrenciaService(
            IOcorrenciaRepository ocorrenciaRepository,
            IUsuarioService usuarioService,
            IMapper mapper)
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoOcorencia>> BuscarPorMatriculaId(int matriculaId)
        {
            IEnumerable<Ocorrencia> ocorrencias = await _ocorrenciaRepository.BuscarPorMatriculaId(matriculaId);
            return _mapper.Map<IEnumerable<DtoOcorencia>>(ocorrencias);
        }

        public async Task<DtoOcorencia> BuscarPorId(int idOcorrencia)
        {
            Ocorrencia ocorrencia = await _ocorrenciaRepository.GetByIdAsync(idOcorrencia);
            return _mapper.Map<DtoOcorencia>(ocorrencia);
        }
        public async Task<DtoTicketTimeline> BuscarTimeline(int idOcorrencia)
        {
            var ocorrencia = await _ocorrenciaRepository.GetByIdAsync(idOcorrencia);
            

            var usuarioResponsavel = await _usuarioService.BuscarPorId(ocorrencia.UsuarioLogadoId);

            DtoDestinatarioTicket destinatarioResponsavel = new DtoDestinatarioTicket();

            if (usuarioResponsavel.UserName != "admin" && !usuarioResponsavel.IsAluno)
            {
                destinatarioResponsavel = new DtoDestinatarioTicket
                {
                    DepartamentoId = usuarioResponsavel.Departamento.Id,
                    UnidadeId = usuarioResponsavel.Unidade.Id,
                    TicketId = ocorrencia.Id,
                };
            }

            destinatarioResponsavel.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();

            destinatarioResponsavel.UsuarioDestinarioTicket.Add(new DtoUsuarioDestinarioTicket { FuncionarioId = usuarioResponsavel.Id });

            DtoTicketTimeline ticketTimelines = new DtoTicketTimeline
            {
                TicketId = ocorrencia.Id,
                StatusTicket = StatusTicketEnum.Ocorrencia,
                Mensagens = new List<DtoMensagensTimeLine>(),
                UsuarioResponsavel = destinatarioResponsavel
            };

           
                var usuario = await _usuarioService.BuscarPorId(ocorrencia.UsuarioLogadoId);

                string atendente = string.Empty;


                var nomeUsuario = usuario.UserName == "admin" ? "admin" : (usuario.IsAluno ? "Via Portal do Aluno" : usuario.Funcionario.Nome);

              
                        atendente = $"Aberto por {nomeUsuario}";

                ticketTimelines.Mensagens.Add(new DtoMensagensTimeLine
                {
                    Data = ocorrencia.DataAbertura.Value,
                    Mensagem = ocorrencia.Mensagem,
                    StatusTicket = StatusTicketEnum.Ocorrencia,
                    Atendente = atendente,
                    Unidade = usuario.UserName == "admin" ? "ADMIN" : usuario.Unidade.Nome,
                });

            return ticketTimelines;
        }
        public async Task<DtoOcorencia> Inserir(DtoOcorencia ocorrencia)
        {
            Ocorrencia _ocorrencia = _mapper.Map<Ocorrencia>(ocorrencia);
            if (ocorrencia.Id > 0) {
                _ocorrencia = await _ocorrenciaRepository.GetByIdAsync(ocorrencia.Id);
                _ocorrencia.Assunto = ocorrencia.Assunto;
                _ocorrencia.Mensagem = ocorrencia.Mensagem;
                _ocorrencia.DataAtendimento = ocorrencia.DataAtendimento;
                _ocorrenciaRepository.UpdateAsync(_ocorrencia);
            }
            else
            {
                _ocorrencia.DataAbertura = DateTime.Now;
                _ocorrencia.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

                _ocorrencia = await _ocorrenciaRepository.AddAsync(_ocorrencia);
            }

            return _mapper.Map<DtoOcorencia>(_ocorrencia);
        }
    }
}
