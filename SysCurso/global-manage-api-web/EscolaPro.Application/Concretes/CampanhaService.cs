using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.BolsaConvenio;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto.CampanhaVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class CampanhaService : ICampanhaService
    {
        private readonly ICampanhaRepository _campanhaRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IMapper _mapper;

        public CampanhaService(
            ICampanhaRepository campanhaRepository,
            ICursoRepository cursoRepository,
            IUnidadeRepository unidadeRepository,
            IMapper mapper)
        {
            _campanhaRepository = campanhaRepository;
            _cursoRepository = cursoRepository;
            _unidadeRepository = unidadeRepository;
            _mapper = mapper;
        }

        public async Task<DtoCampanha> AtivarOuDesativar(int idCampanha)
        {
            var campanha = await _campanhaRepository.GetByIdAsync(idCampanha);

            campanha.IsActive = campanha.IsActive ? campanha.IsActive = false : campanha.IsActive = true;

            int id = await _campanhaRepository.UpdateAsync(campanha);
            var campanhaRetorno = await _campanhaRepository.GetByIdAsync(idCampanha);
            return _mapper.Map<DtoCampanha>(campanhaRetorno);
        }

        public async Task<IEnumerable<DtoCampanha>> BuscarCampanhaVigente(int unidadeId, int cursoId, int tipoPagamento)
        {
            try
            {
                List<DtoCampanha> campanhaRetorno = new List<DtoCampanha>();

                var campanhas = await _campanhaRepository.BuscarCampanhaVigente(unidadeId, cursoId, tipoPagamento);

                foreach (var item in campanhas)
                {
                    var campanha = await BuscarPorId(item.Id);

                    campanhaRetorno.Add(campanha);
                }

                return campanhaRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoCampanha> BuscarPorId(int idCampanha)
        {
            var campanhaRetorno = await _campanhaRepository.BuscarPorId(idCampanha);

            var campanha = _mapper.Map<DtoCampanha>(campanhaRetorno);

            campanha.CampanhaUnidade = new List<DtoCampanhaUnidade>();

            foreach (var item in campanhaRetorno.CampanhaUnidade.ToList())
            {
                var unidade = await _unidadeRepository.GetByIdAsync(item.UnidadeId);
                campanha.CampanhaUnidade.Add(new DtoCampanhaUnidade { CampanhaId = item.CampanhaId, NomeUnidade = unidade.Nome, UnidadeId = item.UnidadeId });
            }

            return campanha;
        }

        public async Task<IEnumerable<DtoCampanha>> BuscarTodos()
        {
            List<DtoCampanha> dtoCampanhas = new List<DtoCampanha>();

            var listaCampanha = await _campanhaRepository.GetAllAsync();

            foreach (var item in listaCampanha.Where(x => !x.IsDelete))
            {
                var campanha = await BuscarPorId(item.Id);
                dtoCampanhas.Add(campanha);
            }

            return dtoCampanhas;
        }

        public async Task<bool> Deletar(int idCampanha)
        {
            var campanha = await _campanhaRepository.GetByIdAsync(idCampanha);
            campanha.IsDelete = true;
            campanha.DeletedAt = DateTime.Now;
            var id = await _campanhaRepository.UpdateAsync(campanha);
            return id > 0 ? true : false;
        }

        public async Task<DtoCampanha> Inserir(DtoCampanha dtoCampanha)
        {
            var campanha = _mapper.Map<Campanha>(dtoCampanha);

            var campanhaCurso = new List<CampanhaCurso>();

            var campanhaUnidade = _mapper.Map<List<CampanhaUnidade>>(dtoCampanha.CampanhaUnidade);

            var retorno = await _campanhaRepository.Inserir(campanha, campanhaUnidade);

            return _mapper.Map<DtoCampanha>(retorno);
        }
    }
}
