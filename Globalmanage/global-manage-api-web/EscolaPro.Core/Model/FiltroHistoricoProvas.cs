using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto
{
    public class FiltroHistoricoProvas
    {
        public List<int>? unidadeSelect { get; set; }
        public string? colegioSelect { get; set; }
        public int? tipoProva { get; set; }
        public DateTime? dataInicioMatricula { get; set; }
        public DateTime? dataFimMatricula { get; set; }
        public int? statusProva { get; set; }
        public int? onibus { get; set; }
    }
}
