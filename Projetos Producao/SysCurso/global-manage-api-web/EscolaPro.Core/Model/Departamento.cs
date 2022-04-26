using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Departamento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid IdUnidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
    }
}
