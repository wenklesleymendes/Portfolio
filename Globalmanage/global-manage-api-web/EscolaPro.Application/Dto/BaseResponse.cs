using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Mensagem { get; set; }
    }
}
