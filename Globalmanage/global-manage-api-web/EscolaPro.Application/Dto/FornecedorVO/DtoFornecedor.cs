using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FornecedorVO
{
    public class DtoFornecedor
    {
        public int Id { get; set; }
        public TipoPessoaEnum TipoPessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool Isento { get; set; }
        public string Observacao { get; set; }
        public DtoContato Contato { get; set; }
        public DtoEndereco Endereco { get; set; }
        public DtoCategoria Categoria { get; set; }
        public DtoDadosBancario DadosBancario { get; set; }
        public ICollection<DtoAnexo> Documentos { get; set; }
        public int? ContatoId { get; set; }
        public int? EnderecoId { get; set; }
        public int? DadosBancarioId { get; set; }
        public int? CategoriaId { get; set; }
        public bool IsActive { get; set; }
    }
}
