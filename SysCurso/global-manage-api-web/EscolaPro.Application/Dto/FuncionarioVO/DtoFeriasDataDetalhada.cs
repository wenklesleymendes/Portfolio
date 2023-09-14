using EscolaPro.Core.Model.ControlePontoEletronico;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoFeriasDataDetalhada
    {
        public DateTime DataVencimento { get; set; }
        public DateTime? FeriasConcecidaInicio { get; set; }
        public DateTime? FeriasConcecidaTermino { get; set; }
        public TipoFeriasFolgaFalta? TipoFerias { get; set; }
        public int DiasVencimento { get; set; }
    }
}
