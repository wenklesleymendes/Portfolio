using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Fornecedores
{
    public class Fornecedor : BaseEntity, IIdentityEntity
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
        public Contato Contato { get; set; }
        public string Observacao { get; set; }
        public Categoria Categoria { get; set; }
        public Endereco Endereco { get; set; }
        public DadosBancario DadosBancario { get; set; }
        public ICollection<Anexo> Documentos { get; set; }
        public int? ContatoId { get; set; }
        public int? EnderecoId { get; set; }
        public int? DadosBancarioId { get; set; }
        public int? CategoriaId { get; set; }
    }
}
