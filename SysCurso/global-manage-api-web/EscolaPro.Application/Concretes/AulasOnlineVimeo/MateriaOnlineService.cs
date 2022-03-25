using AutoMapper;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces.AulasOnlineVimeo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.AulasOnlineVimeo
{
    public class MateriaOnlineService : IMateriaOnlineService
    {
        private readonly IMapper _mapper;
        private readonly IMateriaOnlineRepository _materiaOnlineRepository;
        private readonly IAulaOnlineRepository _aulaOnlineRepository;
        private readonly IMateriaRepository _materiaRepository;

        public MateriaOnlineService(IMapper mapper,
            IMateriaOnlineRepository materiaOnlineRepository,
            IAulaOnlineRepository aulaOnlineRepository,
            IMateriaRepository materiaRepository)
        {
            _mapper = mapper;
            _materiaOnlineRepository = materiaOnlineRepository;
            _aulaOnlineRepository = aulaOnlineRepository;
            _materiaRepository = materiaRepository;
        }

        public async Task<DtoGridGeneric<DtoMateriaOnline>> BuscarPorAulaOnline(int aulaOnlineId)
        {
            try
            {
                var materiaOnlineLista = await _materiaOnlineRepository.BuscarPorAulaOnline(aulaOnlineId);

                var aulaOnline = await _aulaOnlineRepository.GetByIdAsync(aulaOnlineId);

                var gridRetorno = new DtoGridGeneric<DtoMateriaOnline>()
                {
                    Titulo = aulaOnline.NomeAulaOnline,
                    Lista = _mapper.Map<IEnumerable<DtoMateriaOnline>>(materiaOnlineLista)
                };

                return gridRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoMateriaOnline> BuscarPorId(int materiaId)
        {
            try
            {
                var materiaOnline = await _materiaOnlineRepository.GetByIdAsync(materiaId);

                return _mapper.Map<DtoMateriaOnline>(materiaOnline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoMateriaOnline>> BuscarPorMateria(int materiaId)
        {
            try
            {
                var materiaOnline = await _materiaOnlineRepository.BuscarPorMateria(materiaId);

                return _mapper.Map<IEnumerable<DtoMateriaOnline>>(materiaOnline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoMateriaOnline>> BuscarTodos()
        {
            try
            {
                List<DtoMateriaOnline> materiaOnlineLista = new List<DtoMateriaOnline>();

                var materiasOnline = await _materiaOnlineRepository.GetAllAsync();

                foreach (var item in materiasOnline)
                {
                    var materiaOnline = await BuscarPorId(item.Id);

                    materiaOnlineLista.Add(_mapper.Map<DtoMateriaOnline>(materiaOnline));
                }

                return materiaOnlineLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int materiaId)
        {
            var materiaOnline = await _materiaOnlineRepository.GetByIdAsync(materiaId);
            materiaOnline.IsDelete = true;
            var id = await _materiaOnlineRepository.UpdateAsync(materiaOnline);
            return id > 0 ? true : false;
        }

        public async Task<DtoMateriaOnline> Inserir(DtoMateriaOnline dtoMateriaOnline)
        {
            try
            {
                if (dtoMateriaOnline.Id == 0)
                {
                    var materia = await _materiaOnlineRepository.AddAsync(_mapper.Map<MateriaOnline>(dtoMateriaOnline));

                    return await BuscarPorId(materia.Id);
                }
                else
                {
                    var materia = await _materiaOnlineRepository.UpdateAsync(_mapper.Map<MateriaOnline>(dtoMateriaOnline));
                    return await BuscarPorId(dtoMateriaOnline.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoMateria>> BuscarMateriasPorCurso(int cursoId)
        {
            try
            {
                var materiaLista = await _materiaRepository.BuscarPorIdCurso(cursoId);

                return _mapper.Map<IEnumerable<DtoMateria>>(materiaLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
