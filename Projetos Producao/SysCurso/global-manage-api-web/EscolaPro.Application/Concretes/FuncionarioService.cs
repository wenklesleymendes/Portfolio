using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.Funcionario;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IFeriasFuncionarioRepository _feriasFuncionarioRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IAnexoRepository _anexoRepository;
        private readonly ICursoProfessorRepository _cursoProfessorRepository;
        private readonly IMateriaCursoProfessorRepository _materiaCursoProfessorRepository;
        private readonly ICursoService _cursoService;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IInstituicaoBancariaRepository _instituicaoBancariaRepository;
        private readonly IMapper _mapper;

        public FuncionarioService(
            IFuncionarioRepository funcionarioRepository,
            IUnidadeRepository unidadeRepository,
            IAnexoRepository anexoRepository,
            IFeriasFuncionarioRepository feriasFuncionarioRepository,
            ICursoProfessorRepository cursoProfessorRepository,
            IMateriaCursoProfessorRepository materiaCursoProfessorRepository,
            IInstituicaoBancariaRepository instituicaoBancariaRepository,
            ICursoService cursoService,
            IMateriaRepository materiaRepository,
            IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _unidadeRepository = unidadeRepository;
            _anexoRepository = anexoRepository;
            _feriasFuncionarioRepository = feriasFuncionarioRepository;
            _cursoProfessorRepository = cursoProfessorRepository;
            _materiaCursoProfessorRepository = materiaCursoProfessorRepository;
            _cursoService = cursoService;
            _materiaRepository = materiaRepository;
            _instituicaoBancariaRepository = instituicaoBancariaRepository;
            _mapper = mapper;
        }

        public async Task<DtoFuncionario> AtivarOuDesativar(int idFuncionario)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(idFuncionario);

            funcionario.IsActive = funcionario.IsActive ? funcionario.IsActive = false : funcionario.IsActive = true;

            int id = await _funcionarioRepository.UpdateAsync(funcionario);
            var funcionarioRetorno = await _funcionarioRepository.GetByIdAsync(idFuncionario);
            return _mapper.Map<DtoFuncionario>(funcionarioRetorno);
        }

        public async Task<IEnumerable<DtoFuncionarioGrid>> BuscarTodosPorFiltro(int? idUnidade, string nome, bool? ativo, string cpf, DateTime? dataInicioTerminoContrato, DateTime? dataFimTerminoTerminoContrato)
        {
            try
            {
                var funcionarios = await _funcionarioRepository.BuscarTodosPorFiltro(idUnidade, nome, ativo, cpf, dataInicioTerminoContrato, dataFimTerminoTerminoContrato);

                List<DtoFuncionarioGrid> funcionarioList = new List<DtoFuncionarioGrid>();

                foreach (var item in funcionarios.Where(x => !x.IsDelete))
                {
                    var funcionario = await _funcionarioRepository.BuscarPorId(item.Id);

                    var funcionarioGrid = new DtoFuncionarioGrid
                    {
                        Id = funcionario.Id,
                        CPF = !string.IsNullOrEmpty(funcionario.CPF) ? funcionario.CPF : "",
                        DataContratacao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataAtestadoAdmissao : null,
                        NomeColaborador = !string.IsNullOrEmpty(funcionario.Nome) ? funcionario.Nome : "",
                        RegimeContratacao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.TipoRegimeContratacao : 0,
                        IsActive = funcionario.IsActive
                    };

                    if (funcionario.Nome != "admin")
                    {
                        switch (funcionario.DadosContratacao.TipoRegimeContratacao)
                        {
                            case RegimeContratacaoEnum.CLT_SEG_SEX:
                            case RegimeContratacaoEnum.CLT_SEG_SAB:
                            case RegimeContratacaoEnum.PROFESSOR_CLT:
                                funcionarioGrid.DataRecisao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataAtestadoDemissao : null;
                                break;
                            case RegimeContratacaoEnum.ESTAGIO_SEG_SEX:
                            case RegimeContratacaoEnum.PROFESSOR_AUTONOMO:
                            case RegimeContratacaoEnum.PROFISSIONAL_AUTONOMO:
                            case RegimeContratacaoEnum.ESTAGIO_SEG_SAB:
                                funcionarioGrid.DataRecisao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataRecisao : null;
                                break;
                            default:
                                break;
                        }
                    }

                    if (funcionario.Nome != "admin")
                    {
                        funcionarioGrid.TempoTrabalhado = new CalculaDiferencaDatas(funcionarioGrid.DataContratacao.Value, DateTime.Now);

                        funcionarioGrid.Unidade = new List<string>();

                        if (funcionario.SalarioUnidade != null)
                        {
                            foreach (var salarioUnidade in funcionario.SalarioUnidade)
                            {
                                var unidade = await _unidadeRepository.BuscarPorId(salarioUnidade.UnidadeId);
                                funcionarioGrid.Unidade.Add(unidade.Nome);
                            }
                        }

                        var documentos = await _anexoRepository.BuscarAnexo(new Core.Model.Anexos.AnexoFiltrar { IdFuncionario = funcionario.Id });

                        funcionarioGrid.Documentos = ValidarDocumentos(documentos.Where(x => !x.IsDelete).ToList(), funcionario.DadosContratacao.TipoRegimeContratacao);
                    }

                    funcionarioList.Add(funcionarioGrid);
                }

                return funcionarioList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoFuncionario> BuscarPorCPF(string cpf)
        {
            try
            {
                var retorno = await _funcionarioRepository.BuscarPorCPF(cpf);

                var funcionario = await BuscarPorId(retorno.Id);

                var banco = await _instituicaoBancariaRepository.BuscarPorCodigoBanco(funcionario.DadosBancario.CodigoBanco);

                funcionario.DadosBancario = new DtoDadosBancario
                {
                    CodigoBanco = banco.CodigoBanco,
                    NomeBanco = banco.NomeBanco,
                    NumeroAgencia = funcionario.DadosBancario.NumeroAgencia,
                    NumeroConta = funcionario.DadosBancario.NumeroConta,
                    TipoContaBancaria = funcionario.DadosBancario.TipoContaBancaria
                };

                return funcionario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoFuncionario> BuscarPorId(int idFuncionario)
        {
            var funcionario = await _funcionarioRepository.BuscarPorId(idFuncionario);

            List<DtoSalarioUnidade> salarioUnidadeLista = new List<DtoSalarioUnidade>();

            foreach (var item in funcionario.SalarioUnidade)
            {
                var unidade = await _unidadeRepository.BuscarPorId(item.UnidadeId);

                salarioUnidadeLista.Add(new DtoSalarioUnidade
                {
                    FuncionarioId = item.FuncionarioId,
                    NomeUnidade = unidade.Nome,
                    UnidadeId = item.UnidadeId,
                    ValorSalario = item.ValorSalario,
                    DescricaoCargo = item.DescricaoCargo
                });
            }

            var funcionarioRetorno = _mapper.Map<DtoFuncionario>(funcionario);

            funcionarioRetorno.SalarioUnidade = salarioUnidadeLista;

            /// CursoProfessor
            if (funcionario.DadosContratacao.TipoRegimeContratacao == RegimeContratacaoEnum.PROFESSOR_AUTONOMO ||
                funcionario.DadosContratacao.TipoRegimeContratacao == RegimeContratacaoEnum.PROFESSOR_CLT
                )
            {
                List<DtoCursoProfessor> cursoProfessorLista = new List<DtoCursoProfessor>();

                var cursosProfessor = await _cursoProfessorRepository.BuscarPorFuncionarioId(idFuncionario);

                foreach (var cursoItem in cursosProfessor)
                {
                    DtoCursoProfessor dtoCurso = new DtoCursoProfessor();
                    dtoCurso = _mapper.Map<DtoCursoProfessor>(cursoItem);
                    var curso = await _cursoService.BuscarPorId(dtoCurso.IdCurso);
                    dtoCurso.NomeCurso = curso.Descricao;
                    dtoCurso.MateriaCursoProfessor = new List<DtoMateriaCursoProfessor>();

                    var materias = await _materiaCursoProfessorRepository.BuscarPorCurso(cursoItem.Id);

                    foreach (var item in materias)
                    {
                        var materia = await _materiaRepository.GetByIdAsync(item.IdMateria);
                        DtoMateriaCursoProfessor dtoMateriaCursoProfessor = new DtoMateriaCursoProfessor();
                        dtoMateriaCursoProfessor = _mapper.Map<DtoMateriaCursoProfessor>(item);
                        dtoMateriaCursoProfessor.NomeMateria = materia.NomeMateria;

                        dtoCurso.MateriaCursoProfessor.Add(dtoMateriaCursoProfessor);
                    }

                    cursoProfessorLista.Add(dtoCurso);
                }

                funcionarioRetorno.CursoProfessor = new List<DtoCursoProfessor>();
                funcionarioRetorno.CursoProfessor = cursoProfessorLista;
            }

            var banco = await _instituicaoBancariaRepository.BuscarPorCodigoBanco(funcionario.DadosBancario.CodigoBanco);

            funcionarioRetorno.DadosBancario = new DtoDadosBancario
            {
                CodigoBanco = banco.CodigoBanco,
                NomeBanco = banco.NomeBanco,
                NumeroAgencia = funcionario.DadosBancario.NumeroAgencia,
                NumeroConta = funcionario.DadosBancario.NumeroConta,
                TipoContaBancaria = funcionario.DadosBancario.TipoContaBancaria
            };

            return funcionarioRetorno;
        }

        public async Task<IEnumerable<DtoFuncionarioGrid>> BuscarTodos()
        {
            try
            {
                List<DtoFuncionarioGrid> funcionarioList = new List<DtoFuncionarioGrid>();

                var funcionarios = await _funcionarioRepository.GetAllAsync();

                foreach (var item in funcionarios.Where(x => !x.IsDelete))
                {
                    var funcionario = await _funcionarioRepository.BuscarPorId(item.Id);

                    var funcionarioGrid = new DtoFuncionarioGrid
                    {
                        Id = funcionario.Id,
                        CPF = !string.IsNullOrEmpty(funcionario.CPF) ? funcionario.CPF : "",
                        DataContratacao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataAtestadoAdmissao : null,
                        DataRecisao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataRecisao : null,
                        NomeColaborador = !string.IsNullOrEmpty(funcionario.Nome) ? funcionario.Nome : "",
                        RegimeContratacao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.TipoRegimeContratacao : 0,
                        IsActive = funcionario.IsActive
                    };

                    funcionarioGrid.TempoTrabalhado = new CalculaDiferencaDatas(funcionarioGrid.DataContratacao.Value, DateTime.Now);

                    funcionarioGrid.Unidade = new List<string>();

                    if (item.SalarioUnidade != null)
                    {
                        foreach (var salarioUnidade in item.SalarioUnidade)
                        {
                            var unidade = await _unidadeRepository.BuscarPorId(salarioUnidade.UnidadeId);
                            funcionarioGrid.Unidade.Add(unidade.Nome);
                        }
                    }

                    var documentos = await _anexoRepository.BuscarAnexo(new Core.Model.Anexos.AnexoFiltrar { IdFuncionario = item.Id });

                    funcionarioGrid.Documentos = ValidarDocumentos(documentos.Where(x => !x.IsDelete).ToList(), funcionario.DadosContratacao.TipoRegimeContratacao);

                    funcionarioList.Add(funcionarioGrid);
                }

                return funcionarioList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Deletar(int idFuncionario)
        {
            try
            {
                var funcionario = await _funcionarioRepository.GetByIdAsync(idFuncionario);
                funcionario.IsDelete = true;
                int id = await _funcionarioRepository.UpdateAsync(funcionario);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletarFerias(int idFerias)
        {
            try
            {
                var ferias = await _feriasFuncionarioRepository.GetByIdAsync(idFerias);
                ferias.IsDelete = true;
                int id = await _feriasFuncionarioRepository.UpdateAsync(ferias);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoFuncionario> Inserir(DtoFuncionario dtoFuncionario)
        {
            Funcionario funcionario = new Funcionario();

            if (dtoFuncionario.Id == 0)
            {
                funcionario = await _funcionarioRepository.AddAsync(_mapper.Map<Funcionario>(dtoFuncionario));

                return await BuscarPorId(funcionario.Id);
            }
            else
            {
                if (dtoFuncionario != null)
                {
                    if (dtoFuncionario.DadosContratacao != null)
                    {
                        switch (dtoFuncionario.DadosContratacao.TipoRegimeContratacao)
                        {
                            case RegimeContratacaoEnum.CLT_SEG_SEX:
                            case RegimeContratacaoEnum.PROFESSOR_CLT:
                            case RegimeContratacaoEnum.CLT_SEG_SAB:
                                if (dtoFuncionario.DadosContratacao.DataAtestadoDemissao.HasValue)
                                {
                                    dtoFuncionario.IsActive = false;
                                }
                                break;
                            case RegimeContratacaoEnum.ESTAGIO_SEG_SEX:
                            case RegimeContratacaoEnum.PROFESSOR_AUTONOMO:
                            case RegimeContratacaoEnum.PROFISSIONAL_AUTONOMO:
                            case RegimeContratacaoEnum.ESTAGIO_SEG_SAB:
                                if (dtoFuncionario.DadosContratacao.DataRecisao.HasValue)
                                {
                                    dtoFuncionario.IsActive = false;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

                await _funcionarioRepository.Atualizar(_mapper.Map<Funcionario>(dtoFuncionario));

                List<SalarioUnidade> salarioUnidades = new List<SalarioUnidade>();

                foreach (var item in dtoFuncionario.SalarioUnidade)
                {
                    salarioUnidades.Add(new SalarioUnidade
                    {
                        UnidadeId = item.UnidadeId,
                        FuncionarioId = dtoFuncionario.Id,
                        ValorSalario = item.ValorSalario,
                        DescricaoCargo = item.DescricaoCargo

                    });
                }

                await _funcionarioRepository.InserirSalarioUnidade(salarioUnidades);

                /// Atualiza todos os cursos e materias antigos
                /// 
                var cursosAntigos = await _cursoProfessorRepository.BuscarPorFuncionarioId(dtoFuncionario.Id);

                foreach (var item in cursosAntigos)
                {
                    var materiasAntigas = await _materiaCursoProfessorRepository.BuscarPorCurso(item.Id);

                    await _materiaCursoProfessorRepository.RemoveRangeAsync(materiasAntigas);

                    await _cursoProfessorRepository.RemoveAsync(item);
                }

                if (dtoFuncionario.CursoProfessor != null)
                {
                    foreach (var cursoProfessorItem in dtoFuncionario.CursoProfessor)
                    {
                        CursoProfessor cursoProfessor = _mapper.Map<CursoProfessor>(cursoProfessorItem);
                        cursoProfessor.FuncionarioId = dtoFuncionario.Id;

                        var curso = await _cursoProfessorRepository.AddAsync(cursoProfessor);
                    }
                }

                return await BuscarPorId(dtoFuncionario.Id);
            }
        }

        private List<TipoAnexoEnum> ValidarDocumentos(List<Anexo> documentos, RegimeContratacaoEnum regimeContratacao)
        {
            try
            {


                List<TipoAnexoEnum> listaDocumentos = new List<TipoAnexoEnum>();

                listaDocumentos.Add(TipoAnexoEnum.TermoCompromissoEstagio); // Estagiario
                listaDocumentos.Add(TipoAnexoEnum.ContratoTrabalho); // CLT
                listaDocumentos.Add(TipoAnexoEnum.ExameMedico); // CLT e Estagiario
                listaDocumentos.Add(TipoAnexoEnum.ComprovanteEndereco);
                listaDocumentos.Add(TipoAnexoEnum.CopiaRG);
                listaDocumentos.Add(TipoAnexoEnum.CopiaCPF);
                listaDocumentos.Add(TipoAnexoEnum.CertidaoNascimento);
                listaDocumentos.Add(TipoAnexoEnum.ContratoPrestacaoServico);


                switch (regimeContratacao)
                {
                    case RegimeContratacaoEnum.CLT_SEG_SEX:
                    case RegimeContratacaoEnum.CLT_SEG_SAB:
                    case RegimeContratacaoEnum.PROFESSOR_CLT:
                        var documentosPendentesCLT = documentos.Where(x =>
                        x.TipoAnexo == TipoAnexoEnum.ContratoTrabalho ||
                        x.TipoAnexo == TipoAnexoEnum.ExameMedico ||
                        x.TipoAnexo == TipoAnexoEnum.CopiaCPF ||
                        x.TipoAnexo == TipoAnexoEnum.CopiaRG ||
                        x.TipoAnexo == TipoAnexoEnum.ComprovanteEndereco ||
                        x.TipoAnexo == TipoAnexoEnum.CertidaoNascimento
                        ).ToList();

                        listaDocumentos.Remove(TipoAnexoEnum.TermoCompromissoEstagio);
                        listaDocumentos.Remove(TipoAnexoEnum.ContratoPrestacaoServico);

                        foreach (var item in documentosPendentesCLT)
                        {
                            listaDocumentos.Remove(item.TipoAnexo);
                        }

                        break;
                    case RegimeContratacaoEnum.ESTAGIO_SEG_SEX:
                    case RegimeContratacaoEnum.ESTAGIO_SEG_SAB:

                        var documentosPendentesEstag = documentos.Where(x =>
                        x.TipoAnexo == TipoAnexoEnum.TermoCompromissoEstagio ||
                       x.TipoAnexo == TipoAnexoEnum.CopiaCPF ||
                       x.TipoAnexo == TipoAnexoEnum.CopiaRG ||
                       x.TipoAnexo == TipoAnexoEnum.ComprovanteEndereco
                       ).ToList();

                        listaDocumentos.Remove(TipoAnexoEnum.ContratoTrabalho);
                        listaDocumentos.Remove(TipoAnexoEnum.ExameMedico);
                        listaDocumentos.Remove(TipoAnexoEnum.CertidaoNascimento);
                        listaDocumentos.Remove(TipoAnexoEnum.ContratoPrestacaoServico);

                        foreach (var item in documentosPendentesEstag)
                        {
                            listaDocumentos.Remove(item.TipoAnexo);
                        }
                        break;
                    case RegimeContratacaoEnum.PROFESSOR_AUTONOMO:
                    case RegimeContratacaoEnum.PROFISSIONAL_AUTONOMO:
                        var documentosPendentesProfessor = documentos.Where(x =>
                          x.TipoAnexo == TipoAnexoEnum.CopiaCPF ||
                          x.TipoAnexo == TipoAnexoEnum.CopiaRG ||
                          x.TipoAnexo == TipoAnexoEnum.ComprovanteEndereco ||
                          x.TipoAnexo == TipoAnexoEnum.ContratoPrestacaoServico
                          ).ToList();

                        listaDocumentos.Remove(TipoAnexoEnum.ContratoTrabalho);
                        listaDocumentos.Remove(TipoAnexoEnum.ExameMedico);
                        listaDocumentos.Remove(TipoAnexoEnum.CertidaoNascimento);
                        listaDocumentos.Remove(TipoAnexoEnum.TermoCompromissoEstagio);

                        foreach (var item in documentosPendentesProfessor)
                        {
                            listaDocumentos.Remove(item.TipoAnexo);
                        }
                        break;
                    case RegimeContratacaoEnum.AUTONOMO_PRE_CLT_SEG_SEX:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_CLT_SEG_SAB:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_ESTAGIO_SEG_SEX:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_ESTAGIO_SEG_SAB:
                        var documentosPendentesPreEstagio = documentos.Where(x =>
                          x.TipoAnexo == TipoAnexoEnum.ComprovanteEndereco ||
                          x.TipoAnexo == TipoAnexoEnum.CopiaRG ||
                          x.TipoAnexo == TipoAnexoEnum.CopiaCPF ||
                          x.TipoAnexo == TipoAnexoEnum.ContratoPrestacaoServico
                          ).ToList();

                        listaDocumentos.Remove(TipoAnexoEnum.TermoCompromissoEstagio); // Estagiario
                        listaDocumentos.Remove(TipoAnexoEnum.ContratoTrabalho); // CLT
                        listaDocumentos.Remove(TipoAnexoEnum.ExameMedico); // CLT e Estagiario
                        listaDocumentos.Remove(TipoAnexoEnum.CertidaoNascimento);

                        foreach (var item in documentosPendentesPreEstagio)
                        {
                            listaDocumentos.Remove(item.TipoAnexo);
                        }
                        break;
                    default:
                        break;
                }

                if (listaDocumentos.Exists(x => x == TipoAnexoEnum.RG_CPF))
                {
                    listaDocumentos.Remove(TipoAnexoEnum.CopiaRG);
                    listaDocumentos.Remove(TipoAnexoEnum.CopiaCPF);
                }

                return listaDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoFuncionario> BuscarPorMateria(int materiaId)
        {
            try
            {
                var funcionario = await _funcionarioRepository.BuscarPorMateria(materiaId);

                return _mapper.Map<DtoFuncionario>(funcionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
