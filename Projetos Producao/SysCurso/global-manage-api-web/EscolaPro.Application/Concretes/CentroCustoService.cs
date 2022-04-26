using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class CentroCustoService : ICentroCustoService
    {
        private readonly IMapper _mapper;
        private readonly ICentroCustoRepository _centroCustoRepository;

        public CentroCustoService(
            IMapper mapper,
            ICentroCustoRepository centroCustoRepository)
        {
            _mapper = mapper;
            _centroCustoRepository = centroCustoRepository;
        }

        public async Task<IEnumerable<DtoCentroCusto>> BuscarPorUnidade(int idUnidade)
        {
            var centroCusto = await _centroCustoRepository.BuscarPorUnidade(idUnidade);

            return _mapper.Map<IEnumerable<DtoCentroCusto>>(centroCusto);
        }

        public async Task<bool> Deletar(int idCentroCusto)
        {
            var centroDelete = await _centroCustoRepository.GetByIdAsync(idCentroCusto);
            centroDelete.DeletedAt = DateTime.Now;
            centroDelete.IsDelete = true;
            var id = await _centroCustoRepository.UpdateAsync(_mapper.Map<CentroCusto>(centroDelete));

            return id > 0 ? true : false;
        }

        public async Task<DtoCentroCusto> Inserir(DtoCentroCusto dtoCentroCusto)
        {
            if (dtoCentroCusto.Id > 0)
            {
                var centroCustoInserir = _mapper.Map<CentroCusto>(dtoCentroCusto);
                centroCustoInserir.UpdatedAt = DateTime.Now;
                await _centroCustoRepository.UpdateAsync(centroCustoInserir);

                var centroCusto = await _centroCustoRepository.GetByIdAsync(dtoCentroCusto.Id);

                return _mapper.Map<DtoCentroCusto>(centroCusto);
            }
            else
            {

                var centroCusto = await _centroCustoRepository.AddAsync(_mapper.Map<CentroCusto>(dtoCentroCusto));

                return _mapper.Map<DtoCentroCusto>(centroCusto);
            }
        }
    }
}
