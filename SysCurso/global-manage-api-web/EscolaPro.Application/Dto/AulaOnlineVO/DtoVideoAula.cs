using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoVideoAula
    {
        public int Id { get; set; }
        public string TituloAula { get; set; }
        public string URLVideo { get; set; }
        public int MateriaId { get; set; }
        public IEnumerable<DtoPergunta> Pergunta { get; set; }
    }
}
