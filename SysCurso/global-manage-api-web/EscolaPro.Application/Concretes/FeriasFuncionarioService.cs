using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Dto.ControlePontoVO;
using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class FeriasFuncionarioService : IFeriasFuncionarioService
    {
        private readonly IFeriasFuncionarioRepository _feriasFuncionarioRepository;
        private readonly IControlePontoRepository _controlePontoRepository;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IAnexoService _anexoService;
        private readonly IMapper _mapper;

        public FeriasFuncionarioService(
            IFeriasFuncionarioRepository feriasFuncionarioRepository,
            IControlePontoRepository controlePontoRepository,
            IFuncionarioService funcionarioService,
            IAnexoService anexoService,
            IMapper mapper)
        {
            _feriasFuncionarioRepository = feriasFuncionarioRepository;
            _controlePontoRepository = controlePontoRepository;
            _funcionarioService = funcionarioService;
            _anexoService = anexoService;
            _mapper = mapper;
        }

        public async Task<DtoFeriasDetalhamento> BuscarDetalhamentoFerias(int idFuncionario)
        {
            try
            {
                DtoFeriasDetalhamento dtoFeriasDetalhamento = new DtoFeriasDetalhamento();

                var funcionario = await _funcionarioService.BuscarPorId(idFuncionario);

                var vencimentosFeriasLista = VencimentosFerias(funcionario.DadosContratacao.DataAtestadoAdmissao.Value);

                var feriasVencidas = await _feriasFuncionarioRepository.BuscarPorIdFuncionario(funcionario.Id);

                dtoFeriasDetalhamento.FeriasDataDetalhadas = CalcularPeriodoFeriasDetalhado(vencimentosFeriasLista,
                    feriasVencidas.Where(x => x.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.Folga && x.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.Falta).ToList(),
                    funcionario.DadosContratacao.DataAtestadoAdmissao.Value);

                dtoFeriasDetalhamento.DataContratacao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataAtestadoAdmissao : null;
                dtoFeriasDetalhamento.DataRecisao = funcionario.DadosContratacao != null ? funcionario.DadosContratacao.DataRecisao : null;
                dtoFeriasDetalhamento.RegimeContratacao = funcionario.DadosContratacao.TipoRegimeContratacao;
                dtoFeriasDetalhamento.IdFuncionario = funcionario.Id;
                dtoFeriasDetalhamento.Nome = funcionario.Nome;

                return dtoFeriasDetalhamento;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<DtoFeriasFuncionario>> BuscarTodosPorFuncionario(int idFuncionario)
        {
            var listFerias = await _feriasFuncionarioRepository.BuscarPorIdFuncionario(idFuncionario);

            List<DtoFeriasFuncionario> listaFeriasRetorno = new List<DtoFeriasFuncionario>();

            foreach (var ferias in listFerias)
            {
                DtoFeriasFuncionario feriasFuncionario = _mapper.Map<DtoFeriasFuncionario>(ferias);


                var listaAnexo = await _anexoService.BuscarPorFiltro(new Core.Model.Anexos.AnexoFiltrar { IdFerias = ferias.Id });
                //var listaAnexo = await _anexoService.BuscarPorFerias(ferias.Id);

                foreach (var anexo in listaAnexo)
                {
                    feriasFuncionario.Anexo.Add(anexo);
                }

                listaFeriasRetorno.Add(feriasFuncionario);
            }

            return _mapper.Map<IEnumerable<DtoFeriasFuncionario>>(listaFeriasRetorno);
        }

        public async Task<DtoFeriasFuncionario> ConcederFerias(DtoFeriasFuncionario dtoFeriasFuncionario)
        {
            switch (dtoFeriasFuncionario.TipoFeriasFolgaFalta)
            {
                case TipoFeriasFolgaFalta.FeriasGozadas30Dias:
                case TipoFeriasFolgaFalta.FeriasVendidas30Dias:
                    dtoFeriasFuncionario.Termino = dtoFeriasFuncionario.Inicio.AddDays(29);
                    break;
                case TipoFeriasFolgaFalta.FeriasGozadas15Dias15Vendidos:
                    dtoFeriasFuncionario.Termino = dtoFeriasFuncionario.Inicio.AddDays(14);
                    break;
                case TipoFeriasFolgaFalta.FeriasGozadas20Dias10Vendidos:
                    dtoFeriasFuncionario.Termino = dtoFeriasFuncionario.Inicio.AddDays(19);
                    break;
                default:
                    break;
            }

            if (dtoFeriasFuncionario.Id == 0)
            {
                if (dtoFeriasFuncionario.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.Folga)
                {
                    var feriasFuncionario = await _feriasFuncionarioRepository.AddAsync(_mapper.Map<FeriasFuncionario>(dtoFeriasFuncionario));

                    var periodoDeFeriasLista = await GerarPeriodoDeFerias(feriasFuncionario.Inicio, feriasFuncionario.Termino, feriasFuncionario.FuncionarioId, feriasFuncionario.Id, feriasFuncionario.Observacao, feriasFuncionario.TipoFeriasFolgaFalta);


                    if (feriasFuncionario.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.FeriasVendidas30Dias)
                    {
                        await _controlePontoRepository.AddRangeAsync(periodoDeFeriasLista);
                    }

                    return _mapper.Map<DtoFeriasFuncionario>(feriasFuncionario);
                }
                if (dtoFeriasFuncionario.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.Falta)
                {
                    var feriasFuncionario = await _feriasFuncionarioRepository.AddAsync(_mapper.Map<FeriasFuncionario>(dtoFeriasFuncionario));

                    var periodoDeFeriasLista = await GerarPeriodoDeFerias(feriasFuncionario.Inicio, feriasFuncionario.Termino, feriasFuncionario.FuncionarioId, feriasFuncionario.Id, feriasFuncionario.Observacao, feriasFuncionario.TipoFeriasFolgaFalta);

                    if (feriasFuncionario.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.FeriasVendidas30Dias)
                    {
                        await _controlePontoRepository.AddRangeAsync(periodoDeFeriasLista);
                    }

                    return _mapper.Map<DtoFeriasFuncionario>(feriasFuncionario);
                }
                else
                {
                    var periodoDeFeriasLista = await GerarPeriodoDeFerias(dtoFeriasFuncionario.Inicio, dtoFeriasFuncionario.Termino, dtoFeriasFuncionario.FuncionarioId.Value, dtoFeriasFuncionario.Id, dtoFeriasFuncionario.Observacao, dtoFeriasFuncionario.TipoFeriasFolgaFalta);

                    if (dtoFeriasFuncionario.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.FeriasVendidas30Dias)
                    {
                        await _controlePontoRepository.AddRangeAsync(periodoDeFeriasLista);
                    }

                    return _mapper.Map<DtoFeriasFuncionario>(dtoFeriasFuncionario);
                }
            }
            else
            {
                await _feriasFuncionarioRepository.UpdateAsync(_mapper.Map<FeriasFuncionario>(dtoFeriasFuncionario));

                var feriasFuncionario = await _feriasFuncionarioRepository.GetByIdAsync(dtoFeriasFuncionario.Id);

                var pontoEletronicoFeriasOld = await _controlePontoRepository.BuscarPorFeriasId(dtoFeriasFuncionario.Id);

                foreach (var item in pontoEletronicoFeriasOld)
                {
                    item.IsDelete = true;
                    await _controlePontoRepository.UpdateAsync(item);
                }

                var periodoDeFeriasLista = await GerarPeriodoDeFerias(feriasFuncionario.Inicio, feriasFuncionario.Termino, feriasFuncionario.FuncionarioId, dtoFeriasFuncionario.Id, feriasFuncionario.Observacao, dtoFeriasFuncionario.TipoFeriasFolgaFalta); ;

                if (dtoFeriasFuncionario.TipoFeriasFolgaFalta != TipoFeriasFolgaFalta.FeriasVendidas30Dias)
                {
                    await _controlePontoRepository.AddRangeAsync(periodoDeFeriasLista);
                }

                return _mapper.Map<DtoFeriasFuncionario>(feriasFuncionario);
            }

        }

        public async Task<bool> DeletarFerias(int idFerias)
        {
            var ferias = await _feriasFuncionarioRepository.GetByIdAsync(idFerias);

            ferias.IsDelete = true;

            await _feriasFuncionarioRepository.UpdateAsync(ferias);

            if (ferias.Anexo != null)
            {
                if (ferias.Anexo.Count > 0)
                {
                    foreach (var item in ferias.Anexo)
                    {
                        item.IsDelete = true;
                        await _anexoService.Deleter(item.Id);
                    }
                }
            }

            var pontoLista = await _controlePontoRepository.BuscarPorFeriasId(idFerias);

            if (pontoLista != null)
            {
                if (pontoLista.Count() > 0)
                {
                    await _controlePontoRepository.RemoveRangeAsync(pontoLista);
                }
            }

            return ferias.IsDelete;
        }

        public async Task<List<PontoEletronico>> GerarPeriodoDeFerias(DateTime inicioFerias, DateTime terminoFerias, int funcionarioId, int feriasId, string observacao, TipoFeriasFolgaFalta tipoFeriasFolgaFalta)
        {
            int quantidadeDiasFerias = (int)terminoFerias.Subtract(inicioFerias).TotalDays + 1;

            var funcionario = await _funcionarioService.BuscarPorId(funcionarioId);

            List<PontoEletronico> pontosFerias = new List<PontoEletronico>();

            for (int i = 0; i < quantidadeDiasFerias; i++)
            {
                var dataFerias = inicioFerias.AddDays(i);

                var ferias = new PontoEletronico
                {
                    NumeroPIS = funcionario.DadosContratacao.NumeroPIS,
                    Entrada1 = inicioFerias.AddDays(i),
                    FuncionarioId = funcionarioId,
                    Observacao = observacao,
                    FeriasId = feriasId
                };

                switch (tipoFeriasFolgaFalta)
                {
                    case TipoFeriasFolgaFalta.FeriasGozadas15Dias15Vendidos:
                    case TipoFeriasFolgaFalta.FeriasGozadas20Dias10Vendidos:
                    case TipoFeriasFolgaFalta.FeriasGozadas30Dias:
                    case TipoFeriasFolgaFalta.FeriasVendidas30Dias:
                        ferias.TipoOcorrenciaPonto = TipoOcorrenciaPontoEnum.Ferias;
                        break;
                    case TipoFeriasFolgaFalta.Folga:
                        ferias.TipoOcorrenciaPonto = TipoOcorrenciaPontoEnum.DSR;
                        break;
                    case TipoFeriasFolgaFalta.Falta:
                        ferias.TipoOcorrenciaPonto = TipoOcorrenciaPontoEnum.Falta;
                        break;
                    default:
                        break;
                }


                pontosFerias.Add(ferias);
            }

            return pontosFerias;
        }

        public List<DateTime> VencimentosFerias(DateTime dataContratacao)
        {
            List<DateTime> dataVencimentosLista = new List<DateTime>();

            int countAno = 1;
            int ano = dataContratacao.Date.Year;

            while (ano < DateTime.Now.Year)
            {
                int dia = dataContratacao.Date.Day;
                int mes = dataContratacao.Date.Month;
                ano = ano + countAno;

                DateTime proximoVencimento = new DateTime(ano, mes, dia);

                dataVencimentosLista.Add(proximoVencimento);
            }

            return dataVencimentosLista;
        }




        public List<DtoFeriasDataDetalhada> CalcularPeriodoFeriasDetalhado(List<DateTime> dataVencimentos, List<FeriasFuncionario> feriasConcedidas, DateTime dataContratacaoParam)
        {
            List<DtoFeriasDataDetalhada> feriasDetalhamentos = new List<DtoFeriasDataDetalhada>();
            DateTime dataContratacao = dataContratacaoParam;

            foreach (var vencimento in dataVencimentos)
            {
                DtoFeriasDataDetalhada dataDetalhada = new DtoFeriasDataDetalhada();

                FeriasFuncionario feriasConcedida = new FeriasFuncionario();

                feriasConcedida = feriasConcedidas.FirstOrDefault();

                dataDetalhada.DataVencimento = vencimento;

                if (feriasConcedida != null)
                {
                    dataDetalhada.TipoFerias = feriasConcedida.TipoFeriasFolgaFalta;

                    int dia = dataContratacao.AddDays(1).Day;
                    int mes = dataContratacao.Month;
                    int ano = 0;

                    ano = vencimento.Year;

                    dataDetalhada.FeriasConcecidaInicio = feriasConcedida.Inicio;
                    dataDetalhada.FeriasConcecidaTermino = feriasConcedida.Termino;

                    DateTime vencimentoDosAnosAtras = new DateTime(ano, mes, dia);

                    var dias = ((int)feriasConcedida.Inicio.Date.Subtract(vencimentoDosAnosAtras.Date).TotalDays);

                    dataDetalhada.DiasVencimento = dias;

                }
                else
                {
                    DateTime dataAtual = DateTime.Now;

                    if(vencimento.Date < dataAtual.Date)
                    {
                        var dias = ((int)vencimento.Date.Subtract(dataAtual.Date).TotalDays + 1) * -1;

                        dataDetalhada.DiasVencimento = dias;
                    }
                    else
                    {
                        dataDetalhada.DiasVencimento = 0;
                    }
                }

                feriasConcedidas.Remove(feriasConcedida);

                feriasDetalhamentos.Add(dataDetalhada);

            }

            return feriasDetalhamentos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataVencimentos"></param>
        /// <param name="feriasConcedidas"></param>
        /// <param name="dataContratacaoParam"></param>
        /// <returns></returns>


        public List<DtoFeriasDataDetalhada> OldCalcularPeriodoFeriasDetalhado(List<DateTime> dataVencimentos, List<FeriasFuncionario> feriasConcedidas, DateTime dataContratacaoParam)
        {
            List<DtoFeriasDataDetalhada> feriasDetalhamentos = new List<DtoFeriasDataDetalhada>();
            DateTime dataContratacao = dataContratacaoParam;

            foreach (var vencimento in dataVencimentos)
            {
                DtoFeriasDataDetalhada dataDetalhada = new DtoFeriasDataDetalhada();

                FeriasFuncionario feriasConcedida = new FeriasFuncionario();

                feriasConcedida = feriasConcedidas.Where(x => x.Termino.Year - 2 == vencimento.Year).FirstOrDefault();

                bool validarPrimeiraFerias = false;

                if (feriasConcedida == null)
                {
                    if (vencimento.Year - 1 == dataContratacaoParam.Year)
                    {
                        validarPrimeiraFerias = true;
                        feriasConcedida = feriasConcedidas.Where(x => x.Termino.Year == vencimento.Year).FirstOrDefault();
                    }
                    else
                    {
                        feriasConcedida = feriasConcedidas.Where(x => x.Termino.Year == vencimento.Year).FirstOrDefault();
                    }
                }

                dataDetalhada.DataVencimento = vencimento;

                if (feriasConcedida != null)
                {
                    dataDetalhada.TipoFerias = feriasConcedida.TipoFeriasFolgaFalta;

                    int dia = dataContratacao.Day;
                    int mes = dataContratacao.Month;
                    int ano = 0;

                    if (validarPrimeiraFerias)
                    {
                        ano = feriasConcedida.Termino.Year;
                    }
                    else
                    {
                        ano = feriasConcedida.Termino.Year - 2;
                    }

                    if (ano == vencimento.Year)
                    {
                        dataDetalhada.FeriasConcecidaInicio = feriasConcedida.Inicio;
                        dataDetalhada.FeriasConcecidaTermino = feriasConcedida.Termino;

                        DateTime vencimentoDosAnosAtras = new DateTime(ano, mes, dia);

                        var dias = ((int)feriasConcedida.Inicio.Date.Subtract(vencimentoDosAnosAtras.Date).TotalDays);

                        dataDetalhada.DiasVencimento = dias;
                    }
                }
                else
                {
                    DateTime dataAtual = DateTime.Now;

                    var dias = ((int)vencimento.Date.Subtract(dataAtual.Date).TotalDays + 1) * -1;

                    dataDetalhada.DiasVencimento = dias;

                }

                feriasDetalhamentos.Add(dataDetalhada);
            }

            return feriasDetalhamentos;
        }
    }
}
