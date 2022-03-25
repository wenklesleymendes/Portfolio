using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Funcionarios
{
    public class FiltrarFuncionario
    {
        public int? UnidadeId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataInicioTerminoContrato { get; set; }
        public DateTime? DataFimTerminoContrato { get; set; }
    }
}
