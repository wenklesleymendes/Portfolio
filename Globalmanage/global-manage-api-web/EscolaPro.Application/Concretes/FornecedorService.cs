using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Repository.Interfaces.Fornecedores;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorService(
            IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<DtoFornecedor> BuscarPorId(int idFornecedor)
        {
            var fornecedor = await _fornecedorRepository.BuscarPorId(idFornecedor);

            return _mapper.Map<DtoFornecedor>(fornecedor);
        }

        public async Task<IEnumerable<DtoFornecedor>> BuscarTodos()
        {
            var fornecedoresLista = await _fornecedorRepository.GetAllAsync();

            List<DtoFornecedor> fornecedores = new List<DtoFornecedor>();

            foreach (var item in fornecedoresLista.Where(x => !x.IsDelete))
            {
                var fornecedor = await _fornecedorRepository.BuscarPorId(item.Id);

                fornecedores.Add(_mapper.Map<DtoFornecedor>(fornecedor));
            }

            return fornecedores;
        }

        public async Task<bool> DesativarOuAtivar(int idFornecedor)
        {
            var fornecedor = await _fornecedorRepository.GetByIdAsync(idFornecedor);
            fornecedor.IsActive = fornecedor.IsActive ? fornecedor.IsActive = false : fornecedor.IsActive = true;
            await _fornecedorRepository.UpdateAsync(fornecedor);
            return fornecedor.IsActive;
        }

        public async Task<bool> Excluir(int idFornecedor)
        {
            var fornecedor = await _fornecedorRepository.GetByIdAsync(idFornecedor);
            fornecedor.IsDelete = true;
            await _fornecedorRepository.UpdateAsync(fornecedor);
            return fornecedor.IsDelete;
        }

        public async Task<DtoFornecedor> Inserir(DtoFornecedor dtoFornecedor)
        {
            try
            {
                if (dtoFornecedor.Id == 0)
                {
                    var fornecedor = await _fornecedorRepository.Inserir(_mapper.Map<Fornecedor>(dtoFornecedor));

                    return _mapper.Map<DtoFornecedor>(fornecedor);
                }
                else
                {
                    if(dtoFornecedor.Endereco != null)
                    {
                        dtoFornecedor.EnderecoId = dtoFornecedor.Endereco.Id;
                    }

                    if (dtoFornecedor.Contato != null)
                    {
                        dtoFornecedor.ContatoId = dtoFornecedor.Contato.Id;
                    }

                    if (dtoFornecedor.DadosBancario != null)
                    {
                        dtoFornecedor.DadosBancarioId = dtoFornecedor.DadosBancario.Id;
                    }

                    await _fornecedorRepository.Inserir(_mapper.Map<Fornecedor>(dtoFornecedor));
                    return await BuscarPorId(dtoFornecedor.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
