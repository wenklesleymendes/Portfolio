using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class InstituicaoBancariaService : IInstituicaoBancariaService
    {
        private readonly IInstituicaoBancariaRepository _bancoRepository;
        private readonly IMapper _mapper;

        public InstituicaoBancariaService(
            IInstituicaoBancariaRepository bancoRepository,
            IMapper mapper)
        {
            _bancoRepository = bancoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstituicaoBancaria>> BuscarTodos()
        {
            var bancos = await _bancoRepository.GetAllAsync();

            return bancos.OrderBy(x => x.NomeBanco).ToList();
        }
    }
}
