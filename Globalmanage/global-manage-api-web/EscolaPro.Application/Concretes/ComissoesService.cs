using AutoMapper;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MetasComissoes;
using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.MetasComissoesVO.Dashboard;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class ComissoesService : IComissoesService
    {
        private readonly IComissoesRepository _comissoesRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IMapper _mapper;

        public ComissoesService(
            IComissoesRepository comissoesRepository,
            IUnidadeRepository unidadeRepository,
            IMapper mapper)
        {
            _comissoesRepository = comissoesRepository;
            _unidadeRepository = unidadeRepository;
            _mapper = mapper;
        }

        public async Task<DtoComissao> BuscarPorId(int idComissoes)
        {
            var comissoesRetorno = await _comissoesRepository.BuscarPorId(idComissoes);

            DtoComissao dtoMetas = _mapper.Map<DtoComissao>(comissoesRetorno);

            return dtoMetas;
        }

        public async Task<DtoComissao> BuscarPorUnidade(int idUnidade)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DtoComissao>> BuscarTodos()
        {
            List<DtoComissao> dtoComissaos = new List<DtoComissao>();

            var comissoes = await _comissoesRepository.GetAllAsync();

            foreach (var item in comissoes.Where(x => !x.IsDelete))
            {
                var comissao = await BuscarPorId(item.Id);
                dtoComissaos.Add(_mapper.Map<DtoComissao>(comissao));
            }

            return dtoComissaos;
        }

        public async Task<IEnumerable<DtoMinhasComissoes>> DashboardMinhasComissoes(DtoFiltrar filtrar)
        {
            var retorno = await _comissoesRepository.Filtrar(filtrar.UnidadeId, filtrar.DataInicio, filtrar.DataFim, null);

            List<DtoMinhasComissoes> minhasComissoes = new List<DtoMinhasComissoes>();

            minhasComissoes = new List<DtoMinhasComissoes>();
            
            minhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "02/2020", ComissaoEquipe = true, ValorComissao = 20, QuantidadePrimeiraParcelaPaga = 14 });
            minhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "03/2020", ComissaoEquipe = true, ValorComissao = 16, QuantidadePrimeiraParcelaPaga = 36 });
            minhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "04/2020", ComissaoEquipe = true, ValorComissao = 18, QuantidadePrimeiraParcelaPaga = 45 });
            minhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "05/2020", ComissaoEquipe = true, ValorComissao = 15, QuantidadePrimeiraParcelaPaga = 36 });


            return minhasComissoes;
        }

        public async Task<bool> Excluir(int idComissoes)
        {
            var comissoes = await _comissoesRepository.GetByIdAsync(idComissoes);
            comissoes.IsDelete = true;
            await _comissoesRepository.UpdateAsync(comissoes);
            return comissoes.IsDelete;
        }

        public async Task<IEnumerable<DtoComissao>> Filtrar(DtoFiltrar filtrar)
        {
            var retorno = await _comissoesRepository.Filtrar(filtrar.UnidadeId, filtrar.DataInicio, filtrar.DataFim, filtrar.TipoPagamento);

            return _mapper.Map<IEnumerable<DtoComissao>>(retorno);
        }

        public async Task<DtoComissao> Inserir(DtoComissao dtoComissao)
        {
            if (dtoComissao.Id == 0)
            {
                var meta = await _comissoesRepository.AddAsync(_mapper.Map<Comissoes>(dtoComissao));

                return _mapper.Map<DtoComissao>(meta);
            }
            else
            {
                await _comissoesRepository.AtualizarParcelas(dtoComissao.Id, _mapper.Map<List<ComissaoParcela>>(dtoComissao.ComissaoParcelas));

                dtoComissao.ComissaoParcelas = new List<DtoComissaoParcelas>();

                await _comissoesRepository.UpdateAsync(_mapper.Map<Comissoes>(dtoComissao));

                var meta = await _comissoesRepository.GetByIdAsync(dtoComissao.Id);

                return _mapper.Map<DtoComissao>(meta);
            }
        }
    }
}
