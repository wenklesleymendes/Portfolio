using AutoMapper;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Interfaces.AgendaProvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.AgendaProvas
{
    public class UnidadeTransporteProvaService : IUnidadeTransporteProvaService
    {
        private readonly IUnidadeTransporteProvaRepository _unidadeTransporteProvaRepository;
        private readonly IAgendaProvaRepository _agendaProvaRepository;
        private readonly IMapper _mapper;

        public UnidadeTransporteProvaService(IUnidadeTransporteProvaRepository unidadeTransporteProvaRepository,
            IAgendaProvaRepository agendaProvaRepository,
            IMapper mapper)
        {
            _unidadeTransporteProvaRepository = unidadeTransporteProvaRepository;
            _agendaProvaRepository = agendaProvaRepository;
            _mapper = mapper;
        }

        public DtoUnidadeTransporteProva BuscarProximoOnibus(int agendaProvaId, int unidadeId)
        {
            var transportes = _unidadeTransporteProvaRepository.BuscarPorUnidadeId(agendaProvaId, unidadeId);
            DtoUnidadeTransporteProva ret = null;

            foreach(var transporte in transportes)
            {
                if (transporte.ProvaAlunos.Count < transporte.TotalVagas)
                {
                    ret = _mapper.Map<DtoUnidadeTransporteProva>(transporte);
                    ret.provaAlunos = null;
                    ret.VagasRestantes = transporte.TotalVagas - transporte.ProvaAlunos.Count;
                    break;
                }
            }

            if (ret == null)
            {
                ret = new DtoUnidadeTransporteProva();
                ret.NumeroOnibus = transportes.Count() == 0 ? 1 : transportes.LastOrDefault().NumeroOnibus + 1;
                ret.TotalVagas = 45;
                ret.VagasRestantes = ret.TotalVagas;

                if (transportes.Count() == 0)
                {
                    var unidadeParticipante = _agendaProvaRepository.BuscarUnidadeParticipante(agendaProvaId, unidadeId);
                    ret.UnidadeParticipanteProvaId = unidadeParticipante.Id;
                    ret.UnidadeParticipanteProva = _mapper.Map<DtoUnidadeParticipanteProva>(unidadeParticipante);
                }
                else
                {
                    ret.UnidadeParticipanteProvaId = transportes.FirstOrDefault().UnidadeParticipanteProvaId;
                    ret.UnidadeParticipanteProva = _mapper.Map<DtoUnidadeParticipanteProva>(transportes.FirstOrDefault().UnidadeParticipanteProva);
                }
            }

            return ret;
        }

        public async Task<DtoUnidadeTransporteProva> BuscarOnibus(int UnidadeTransporteProvaId)
        {
            try
            {
                var retTemp = await _unidadeTransporteProvaRepository.GetByIdAsync(UnidadeTransporteProvaId);
                return _mapper.Map<DtoUnidadeTransporteProva>(retTemp);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
