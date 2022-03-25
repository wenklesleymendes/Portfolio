using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IWhatsAppService
    {
        Task<string> SendMessage(string phone, string tokenUnidade, string text, int alunoId);
    }
}
