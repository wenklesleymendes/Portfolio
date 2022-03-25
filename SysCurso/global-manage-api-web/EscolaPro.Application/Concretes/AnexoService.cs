using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class AnexoService : IAnexoService
    {
        private readonly IAnexoRepository _anexoRepository;
        private readonly IMapper _mapper;

        public AnexoService(IAnexoRepository anexoRepository,
            IMapper mapper)
        {
            _anexoRepository = anexoRepository;
            _mapper = mapper;
        }

        public async Task<DtoAnexo> Atualizar(DtoAnexo dtoAnexo)
        {
            var id = await _anexoRepository.UpdateAsync(_mapper.Map<Anexo>(dtoAnexo));

            var anexo = await _anexoRepository.GetByIdAsync(id);

            return _mapper.Map<DtoAnexo>(anexo);
        }

        public async Task<DtoAnexo> BuscarComprovante(int matriculaId)
        {
            try
            {
                var anexo = await _anexoRepository.DownloadPorTipoAnexo(matriculaId, true);

                return _mapper.Map<DtoAnexo>(anexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoAnexo>> BuscarPorDespesa(int despesaId, bool documento)
        {
            try
            {
                var documentosAnexo = await _anexoRepository.BuscarPorIdDespesa(despesaId, documento);

                return _mapper.Map<IEnumerable<DtoAnexo>>(documentosAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoAnexo>> BuscarPorFiltro(AnexoFiltrar anexoFiltrar)
        {
            try
            {
                var anexo = await _anexoRepository.BuscarAnexo(anexoFiltrar);

                return _mapper.Map<IEnumerable<DtoAnexo>>(anexo.Where(x => !x.IsDelete));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> Deleter(int idAnexo)
        {
            var anexoDelete = await _anexoRepository.GetByIdAsync(idAnexo);
            anexoDelete.IsDelete = true;
            var idRemovido = await _anexoRepository.UpdateAsync(_mapper.Map<Anexo>(anexoDelete));
            return idRemovido > 0 ? true : false;
        }

        public async Task<bool> DeleterDocumento(int? matriculaAlunoId, TipoAnexoEnum tipoAnexo)
        {
            try
            {
                return await _anexoRepository.DeleterDocumento(matriculaAlunoId, tipoAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Download(int idAnexo)
        {
            try
            {
                var anexo = await _anexoRepository.DownloadArquivo(idAnexo);

                var dtoAnexo = _mapper.Map<DtoAnexo>(anexo);

                return Convert.ToBase64String(anexo.Arquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAnexo> DownloadDocumentoPorTipoEnum(int matriculaId, TipoAnexoEnum tipoAnexoEnum)
        {
            try
            {
                var anexo = await _anexoRepository.DownloadDocumentoPorTipoEnum(matriculaId, tipoAnexoEnum);

                return _mapper.Map<DtoAnexo>(anexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DownloadPorTipoAnexo(int matriculaId, bool isComprovante = false)
        {
            try
            {
                var anexo = await _anexoRepository.DownloadPorTipoAnexo(matriculaId, isComprovante);

                var dtoAnexo = _mapper.Map<DtoAnexo>(anexo);

                if (anexo != null)
                {
                    return Convert.ToBase64String(anexo.Arquivo);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ExisteAnexo(int solicitacaoId)
        {
            try
            {
                return await _anexoRepository.ExisteAnexo(solicitacaoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAnexo> Inserir(DtoAnexo dtoAnexo)
        {
            if (dtoAnexo != null)
            {
                if (dtoAnexo.Id == null)
                {
                    dtoAnexo.Id = 0;
                }
            }

            if (dtoAnexo.Id == 0)
            {
                var anexo = await _anexoRepository.AddAsync(_mapper.Map<Anexo>(dtoAnexo));

                return _mapper.Map<DtoAnexo>(anexo);
            }
            else
            {
                await _anexoRepository.UpdateAsync(_mapper.Map<Anexo>(dtoAnexo));

                var anexo = await _anexoRepository.GetByIdAsync(dtoAnexo.Id);

                return _mapper.Map<DtoAnexo>(anexo);
            }

        }

        public async Task<DtoAnexo> InserirPendenciaDocumental(DtoAnexo dtoAnexo)
        {
            try
            {
                var anexoOld = await _anexoRepository.BuscarAnexo(new AnexoFiltrar { MatriculaAlunoId = dtoAnexo.MatriculaAlunoId.Value });

                if (anexoOld.Any())
                {
                    foreach (var item in anexoOld)
                    {
                        if (item.TipoAnexo == TipoAnexoEnum.DeclaracaoPendenciaDocumental)
                        {
                            await Deleter(item.Id);
                        }
                    }
                }

                return await Inserir(dtoAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAnexo> RecusarDocumento(AnexoRecusar anexo)
        {
            try
            {
                var anexoRetorno = await _anexoRepository.BuscarPorId(anexo.AnexoId);

                anexoRetorno.Mensagem = anexo.Mensagem;
                anexoRetorno.IsRecusado = true;
                anexoRetorno.TipoRecusa = anexo.TipoRecusa;

                await _anexoRepository.UpdateAsync(anexoRetorno);

                return _mapper.Map<DtoAnexo>(anexoRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
