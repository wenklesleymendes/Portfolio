using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class MatriculaAlunoRepository : DomainRepository<MatriculaAluno>, IMatriculaAlunoRepository
    {
        private readonly IAnexoRepository _anexoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IInconsistenciaDocumentoRepository _inconsistenciaDocumentoRepository;
        private int contadorUpdate;

        public MatriculaAlunoRepository(ApplicationContext dbContext,
            IAnexoRepository anexoRepository,
            IUsuarioRepository usuarioRepository,
            IInconsistenciaDocumentoRepository inconsistenciaDocumentoRepository) : base(dbContext)
        {
            _anexoRepository = anexoRepository;
            _usuarioRepository = usuarioRepository;
            _inconsistenciaDocumentoRepository = inconsistenciaDocumentoRepository;
            contadorUpdate = 0;
        }

        public async Task<IEnumerable<MatriculaAluno>> BuscarMinhasMatriculas(int alunoId, int usuarioLogadoId, bool consultaDocumentos = false)
        {
            try
            {
                var usuario = await _usuarioRepository.BuscarPorId(usuarioLogadoId);

                if (consultaDocumentos)
                {

                    if (usuario.IsAluno)
                    {
                        IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => x.AlunoId == alunoId &&
                                                                                                    !x.IsDelete &&
                                                                                                     x.Status &&
                                                                                                    !string.IsNullOrEmpty(x.NumeroMatricula)), null)
                                                                  .Include(x => x.Curso)
                                                                  .Include(x => x.Turma)
                                                                  .Include(x => x.Unidade)
                                                                  .Include(x => x.Aluno).AsNoTracking());
                        return query.ToList();
                    }
                    else
                    {
                        IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => x.AlunoId == alunoId &&
                                                                    (!usuario.PerfilUsuario.VerTodasUnidades ? x.UnidadeId.Value == usuario.Unidade.Id : true) &&
                                                                                                    !x.IsDelete), null)
                                                                     .Include(x => x.Curso)
                                                                     .Include(x => x.Turma)
                                                                     .Include(x => x.Unidade)
                                                                     .Include(x => x.Aluno));
                        return query.ToList();
                    }
                }
                else
                {
                    IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => x.AlunoId == alunoId &&
                                                                                                   (x.Status ||
                                                                                                   !usuario.IsAluno) &&
                                                                                                  !x.IsDelete), null)
                                                                .Include(x => x.Curso)
                                                                .Include(x => x.Turma)
                                                                .Include(x => x.Unidade));
                    return query.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MatriculaAluno> BuscarPorId(int matriculaId)
        {
            try
            {
                IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => x.Id == matriculaId), null)
                                             .Include(x => x.Curso)
                                             .Include(x => x.Turma)
                                             .Include(x => x.Unidade)
                                             .Include(x => x.Aluno)
                                             .Include(x => x.PlanoPagamentoAluno)
                                             .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AlterarStatus(int matriculaId, bool status)
        {
            try
            {
                MatriculaAluno matricula = await this.BuscarPorId(matriculaId);
                matricula.Status = status;
                await this.UpdateAsync(matricula);
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> QuantidadeMatriculasCadastradas(int turmaId)
        {
            try
            {
                IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => x.TurmaId == turmaId && !string.IsNullOrEmpty(x.NumeroMatricula)), null).AsNoTracking());

                return query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MatriculaAluno> SalvarPlanoPagamento(MatriculaAluno matricula, PlanoPagamentoAluno planoPagamentoAluno)
        {
            try
            {
                dbContext.Entry<PlanoPagamentoAluno>(planoPagamentoAluno).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                await dbContext.SaveChangesAsync();

                matricula.NumeroMatricula = matricula.NumeroMatricula;
                matricula.PlanoPagamentoAlunoId = planoPagamentoAluno.Id;

                if (contadorUpdate == 0)
                {
                    await UpdateAsync(matricula);
                    contadorUpdate++;
                }
                else
                {
                    contadorUpdate = 0;
                }

                return await BuscarPorId(matricula.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> VerificarMatriculaExistente(string numeroMatricula, int unidadeId)
        {
            try
            {
                IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => x.NumeroMatricula == numeroMatricula && x.UnidadeId == unidadeId), null).AsNoTracking());

                return query.Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VerificarMatriculaExistente2(MatriculaAluno matricula)
        {
            try
            {
                var retorno = string.Empty;
                do
                {
                    retorno = dbContext.Set<MatriculaAluno>().Where(x => x.NumeroMatricula == matricula.NumeroMatricula)
                                                             .OrderByDescending(x => x.NumeroMatricula)
                                                             .Select(x => x.NumeroMatricula)
                                                             .AsNoTracking()
                                                             .FirstOrDefault();
                    if (!string.IsNullOrEmpty(retorno))
                    {
                        matricula.NumeroMatricula = (Convert.ToInt64(matricula.NumeroMatricula) + 1).ToString();
                    }
                    else
                    {
                        dbContext.Update(matricula);
                    }
                } while (!string.IsNullOrEmpty(retorno));

                return matricula.NumeroMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BuscarUltimaMatricula(int unidadeId)
        {
            try
            {
                return dbContext.Set<MatriculaAluno>()
                    .Where(x => x.UnidadeId == unidadeId)
                    .OrderByDescending(x => x.NumeroMatricula)
                    .Select(x => x.NumeroMatricula)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async Task<DocumentoPendente> ConsultarDocumentosPendentes(MatriculaAluno matriculaAluno)
        {
            try
            {
                var anexos = await _anexoRepository.BuscarAnexo(new AnexoFiltrar
                {
                    MatriculaAlunoId = matriculaAluno.Id
                });

                var documentosPendentesLista = await ValidarDocumentos(anexos.ToList(), matriculaAluno);

                var pendenciaDocumental = await _anexoRepository.DownloadDocumentoPorTipoEnum(matriculaAluno.Id, TipoAnexoEnum.ContratoProcuracaoEja);

                DocumentoPendente dtoDocumentos = new DocumentoPendente
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

        private async Task<List<TipoAnexoEnum>> ValidarDocumentos(List<Anexo> documentos, MatriculaAluno matricula)
        {
            try
            {

                var inconsistenciaDocumentos = await _inconsistenciaDocumentoRepository.BuscarPorMatricula(matricula.Id);

                List<TipoAnexoEnum> listaDocumentos = new List<TipoAnexoEnum>();

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
                            if (matricula.Curso.Descricao.ToUpper() == "ENSINO MÉDIO")
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
                var menor45 = Convert.ToDateTime(matricula.Aluno.DataNascimento).AddYears(-45) > DateTime.Now;

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

        public bool VerificarEnsinoMedio(int matriculaId)
        {
            return dbContext.Set<MatriculaAluno>()
                .AsNoTracking()
                .Include(x => x.Curso)
                .Any(x => x.Id == matriculaId && x.Curso.Descricao == "Ensino Médio");
        }

        public MatriculaAluno GetMatriculaProva(int matriculaId)
        {
            return dbContext.Set<MatriculaAluno>()
                .AsNoTracking()
                .Include(x => x.PlanoPagamentoAluno)
                .Include(x => x.Turma)
                .Include(x => x.Aluno)
                .Where(x => x.Id == matriculaId)
                .Select(x => new MatriculaAluno()
                {
                    Id = x.Id,
                    PlanoPagamentoAluno = x.PlanoPagamentoAluno,
                    Turma = x.Turma,
                    Aluno = x.Aluno
                })
                .FirstOrDefault();
        }

        public MatriculaAluno GetInformacoesEmail(int matriculaId)
        {
            return dbContext.Set<MatriculaAluno>()
                .AsNoTracking()
                .Include(x => x.Aluno)
                    .ThenInclude(x => x.Contato)
                .Include(x => x.Unidade)
                    .ThenInclude(x => x.Endereco)
                .Include(x => x.Unidade)
                    .ThenInclude(x => x.Contato)
                .Include(x => x.Curso)
                .Where(x => x.Id == matriculaId)
                .Select(x => new MatriculaAluno()
                {
                    Aluno = x.Aluno,
                    Unidade = x.Unidade,
                    Curso = x.Curso
                })
                .FirstOrDefault();
        }



        public async Task<IList<MatriculaAluno>> GetRange(IEnumerable<int> ids)
        {
            try
            {
                IQueryable<MatriculaAluno> query = await Task.FromResult(GenerateQuery((x => ids.Contains(x.Id)), null)
                                                             .Include(x=> x.Aluno)
                                                             .Include(x=> x.Unidade)
                                                             .AsNoTracking());

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
