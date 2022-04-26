using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.DisparoSmsVO
{
    public class SmsBody
    {
        public string numero { get; set; }
        public string servico { get; set; }
        public string mensagem { get; set; }
        public string parceiro_id { get; set; }
        public string codificacao { get; set; }
        public int alunoId { get; set; }
    }
}
