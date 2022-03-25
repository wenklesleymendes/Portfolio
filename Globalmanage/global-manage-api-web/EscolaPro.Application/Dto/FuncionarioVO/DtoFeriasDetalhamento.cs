using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoFeriasDetalhamento
    {
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public DateTime? DataContratacao { get; set; }
        public DateTime? DataRecisao { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public List<DtoFeriasDataDetalhada> FeriasDataDetalhadas { get; set; }
    }
}
