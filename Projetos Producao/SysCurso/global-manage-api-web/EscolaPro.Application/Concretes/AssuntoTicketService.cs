using AutoMapper;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class AssuntoTicketService : IAssuntoTicketService
    {
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFuncionarioAssuntoTicketRepository _funcionarioAssuntoTicketRepository;
        private readonly IMapper _mapper;

        public AssuntoTicketService(
            IAssuntoTicketRepository assuntoTicketRepository,
            IUsuarioRepository usuarioRepository,
            IFuncionarioAssuntoTicketRepository funcionarioAssuntoTicketRepository,
            IMapper mapper)
        {
            _assuntoTicketRepository = assuntoTicketRepository;
            _usuarioRepository = usuarioRepository;
            _funcionarioAssuntoTicketRepository = funcionarioAssuntoTicketRepository;
            _mapper = mapper;
        }

        public async Task<DtoAssuntoTicket> BuscarPorId(int idTicket)
        {
            var ticket = await _assuntoTicketRepository.BuscarPorId(idTicket);

            return _mapper.Map<DtoAssuntoTicket>(ticket);
        }

        public async Task<IEnumerable<DtoAssuntoTicket>> BuscarPorUnidadeDepartamento(int? idUnidade, int? idDepartamento)
        {
           var ticketLista = await _assuntoTicketRepository.BuscarPorUnidadeDepartamento(idUnidade, idDepartamento);

            return _mapper.Map<IEnumerable<DtoAssuntoTicket>>(ticketLista);
        }

        public async Task<IEnumerable<DtoAssuntoTicket>> BuscarTodos()
        {
            var ticketLista = await _assuntoTicketRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DtoAssuntoTicket>>(ticketLista.Where(x => !x.IsDelete));
        }

        public async Task<bool> Deletar(int idTicket)
        {
            var ticket = await _assuntoTicketRepository.GetByIdAsync(idTicket);
            ticket.IsDelete = true;
            var ticketRetorno = await _assuntoTicketRepository.UpdateAsync(ticket);
            return ticketRetorno > 0 ? true : false;
        }

        public async Task<DtoAssuntoTicket> Inserir(DtoAssuntoTicket ticket)
        {
            try
            {
                if (ticket.Id == 0)
                {
                    AssuntoTicket _ticket = _mapper.Map<AssuntoTicket>(ticket);

                    _ticket.FuncionarioAssuntoTicket = _mapper.Map<IList<FuncionarioAssuntoTicket>>(ticket.FuncionarioAssuntoTicket);

                    var ticketRetorno = await _assuntoTicketRepository.AddAsync(_ticket);

                    if (ticket.FuncionarioIds != null)
                    {
                        _ticket.FuncionarioAssuntoTicket = new List<FuncionarioAssuntoTicket>();

                        foreach (var funcionarioId in ticket.FuncionarioIds)
                        {
                            var usuario = await _usuarioRepository.BuscarPorFuncionarioId(funcionarioId);

                            var funcionarioAssuntoTicket =new FuncionarioAssuntoTicket
                            {
                                UsuarioId = usuario.Id,
                                AssuntoTicketId = _ticket.Id,
                            };

                            await _funcionarioAssuntoTicketRepository.AddAsync(funcionarioAssuntoTicket);
                        }

                    }

                   

                    return _mapper.Map<DtoAssuntoTicket>(ticketRetorno);
                }
                else
                {

                    var _ticket = _mapper.Map<AssuntoTicket>(ticket);

                    await _funcionarioAssuntoTicketRepository.ApagarFuncionariosAntigo(_ticket.Id);

                    if (ticket.FuncionarioIds != null)
                    {
                        foreach (var funcionarioId in ticket.FuncionarioIds)
                        {
                            var usuario = await _usuarioRepository.BuscarPorFuncionarioId(funcionarioId);

                            var funcionarioTemp = new FuncionarioAssuntoTicket
                            {
                                UsuarioId = usuario.Id,
                                AssuntoTicketId = ticket.Id,
                            };

                            funcionarioTemp = await _funcionarioAssuntoTicketRepository.AddAsync(_mapper.Map<FuncionarioAssuntoTicket>(funcionarioTemp));

                            _ticket.FuncionarioAssuntoTicket.Add(funcionarioTemp);
                        }

                    }

                    await _assuntoTicketRepository.UpdateAsync(_ticket);

                    var ticketRetorno = await _assuntoTicketRepository.BuscarPorId(_ticket.Id);

                    return _mapper.Map<DtoAssuntoTicket>(ticketRetorno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
