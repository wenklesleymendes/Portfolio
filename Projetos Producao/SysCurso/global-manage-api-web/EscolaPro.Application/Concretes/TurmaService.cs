using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _repository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IUnidadeTurmaRepository _unidadeTurmaRepository;
        private readonly ITurmaCursoRepository _turmaCursoRepository;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IPerfilUsuarioService _perfilUsuarioService;

        public TurmaService(ITurmaRepository repository,
            ICursoRepository cursoRepository,
            IUnidadeTurmaRepository unidadeTurmaRepository,
            ITurmaCursoRepository turmaCursoRepository,
            IMatriculaAlunoRepository matriculaAlunoRepository,
            IUnidadeRepository unidadeRepository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            IPerfilUsuarioService perfilUsuarioService)
        {
            _cursoRepository = cursoRepository;
            _unidadeTurmaRepository = unidadeTurmaRepository;
            _turmaCursoRepository = turmaCursoRepository;
            _unidadeRepository = unidadeRepository;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _perfilUsuarioService = perfilUsuarioService;
        }

        public async Task<IEnumerable<DtoTurma>> BuscarPorCursoId(int cursoId, int unidadeId, int? usuarioLogadoId)
        {
            try
            {
                var usuarioLogado = await _usuarioRepository.BuscarPorId(usuarioLogadoId.Value);

                var permisaoUsuario = await _perfilUsuarioService.BuscarPorId((int)usuarioLogado.PerfilUsuarioId);

                IEnumerable<Turma> turmaLista;
                if (!permisaoUsuario.VerTodasUnidades)
                {
                    turmaLista = await _turmaCursoRepository.BuscarPorCursoId(cursoId, unidadeId, usuarioLogadoId);
                }
                else
                {
                    turmaLista = await _turmaCursoRepository.BuscarPorCursoId(cursoId, unidadeId, -1);
                }

                List<DtoTurma> turmaRetorno = new List<DtoTurma>();


                foreach (var item in turmaLista)
                {
                    var turma = await BuscarPorId(item.Id);

                    var curso = await _cursoRepository.GetByIdAsync(cursoId);

                    var quantidadeMatriculaCadastradas = await _matriculaAlunoRepository.QuantidadeMatriculasCadastradas(turma.Id);

                    turma.Curso.RemoveAll(x => x.Id != cursoId);
                    turma.Unidade.RemoveAll(x => x.Id != unidadeId);

                    turma.QuantidadeVagas = turma.QuantidadeVagas - quantidadeMatriculaCadastradas;

                    if (turma.Presencial)
                    {
                        if (turma.QuantidadeVagas > 0)
                        {
                            turmaRetorno.Add(turma);
                        }
                    }
                    else
                    {
                        turma.QuantidadeVagas = null;
                        turmaRetorno.Add(turma);
                    }
                }

                List<DtoTurma> turmaRetorno2 = new List<DtoTurma>();
                foreach (var item in turmaRetorno)
                {
                    if (item.Unidade.Count > 0)
                    {
                        foreach (var item2 in item.Unidade)
                        {
                            if (item2.Id == unidadeId)
                            {
                                turmaRetorno2.Add(item);
                            }
                        }
                    }
                }
                
                return turmaRetorno2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoTurma> BuscarPorId(int id)
        {
            var turmaRetorno = await _repository.GetByIdAsync(id);

            var turma = _mapper.Map<DtoTurma>(turmaRetorno);

            var unidadeTurma = await _unidadeTurmaRepository.BuscarPorIdTurma(id);

            turma.Unidade = new List<DtoUnidadeTurma>();

            foreach (var item in unidadeTurma)
            {
                var retorno = await _unidadeRepository.BuscarPorId(item.UnidadeId.Value);

                var unidade = _mapper.Map<DtoUnidadeTurma>(retorno);

                turma.Unidade.Add(unidade);
            }

            var cursoTurma = await _turmaCursoRepository.BuscarPorIdTurma(id);

            turma.Curso = new List<DtoCurso>();

            foreach (var item in cursoTurma)
            {
                var retorno = await _cursoRepository.GetByIdAsync(item.CursoId);

                var curso = _mapper.Map<DtoCurso>(retorno);

                turma.Curso.Add(curso);
            }

            return turma;
        }

        public async Task<List<DtoTurma>> BuscarTodos()
        {
            List<DtoTurma> turmasRetorno = new List<DtoTurma>();

            var turmas = await _repository.GetAllAsync();

            foreach (var item in turmas.Where(x => !x.IsDelete))
            {
                var turma = await BuscarPorId(item.Id);

                turmasRetorno.Add(turma);
            }

            return turmasRetorno;
        }

        public async Task<bool> Deletar(int idTurma)
        {
            var turma = await _repository.GetByIdAsync(idTurma);
            turma.IsDelete = true;
            int id = await _repository.UpdateAsync(turma);
            return id > 0 ? true : false;
        }

        public Task<IEnumerable<DtoTurma>> Filtrar(DtoTurmaFiltrar turma)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoTurma> Inserir(DtoTurma model)
        {
            var turma = _mapper.Map<Turma>(model);

            if (model.Id == 0)
            {
                turma = await _repository.AddAsync(turma);
            }
            else
            {
                await _repository.UpdateAsync(turma);
                turma = await _repository.GetByIdAsync(model.Id);

                var unidadeTurmas = await _unidadeTurmaRepository.BuscarPorIdTurma(turma.Id);

                foreach (var item in unidadeTurmas.ToList())
                {
                    item.TurmaId = turma.Id;
                    await _unidadeTurmaRepository.Deletar(item);
                }

                var turmaCursos = await _turmaCursoRepository.BuscarPorIdTurma(turma.Id);

                foreach (var item in turmaCursos.ToList())
                {
                    item.TurmaId = item.TurmaId;
                    await _turmaCursoRepository.Deletar(item);
                }
            }

            foreach (var item in model.Unidade)
            {
                await _unidadeTurmaRepository.AddAsync(new TurmaUnidade { UnidadeId = item.Id, TurmaId = turma.Id });
            }

            foreach (var item in model.Curso)
            {
                await _turmaCursoRepository.AddAsync(new TurmaCurso { CursoId = item.Id, TurmaId = turma.Id });
            }

            return await BuscarPorId(turma.Id);
        }

        public async Task<DtoTurma> TransferirDeTurma(DtoTransferirTurma turma)
        {
            try
            {
                var matricula = await _matriculaAlunoRepository.BuscarPorId(turma.MatriculaId);

                matricula.TurmaId = turma.TurmaId;
                matricula.CursoId = turma.CursoId;

                await _matriculaAlunoRepository.UpdateAsync(matricula);

                return await BuscarPorId(turma.TurmaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
