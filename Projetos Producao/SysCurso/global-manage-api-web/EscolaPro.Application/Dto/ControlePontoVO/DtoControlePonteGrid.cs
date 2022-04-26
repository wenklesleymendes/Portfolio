using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ControlePontoVO
{
    public class DtoControlePonteGrid
    {
        public int FuncionarioId { get; set; }
        public string NomeColaborador { get; set; }
        public string Matricula { get; set; }
        public string NomeUnidade { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public IEnumerable<DtoControlePontoHorario> ControlePontoHorarios { get; set; }
        public string SaldoDevedorTotal { get; set; }
        public string StatusFerias { get; set; }
        public int FeriasId { get; set; }
        public string SaldoTotal { get; set; }
    }
}
