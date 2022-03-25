using EscolaPro.Core.Model;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.ReguaContato;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.ReguaContato;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.ReguaContato;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.ReguaContato
{
    public class ReguaContatoService : IReguaContatoService
    {

        private readonly AppSettings _appSettings;
        public readonly List<TipoMensagem> tipoMensagems;
        private readonly IAlunoService _alunoService;
        public readonly IWhatsAppService _whatsAppService;
        public readonly IDisparoSmsService _disparoSmsService;
        public readonly IEmailSenderService _emailSenderService;
        public readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        public readonly IReguaContatoFilaRepository _reguaContatoFilaRepository;
        public readonly IReguaContatoRegraRepository _reguaContatoRegraRepository;
        public readonly IReguaContatoHistoricoService _reguaContatoHistoricoService;
        public readonly IReguaContatoHistoricoRepository _reguaContatoHistoricoRepository;
        public readonly IReguaContatoParametroRepository _reguaContatoParametroRepository;
        public readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        public ReguaContatoService(IWhatsAppService whatsAppService,
                                   IOptions<AppSettings> appSettings,
                                   IAlunoService alunoService,
                                   IDisparoSmsService disparoSmsService,
                                   IEmailSenderService emailSenderService,
                                   IMatriculaAlunoRepository matriculaAlunoRepository,
                                   IReguaContatoFilaRepository reguaContatoFilaRepository,
                                   IReguaContatoRegraRepository reguaContatoRegraRepository,
                                   IReguaContatoHistoricoService reguaContatoHistoricoService,
                                   IReguaContatoHistoricoRepository reguaContatoHistoricoRepository,
                                   IReguaContatoParametroRepository reguaContatoParametroRepository,
                                   IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository)
        {
            _appSettings = appSettings.Value;
            _alunoService = alunoService;
            _whatsAppService = whatsAppService;
            _disparoSmsService = disparoSmsService;
            _emailSenderService = emailSenderService;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _reguaContatoFilaRepository = reguaContatoFilaRepository;
            _reguaContatoRegraRepository = reguaContatoRegraRepository;
            _reguaContatoHistoricoService = reguaContatoHistoricoService;
            _reguaContatoHistoricoRepository = reguaContatoHistoricoRepository;
            _reguaContatoParametroRepository = reguaContatoParametroRepository;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            tipoMensagems = new List<TipoMensagem>() { TipoMensagem.Email, TipoMensagem.SMS, TipoMensagem.WhatsApp };
        }
        public async Task CarregaReguaContatoCobranca()
        {
            List<ReguaContatoRegra> regras = await _reguaContatoRegraRepository.BuscarRegra(TipoRegra.Cobranca);
            DateTime dataInclusao = DateTime.Now;
            IList<ReguaContatoFila> reguaContatoFilas = new List<ReguaContatoFila>();
            foreach (var regra in regras)
            {
                IList<ReguaContatoParametro> reguaContatoParameros;
                if (regra.ReguaContatoParametro.Count > 0)
                    reguaContatoParameros = regra.ReguaContatoParametro.Where(x => x.Nome == "QuantidadeDias").ToList();
                else
                    reguaContatoParameros = await _reguaContatoParametroRepository.Buscar(regra.Id);
                foreach (var reguaContatoParamero in reguaContatoParameros)
                {
                    int.TryParse(reguaContatoParamero.Valor, out int quantidadeDias);
                    IEnumerable<Core.Model.Pagamentos.Pagamento> pagamentos = await _alunoFinanceiroContratoRepository.ParcelasAVencer(quantidadeDias, regra.Id);

                    foreach (var pagamento in pagamentos)
                    {
                        MatriculaAluno matriculaAluno = await _matriculaAlunoRepository.BuscarPorId(pagamento.MatriculaId);
                        if (matriculaAluno != null)
                        {
                            ReguaContatoFila fila = new ReguaContatoFila
                            {
                                AlunoId = matriculaAluno.AlunoId,
                                UnidadeId = matriculaAluno.UnidadeId,
                                ReguaContatoRegrasId = regra.Id,
                                StatusFila = StatusFila.NaoEnviado,
                                DataInclusao = dataInclusao,
                                PagamentoId = pagamento.Id,
                                IsActive = true,
                                Prioridade = 1,
                                EnviadaComSucesso = false
                            };
                            reguaContatoFilas.Add(fila);
                        }
                    }
                }

            }
            if (reguaContatoFilas.Count > 0)
                await _reguaContatoFilaRepository.AddRangeAsync(reguaContatoFilas);
        }

        public async Task<IEnumerable<ReguaContatoFila>> GetContatoFilas(TipoMensagem tipoMensagem)
        {
            var lista = await _reguaContatoFilaRepository.BuscarPorTipoMensage(tipoMensagem);

            //if (lista?.Count > 0)
            //    await _reguaContatoFilaRepository.UpdateStatus(lista, StatusFila.EmEmpera);

            return lista;
        }

        public async Task DispararContato(ReguaContatoFilaDto reguaContatoFila)
        {

            ReguaContatoFila regua = await _reguaContatoFilaRepository.BuscarPorId(reguaContatoFila.Id);
            var matriculaAluno = await _matriculaAlunoRepository.BuscarPorId(regua.Pagamento.MatriculaId);

            switch (regua.ReguaContatoRegras.TipoMensagem)
            {
                case TipoMensagem.Email:
                    if(regua.Aluno.Contato.ReceberEmail && matriculaAluno.Status)
                    await EnvioEmail(regua);
                    else
                        await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.NaoReceber);
                    break;
                case TipoMensagem.SMS:
                    if (regua.Aluno.Contato.RecebeSMS && matriculaAluno.Status)
                        await EnvioSMS(regua);
                    else
                        await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.NaoReceber);
                    break;
                case TipoMensagem.WhatsApp:
                    if (regua.Aluno.Contato.ReceberWhatsApp && matriculaAluno.Status)
                        await EnvioWhatsApp(regua);
                    else
                        await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.NaoReceber);
                    break;
            }

        }

        private async Task EnvioEmail(ReguaContatoFila reguaContatoFila)
        {
            try
            {
                var mailBody = Helpers.CoreHelpers.MontarEmailCobrancaReguaContato(reguaContatoFila);
                int dias = DateTime.Now.Date.Subtract(reguaContatoFila.Pagamento.DataVencimento.Value.Date).Days;
                dias *= -1;

                string primeiroNome = string.Empty;
                if (!string.IsNullOrEmpty(reguaContatoFila?.Aluno?.Nome))
                {
                    var nomes = reguaContatoFila.Aluno.Nome.Split(" ");
                    primeiroNome = nomes[0];
                }


                List<Attachment> attachments = new List<Attachment>();

                if (!string.IsNullOrEmpty(reguaContatoFila.Pagamento.BoletoHTML))
                {
                    var boletoPdf = await Core.Helpers.CoreHelpers.ConverterBoletoPDF(new List<string>() { reguaContatoFila.Pagamento.BoletoHTML }, _appSettings.BoletoServiceUrl);

                    Stream stream = new MemoryStream(Convert.FromBase64String(boletoPdf.FirstOrDefault()));

                    attachments.Add(new Attachment(stream, $"{reguaContatoFila.Pagamento.Descricao.Replace("/", "-")}.pdf"));
                }

                reguaContatoFila.ReguaContatoRegras.Titulo = reguaContatoFila.ReguaContatoRegras.Titulo.Replace("{nome-aluno}", primeiroNome).Replace("{dias}", dias.ToString());
                MatriculaAluno matricula = reguaContatoFila.Aluno.Matriculas.FirstOrDefault(x => x.Id == reguaContatoFila.Pagamento.MatriculaId);
                await _emailSenderService.SendEmailAsync(new string[] { reguaContatoFila.Aluno.Contato.Email }, reguaContatoFila.ReguaContatoRegras.Titulo, mailBody, attachments, matricula.Curso.NacionatalTec, null, reguaContatoFila.Aluno.Id);
                await IncluirHistorico(reguaContatoFila, mailBody);
                await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.EnviadoComSucesso);
            }
            catch (Exception)
            {
                await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.ErroNoEnvio);
                throw;
            }
        }

        private async Task EnvioSMS(ReguaContatoFila reguaContatoFila)
        {
            try
            {

                var aluno = await _alunoService.BuscarPorId(reguaContatoFila.AlunoId);
                if (!aluno.Contato.RecebeSMS)
                    return;

                var mensagemBody = Helpers.CoreHelpers.MontarSMSCobrancaReguaContato(reguaContatoFila);
                await _disparoSmsService.Enviar(new Dto.DisparoSmsVO.SmsBody { alunoId = reguaContatoFila.AlunoId, mensagem = mensagemBody, numero = reguaContatoFila.Aluno.Contato.Celular });
                await IncluirHistorico(reguaContatoFila, mensagemBody);
                await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.EnviadoComSucesso);
            }
            catch (Exception)
            {
                await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.ErroNoEnvio);
                throw;
            }
        }

        private async Task EnvioWhatsApp(ReguaContatoFila reguaContatoFila)
        {
            try
            {


                var aluno = await _alunoService.BuscarPorId(reguaContatoFila.AlunoId);
                if (!aluno.Contato.ReceberWhatsApp)
                    return;

                var mensagemBody = Helpers.CoreHelpers.MontarWhatsAppCobrancaReguaContato(reguaContatoFila);
                int dias = DateTime.Now.Date.Subtract(reguaContatoFila.Pagamento.DataVencimento.Value.Date).Days;
                dias = dias * -1;
                reguaContatoFila.ReguaContatoRegras.Titulo = reguaContatoFila.ReguaContatoRegras.Titulo.Replace("{NomeFantasia}", reguaContatoFila.Unidade.NomeFantasia).Replace("{dias}", dias.ToString());
                string retorno = await _whatsAppService.SendMessage(reguaContatoFila.Unidade.Contato.Instance, reguaContatoFila.Unidade.Contato.Token, reguaContatoFila.Aluno.Contato.Celular, mensagemBody, reguaContatoFila.Aluno.Id);
                await IncluirHistorico(reguaContatoFila, mensagemBody);
                Thread.Sleep(_appSettings.TimePauseWhatsApp);
                retorno = await _whatsAppService.SendMessage(reguaContatoFila.Unidade.Contato.Instance, reguaContatoFila.Unidade.Contato.Token, reguaContatoFila.Aluno.Contato.Celular, reguaContatoFila.Pagamento.NumeroLinhaDigitavel, reguaContatoFila.Aluno.Id);
                await IncluirHistorico(reguaContatoFila, reguaContatoFila.Pagamento.NumeroLinhaDigitavel);
                await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.EnviadoComSucesso);
            }
            catch (Exception ex)
            {
                await _reguaContatoFilaRepository.UpdateStatus(reguaContatoFila.Id, StatusFila.ErroNoEnvio);
                throw;
            }
        }

        private async Task IncluirHistorico(ReguaContatoFila reguaContatoFila, string texto)
        {
            var reguaContatoHistorico = new ReguaContatoHistorico();
            reguaContatoHistorico.AlunoId = reguaContatoFila.AlunoId;
            reguaContatoHistorico.Titulo = reguaContatoFila.ReguaContatoRegras.Titulo;
            reguaContatoHistorico.Texto = texto;
            reguaContatoHistorico.DataEnvio = DateTime.Now;
            reguaContatoHistorico.TipoMensagem = TipoMensagemEnum.Email;

            var retornoSalvar = await _reguaContatoHistoricoService.Inserir(reguaContatoHistorico);
        }
    }
}
