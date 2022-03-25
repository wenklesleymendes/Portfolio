using AutoMapper;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class RespostaService : IRespostaService
    {
        private readonly IRespostaRepository _respostaRepository;
        private readonly IMapper _mapper;

        public RespostaService(
            IRespostaRepository respostaRepository,
            IMapper mapper)
        {
            _respostaRepository = respostaRepository;
            _mapper = mapper;
        }

        public Task<DtoResposta> BuscarPorId(int respostaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DtoResposta>> BuscarPorPergunta(int perguntaId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(int respostaId)
        {
            throw new NotImplementedException();
        }

        public Task<DtoResposta> Inserir(DtoResposta dtoResposta)
        {
            throw new NotImplementedException();
        }
    }
}
