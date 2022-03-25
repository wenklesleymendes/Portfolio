using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Interfaces.AgendaProvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.AgendaProvas
{
    public class ColegioAutorizadoService : IColegioAutorizadoService
    {
        private readonly IColegioAutorizadoRepository _colegioAutorizadoRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public ColegioAutorizadoService(
            IColegioAutorizadoRepository colegioAutorizadoRepository,
            IEnderecoRepository enderecoRepository,
            IMapper mapper)
        {
            _colegioAutorizadoRepository = colegioAutorizadoRepository;
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        public async Task<DtoColegioAutorizado> BuscarPorId(int colegioAutorizadoId)
        {
            try
            {
                var colegioAutorizado = await _colegioAutorizadoRepository.BuscarPorId(colegioAutorizadoId);

                return _mapper.Map<DtoColegioAutorizado>(colegioAutorizado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoColegioAutorizado>> BuscarTodos()
        {
            try
            {
                List<DtoColegioAutorizado> colegioAutorizadoLista = new List<DtoColegioAutorizado>();

                var colegioAutoriazado = await _colegioAutorizadoRepository.GetAllAsync();

                foreach (var item in colegioAutoriazado.Where(x => !x.IsDelete))
                {
                    colegioAutorizadoLista.Add(_mapper.Map<DtoColegioAutorizado>(item));
                }

                return colegioAutorizadoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int colegioAutorizadoId)
        {
            try
            {
                var colegio = await _colegioAutorizadoRepository.GetByIdAsync(colegioAutorizadoId);
                colegio.IsDelete = true;
                var id = await _colegioAutorizadoRepository.UpdateAsync(colegio);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoColegioAutorizado> Inserir(DtoColegioAutorizado dtoColegioAutorizado)
        {
            try
            {
                if (dtoColegioAutorizado.Id == 0)
                {
                    var colegio = await _colegioAutorizadoRepository.AddAsync(_mapper.Map<ColegioAutorizado>(dtoColegioAutorizado));

                    return _mapper.Map<DtoColegioAutorizado>(colegio);
                }
                else
                {
                    await _enderecoRepository.UpdateAsync(_mapper.Map<Endereco>(dtoColegioAutorizado.Endereco));

                    await _colegioAutorizadoRepository.UpdateAsync(_mapper.Map<ColegioAutorizado>(dtoColegioAutorizado));

                    return await BuscarPorId(dtoColegioAutorizado.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
