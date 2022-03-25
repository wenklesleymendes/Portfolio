using AutoMapper;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Repository.Interfaces.Fornecedores;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(
            ICategoriaRepository categoriaRepository,
            IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<DtoCategoria> BuscarPorId(int idCategoria)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(idCategoria);
            return _mapper.Map<DtoCategoria>(categoria);
        }

        public async Task<IEnumerable<DtoCategoria>> BuscarTodos()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DtoCategoria>>(categorias.Where(x => !x.IsDelete));
        }

        public async Task<bool> Excluir(int idCategoria)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(idCategoria);
            categoria.IsDelete = true;
            await _categoriaRepository.UpdateAsync(categoria);
            return categoria.IsDelete;
        }

        public async Task<DtoCategoria> Inserir(DtoCategoria dtoCategoria)
        {
            try
            {
                if (dtoCategoria.Id == 0)
                {
                    var categoria = await _categoriaRepository.AddAsync(_mapper.Map<Categoria>(dtoCategoria));

                    return _mapper.Map<DtoCategoria>(categoria);
                }
                else
                {
                    await _categoriaRepository.UpdateAsync(_mapper.Map<Categoria>(dtoCategoria));
                    return await BuscarPorId(dtoCategoria.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
