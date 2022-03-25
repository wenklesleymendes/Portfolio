using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ControlePontoEletronico
{
    public class FuncionarioRobo
    {
        public int FuncionarioId { get; set; }
        public string NumeroPIS { get; set; }
        public string CPF { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public RegimeContratacaoEnum? RegimeContratacaoAntigo { get; set; }
        public DateTime? DataRegimeAnterior { get; set; }
    }
}
