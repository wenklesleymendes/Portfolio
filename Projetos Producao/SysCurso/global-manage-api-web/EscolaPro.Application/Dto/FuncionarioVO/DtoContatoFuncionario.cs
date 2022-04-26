using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoContatoFuncionario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string RecebeSMS { get; set; }
        public string TelefoneFixo { get; set; }
    }
}
