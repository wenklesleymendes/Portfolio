using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoSolicitacaoEmail
    {
        public int Id { get; set; }
        public DateTime DataEnvio { get; set; }
        public string CorpoEmail { get; set; }
    }
}
