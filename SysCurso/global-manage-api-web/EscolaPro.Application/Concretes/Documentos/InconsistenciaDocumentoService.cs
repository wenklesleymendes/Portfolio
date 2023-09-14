using AutoMapper;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto.DocumentosAlunoVO;
using EscolaPro.Service.Interfaces.Documentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Documentos
{
    public class InconsistenciaDocumentoService : IInconsistenciaDocumentoService
    {
        private readonly IInconsistenciaDocumentoRepository _inconsistenciaDocumentoRepository;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IMapper _mapper;

        public InconsistenciaDocumentoService(
            IInconsistenciaDocumentoRepository inconsistenciaDocumentoRepository,
            IMatriculaAlunoRepository matriculaAlunoRepositorio,
            IMapper mapper)
        {
            _mapper = mapper;
            _inconsistenciaDocumentoRepository = inconsistenciaDocumentoRepository;
            _matriculaAlunoRepository = matriculaAlunoRepositorio;
        }

        public async Task<IEnumerable<DtoInconsistenciaDocumento>> BuscarPorMatriculaId(int matriculaId)
        {
            try
            {
                var retorno = await _inconsistenciaDocumentoRepository.BuscarPorMatricula(matriculaId);

                return _mapper.Map<IEnumerable<DtoInconsistenciaDocumento>>(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int[]> BuscarTodos(int matriculaId)
        {
            try
            {
                if (matriculaId != 0 && _matriculaAlunoRepository.VerificarEnsinoMedio(matriculaId))
                {
                    int[] documentosEnums = { 21, 34, 35, 36 };
                    return documentosEnums;
                }
                else {
                    int[] documentosEnums = { 21, 34, 35 };
                    return documentosEnums;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int inconsistenciaDocumentoId)
        {
            try
            {
                var inconsistenciaDocumento = await _inconsistenciaDocumentoRepository.GetByIdAsync(inconsistenciaDocumentoId);

                return await _inconsistenciaDocumentoRepository.RemoveAsync(inconsistenciaDocumentoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoInconsistenciaDocumento>> Inserir(DtoInconsistenciaDocumentoRequest dtoInconsistencia)
        {
            try
            {
                var inconsistenciaDocumentosOld = await _inconsistenciaDocumentoRepository.BuscarPorMatricula(dtoInconsistencia.MatriculaId);

                await _inconsistenciaDocumentoRepository.RemoveRangeAsync(inconsistenciaDocumentosOld);

                foreach (var tipoAnexo in dtoInconsistencia.TipoAnexoEnum)
                {

                    await _inconsistenciaDocumentoRepository.AddAsync(new InconsistenciaDocumento { DocumentoEnum = tipoAnexo, MatriculaAlunoId = dtoInconsistencia.MatriculaId });
                }

                return await BuscarPorMatriculaId(dtoInconsistencia.MatriculaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
