using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.EmailVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.ReguaContato;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSettings _emailSettings;
        private readonly AppSettings _appSettings;
        private readonly IReguaContatoHistoricoService _reguaContatoHistoricoService;

        public EmailSenderService(IOptions<EmailSettings> emailSettings, IOptions<AppSettings> appSettings, IReguaContatoHistoricoService reguaContatoHistoricoService)
        {
            _emailSettings = emailSettings.Value;
            _appSettings = appSettings.Value;
        _reguaContatoHistoricoService = reguaContatoHistoricoService;
        }

        public async Task SendEmailAsync(string[] email, string subject, string message, List<Attachment> attachments, bool nacionalTec = false, AlternateView alternateView = null, int alunoId = 6)
        {
            try
            {
                // Descomentar esse metodo para enviar email
                if (_appSettings.EnviarEmail)
                {
                    if (nacionalTec)
                        await ExecuteNacional(email, subject, message, attachments, alternateView, alunoId);
                    else
                        await Execute(email, subject, message, attachments, alternateView, alunoId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Execute(string[] emails, string subject, string message, List<Attachment> attachments, AlternateView alternateView, int alunoId)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //mail.From = new MailAddress("atendimento@contatosupletivo.com.br");
                //mail.From = new MailAddress("supletivo@contatosupletivo.br");
                mail.From = new MailAddress(_emailSettings.FromEmail, "Curso Supletivo");

                foreach (var email in emails)
                {
                    mail.To.Add(email); // para
                }

                mail.Subject = subject; // assunto
                mail.Body = message; // mensagem
                mail.IsBodyHtml = true;

                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        mail.Attachments.Add(item);
                    }
                }

                if (alternateView != null)
                {
                    mail.AlternateViews.Add(alternateView);
                }

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain))
                {
                    smtp.EnableSsl = _emailSettings.UseSSL; // GMail requer SSL
                    smtp.Port = _emailSettings.PrimaryPort;       // porta para SSL
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                    smtp.UseDefaultCredentials = true; // vamos utilizar credencias especificas
                    
                    // seu usuário e senha para autenticação
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    //smtp.Credentials = new NetworkCredential("supletivo@contatosupletivo.br", "St77@!98vA21M");
                    // envia o e-mail
                    await smtp.SendMailAsync(mail);

                    var reguaContatoHistorico = new ReguaContatoHistorico();
                    //reguaContatoHistorico.ReguaContatoRegrasId = 1;
                    //reguaContatoHistorico.ReguaContatoFilaId = 1;
                    reguaContatoHistorico.AlunoId = alunoId;
                    reguaContatoHistorico.Titulo = mail.Subject.ToString();
                    reguaContatoHistorico.Texto = mail.Body.ToString();
                    reguaContatoHistorico.DataEnvio = DateTime.Now;
                    reguaContatoHistorico.TipoMensagem = TipoMensagemEnum.Email;

                    var retornoSalvar = await _reguaContatoHistoricoService.Inserir(reguaContatoHistorico);
                }
            }
            catch (Exception ex)
            {
                 throw;
            }
        }

        public async Task ExecuteNacional(string[] emails, string subject, string message, List<Attachment> attachments, AlternateView alternateView, int alunoId)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(_emailSettings.FromEmailNacional, "Cursos NacionalTec");

                foreach (var email in emails)
                {
                    mail.To.Add(email); // para
                }

                mail.Subject = subject; // assunto
                mail.Body = message; // mensagem
                mail.IsBodyHtml = true;

                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        mail.Attachments.Add(item);
                    }
                }

                if (alternateView != null)
                {
                    mail.AlternateViews.Add(alternateView);
                }

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomainNacional))
                {
                    smtp.EnableSsl = _emailSettings.UseSSLNacional; // GMail requer SSL
                    smtp.Port = _emailSettings.PrimaryPortNacional;       // porta para SSL
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                    smtp.UseDefaultCredentials = true; // vamos utilizar credencias especificas

                    // seu usuário e senha para autenticação
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmailNacional, _emailSettings.UsernamePasswordNacional);

                    // envia o e-mail
                    await smtp.SendMailAsync(mail);

                    var reguaContatoHistorico = new ReguaContatoHistorico();
                    reguaContatoHistorico.AlunoId = alunoId;
                    reguaContatoHistorico.Titulo = mail.Subject.ToString();
                    reguaContatoHistorico.Texto = mail.Body.ToString();
                    reguaContatoHistorico.DataEnvio = DateTime.Now;
                    reguaContatoHistorico.TipoMensagem = TipoMensagemEnum.Email;

                    var retornoSalvar = await _reguaContatoHistoricoService.Inserir(reguaContatoHistorico);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

