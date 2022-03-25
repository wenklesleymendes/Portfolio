using AutoMapper;
using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Repository.Interfaces.CadastroAluno;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class NacionalidadeService : INacionalidadeService
    {
        private readonly INacionalidadeRepository _nacionalidadeRepository;
        private readonly IMapper _mapper;

        public NacionalidadeService(
            INacionalidadeRepository nacionalidadeRepository,
            IMapper mapper)
        {
            _nacionalidadeRepository = nacionalidadeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoNacionalidade>> BuscarTodos()
        {
            try
            {
                var nacionalidades = await _nacionalidadeRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<DtoNacionalidade>>(nacionalidades.Where(x => !x.IsDelete));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int naturalidadeId)
        {
            try
            {
                var nacionalidade = await _nacionalidadeRepository.GetByIdAsync(naturalidadeId);

                nacionalidade.IsDelete = true;

                var id = await _nacionalidadeRepository.UpdateAsync(nacionalidade);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoNacionalidade> Inserir(DtoNacionalidade dtoNacionalidade)
        {
            try
            {
                var nacionalidade = await _nacionalidadeRepository.AddAsync(_mapper.Map<Nacionalidade>(dtoNacionalidade));

                return _mapper.Map<DtoNacionalidade>(nacionalidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
