using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.EmailVO
{
    public class DtoEmailEnviado
    {
        public int Id { get; set; }
        public DateTime DataEnvio { get; set; }
        public string EmailPara { get; set; }
        public string EmailEnviado { get; set; }
        public string CorpoEmail { get; set; }
    }
}
