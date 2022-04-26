using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Dto.AlunosVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoProvaAlunoInformacoes
    {
        public bool PendenciaPagamento { get; set; }
        public bool PendenciaDocumental { get; set; }
        public bool DentroPrazo { get; set; }
        public string AlunoRG { get; set; }
        public IEnumerable<Unidade> Unidades { get; set; }
        public IEnumerable<DtoColegioAutorizado> ColegiosAutorizados { get; set; }
    }
}
