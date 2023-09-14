using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class VideoAulaService : IVideoAulaService
    {
        private readonly IVideoAulaRepository _videoAulaRepository;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IMapper _mapper;
        public VideoAulaService(
            IVideoAulaRepository videoAulaRepository,
            IMateriaRepository materiaRepository,
            IMapper mapper)
        {
            _videoAulaRepository = videoAulaRepository;
            _materiaRepository = materiaRepository;
            _mapper = mapper;
        }

        public async Task<DtoVideoAula> BuscarPorId(int videoAulaId)
        {
            try
            {
                var retorno = await _videoAulaRepository.GetByIdAsync(videoAulaId);

                return _mapper.Map<DtoVideoAula>(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoGridGeneric<DtoVideoAula>> BuscarPorMateria(int materiaId)
        {
            try
            {
                var videoAulaLista = await _videoAulaRepository.BuscarPorMateria(materiaId);

                var materia = await _materiaRepository.GetByIdAsync(materiaId);

                DtoGridGeneric<DtoVideoAula> gridGeneric = new Dto.AulaOnlineVO.DtoGridGeneric<DtoVideoAula>
                {
                    Titulo = materia.NomeMateria,
                    Lista = _mapper.Map<IEnumerable<DtoVideoAula>>(videoAulaLista.Where(x => !x.IsDelete))
                };

                return gridGeneric;//_mapper.Map<IEnumerable<DtoVideoAula>>(videoAulaLista.Where(x => !x.IsDelete));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int videoAulaId)
        {
            try
            {
                var videoAula = await _videoAulaRepository.GetByIdAsync(videoAulaId);
                videoAula.IsDelete = true;
                var sucesso = await _videoAulaRepository.UpdateAsync(videoAula);
                return sucesso > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoVideoAula> Inserir(DtoVideoAula dtoVideoAula)
        {
            try
            {
                if (dtoVideoAula.Id == 0)
                {
                    var retorno = await _videoAulaRepository.AddAsync(_mapper.Map<VideoAula>(dtoVideoAula));

                    return await BuscarPorId(retorno.Id);
                }
                else
                {
                    await _videoAulaRepository.UpdateAsync(_mapper.Map<VideoAula>(dtoVideoAula));

                    return await BuscarPorId(dtoVideoAula.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoVideoPausado> SalvarUltimaSessao(DtoVideoPausado videoPausado)
        {
            try
            {
                var videoPausadoRetorno = await _videoAulaRepository.SalvarUltimaSessao(_mapper.Map<VideoPausado>(videoPausado));

                return _mapper.Map<DtoVideoPausado>(videoPausadoRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DtoVideoPausado> BuscarUltimaSessao(int matriculaId)
        {
            try
            {
                var videoPausadoRetorno = await _videoAulaRepository.BuscarUltimaSessao(matriculaId);

                return _mapper.Map<DtoVideoPausado>(videoPausadoRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
