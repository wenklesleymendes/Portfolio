using EscolaPro.Core.Model.Funcionario;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Service.Dto.FuncionarioVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FolhaPagamentoVO
{
    public class DtoFolhaPagamentoGrid
    {
        public int Id { get; set; }
        public string NomeColaborador { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public string Unidade { get; set; }
        public decimal ValorPagamento { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public int? FuncionarioId { get; set; }
        public DateTime? Competencia { get; set; }
    }
}
