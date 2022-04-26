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
using EscolaPro.Service.Dto.UnidadeVO;
using AutoMapper;
using EscolaPro.Service.Dto.AlunosVO;

namespace EscolaPro.Service.Concretes
{
    public class UnidadeService : IUnidadeService
    {
        private readonly IUnidadeRepository _repository;
        private readonly IUnidadeDespesaRepository _unidadeDespesaRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IDadosBancarioRepository _dadosBancarioRepository;
        private readonly IContatoRepository _contatoRepository;
        private readonly IHistoricoOcorrenciasRepository _historicoOcorrenciasRepository;
        private readonly IContratoLocacaoRepository _contratoLocacaoRepository;
        private readonly IHorarioFuncionamentoRepository _horarioFuncionamentoRepository;
        private readonly ICentroCustoRepository _centroCustoRepository;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UnidadeService(
            IUnidadeRepository repository,
            IUnidadeDespesaRepository unidadeDespesaRepository,
            IEnderecoRepository enderecoRepository,
            IDadosBancarioRepository dadosBancarioRepository,
            IContatoRepository contatoRepository,
            IHistoricoOcorrenciasRepository historicoOcorrenciasRepository,
            IContratoLocacaoRepository contratoLocacaoRepository,
            IHorarioFuncionamentoRepository horarioFuncionamentoRepository,
            ICentroCustoRepository centroCustoRepository,
            IAnexoRepository anexoRepository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper
            )
        {
            _repository = repository;
            _unidadeDespesaRepository = unidadeDespesaRepository;
            _enderecoRepository = enderecoRepository;
            _dadosBancarioRepository = dadosBancarioRepository;
            _contatoRepository = contatoRepository;
            _historicoOcorrenciasRepository = historicoOcorrenciasRepository;
            _contratoLocacaoRepository = contratoLocacaoRepository;
            _horarioFuncionamentoRepository = horarioFuncionamentoRepository;
            _centroCustoRepository = centroCustoRepository;
            _anexoRepository = anexoRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<DtoUnidadeResponse> BuscarPorId(int idUnidade, bool editar = true)
        {
            var unidade = await _repository.BuscarPorId(idUnidade);

            if (editar)
            {
                var anexos = await _anexoRepository.BuscarAnexo(new Core.Model.Anexos.AnexoFiltrar { IdUnidade = unidade.Id });

                unidade.Anexo = anexos.Where(x => !x.IsDelete).ToList();
            }
            else
            {
                unidade.Anexo = new List<Anexo>();
            }

            return _mapper.Map<DtoUnidadeResponse>(unidade);
        }

        public async Task<IEnumerable<DtoUnidadeResponse>> BuscarTodos(int usuarioLogadoId)
        {
            try
            {
                var usuarioLogado = await _usuarioRepository.BuscarPorId(usuarioLogadoId);

                List<Unidade> unidadeLista = new List<Unidade>();

                if (!usuarioLogado.PerfilUsuario.VerTodasUnidades)
                {
                    if (usuarioLogado.UserName == "admin")
                    {
                        var unidades = await _repository.BuscarTodos();

                        foreach (var item in unidades)
                        {
                            unidadeLista.Add(item);
                        }
                    }
                    else
                    {
                        var unidade = await BuscarPorId(usuarioLogado.UnidadeId.Value, false);

                        unidadeLista.Add(_mapper.Map<Unidade>(unidade));
                    }
                }
                else
                {
                    var unidades = await _repository.BuscarTodos();

                    foreach (var item in unidades)
                    {
                        unidadeLista.Add(item);
                        //var unidade = await BuscarPorId(item.Id, false);

                        //unidadeLista.Add(_mapper.Map<Unidade>(unidade));
                    }
                }

                var unidadeList = _mapper.Map<List<DtoUnidadeResponse>>(unidadeLista);

                return unidadeList.Where(x => !x.IsDelete).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoUnidadeResponse>> BuscarUnidadesTicket(int usuarioLogadoId)
        {
            try
            {
                var usuarioLogado = await _usuarioRepository.BuscarPorId(usuarioLogadoId);

                List<Unidade> unidadeLista = new List<Unidade>();

                if (!usuarioLogado.PerfilUsuario.VerTodasUnidades)
                {
                    if (usuarioLogado.UserName == "admin")
                    {
                        var unidades = await _repository.BuscarTodos();

                        foreach (var item in unidades)
                        {
                            unidadeLista.Add(item);
                        }
                    }
                    else
                    {
                        var unidade = await BuscarPorId(usuarioLogado.UnidadeId.Value, false);

                        unidadeLista.Add(_mapper.Map<Unidade>(unidade));

                        var unidades = await _repository.BuscarPorTipo(Core.Model.Enums.TipoUnidade.Suporte);

                        unidadeLista.AddRange(unidades);

                    }
                }
                else
                {
                    var unidades = await _repository.BuscarTodos();

                    foreach (var item in unidades)
                    {
                        unidadeLista.Add(item);
                        //var unidade = await BuscarPorId(item.Id, false);

                        //unidadeLista.Add(_mapper.Map<Unidade>(unidade));
                    }
                }

                var unidadeList = _mapper.Map<List<DtoUnidadeResponse>>(unidadeLista);

                return unidadeList.Where(x => !x.IsDelete).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Deletar(int idUnidade)
        {
            try
            {
                var unidade = await _repository.GetByIdAsync(idUnidade);
                unidade.IsDelete = true;
                await _repository.UpdateAsync(unidade);
                var retorno = await _repository.GetByIdAsync(idUnidade);
                return retorno.IsDelete ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<DtoUnidadeResponse> Inserir(DtoUnidadeRequest model)
        {
            DtoUnidadeResponse unidade = new DtoUnidadeResponse();

            var unidadeUpdateOrInserir = _mapper.Map<Unidade>(model);

            if (unidadeUpdateOrInserir.Id == 0)
            {
                var unidadeRetorno = await _repository.AddAsync(unidadeUpdateOrInserir);
                

                unidade = _mapper.Map<DtoUnidadeResponse>(unidadeRetorno);
            }
            else
            {
                var imagem = await SelecionarFoto(model.Id);

                unidadeUpdateOrInserir.Foto = imagem.Foto;
                unidadeUpdateOrInserir.Extensao = imagem.Extensao;

                await _repository.UpdateAsync(unidadeUpdateOrInserir);

                if (unidadeUpdateOrInserir.Endereco != null)
                    await _enderecoRepository.UpdateAsync(unidadeUpdateOrInserir.Endereco);

                if (unidadeUpdateOrInserir.Contato != null)
                    await _contatoRepository.UpdateAsync(unidadeUpdateOrInserir.Contato);

                if (unidadeUpdateOrInserir.ContratoLocacao != null)
                    await _contratoLocacaoRepository.UpdateAsync(unidadeUpdateOrInserir.ContratoLocacao);

                if (unidadeUpdateOrInserir.DadosBancario != null)
                {
                    if (unidadeUpdateOrInserir.DadosBancario.Id > 0)
                    {
                        await _dadosBancarioRepository.UpdateAsync(unidadeUpdateOrInserir.DadosBancario);
                    }
                    else
                    {
                        var dadosBancarioRetorno = await _dadosBancarioRepository.AddAsync(unidadeUpdateOrInserir.DadosBancario);
                        unidadeUpdateOrInserir.DadosBancarioId = dadosBancarioRetorno.Id;
                    }
                }

                // Historico Ocorrencias
                var historicosExcluir = await _historicoOcorrenciasRepository.PorIdUnidade(model.Id);

                await _historicoOcorrenciasRepository.RemoveRangeAsync(historicosExcluir);

                foreach (var item in unidadeUpdateOrInserir.HistoricoOcorrencias)
                {
                    item.Id = 0;
                    item.UnidadeId = model.Id;
                    item.DataCadastro = DateTime.Now;
                    await _historicoOcorrenciasRepository.AddAsync(item);
                }

                // Despesas
                var despesasExcluir = await _unidadeDespesaRepository.PorIdUnidade(model.Id);

                await _unidadeDespesaRepository.RemoveRangeAsync(despesasExcluir);

                foreach (var item in unidadeUpdateOrInserir.UnidadeDespesas)
                {
                    item.Id = 0;
                    item.UnidadeId = model.Id;
                    await _unidadeDespesaRepository.AddAsync(item);
                }

                // Centro de Custos
                if (unidadeUpdateOrInserir.CentroCusto != null)
                    await _centroCustoRepository.UpdateRangeAsync(unidadeUpdateOrInserir.CentroCusto);

                if (unidadeUpdateOrInserir.Anexo != null)
                {
                    foreach (var item in unidadeUpdateOrInserir.Anexo)
                    {
                        if (item.Arquivo.Length > 0)
                        {
                            await _anexoRepository.UpdateAsync(item);
                        }
                    }
                }

                var horariosold = await _horarioFuncionamentoRepository.PorIdUnidade(model.Id);

                if (horariosold != null)
                    await _horarioFuncionamentoRepository.RemoveRangeAsync(horariosold);


                foreach (var item in model.HorarioFuncionamento)
                {
                    item.Id = 0;
                    item.UnidadeId = model.Id;
                    await _horarioFuncionamentoRepository.AddAsync(_mapper.Map<HorarioFuncionamento>(item));

                }

                var unidadeUpdate = await _repository.GetByIdAsync(unidadeUpdateOrInserir.Id);

                unidade = _mapper.Map<Unidade, DtoUnidadeResponse>(unidadeUpdate);
                //unidade = _mapper.Map<DtoUnidadeResponse>(unidadeUpdate);
            }

            return await BuscarPorId(unidade.Id);
        }

        public async Task<DtoAlunoFoto> SelecionarFoto(int unidadeId)
        {
            try
            {
                var unidade = await _repository.SelecionarFoto(unidadeId);

                return new DtoAlunoFoto { UnidadeId = unidade.Id, Extensao = unidade.Extensao, Foto = unidade.Foto };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAlunoFoto> UploadFoto(byte[] file, int unidadeId, string extensao)
        {
            try
            {
                await _repository.UploadFoto(file, unidadeId, extensao);

                return await SelecionarFoto(unidadeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirFoto(int unidadeId)
        {
            try
            {
                var unidade = await _repository.BuscarPorId(unidadeId);
                unidade.Foto = null;
                var sucesso = await _repository.UpdateAsync(unidade);
                return sucesso > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
