using AutoMapper;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.ProvasCertificados
{
    public class CertificadoProvaService : ICertificadoProvaService
    {

        private readonly IMapper _mapper;
        private readonly ICertificadoProvaRepository _certificadoProvaRepository;

        public CertificadoProvaService(IMapper mapper,
                                       ICertificadoProvaRepository certificadoProvaRepository)
        {
            _mapper = mapper;
            _certificadoProvaRepository = certificadoProvaRepository;
        }

        public async Task<DtoCertificadoProva> BuscarPorId(int certificadoProvaId)
        {
            var certificado = await _certificadoProvaRepository.GetByIdAsync(certificadoProvaId);
            return  _mapper.Map<DtoCertificadoProva>(certificado);
        }

        public async Task<IEnumerable<DtoCertificadoProva>> BuscarPorMatriculaId(int matriculaId)
        {
           
            IEnumerable<CertificadoProva> certificados = await _certificadoProvaRepository.BuscarPorMatriculaId(matriculaId);

            return _mapper.Map<IEnumerable<DtoCertificadoProva>>(certificados);

        }

        public async Task<DtoCertificadoProva> BuscarSolicitacaoAtual(int matriculaId)
        {
            var certificado = await _certificadoProvaRepository.BuscarSolicitacaoAtual(matriculaId);
            return _mapper.Map<DtoCertificadoProva>(certificado);
        }

        public async Task<DtoCertificadoProva> Inserir(DtoCertificadoProva dtoCertificadoProva)
        {
            CertificadoProva certificado;
           if (dtoCertificadoProva.Id == 0)
            {
                certificado = _mapper.Map<CertificadoProva>(dtoCertificadoProva);
                CertificadoProva retorno = await _certificadoProvaRepository.AddAsync(certificado);
                return _mapper.Map<DtoCertificadoProva>(retorno);
            }
            else
            {
                certificado = await _certificadoProvaRepository.GetByIdAsync(dtoCertificadoProva.Id);
                certificado.DataEntregaAluno = dtoCertificadoProva.DataEntregaAluno;
                certificado.DataRecebimentoSuporte = dtoCertificadoProva.DataRecebimentoSuporte;
                certificado.AnexoId = dtoCertificadoProva.AnexoId;
                certificado.GDAE = dtoCertificadoProva.GDAE;
                certificado.StatusCertificado = dtoCertificadoProva.StatusCertificado;
                certificado.UpdatedAt = DateTime.Now;

                await _certificadoProvaRepository.UpdateAsync(certificado);
                return _mapper.Map<DtoCertificadoProva>(certificado);
            }
        }
    }
}
