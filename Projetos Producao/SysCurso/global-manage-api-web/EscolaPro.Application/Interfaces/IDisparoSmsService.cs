using EscolaPro.Service.Dto.DisparoSmsVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IDisparoSmsService
    {
        Task<bool> Enviar(SmsBody smsBody);
    }
}
