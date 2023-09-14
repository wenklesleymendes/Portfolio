using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MetasComissoes;
using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.MetasComissoesVO.Dashboard;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class MetasService : IMetasService
    {
        private readonly IMetasRepository _metasRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IComissoesService _comissoesService;
        private readonly IMapper _mapper;

        public MetasService(
            IMetasRepository metasRepository,
            IUnidadeRepository unidadeRepository,
            IComissoesService comissoesService,
            IMapper mapper)
        {
            _metasRepository = metasRepository;
            _unidadeRepository = unidadeRepository;
            _comissoesService = comissoesService;
            _mapper = mapper;
        }

        public async Task<DtoMetas> BuscarPorId(int idMeta)
        {
            var metaRetorno = await _metasRepository.BuscarPorId(idMeta);

            return _mapper.Map<DtoMetas>(metaRetorno);
        }

        public async Task<DtoMetas> BuscarPorUnidade(int idUnidade)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DtoMetas>> BuscarTodos()
        {
            List<DtoMetas> dtoMetas = new List<DtoMetas>();

            var metas = await _metasRepository.GetAllAsync();

            foreach (var item in metas.Where(x => !x.IsDelete))
            {
                var meta = await BuscarPorId(item.Id);
                dtoMetas.Add(_mapper.Map<DtoMetas>(meta));
            }

            return dtoMetas;
        }

        public async Task<DtoDashboardMetasComissoes> ConsultarDashboard(DtoFiltrarMeta filtrar)
        {
            DtoDashboardMetasComissoes dtoDashboard = new DtoDashboardMetasComissoes();

            var metas = await _metasRepository.Filtrar(filtrar.UnidadeId, filtrar.NomeMeta);

            var comissoes = await _comissoesService.BuscarTodos();

            List<DtoVisaoDiaria> visaoDiarias = new List<DtoVisaoDiaria>();

            visaoDiarias.Add(new DtoVisaoDiaria { DataDiaria = DateTime.Now.AddDays(-2), QuantidadeMatriculasRealizadas = 80, QuantidadeMeta = 48 });
            visaoDiarias.Add(new DtoVisaoDiaria { DataDiaria = DateTime.Now.AddDays(-1), QuantidadeMatriculasRealizadas = 60, QuantidadeMeta = 33 });
            visaoDiarias.Add(new DtoVisaoDiaria { DataDiaria = DateTime.Now, QuantidadeMatriculasRealizadas = 50, QuantidadeMeta = 48 });

            List<DtoVisaoMensal> visaoMensals = new List<DtoVisaoMensal>();

            visaoMensals.Add(new DtoVisaoMensal { Mes = DateTime.Now.AddMonths(-2), QuantidadeMatriculasRealizadas = 80, QuantidadeMeta = 48 });
            visaoMensals.Add(new DtoVisaoMensal { Mes = DateTime.Now.AddMonths(-1), QuantidadeMatriculasRealizadas = 60, QuantidadeMeta = 33 });
            visaoMensals.Add(new DtoVisaoMensal { Mes = DateTime.Now, QuantidadeMatriculasRealizadas = 50, QuantidadeMeta = 48 });

            List<DtoMinhasComissoes> minhasComissoes = new List<DtoMinhasComissoes>();

            minhasComissoes.Add(new DtoMinhasComissoes { });

            dtoDashboard.MinhasComissoes = new List<DtoMinhasComissoes>();

            dtoDashboard.MinhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "02/2020", ComissaoEquipe = true, ValorComissao = 20, QuantidadePrimeiraParcelaPaga = 14 });
            dtoDashboard.MinhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "03/2020", ComissaoEquipe = true, ValorComissao = 16, QuantidadePrimeiraParcelaPaga = 36 });
            dtoDashboard.MinhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "04/2020", ComissaoEquipe = true, ValorComissao = 18, QuantidadePrimeiraParcelaPaga = 45 });
            dtoDashboard.MinhasComissoes.Add(new DtoMinhasComissoes { UnidadeId = 1, Data = "05/2020", ComissaoEquipe = true, ValorComissao = 15, QuantidadePrimeiraParcelaPaga = 36 });

            dtoDashboard.TotalMinhasComissoes = dtoDashboard.MinhasComissoes.Sum(x => x.ValorComissao);

            dtoDashboard.VisaoDiaria = visaoDiarias;
            dtoDashboard.VisaoMensal = visaoMensals;

            dtoDashboard.MetaTotal = 350;
            dtoDashboard.TotalMatriculasRealizadas = 150;

            dtoDashboard.ValorComissaoEquipe = 768;
            dtoDashboard.ValorComissaoIndividual = 250;

            return dtoDashboard;
        }

        public async Task<bool> Excluir(int idMeta)
        {
            var meta = await _metasRepository.GetByIdAsync(idMeta);
            meta.IsDelete = true;
            await _metasRepository.UpdateAsync(meta);
            return meta.IsDelete;
        }

        public async Task<IEnumerable<DtoMetas>> Filtrar(DtoFiltrarMeta filtrar)
        {
            List<DtoMetas> dtoMetas = new List<DtoMetas>();

            var metas = await _metasRepository.Filtrar(filtrar.UnidadeId, filtrar.NomeMeta);

            foreach (var item in metas.Where(x => !x.IsDelete))
            {
                var meta = await BuscarPorId(item.Id);
                dtoMetas.Add(_mapper.Map<DtoMetas>(meta));
            }

            return dtoMetas;
        }

        public async Task<DtoMetas> Inserir(DtoMetas metaParam)
        {
            if (metaParam.Id == 0)
            {
                var meta = await _metasRepository.AddAsync(_mapper.Map<Meta>(metaParam));

                return _mapper.Map<DtoMetas>(meta);
            }
            else
            {
                await _metasRepository.MetaPeriodosDeletar(metaParam.Id);

                await _metasRepository.AdicionarDetalhamentoMeta(metaParam.Id, _mapper.Map<List<DetalhamentoMeta>>(metaParam.DetalhamentoMeta));

                await _metasRepository.UpdateAsync(_mapper.Map<Meta>(metaParam));

                var meta = await _metasRepository.GetByIdAsync(metaParam.Id);

                return await BuscarPorId(meta.Id);
            }
        }

        public async Task<List<string>> ListaNomeMetas()
        {
            var metas = await _metasRepository.GetAllAsync();

            List<string> nomes = new List<string>();

            foreach (var item in metas.Where(x => !x.IsDelete))
            {
                nomes.Add(item.Descricao);
            }

            return nomes.DistinctBy(x => x).ToList();
        }
    }
}
