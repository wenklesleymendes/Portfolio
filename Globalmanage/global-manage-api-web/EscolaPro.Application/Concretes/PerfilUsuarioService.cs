using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class PerfilUsuarioService : IPerfilUsuarioService
    {
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IMapper _mapper;

        public PerfilUsuarioService(
            IPerfilUsuarioRepository perfilUsuarioRepository,
            IMapper mapper)
        {
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _mapper = mapper;
        }

        public async Task<DtoPerfilUsuario> BuscarPorId(int idPerfil)
        {
            var perfil = await _perfilUsuarioRepository.BuscarPorId(idPerfil);

            return _mapper.Map<DtoPerfilUsuario>(perfil);
        }

        public async Task<IEnumerable<DtoPerfilUsuario>> BuscarTodos()
        {
            var listaPerfil = await _perfilUsuarioRepository.GetAllAsync();

            var perfils = listaPerfil.Where(x => !x.IsDelete).ToList();

            return _mapper.Map<IEnumerable<DtoPerfilUsuario>>(perfils);
        }

        public async Task<IEnumerable<DtoPerfilUsuario>> BuscarTodosAtivos()
        {
            try
            {
                var listaPerfil = await _perfilUsuarioRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<DtoPerfilUsuario>>(listaPerfil.Where(x => !x.IsDelete && x.IsActive));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ConsultarPerfilExistente(PerfilSistemaEnum perfilSistemaEnum)
        {
            try
            {
                return await _perfilUsuarioRepository.ConsultarPerfilExistente(perfilSistemaEnum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoPerfilUsuario> DesativarOuAtivar(int idPerfil)
        {
            var perfil = await _perfilUsuarioRepository.GetByIdAsync(idPerfil);

            perfil.IsActive = perfil.IsActive = perfil.IsActive ? perfil.IsActive = false : perfil.IsActive = true;

            await _perfilUsuarioRepository.UpdateAsync(perfil);

            return _mapper.Map<DtoPerfilUsuario>(perfil);
        }

        public async Task<bool> Excluir(int idPerfil)
        {
            var perfil = await _perfilUsuarioRepository.GetByIdAsync(idPerfil);
            perfil.IsDelete = true;
            var id = await _perfilUsuarioRepository.UpdateAsync(perfil);
            return id > 0 ? true : false;
        }

        public async Task<DtoPerfilUsuario> Inserir(DtoPerfilUsuario dtoPerfilUsuario)
        {
            if (dtoPerfilUsuario.Id == 0)
            {
                var perfilInserido = await _perfilUsuarioRepository.AddAsync(_mapper.Map<PerfilUsuario>(dtoPerfilUsuario));
                return _mapper.Map<DtoPerfilUsuario>(perfilInserido);
            }
            else
            {
                await _perfilUsuarioRepository.UpdateAsync(_mapper.Map<PerfilUsuario>(dtoPerfilUsuario));

                var perfilAtualizado = await _perfilUsuarioRepository.GetByIdAsync(dtoPerfilUsuario.Id);

                return _mapper.Map<DtoPerfilUsuario>(perfilAtualizado);
            }
        }

        public int BuscaPerfilAdminId()
        {
            return _perfilUsuarioRepository.BuscarPerfilAdminId();
        }
    }
}
