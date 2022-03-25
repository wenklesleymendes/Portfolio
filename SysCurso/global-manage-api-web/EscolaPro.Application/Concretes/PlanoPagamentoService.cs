using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.PlanoPagamentoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class PlanoPagamentoService : IPlanoPagamentoService
    {
        private readonly IMapper _mapper;
        private readonly IPlanoPagamentoRepository _planoPagamento;
        private readonly IPlanoPagamentoCursoRepository _planoPlamentoCursoRepository;
        private readonly IPlanoPagamentoUnidadeRepository _planoPagamentoUnidadeRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly ICursoRepository _cursoRepository;

        public PlanoPagamentoService(
            IMapper mapper,
            IPlanoPagamentoRepository planoPagamento,
            IPlanoPagamentoCursoRepository planoPlamentoCursoRepository,
            IPlanoPagamentoUnidadeRepository planoPagamentoUnidadeRepository,
            IUnidadeRepository unidadeRepository,
            ICursoRepository cursoRepository)
        {
            _mapper = mapper;
            _planoPagamento = planoPagamento;
            _planoPagamentoUnidadeRepository = planoPagamentoUnidadeRepository;
            _planoPlamentoCursoRepository = planoPlamentoCursoRepository;
            _unidadeRepository = unidadeRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<DtoPlanoPagamento> AtivarOuDesativar(int idPlanoPagamento)
        {
            var planoPagamento = await _planoPagamento.GetByIdAsync(idPlanoPagamento);

            planoPagamento.IsActive = planoPagamento.IsActive ? planoPagamento.IsActive = false : planoPagamento.IsActive = true;

            int id = await _planoPagamento.UpdateAsync(planoPagamento);
            var campanhaRetorno = await _planoPagamento.GetByIdAsync(idPlanoPagamento);
            return _mapper.Map<DtoPlanoPagamento>(campanhaRetorno);
        }

        public async Task<IEnumerable<DtoPlanoPagamento>> BuscarPlanoPagamento(int formaPagamento, int? quantidadeParcela, int cursoId, int unidadeId)
        {
            try
            {
                List<DtoPlanoPagamento> dtoPlanoPagamentos = new List<DtoPlanoPagamento>();

                var planoIds = await _planoPagamento.BuscarPlanoPagamento(formaPagamento, quantidadeParcela, cursoId, unidadeId);

                foreach (var item in planoIds)
                {
                    var planoPagamento = await BuscarPorId(item.Id);

                    dtoPlanoPagamentos.Add(planoPagamento);
                }

                return dtoPlanoPagamentos.OrderBy(x => x.QuantidadeParcela);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoPlanoPagamento>> BuscarPorCursoUnidade(int cursoId, int unidadeId)
        {
            try
            {
                List<DtoPlanoPagamento> dtoPlanoPagamentos = new List<DtoPlanoPagamento>();

                var planoIds = await _planoPagamento.PorCursoUnidade(cursoId, unidadeId);

                foreach (var item in planoIds)
                {
                    var planoPagamento = await BuscarPorId(item.Id);

                    dtoPlanoPagamentos.Add(planoPagamento);
                }

                return dtoPlanoPagamentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoPlanoPagamento> BuscarPorId(int idPlanoPagamento)
        {
            try
            {
                var planoPagamento = await _planoPagamento.BuscarPorId(idPlanoPagamento);

                //var planoPagamentoRetorno = _mapper.Map<DtoPlanoPagamento>(planoPagamento);

                //var idUnidades = await _planoPagamentoUnidadeRepository.BuscarPorPlanoPagamento(idPlanoPagamento);

                //planoPagamentoRetorno.Unidade = new List<DtoUnidadeTurma>();

                //foreach (var item in idUnidades)
                //{
                //    var unidade = await _unidadeRepository.BuscarPorId(item.UnidadeId);

                //    planoPagamentoRetorno.Unidade.Add(new Dto.UnidadeVO.DtoUnidadeTurma { Id = unidade.Id, Nome = unidade.Nome });
                //}

                //var idsCursos = await _planoPlamentoCursoRepository.BuscarPorPlanoPagamento(idPlanoPagamento);

                //planoPagamentoRetorno.Curso = new List<Dto.DtoCurso>();

                //foreach (var item in idsCursos)
                //{
                //    var curso = await _cursoRepository.BuscarPorId(item.CursoId);
                //    planoPagamentoRetorno.Curso.Add(new Dto.DtoCurso { Id = curso.Id, Descricao = curso.Descricao });
                //}

                return _mapper.Map<DtoPlanoPagamento>(planoPagamento); //planoPagamentoRetorno;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DtoPlanoPagamento>> BuscarPorIdCurso(int idCurso)
        {
            var planoPagamentosIds = await _planoPlamentoCursoRepository.BuscarPorCursoId(idCurso);

            List<DtoPlanoPagamento> dtoPlanoPagamentos = new List<DtoPlanoPagamento>();

            foreach (var item in planoPagamentosIds.ToList())
            {
                var planoPagamento = await _planoPagamento.GetByIdAsync(item.PlanoPagamentoId);

                dtoPlanoPagamentos.Add(_mapper.Map<DtoPlanoPagamento>(planoPagamento));
            }


            return dtoPlanoPagamentos;
        }

        public async Task<IEnumerable<DtoPlanoPagamento>> BuscarTodos()
        {
            var planos = await _planoPagamento.GetAllAsync();

            List<DtoPlanoPagamento> planosRetorno = new List<DtoPlanoPagamento>();

            foreach (var item in planos.Where(x => !x.IsDelete))
            {
                var planoPagamento = await BuscarPorIdAlterar(item.Id);

                planosRetorno.Add(planoPagamento);
            }

            return planosRetorno;
        }

        public async Task<bool> Deletar(int idPlanoPagamento)
        {
            var planoPagamento = await _planoPagamento.GetByIdAsync(idPlanoPagamento);
            planoPagamento.DeletedAt = DateTime.Now;
            planoPagamento.IsDelete = true;
            var id = await _planoPagamento.UpdateAsync(planoPagamento);
            return id > 0 ? true : false;
        }

        public async Task<DtoPlanoPagamento> Inserir(DtoPlanoPagamento model)
        {
            try
            {
                DtoPlanoPagamento planoPagamento = new DtoPlanoPagamento();

                if (model.Id == 0)
                {
                    var retorno = await _planoPagamento.AddAsync(_mapper.Map<PlanoPagamento>(model));

                    planoPagamento = _mapper.Map<DtoPlanoPagamento>(retorno);
                }
                else
                {
                    var id = await _planoPagamento.UpdateAsync(_mapper.Map<PlanoPagamento>(model));

                    var retorno = await _planoPagamento.GetByIdAsync(model.Id);

                    planoPagamento = _mapper.Map<DtoPlanoPagamento>(retorno);

                    var planoCursosDelete = await _planoPlamentoCursoRepository.BuscarPorPlanoPagamento(planoPagamento.Id);

                    foreach (var item in planoCursosDelete.ToList())
                    {
                        await _planoPlamentoCursoRepository.Deletar(item);
                    }

                    var planoUnidadesDelete = await _planoPagamentoUnidadeRepository.BuscarPorPlanoPagamento(planoPagamento.Id);

                    foreach (var item in planoUnidadesDelete.ToList())
                    {
                        await _planoPagamentoUnidadeRepository.Deletar(item);
                    }
                }


                foreach (var item in model.Curso)
                {
                    await _planoPlamentoCursoRepository.AddAsync(new PlanoPagamentoCurso { CursoId = item.Id, PlanoPagamentoId = planoPagamento.Id });
                }

                foreach (var item in model.Unidade)
                {
                    await _planoPagamentoUnidadeRepository.AddAsync(new PlanoPagamentoUnidade { UnidadeId = item.Id, PlanoPagamentoId = planoPagamento.Id });
                }

                return await BuscarPorId(planoPagamento.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoPlanoPagamento> BuscarPorIdAlterar(int idPlanoPagamento)
        {
            try
            {
                var planoPagamento = await _planoPagamento.BuscarPorId(idPlanoPagamento);

                var curso = new List<Curso>();
                foreach (var item in planoPagamento.PlanoPagamentoCurso)
                {
                    curso.Add(await _cursoRepository.BuscarPorId(item.CursoId));
                }

                var dtoCurso = new List<DtoCurso>();
                for (int i = 0; i < curso.Count; i++)
                {
                    dtoCurso.Add(_mapper.Map<DtoCurso>(curso[i]));
                }

                var unidade = new List<Unidade>();
                foreach (var item in planoPagamento.PlanoPagamentoUnidade)
                {
                    unidade.Add(await _unidadeRepository.BuscarPorId(item.UnidadeId));
                }

                var dtoUnidadeTurma = new List<DtoUnidadeTurma>();
                for (int i = 0; i < unidade.Count; i++)
                {
                    dtoUnidadeTurma.Add(_mapper.Map<DtoUnidadeTurma>(unidade[i]));
                }

                DtoPlanoPagamento dtoPlanoPagamento = new DtoPlanoPagamento
                {
                    Id = planoPagamento.Id,
                    TipoPagamento = planoPagamento.TipoPagamento,
                    QuantidadeParcela = planoPagamento.QuantidadeParcela,
                    ValorParcela = planoPagamento.ValorParcela,
                    ValorTotalPlano = planoPagamento.ValorTotalPlano,
                    PorcentagemDescontoPontualidade = planoPagamento.PorcentagemDescontoPontualidade,
                    ValorTotalInscricaoProva = planoPagamento.ValorTotalInscricaoProva,
                    ValorMaterialDidatico = planoPagamento.ValorMaterialDidatico,
                    IsentarMaterialDidatico = planoPagamento.IsentarMaterialDidatico,
                    ValorTaxaMatricula = planoPagamento.ValorTaxaMatricula,
                    IsentarMatricula = planoPagamento.IsentarMatricula,
                    IsActive = planoPagamento.IsActive,
                    Curso = dtoCurso,
                    Unidade = dtoUnidadeTurma,
                };

                return dtoPlanoPagamento;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
