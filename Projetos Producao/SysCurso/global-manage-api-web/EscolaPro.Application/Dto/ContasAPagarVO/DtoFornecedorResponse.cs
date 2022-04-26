using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoFornecedorResponse
    {
        public int Id { get; set; }
        public TipoPessoaEnum TipoPessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CpfCnpj { get; set; }
        public DtoEndereco Endereco { get; set; }
        public DtoDadosBancario DadosBancario { get; set; }
    }
}
