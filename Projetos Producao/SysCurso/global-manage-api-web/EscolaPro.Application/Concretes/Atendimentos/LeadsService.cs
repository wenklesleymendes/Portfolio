using AutoMapper;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces.Atendimentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Atendimentos
{
    public class LeadsService : ILeadsService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly IMapper _mapper;

        public LeadsService(ILeadsRepository leadsRepository, IMapper mapper)
        {
            _leadsRepository = leadsRepository;
            _mapper = mapper;
        }

        public async Task<DtoLeads> Inserir(string leads)
        {
            try
            {
                DtoLeads dtoLeads = new DtoLeads
                {
                    Texto = leads,
                    DataCriacao = DateTime.Now,
                    Origem = "Leadster",
                    Status = 0,
                };

                var retorno = await _leadsRepository.AddAsync(_mapper.Map<Leads>(dtoLeads));

                return _mapper.Map<DtoLeads>(retorno);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<DtoLeads>> BuscarTodos()
        {
            var leads = await _leadsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DtoLeads>>(leads);
        }
    }
}
