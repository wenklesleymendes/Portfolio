using AutoMapper;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class ApostilaOnlineService : IApostilaOnlineService
    {
        private readonly IApostilaOnlineRepository _apostilaOnlineRepository;
        private readonly IMapper _mapper;

        public ApostilaOnlineService(IApostilaOnlineRepository apostilaOnlineRepository,
                                     IMapper mapper)
        {
            _apostilaOnlineRepository = apostilaOnlineRepository;
            _mapper = mapper;
        }
        public async Task<DtoApostilaOnline> BucarApostilaPorIdMateria(int materiaId)
        {
            var apostila = await _apostilaOnlineRepository.BucarApostilaPorIdMateria(materiaId);
            if (apostila != null)
                return _mapper.Map<DtoApostilaOnline>(apostila);
            else
                return null;
        }
        public async Task<List<DtoApostilaOnline>> BucarApostilaPorCursoId(int cursoId)
        {
            var apostila = await _apostilaOnlineRepository.BucarApostilaPorCursoId(cursoId);
            if (apostila != null)
                return _mapper.Map<List<DtoApostilaOnline>>(apostila);
            else
                return null;
        }
    }
}
