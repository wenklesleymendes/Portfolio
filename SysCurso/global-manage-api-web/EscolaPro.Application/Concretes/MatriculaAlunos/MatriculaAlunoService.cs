using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.ProfessorVO;
using EscolaPro.Service.Dto.DocumentosAlunoVO;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.UnidadeVO;
using Microsoft.VisualBasic;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using EscolaPro.Service.Dto.DisparoSmsVO;
using EscolaPro.Service.Helpers;
using EscolaPro.Repository.Interfaces;

namespace EscolaPro.Service.Concretes.MatriculaAlunos
{
    public class MatriculaAlunoService : IMatriculaAlunoService
    {
        private readonly IMapper _mapper;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IAlunoService _alunoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IAnexoService _anexoService;
        private readonly ITurmaService _turmaService;
        private readonly ICursoService _cursoService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IUnidadeService _unidadeService;
        private readonly ISolicitacaoAlunoRepository _solicitacaoAlunoRepository;
        private readonly IInconsistenciaDocumentoRepository _inconsistenciaDocumentoRepository;
        private readonly IDisparoSmsService _disparoSmsService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public MatriculaAlunoService(
            IMapper mapper,
            IMatriculaAlunoRepository matriculaAlunoRepository,
            IAlunoService alunoService,
            IAnexoService anexoService,
            IUsuarioService usuarioService,
            ITurmaService turmaService,
            ICursoService cursoService,
            IUnidadeService unidadeService,
            ISolicitacaoAlunoRepository solicitacaoAlunoRepository,
            IInconsistenciaDocumentoRepository inconsistenciaDocumentoRepository,
            IFuncionarioService funcionarioService,
            IDisparoSmsService disparoSmsService,
            IEmailSenderService emailSenderService,
            IWhatsAppService whatsAppService,
            IAlunoRepository alunoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _alunoService = alunoService;
            _anexoService = anexoService;
            _usuarioService = usuarioService;
            _turmaService = turmaService;
            _cursoService = cursoService;
            _funcionarioService = funcionarioService;
            _solicitacaoAlunoRepository = solicitacaoAlunoRepository;
            _inconsistenciaDocumentoRepository = inconsistenciaDocumentoRepository;
            _unidadeService = unidadeService;
            _disparoSmsService = disparoSmsService;
            _emailSenderService = emailSenderService;
            _whatsAppService = whatsAppService;
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<DtoListaMatriculaGrid>> BuscarMinhasMatriculas(int alunoId, int usuarioLogadoId)
        {
            try
            {
                var usuario = await _usuarioService.BuscarPorId(usuarioLogadoId);

                var matriculaLista = await _matriculaAlunoRepository.BuscarMinhasMatriculas(alunoId, usuarioLogadoId);

                List<DtoListaMatriculaGrid> matriculaGrids = new List<DtoListaMatriculaGrid>();

                if (usuario.IsAluno)
                {
                    foreach (var matricula in matriculaLista)
                    {
                        if (!string.IsNullOrEmpty(matricula.NumeroMatricula))
                        {
                            var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                            matriculaGrids.Add(new DtoListaMatriculaGrid
                            {
                                MatriculaId = matricula.Id,
                                Unidade = matricula.Unidade.Nome,
                                StatusMatricula = matricula.Status,
                                Ano = matricula.Turma.Ano,
                                Curso = matricula.Curso.Descricao,
                                NumeroMatricula = matricula.NumeroMatricula,
                                Semestre = matricula.Turma.Semestre,
                                TurmaId = matricula.TurmaId,
                                CursoId = matricula.CursoId,
                                NacionalTec = matricula.Curso.NacionatalTec,
                                UnidadeId = matricula.UnidadeId.Value,
                                AlunoId = matricula.AlunoId
                            });
                        }
                    }
                }
                else
                {
                    foreach (var matricula in matriculaLista.Where(x => !usuario.PerfilUsuario.VerTodasUnidades ? x.UnidadeId.Value == usuario.Unidade.Id : true))
                    {

                        var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                        matriculaGrids.Add(new DtoListaMatriculaGrid
                        {
                            MatriculaId = matricula.Id,
                            Unidade = matricula.Unidade.Nome,
                            StatusMatricula = matricula.Status,
                            Ano = matricula.Turma.Ano,
                            Curso = matricula.Curso.Descricao,
                            NumeroMatricula = matricula.NumeroMatricula,
                            Semestre = matricula.Turma.Semestre,
                            TurmaId = matricula.TurmaId,
                            CursoId = matricula.CursoId,
                            NacionalTec = matricula.Curso.NacionatalTec,
                            UnidadeId = matricula.UnidadeId.Value
                        });

                    }
                }

                return matriculaGrids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AtivarAluno(int matriculaAlunoId)
        {
            
                var matricula = await _matriculaAlunoRepository.BuscarPorId(matriculaAlunoId);
                var aluno = await _alunoRepository.BuscarPorId(matricula.AlunoId);
                if (!aluno.IsActive)
                {
                    aluno.IsActive = true;
                    await _alunoRepository.UpdateAsync(aluno);                    
                }
                var usuario = await _usuarioRepository.BuscarPorAlunoId(aluno.Id);
                if (!usuario.IsActive)
                {
                    usuario.IsActive = true;
                    await _usuarioRepository.UpdateAsync(usuario);
                }
        }
        public async Task<DtoMatriculaAlunoResponse> BuscarPorId(int matriculaId)
        {
            var matricula = await _matriculaAlunoRepository.BuscarPorId(matriculaId);

            var matriculaRetorno = _mapper.Map<DtoMatriculaAlunoResponse>(matricula);

            var documentosPendentes = await ConsultarDocumentosPendentes(matricula);

            matriculaRetorno.QuantidadeDocumentosPendentes = documentosPendentes.DocumentosPendentes.Count();

            var historicoSolicitacaoAluno = await _solicitacaoAlunoRepository.BuscarHistorico(matriculaId);

            List<DtoSolicitacaoAluno> dtoSolicitacaoAlunos = new List<DtoSolicitacaoAluno>();

            matriculaRetorno.ExistePendenciaSolicitacaoAnexo = false;

            foreach (var item in historicoSolicitacaoAluno.Where(x => x.Solicitacao.IsAnexo))
            {
                var anexoId = await _anexoService.ExisteAnexo(item.Id);

                if (anexoId == 0)
                {
                    matriculaRetorno.ExistePendenciaSolicitacaoAnexo = true;
                }
            }

            return matriculaRetorno;
        }

        public async Task<DtoMatriculaAlunoResponse> MatricularAluno(DtoMatriculaAluno dtoMatriculaAluno)
        {
            try
            {
                dtoMatriculaAluno.DataMatricula = DateTime.Now.Date;
                dtoMatriculaAluno.NumeroMatricula = string.Empty;
                dtoMatriculaAluno.Status = true;

                var usuario = await _usuarioService.BuscarPorId(dtoMatriculaAluno.UsuarioLogadoId);

                //TO DO Mudar esse metodo para quando for gerado o financeiro do aluno 
                var aluno = await _alunoService.BuscarPorId(dtoMatriculaAluno.AlunoId);

                if (usuario.UserName == "admin")
                {
                    dtoMatriculaAluno.UnidadeId = aluno.UnidadeId;
                }
                else
                {
                    dtoMatriculaAluno.UnidadeId = usuario.Unidade.Id;
                }

                var matricula = await _matriculaAlunoRepository.AddAsync(_mapper.Map<MatriculaAluno>(dtoMatriculaAluno));

                var usuarioExistente = await _usuarioService.BuscarPorAlunoId(matricula.AlunoId);

                if (usuarioExistente == null)
                {
                    await _usuarioService.Inserir(new Dto.ControleUsuarioVO.DtoUsuarioRequest
                    {
                        UserName = aluno.CPF,
                        Password = aluno.CPF.Substring(0, 4),
                        CPF = aluno.CPF,
                        IsActive = true
                    }, true);
                }
                
                var matricularAluno = await BuscarPorId(matricula.Id);

                return matricularAluno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoDocumentosPendentes> ConsultarDocumentosPendentes(MatriculaAluno matriculaAluno)
        {
            try
            {
                var anexos = await _anexoService.BuscarPorFiltro(new AnexoFiltrar
                {
                    MatriculaAlunoId = matriculaAluno.Id
                });

                var documentosPendentesLista = await ValidarDocumentos(anexos.ToList(), matriculaAluno);

                var pendenciaDocumental = await _anexoService.DownloadDocumentoPorTipoEnum(matriculaAluno.Id, TipoAnexoEnum.ContratoProcuracaoEja);

                DtoDocumentosPendentes dtoDocumentos = new DtoDocumentosPendentes
                {
                    DeclaracaoPendenciaDocumental = pendenciaDocumental == null ? true : false,
                    DocumentosPendentes = documentosPendentesLista
                };

                return dtoDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<TipoAnexoEnum>> ValidarDocumentos(List<DtoAnexo> documentos, MatriculaAluno matriculaAluno)
        {
            try
            {

                var inconsistenciaDocumentos = await _inconsistenciaDocumentoRepository.BuscarPorMatricula(matriculaAluno.Id);

                List<TipoAnexoEnum> listaDocumentos = new List<TipoAnexoEnum>();

                var matricula = await _matriculaAlunoRepository.BuscarPorId(matriculaAluno.Id);

                if (!matricula.Curso.NacionatalTec)
                {
                    listaDocumentos.Add(TipoAnexoEnum.Redacao);
                }

                listaDocumentos.Add(TipoAnexoEnum.CopiaRG); // Estágiario
                listaDocumentos.Add(TipoAnexoEnum.CopiaCPF); // CLT
                listaDocumentos.Add(TipoAnexoEnum.Foto3X4); // CLT e Estágiario
                listaDocumentos.Add(TipoAnexoEnum.ComprovanteEndereco);
                listaDocumentos.Add(TipoAnexoEnum.HistoricoEscolar);
                listaDocumentos.Add(TipoAnexoEnum.CertidaoNascimento);

                foreach (var item in inconsistenciaDocumentos)
                {
                    var tipoAnexo = (TipoAnexoEnum)item.DocumentoEnum;

                    if (tipoAnexo == TipoAnexoEnum.ComprovanteAlfabetizacao)
                    {
                        if (!listaDocumentos.Exists(x => x == TipoAnexoEnum.ComprovanteAlfabetizacao))
                        {
                            if (matricula.Curso.Descricao.ToUpper() != "ENSINO MÉDIO")
                            {
                                listaDocumentos.Add(tipoAnexo);
                            }
                        }
                    }
                    else
                    {
                        listaDocumentos.Add(tipoAnexo);
                    }
                }

                // apenas para homens e maiores de 18 anos
                var maiorIdade = Convert.ToDateTime(matricula.Aluno.DataNascimento).AddYears(18) < DateTime.Now;
                var menor45 = Convert.ToDateTime(matricula.Aluno.DataNascimento).AddYears(45) > DateTime.Now;

                if (matricula.Aluno.Sexo == Core.Model.SexoEnum.Masculino && maiorIdade && menor45)
                {
                    listaDocumentos.Add(TipoAnexoEnum.Reservista);
                }

                // Alunos maiores de 18 anos
                if (maiorIdade)
                {
                    listaDocumentos.Add(TipoAnexoEnum.TituloEleitor);
                }

                if (!maiorIdade)
                {
                    listaDocumentos.Add(TipoAnexoEnum.CopiaCPFResponsavel);
                    listaDocumentos.Add(TipoAnexoEnum.CopiaRGResponsavel);
                    listaDocumentos.Add(TipoAnexoEnum.CopiaRGcomCPFResponsavel);
                }

                foreach (var item in documentos)
                {
                    var remove = documentos.Where(x => x.TipoAnexo == item.TipoAnexo && !x.IsRecusado && !x.IsDelete).FirstOrDefault();

                    if (remove != null)
                    {
                        switch (remove.TipoAnexo)
                        {
                            case TipoAnexoEnum.CopiaRGcomCPFResponsavel:
                                if (!maiorIdade)
                                {
                                    listaDocumentos.Remove(TipoAnexoEnum.CopiaCPFResponsavel);
                                    listaDocumentos.Remove(TipoAnexoEnum.CopiaRGResponsavel);
                                    listaDocumentos.Remove(TipoAnexoEnum.CopiaRGcomCPFResponsavel);
                                }
                                break;
                            case TipoAnexoEnum.RG_CPF:
                                listaDocumentos.Remove(TipoAnexoEnum.CopiaCPF);
                                listaDocumentos.Remove(TipoAnexoEnum.CopiaRG);
                                break;
                            case TipoAnexoEnum.RG_CPF_TituloEleitor:
                                listaDocumentos.Remove(TipoAnexoEnum.CopiaCPF);
                                listaDocumentos.Remove(TipoAnexoEnum.CopiaRG);
                                listaDocumentos.Remove(TipoAnexoEnum.TituloEleitor);
                                break;
                            case TipoAnexoEnum.CopiaRGResponsavel:
                                if (!maiorIdade)
                                {
                                    listaDocumentos.Remove(TipoAnexoEnum.CopiaRGResponsavel);
                                }
                                break;
                            case TipoAnexoEnum.CopiaCPFResponsavel:
                                if (!maiorIdade)
                                {
                                    listaDocumentos.Remove(TipoAnexoEnum.CopiaCPFResponsavel);
                                }
                                break;
                            case TipoAnexoEnum.ComprovanteAlfabetizacao:
                                listaDocumentos.Remove(TipoAnexoEnum.ComprovanteAlfabetizacao);
                                listaDocumentos.Remove(TipoAnexoEnum.HistoricoEscolar);
                                break;
                            default:
                                listaDocumentos.Remove(remove.TipoAnexo);
                                break;
                        }
                    }
                }

                if (!maiorIdade)
                {
                    if (listaDocumentos.Exists(x => x != TipoAnexoEnum.CopiaCPFResponsavel && x != TipoAnexoEnum.CopiaRGResponsavel))
                    {
                        listaDocumentos.Remove(TipoAnexoEnum.CopiaRGcomCPFResponsavel);
                    }
                }

                return listaDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoGridProfessorTurma>> ConsultarMeusProfessores(int matriculaId)
        {
            try
            {
                var matricula = await _matriculaAlunoRepository.GetByIdAsync(matriculaId);

                List<DtoGridProfessorTurma> gridProfessorTurmas = new List<DtoGridProfessorTurma>();

                var curso = await _cursoService.BuscarPorId(matricula.CursoId);

                foreach (var item in curso.Materia)
                {
                    var professor = await _funcionarioService.BuscarPorMateria(item.Id);
                }

                return gridProfessorTurmas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoDocumentoAluno> GerarDocumentosPendencia(int matriculaId)
        {
            try
            {
                var matriculaAluno = await _matriculaAlunoRepository.BuscarPorId(matriculaId);

                var aluno = await _alunoService.BuscarPorId(matriculaAluno.AlunoId);

                var curso = await _cursoService.BuscarPorId(matriculaAluno.CursoId);

                var documentosPendentes = await ConsultarDocumentosPendentes(matriculaAluno);

                var unidade = await _unidadeService.BuscarPorId(matriculaAluno.UnidadeId.Value);

                DtoDocumentoAluno dtoDocumentoAluno = new DtoDocumentoAluno
                {
                    Aluno = _mapper.Map<DtoAluno>(matriculaAluno.Aluno),
                    Unidade = unidade,
                    Curso = _mapper.Map<DtoCurso>(matriculaAluno.Curso),
                    Documentos = documentosPendentes.DocumentosPendentes
                };

                return dtoDocumentoAluno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoTurma> BuscarMinhaTurma(int matriculaId)
        {
            try
            {
                var matricula = await BuscarPorId(matriculaId);

                var turma = await _turmaService.BuscarPorId(matricula.TurmaId);

                turma.Curso.RemoveAll(x => x.Id != matricula.CursoId);
                turma.Unidade = new List<DtoUnidadeTurma>();

                return turma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> QuantidadeMatriculasCadastradas(int turmaId)
        {
            try
            {
                var quantidadeVagas = await _matriculaAlunoRepository.QuantidadeMatriculasCadastradas(turmaId);

                return quantidadeVagas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int matriculaId)
        {
            try
            {
                var matricula = await _matriculaAlunoRepository.GetByIdAsync(matriculaId);
                matricula.IsDelete = true;
                var id = await _matriculaAlunoRepository.UpdateAsync(matricula);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoMatriculaAlunoResponse> GerarNumeroMatricula(DtoMatriculaAlunoResponse matricula, PlanoPagamentoAluno planoPagamentoAluno, bool rollback = false)
        {
            try
            {
                var stringMatricula = _matriculaAlunoRepository.BuscarUltimaMatricula(matricula.UnidadeId);
                var numeroMatricula = 1;

                if (!string.IsNullOrEmpty(stringMatricula))
                    numeroMatricula = int.Parse(stringMatricula) + 1;

                while (await _matriculaAlunoRepository.VerificarMatriculaExistente(numeroMatricula.ToString(), matricula.UnidadeId))
                {
                    numeroMatricula++;
                }

                matricula.NumeroMatricula = numeroMatricula.ToString();

                if (rollback)
                {
                    matricula.NumeroMatricula = "";
                }

                await _matriculaAlunoRepository.SalvarPlanoPagamento(_mapper.Map<MatriculaAluno>(matricula), planoPagamentoAluno);
                return matricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> JaExistenteMatricula(int alunoId, int usuarioLogadoId)
        {
            try
            {
                var matriculas = await _matriculaAlunoRepository.BuscarMinhasMatriculas(alunoId, usuarioLogadoId, false);

                var usuarioLogado = await _usuarioService.BuscarPorId(usuarioLogadoId);

                if (usuarioLogado.UserName == "admin")
                {
                    return true;
                }
                else
                {
                    if (matriculas.Where(x => x.UnidadeId == usuarioLogado.Unidade.Id).Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(MatriculaAluno matriculaAluno)
        {
            try
            {
                await _matriculaAlunoRepository.UpdateAsync(matriculaAluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoMatriculaAlunoResponse> BuscarPorIdSimples(int id)
        {
            try
            {
                var matricula = await _matriculaAlunoRepository.BuscarPorId(id);

                return _mapper.Map<DtoMatriculaAlunoResponse>(matricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SalvarPlanoPagamento(MatriculaAluno matriculaAluno, PlanoPagamentoAluno planoPagamentoAluno)
        {
            try
            {
                await _matriculaAlunoRepository.SalvarPlanoPagamento(matriculaAluno, planoPagamentoAluno);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool VerificarEnsinoMedio(int matriculaId)
        {
            try
            {
                return _matriculaAlunoRepository.VerificarEnsinoMedio(matriculaId);
            }
            catch
            {
                throw;
            }
        }
    }
}