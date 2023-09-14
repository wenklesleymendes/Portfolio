using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscolaPro.Service.Dto.EmailVO
{
    public class EmailModel
    {
        public string[] Destinos { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public bool NacionalTec { get; set; }
    }
}
