using AutoMapper;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service
{
    public class ContatosService : IContatosService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IMapper _mapper;


        public ContatosService(IContatoRepository contatoRepository,IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _mapper = mapper;
        }

        public async Task<bool> ExisteContatoCelular(string celular)
        {
            var contato = await _contatoRepository.BuscarPorCelular(celular);

            if (contato != null)
            {
                return _mapper.Map<DtoContato>(contato).Celular.Contains(celular);
            }

            return false;
        }

        public async Task<bool> ExisteContatoEmail(string email)
        {
            var contato = await _contatoRepository.BuscarPorEmail(email);

            if (contato != null)
            {
                return _mapper.Map<DtoContato>(contato).Email.Contains(email);
            }

            return false;
        }

        public async Task<DtoContato> BuscarPorEmail(string email)
        {
            var contato = await _contatoRepository.BuscarPorEmail(email);
            return _mapper.Map<DtoContato>(contato);
        }

        public async Task<DtoContato> BuscarPorCelular(string celular)
        {
            var contato = await _contatoRepository.BuscarPorCelular(celular);
            return _mapper.Map<DtoContato>(contato);
        }
    }
}
