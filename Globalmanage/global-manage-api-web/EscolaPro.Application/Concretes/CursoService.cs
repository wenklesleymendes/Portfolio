using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using EscolaPro.Core.Model.CursoTurma;
using AutoMapper;
using EscolaPro.Service.Dto;

namespace EscolaPro.Service.Concretes
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITurmaCursoRepository _turmaCursoRepository;
        private readonly IUnidadeTurmaRepository _turmaUnidadeRepository;
        private readonly ITurmaRepository _turmaRepository;

        public CursoService(
            ICursoRepository repository,
            IMapper mapper,
            ITurmaCursoRepository turmaCursoRepository,
            IUnidadeTurmaRepository turmaUnidadeRepository,
            ITurmaRepository turmaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _turmaCursoRepository = turmaCursoRepository;
            _turmaUnidadeRepository = turmaUnidadeRepository;
            _turmaRepository = turmaRepository;
        }

        public async Task<bool> AtivarOuDesativar(int idCurso)
        {
            var curso = await _repository.GetByIdAsync(idCurso);

            curso.IsActive = curso.IsActive ? curso.IsActive = false : curso.IsActive = true;

            int id = await _repository.UpdateAsync(curso);
            var cursoRetorno = await _repository.GetByIdAsync(idCurso);

            return curso.IsActive;
        }

        public async Task<IEnumerable<Curso>> BuscarCursosComMateria()
        {
            var cursos = await _repository.BuscarCursosComMateria();

            return _mapper.Map<IEnumerable<Curso>>(cursos);
        }

        public async Task<DtoCurso> BuscarPorId(int idCurso)
        {
            var curso = await _repository.BuscarPorId(idCurso);

            return _mapper.Map<DtoCurso>(curso);
        }

        public async Task<IEnumerable<Curso>> BuscarTodos()
        {
            var cursos = await _repository.GetAllAsync();

            return cursos.Where(x => !x.IsDelete).ToList();
        }

        public async Task<bool> Deletar(int idCurso)
        {
            var curso = await _repository.GetByIdAsync(idCurso);
            curso.IsDelete = true;
            int id = await _repository.UpdateAsync(curso);
            return id > 0 ? true : false;
        }

        public async Task<bool> DeletarMateria(int id)
        {
            return await _repository.RemoverMateria(id);
        }

        public async Task<Curso> Inserir(Curso model)
        {
            if (model.Id == 0)
            {
                return await _repository.AddAsync(model);
            }
            else
            {
                await _repository.UpdateAsync(model);

                return await _repository.GetByIdAsync(model.Id);
            }

        }

        public async Task<IEnumerable<Materia>> InserirMateria(Materia materia)
        {
            return await _repository.InserirMateria(materia);
        }

        
        public async Task<IEnumerable<Curso>> BuscarPorUnidade(int unidadeId, int? usuarioLogadoId)
        {
            List<Curso> cursosFiltrados = new List<Curso>();
            List<TurmaUnidade> turmaUnidadesFiltrados = new List<TurmaUnidade>();
            List<TurmaUnidade> turmaUnidadesFiltrados2 = new List<TurmaUnidade>();
            List<TurmaCurso> turmaCursosFiltrados = new List<TurmaCurso>();
            List<TurmaCurso> turmaCursosFiltrados2 = new List<TurmaCurso>();

            var cursos = await _repository.GetAllAsync();
            var turmaCursoIEnumerable = await _turmaCursoRepository.GetAllAsync();
            var turmaUnidadeIEnumerable = await _turmaUnidadeRepository.GetAllAsync();

            var turmas = await _turmaRepository.GetAllAsync();
            turmas = turmas.Where(x => x.IsDelete == false);

            List<TurmaCurso> turmaCursosFiltrados3 = new List<TurmaCurso>();
            foreach (var item in turmas)
            {
                turmaCursosFiltrados3.AddRange(turmaCursoIEnumerable.Where(x => x.TurmaId == item.Id));
            }

            List<TurmaUnidade> turmaUnidadesFiltrados3 = new List<TurmaUnidade>();
            foreach (var item in turmas)
            {
                turmaUnidadesFiltrados3.AddRange(turmaUnidadeIEnumerable.Where(x => x.TurmaId == item.Id));
            }

            foreach (var item in cursos)
            {
                turmaCursosFiltrados.AddRange(turmaCursosFiltrados3.Where(x => x.CursoId == item.Id));
                foreach (var item2 in turmaCursosFiltrados)
                {
                    var turmaUnidade = turmaUnidadesFiltrados3.Where(y => y.TurmaId == item2.TurmaId);
                    var turmaUnidadeLista = turmaUnidade.ToList();
                    turmaUnidadesFiltrados.AddRange(turmaUnidadeLista);
                }
            }

            foreach (var item in turmaCursosFiltrados3)
            {
                turmaUnidadesFiltrados2.AddRange(turmaUnidadesFiltrados3.Where(x => x.TurmaId == item.TurmaId && x.UnidadeId == unidadeId));
            }

            foreach (var item in turmaUnidadesFiltrados2)
            {
                turmaCursosFiltrados2.AddRange(turmaCursosFiltrados.Where(x => x.TurmaId == item.TurmaId));
            }

            foreach (var item in turmaCursosFiltrados2)
            {
                cursosFiltrados.AddRange(cursos.Where(x => x.Id == item.CursoId));
            }

            var cursosFiltradosDistintos = cursosFiltrados.Distinct().ToList();

            return cursosFiltradosDistintos.Where(x => !x.IsDelete);
        }
    }
}
