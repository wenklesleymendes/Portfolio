using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AgendaProvaVO
{
    public class DtoColegioAutorizado
    {
        public int Id { get; set; }
        public string NomeColegioAutorizado { get; set; }
        public string PrimeiroContatoNome { get; set; }
        public string PrimeiroContatoTelefone { get; set; }
        public string PrimeiroContatoEmail { get; set; }
        public string SegundoContatoNome { get; set; }
        public string SegundoContatoTelefone { get; set; }
        public string SegundoContatoEmail { get; set; }
        public string Site { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public DtoEndereco Endereco { get; set; }
        public int EnderecoId { get; set; }
    }
}
