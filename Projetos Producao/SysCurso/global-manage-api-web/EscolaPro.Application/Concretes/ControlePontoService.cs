using AutoMapper;
using EscolaPro.ControlePonto;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.ControlePontoVO;
using EscolaPro.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EscolaPro.Service.Concretes
{
    public class ControlePontoService : IControlePontoService
    {
        private readonly IControlePontoRepository _controlePontoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IPontoEletronicoRobo _pontoEletronicoRobo;
        private readonly IArquivoPontoRepository _arquivoPontoRepository;
        private readonly IMapper _mapper;

        public ControlePontoService(
            IControlePontoRepository controlePontoRepository,
            IFuncionarioRepository funcionarioRepository,
            IUnidadeRepository unidadeRepository,
            IAnexoRepository anexoRepository,
            IPontoEletronicoRobo pontoEletronicoRobo,
            IArquivoPontoRepository arquivoPontoRepository,
            IMapper mapper)
        {
            _controlePontoRepository = controlePontoRepository;
            _funcionarioRepository = funcionarioRepository;
            _unidadeRepository = unidadeRepository;
            _anexoRepository = anexoRepository;
            _pontoEletronicoRobo = pontoEletronicoRobo;
            _arquivoPontoRepository = arquivoPontoRepository;
            _mapper = mapper;
        }

        public async Task<DtoControlePontoHorario> Atualizar(DtoControlePontoHorario dtoControlePonto)
        {
            if (!dtoControlePonto.Entrada1.HasValue)
            {
                dtoControlePonto.Entrada1 = DateTime.Parse(dtoControlePonto.DataCadastrado);
            }

            var controlePonto = await _controlePontoRepository.Atualizar(_mapper.Map<PontoEletronico>(dtoControlePonto));

            return _mapper.Map<DtoControlePontoHorario>(controlePonto);
        }

        public async Task<DtoControlePonteGrid> BuscarPorCPF(string cpf, DateTime dataInicio, DateTime dataFim, bool consultaSaldoPonto = false)
        {
            var funcionario = await _funcionarioRepository.BuscarPorCPF(cpf);

            var funcionarioDadosCompleto = await _funcionarioRepository.BuscarPorId(funcionario.Id);

            var pontoEletronicoLista = await _controlePontoRepository.BuscarPorFuncionario(funcionario.Id, dataInicio, dataFim);

            List<DtoControlePontoHorario> controlePontoLista = new List<DtoControlePontoHorario>();

            foreach (var item in pontoEletronicoLista)
            {
                var anexos = await _anexoRepository.BuscarAnexo(new Core.Model.Anexos.AnexoFiltrar { IdPontoEletronico = item.Id });

                var dtoAnexo = _mapper.Map<List<DtoAnexo>>(anexos.ToList());

                var controlePontoItem = new DtoControlePontoHorario
                {
                    Id = item.Id,
                    DataCadastrado = item.Entrada1.HasValue ? item.Entrada1.Value.ToString("dd/MM/yyyy") : "",
                    Entrada1 = item.Entrada1,
                    Entrada2 = item.Entrada2,
                    Entrada3 = item.Entrada3,
                    Entrada4 = item.Entrada4,
                    Saida1 = item.Saida1,
                    Saida2 = item.Saida2,
                    Saida3 = item.Saida3,
                    Saida4 = item.Saida4,
                    FuncionarioId = funcionario.Id,
                    //RegimeContratacao = funcionarioDadosCompleto.DadosContratacao.TipoRegimeContratacao,
                    RegimeContratacao = item.RegimeContratacao,
                    ApenasFerias = item.ApenasFerias,
                    FeriasId = item.FeriasId,
                    UsuarioId = item.UsuarioId,
                    NumeroPIS = item.NumeroPIS,
                    Observacao = item.Observacao,
                    TipoOcorrenciaPonto = item.TipoOcorrenciaPonto,
                    Pago = item.Pago,
                };

                if (controlePontoItem.TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Ferias ||
                    controlePontoItem.TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.DSR ||
                    controlePontoItem.TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Falta)
                {
                    controlePontoItem.Entrada1 = null;
                    controlePontoItem.Entrada2 = null;
                    controlePontoItem.Entrada3 = null;
                    controlePontoItem.Entrada4 = null;
                    controlePontoItem.Saida1 = null;
                    controlePontoItem.Saida2 = null;
                    controlePontoItem.Saida3 = null;
                    controlePontoItem.Saida4 = null;
                }

                controlePontoItem.Anexo = dtoAnexo;

                controlePontoLista.Add(controlePontoItem);
            }

            DtoControlePonteGrid dtoControlePonte = new DtoControlePonteGrid();

            dtoControlePonte.FuncionarioId = funcionario.Id;
            dtoControlePonte.Matricula = funcionario.DadosContratacao.Matricula;
            dtoControlePonte.NomeColaborador = funcionarioDadosCompleto.Nome;
            dtoControlePonte.RegimeContratacao = funcionarioDadosCompleto.DadosContratacao.TipoRegimeContratacao;

            dtoControlePonte.ControlePontoHorarios = controlePontoLista;

            if (funcionarioDadosCompleto.Ferias.Count > 0)
            {
                dtoControlePonte.FeriasId = funcionarioDadosCompleto.Ferias.Last().Id;
            }

            var totalSpan = Helpers.CoreHelpers.ValidarSaldo(consultaSaldoPonto, controlePontoLista);

            dtoControlePonte.SaldoDevedorTotal = totalSpan;

            //var saldoFormatado = (int)totalSpan.TotalHours + totalSpan.ToString(@"\:mm"); 

            //if (saldoFormatado == "-00:00")
            //{
            //    dtoControlePonte.SaldoDevedorTotal = "00:00";
            //}
            //else
            //{
            //    TimeSpan horarioNegativo = new TimeSpan(0, 0, 0);

            //    if (totalSpan < horarioNegativo)
            //    {
            //        dtoControlePonte.SaldoDevedorTotal = saldoFormatado;
            //    }
            //    else
            //    {
            //        dtoControlePonte.SaldoDevedorTotal = saldoFormatado;
            //    }
            //}

            if (funcionarioDadosCompleto.Ferias.Count > 0)
            {
                dtoControlePonte.StatusFerias = ValidarStatusFeriasFuncionario.ValidarFerias(funcionarioDadosCompleto.Ferias.ToList(), funcionarioDadosCompleto.DadosContratacao.DataAtestadoAdmissao.Value);
            }
            else
            {
                dtoControlePonte.StatusFerias = ValidarStatusFeriasFuncionario.ValidarFerias(funcionarioDadosCompleto.Ferias.ToList(), funcionarioDadosCompleto.DadosContratacao.DataAtestadoAdmissao.Value);
            }

            foreach (var item in funcionarioDadosCompleto.SalarioUnidade)
            {
                var unidade = await _unidadeRepository.GetByIdAsync(item.UnidadeId);
                dtoControlePonte.NomeUnidade = unidade.Nome;
            }

            return _mapper.Map<DtoControlePonteGrid>(dtoControlePonte);
        }

        public async Task<DtoSaldoBancoHoras> BuscarSaldoHorasExtras(string cpf, DateTime dataInicio, DateTime dataFim)
        {
            var controlePontoLista = await BuscarPorCPF(cpf, dataInicio, dataFim, true);

            return new DtoSaldoBancoHoras { Saldo = controlePontoLista.SaldoDevedorTotal };
        }

        public async Task<bool> ExcluirArquivoPonto(int idArquivoPonto)
        {
            try
            {
                var arquivoPonto = await _arquivoPontoRepository.GetByIdAsync(idArquivoPonto);

                var pontoEletronicos = await _controlePontoRepository.BuscarPorNomeArquivo(arquivoPonto.NomeArquivo);

                var id = await _controlePontoRepository.RemoveRangeAsync(pontoEletronicos);

                await _arquivoPontoRepository.RemoveAsync(arquivoPonto);

                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirPontoEletronico(int idPontoEletronico)
        {
            try
            {
                var pontoEletronico = await _controlePontoRepository.GetByIdAsync(idPontoEletronico);

                int id = await _controlePontoRepository.RemoveAsync(pontoEletronico);

                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoArquivoPonto>> ListaArquivosPonto()
        {
            try
            {
                var arquivoPontos = await _arquivoPontoRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<DtoArquivoPonto>>(arquivoPontos.Where(x => !x.IsDelete));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoArquivoPonto> SalvarArquivo(string fileName, DateTime dataCadastro)
        {
            try
            {
                var arquivo = await _arquivoPontoRepository.AddAsync(new ArquivoPonto { Id = 0, DataCadastro = dataCadastro, NomeArquivo = fileName });

                return _mapper.Map<DtoArquivoPonto>(arquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UploadArquivo(string path, string nomeArquivo)
        {
            try
            {
                var pontoEletronico = await _pontoEletronicoRobo.MontarArquivo(path, nomeArquivo);

                int id = await _controlePontoRepository.AddRangeAsync(pontoEletronico);

                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
