using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string[] email, string subject, string message, List<Attachment> attachments, bool nacionalTec = false, AlternateView alternateView = null, int alunoId = 6);
    }
}
