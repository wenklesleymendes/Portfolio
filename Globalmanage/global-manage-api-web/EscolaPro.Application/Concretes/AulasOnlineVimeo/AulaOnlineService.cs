using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class AulaOnlineService : IAulaOnlineService
    {
        private readonly IAulaOnlineRepository _aulaOnlineRepository;
        private readonly ICursoService _cursoService;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly IVideoAulaService _videoAulaService;
        private readonly IPerguntaService _perguntaService;
        private readonly IAnexoService _anexoService;
        private readonly IMapper _mapper;
        public AulaOnlineService(
            IAulaOnlineRepository aulaOnlineRepository,
            IMateriaRepository materiaRepository,
            ICursoService cursoService,
            IMatriculaAlunoService matriculaAlunoService,
            IVideoAulaService videoAulaService,
            IPerguntaService perguntaService,
            IAnexoService anexoService,
            IMapper mapper)
        {
            _aulaOnlineRepository = aulaOnlineRepository;
            _materiaRepository = materiaRepository;
            _cursoService = cursoService;
            _matriculaAlunoService = matriculaAlunoService;
            _videoAulaService = videoAulaService;
            _perguntaService = perguntaService;
            _anexoService = anexoService;
            _mapper = mapper;
        }

        public async Task<DtoGridMateriaOnline> BuscarMaterias(int aulaOnlineId)
        {
            try
            {
                var aulaOnline = await BuscarPorId(aulaOnlineId);

                DtoGridMateriaOnline materiaOnlineGrid = new DtoGridMateriaOnline();

                materiaOnlineGrid.AulaOnlineId = aulaOnline.Id;
                materiaOnlineGrid.NomeAulaOnline = aulaOnline.NomeAulaOnline;
                materiaOnlineGrid.MateriaOnline = new List<DtoMateriaOnline>();

                foreach (var item in aulaOnline.Curso)
                {
                    var materiaLista = await _materiaRepository.BuscarPorIdCurso(item.CursoId);

                    foreach (var materia in materiaLista)
                    {
                        materiaOnlineGrid.MateriaOnline.Add(new DtoMateriaOnline { Id = materia.Id, NomeCurso = item.NomeCurso, NomeMateria = materia.NomeMateria });
                    }
                }

                return materiaOnlineGrid;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<DtoAulaOnline> BuscarPorCurso(int cursoId)
        {
            try
            {
                var aulasOnline = await _aulaOnlineRepository.BuscarPorCurso(cursoId);

                DtoAulaOnline dtoAulaOnline = _mapper.Map<DtoAulaOnline>(aulasOnline);

                List<DtoCursoOnline> dtoCursos = new List<DtoCursoOnline>();

                if (aulasOnline.Curso != null)
                {
                    foreach (var item in aulasOnline.Curso)
                    {
                        var curso = await _cursoService.BuscarPorId(item.CursoId);

                        dtoCursos.Add(new DtoCursoOnline
                        {
                            Id = item.Id,
                            AulaOnlineId = item.AulaOnlineId,
                            CursoId = item.CursoId,
                            NomeCurso = curso.Descricao
                        });
                    }

                    dtoAulaOnline.Curso = dtoCursos;
                }
                else
                {
                    return new DtoAulaOnline();
                }

                return dtoAulaOnline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAulaOnline> BuscarPorId(int aulaOnlineId)
        {
            try
            {
                var aulasOnline = await _aulaOnlineRepository.BuscarPorId(aulaOnlineId);

                DtoAulaOnline dtoAulaOnline = _mapper.Map<DtoAulaOnline>(aulasOnline);

                List<DtoCursoOnline> dtoCursos = new List<DtoCursoOnline>();

                foreach (var item in aulasOnline.Curso)
                {
                    var curso = await _cursoService.BuscarPorId(item.CursoId);

                    dtoCursos.Add(new DtoCursoOnline
                    {
                        Id = item.Id,
                        AulaOnlineId = item.AulaOnlineId,
                        CursoId = item.CursoId,
                        NomeCurso = curso.Descricao
                    });
                }

                dtoAulaOnline.Curso = dtoCursos;

                return dtoAulaOnline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoAulaOnline>> BuscarTodos()
        {
            var aulasOnline = await _aulaOnlineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DtoAulaOnline>>(aulasOnline.Where(x => !x.IsDelete));
        }

        public async Task<bool> Excluir(int aulaOnlineId)
        {
            var aulaOnline = await _aulaOnlineRepository.GetByIdAsync(aulaOnlineId);
            aulaOnline.IsDelete = true;
            var sucesso = await _aulaOnlineRepository.UpdateAsync(aulaOnline);
            return sucesso > 0 ? true : false;
        }

        public async Task<DtoAulaOnline> Inserir(DtoAulaOnline dtoAulaOnline)
        {
            try
            {
                if (dtoAulaOnline.Id == 0)
                {
                    var aulaOnline = await _aulaOnlineRepository.AddAsync(_mapper.Map<AulaOnline>(dtoAulaOnline));

                    return _mapper.Map<DtoAulaOnline>(aulaOnline);
                }
                else
                {
                    await _aulaOnlineRepository.Atualizar(_mapper.Map<AulaOnline>(dtoAulaOnline));

                    var aulaOnline = await _aulaOnlineRepository.BuscarPorId(dtoAulaOnline.Id);

                    return _mapper.Map<DtoAulaOnline>(aulaOnline);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoGridMateriaOnline> MinhasAulasOnline(int matriculaId)
        {
            try
            {
                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                var aulaOnline = await BuscarPorCurso(matricula.CursoId);

                if(aulaOnline.Curso == null)
                {
                    return new DtoGridMateriaOnline();
                }

                var cursoAula = aulaOnline.Curso.Where(x => x.CursoId == matricula.CursoId).FirstOrDefault();

                var aulaOnline2 = await _cursoService.BuscarPorId(matricula.CursoId);

                DtoGridMateriaOnline dtoGridMateriaOnline = new DtoGridMateriaOnline
                {
                    AulaOnlineId = aulaOnline.Id,
                    //NomeAulaOnline = aulaOnline.NomeAulaOnline
                    //NomeAulaOnline = aulaOnline.Curso.FirstOrDefault().NomeCurso
                    NomeAulaOnline = aulaOnline2.NacionatalTec ? "NacionalTec" : "Supletivo Preparatório"
                };

                var materiaLista = await _materiaRepository.BuscarPorIdCurso(cursoAula.CursoId);

                dtoGridMateriaOnline.MateriaOnline = new List<DtoMateriaOnline>();
                var materiaOnline = new List<DtoMateriaOnline>();

                foreach (var materia in materiaLista)
                {
                    DtoMateriaOnline materiaRetorno = new DtoMateriaOnline 
                    { 
                        NomeMateria = materia.NomeMateria,
                        Id = materia.Id,
                        Ordenacao = materia.Ordenacao
                    };

                    var videoAulas = await _videoAulaService.BuscarPorMateria(materia.Id);

                    List<DtoVideoAula> dtoVideoAulas = new List<DtoVideoAula>();

                    foreach (var item in videoAulas.Lista)
                    {
                        var perguntas = await _perguntaService.BuscarPorVideoAula(item.Id);

                        List<DtoPergunta> dtoPerguntas = new List<DtoPergunta>();

                        foreach (var perguntaItem in perguntas.Lista)
                        {
                            DtoPergunta dtoPergunta = perguntaItem;

                            var anexoPergunta = await _anexoService.BuscarPorFiltro(new Core.Model.Anexos.AnexoFiltrar { PerguntaId = perguntaItem.Id });

                            if (anexoPergunta.Any())
                            {
                                dtoPergunta.AnexoId = anexoPergunta.FirstOrDefault().Id.Value;
                                dtoPergunta.Extensao = anexoPergunta.FirstOrDefault().Extensao;
                            }
                            
                            dtoPerguntas.Add(dtoPergunta);
                            item.Pergunta = dtoPerguntas;

                            List<DtoResposta> dtoRespostas = new List<DtoResposta>();

                            foreach (var respostaItem in perguntaItem.Resposta)
                            {
                                DtoResposta dtoResposta = new DtoResposta();

                                dtoResposta = respostaItem;

                                var anexoResposta = await _anexoService.BuscarPorFiltro(new Core.Model.Anexos.AnexoFiltrar { RespostaId = respostaItem.Id });

                                if (anexoResposta.Any())
                                {
                                    dtoResposta.AnexoId = anexoResposta.FirstOrDefault().Id.Value;
                                    dtoResposta.Extensao = anexoResposta.FirstOrDefault().Extensao;
                                }

                                dtoRespostas.Add(dtoResposta);
                            }
                        }

                        DtoVideoAula dtoVideoAula = item;

            

                       // dtoVideoAula.Pergunta = perguntas;

                        dtoVideoAulas.Add(dtoVideoAula);
                    }

                    materiaRetorno.VideoAula = dtoVideoAulas;

                    //dtoGridMateriaOnline.MateriaOnline.Add(materiaRetorno);
                    materiaOnline.Add(materiaRetorno);
                }

                var materiaOrdenada = materiaOnline.OrderBy(x => x.Ordenacao);

                dtoGridMateriaOnline.MateriaOnline.AddRange(materiaOrdenada);

                return dtoGridMateriaOnline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
