using AutoMapper;
using EscolaPro.Core.Model.PortalAlunoProfessor;
using EscolaPro.Repository.Interfaces.PortalAlunoProfessor;
using EscolaPro.Service.Dto.PortalAlunoProfessorVO;
using EscolaPro.Service.Interfaces.PortalAluno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.PortalAluno
{
    public class MensagemAlunoProfessorService : IMensagemAlunoProfessorService
    {
        private readonly IMapper _mapper;
        private readonly IMensagemAlunoProfessorRepository _mensagemAlunoProfessorRepository;

        public MensagemAlunoProfessorService(IMapper mapper,
            IMensagemAlunoProfessorRepository mensagemAlunoProfessorRepository)
        {
            _mapper = mapper;
            _mensagemAlunoProfessorRepository = mensagemAlunoProfessorRepository;
        }

        public async Task<IEnumerable<DtoMensagemAlunoProfessor>> BuscarPorMatricula(int matriculaId)
        {
            try
            {
                var mensagens = await _mensagemAlunoProfessorRepository.BuscarPorMatricula(matriculaId);

                return _mapper.Map<IEnumerable<DtoMensagemAlunoProfessor>>(mensagens);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DtoMensagemAlunoProfessor> BuscarPorId(int mensagemId)
        {
            try
            {
                var mensagem = await _mensagemAlunoProfessorRepository.GetByIdAsync(mensagemId);

                return _mapper.Map<DtoMensagemAlunoProfessor>(mensagem);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<DtoMensagemAlunoProfessor>> BuscarPorProfessor(int professorId)
        {
            try
            {
                var mensagens = await _mensagemAlunoProfessorRepository.BuscarPorProfessor(professorId);

                return _mapper.Map<IEnumerable<DtoMensagemAlunoProfessor>>(mensagens);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Excluir(int mensagemId)
        {
            try
            {
                var mensagem = await _mensagemAlunoProfessorRepository.GetByIdAsync(mensagemId);
                mensagem.IsDelete = true;
                var id = await _mensagemAlunoProfessorRepository.UpdateAsync(mensagem);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoMensagemAlunoProfessor> Inserir(DtoMensagemAlunoProfessor dtoMensagemAluno)
        {
            try
            {
                var retorno = await _mensagemAlunoProfessorRepository.AddAsync(_mapper.Map<MensagemAlunoProfessor>(dtoMensagemAluno));

                return _mapper.Map<DtoMensagemAlunoProfessor>(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
