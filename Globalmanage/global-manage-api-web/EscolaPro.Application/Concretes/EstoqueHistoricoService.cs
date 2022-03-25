using AutoMapper;
using EscolaPro.Core.Model.EstoqueProdutos;
using EscolaPro.Repository.Interfaces.EstoqueProdutos;
using EscolaPro.Service.Dto.EstoqueVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class EstoqueHistoricoService : IEstoqueHistoricoService
    {
        private readonly IEstoqueHistoricoRepository _estoqueHistoricoRepository;
        private readonly IMapper _mapper;
        public EstoqueHistoricoService(
            IEstoqueHistoricoRepository estoqueHistoricoRepository,
            IMapper mapper)
        {
            _estoqueHistoricoRepository = estoqueHistoricoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoHistoricoEstoque>> BuscarPorIdEstoque(int idEstoque)
        {
            try
            {
                var historicoEstoques  = await _estoqueHistoricoRepository.BuscarPorIdEstoque(idEstoque);

                return _mapper.Map<IEnumerable<DtoHistoricoEstoque>>(historicoEstoques);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoHistoricoEstoque> Inserir(DtoHistoricoEstoque historicoEstoque)
        {
            try
            {
                var historico = await _estoqueHistoricoRepository.AddAsync(_mapper.Map<HistoricoEstoque>(historicoEstoque));

                return _mapper.Map<DtoHistoricoEstoque>(historico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
