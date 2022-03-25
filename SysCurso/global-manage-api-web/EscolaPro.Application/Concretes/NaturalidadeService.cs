using AutoMapper;
using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Repository.Interfaces.CadastroAluno;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class NaturalidadeService : INaturalidadeService
    {
        private readonly INaturalidadeRepository _naturalidadeRepository;
        private readonly IMapper _mapper;

        public NaturalidadeService(
            INaturalidadeRepository naturalidadeRepository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _naturalidadeRepository = naturalidadeRepository;
        }

        public async Task<IEnumerable<DtoNaturalidade>> BuscarTodos()
        {
            try
            {
                var nacionalidades = await _naturalidadeRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<DtoNaturalidade>>(nacionalidades.Where(x => !x.IsDelete));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int nacionalidadeId)
        {
            try
            {
                var nacionalidade = await _naturalidadeRepository.GetByIdAsync(nacionalidadeId);
                nacionalidade.IsDelete = true;
                var id = await _naturalidadeRepository.UpdateAsync(nacionalidade);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoNaturalidade> Inserir(DtoNaturalidade dtoNaturalidade)
        {
            try
            {
                var naturalidade = await _naturalidadeRepository.AddAsync(_mapper.Map<Naturalidade>(dtoNaturalidade));

                return _mapper.Map<DtoNaturalidade>(naturalidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
