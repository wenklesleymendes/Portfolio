using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FolhaPagamentoVO
{
    public class DtoHoleriteFolhaPagamento
    {
        public DtoFuncionario Funcionario { get; set; }
        public List<DtoPagamentoFolha> ListaPagamento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorTotalAPagar { get; set; }
        public decimal TotalDesconto { get; set; }
        public DtoUnidadeResponse Unidade { get; set; }
        public DateTime? Competencia { get; set; }
    }

    public class DtoPagamentoFolha
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
