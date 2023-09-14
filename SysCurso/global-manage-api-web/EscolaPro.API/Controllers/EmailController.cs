using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.EmailVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailSenderService emailSenderService, ILogger<EmailController> logger)
        {
            _emailSenderService = emailSenderService;
            _logger = logger;
        }

        [Route("EnviarEmail")]
        [HttpPost]
        public async Task<IActionResult> EnviarEmail(EmailModel email)
        {

            try
            {
                _logger.LogDebug("Mail Controller");
                await _emailSenderService.SendEmailAsync(email.Destinos, email.Assunto, email.Mensagem, null, email.NacionalTec);
                return Ok();
            }
            catch (Exception ex)
            {
                return RedirectToAction("EmailFalhou");
            }
        }
    }
}