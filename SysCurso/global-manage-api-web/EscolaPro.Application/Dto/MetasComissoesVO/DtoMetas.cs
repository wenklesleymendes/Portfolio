using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MetasComissoesVO
{
    public class DtoMetas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime? InicioMeta { get; set; }
        public DateTime? TerminoMeta { get; set; }
        public int Quantidade { get; set; }
        public decimal BonusPeriodo { get; set; }
        public int UnidadeId { get; set; }
        public IEnumerable<DtoDetalhamentoMeta> DetalhamentoMeta { get; set; }
    }
}
