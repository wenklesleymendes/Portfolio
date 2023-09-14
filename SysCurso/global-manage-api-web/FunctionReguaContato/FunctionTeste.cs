using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionReguaContato
{
    public static class FunctionTeste
    {
        [FunctionName("FunctionTeste")]
        public static void Run([TimerTrigger("*/2 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //await _emailSenderService.SendEmailAsync(new string[] { matriculaMail.Aluno.Contato.Email }, "Informações Sobre Sua Prova", mailBody, null, matriculaMail.Curso.NacionatalTec, null);

            System.Diagnostics.Debug.WriteLine("Teste disparo Function: " + DateTime.Now);
        }
    }
}
