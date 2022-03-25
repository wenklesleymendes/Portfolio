using AutoMapper;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class AgendaProvaService : IAgendaProvaService
    {
        private readonly IAgendaProvaRepository _agendaProvaRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;

        public AgendaProvaService(
            IAgendaProvaRepository agendaProvaRepository,
            IUnidadeRepository unidadeRepository,
            ICursoRepository cursoRepository,
            IMapper mapper)
        {
            _agendaProvaRepository = agendaProvaRepository;
            _unidadeRepository = unidadeRepository;
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }

        public async Task<DtoAgendaProva> BuscarPorId(int idAgendaProva)
        {
            try
            {
                var agendaProva = await _agendaProvaRepository.BuscarPorId(idAgendaProva);

                agendaProva.UnidadeParticipanteProva = new List<UnidadeParticipanteProva>();

                var unidadeParticipante = _agendaProvaRepository.BuscarUnidadeParticipante(agendaProva.Id);

                agendaProva.UnidadeParticipanteProva = unidadeParticipante;

                return _mapper.Map<DtoAgendaProva>(agendaProva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoAgendaProva>> BuscarProvasDisponiveis(int colegioId, int unidadeId, int cursoId)
        {
            try
            {
                var agendaProva = _agendaProvaRepository.BuscarProvasDisponiveis(colegioId, unidadeId, cursoId);

                List<DtoAgendaProva> agendaProvasLista = new List<DtoAgendaProva>();

                foreach (var item in agendaProva)
                {
                    var agenda = await BuscarPorId(item.Id);

                    agendaProvasLista.Add(agenda);
                }

                return agendaProvasLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoAgendaGrid>> BuscarTodos()
        {
            try
            {
                var agendaProvas = await _agendaProvaRepository.GetAllAsync();

                List<DtoAgendaGrid> agendasLista = new List<DtoAgendaGrid>();

                foreach (var item in agendaProvas.Where(x => !x.IsDelete))
                {
                    var agendaProva = await BuscarPorId(item.Id);

                    string unidades = string.Empty;

                    foreach (var unidadeParticipante in agendaProva.UnidadeParticipanteProva.ToList())
                    {
                        var unidade = await _unidadeRepository.GetByIdAsync(unidadeParticipante.UnidadeId);

                        unidades = $"{unidade.Nome}, {unidades}";
                    }

                    DtoAgendaGrid dtoAgenda = new DtoAgendaGrid
                    {
                        Id = agendaProva.Id,
                        TipoProva = agendaProva.TipoProva,
                        DataInicioInscricao = agendaProva.InicioInscricao.HasValue ? agendaProva.InicioInscricao : null,
                        DataTerminoInscricao = agendaProva.TerminoInscricao.HasValue ? agendaProva.TerminoInscricao : null,
                        DataProva = agendaProva.DataProva.HasValue ? agendaProva.DataProva : null,
                        QuantidadeVagas = agendaProva.QuantidadeVagas,
                        Unidade = unidades
                    };

                    agendasLista.Add(dtoAgenda);
                }

                return agendasLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int idAgendaProva)
        {
            try
            {
                var agendaProva = await _agendaProvaRepository.GetByIdAsync(idAgendaProva);
                agendaProva.IsDelete = true;
                await _agendaProvaRepository.UpdateAsync(agendaProva);
                return agendaProva.IsDelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAgendaProva> Inserir(DtoAgendaProva dtoAgendaProva)
        {
            try
            {
                if (dtoAgendaProva.Id == 0)
                {
                    var agenda = await _agendaProvaRepository.AddAsync(_mapper.Map<AgendaProva>(dtoAgendaProva));

                    return _mapper.Map<DtoAgendaProva>(agenda);
                }
                else
                {
                    await _agendaProvaRepository.Deletar(dtoAgendaProva.Id);

                    await _agendaProvaRepository.AdicionarUnidadeParticipante(_mapper.Map<List<UnidadeParticipanteProva>>(dtoAgendaProva.UnidadeParticipanteProva.ToList()), dtoAgendaProva.Id);

                    await _agendaProvaRepository.UpdateAsync(_mapper.Map<AgendaProva>(dtoAgendaProva));

                    return await BuscarPorId(dtoAgendaProva.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
