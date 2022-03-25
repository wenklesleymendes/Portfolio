using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Cache;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Unidade : BaseEntity, IIdentityEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public ICollection<HorarioFuncionamento?> HorarioFuncionamento { get; set; }
        public DateTime? VigenciaInicioAVCB { get; set; }
        public DateTime? VigenciaTerminoAVCB { get; set; }
        public DateTime? VigenciaInicioAlvara { get; set; }
        public DateTime? VigenciaTerminoAlvara { get; set; }
        public Endereco? Endereco { get; set; }
        public DadosBancario? DadosBancario { get; set; }
        public ICollection<Anexo?> Anexo { get; set; }
        public ICollection<CentroCusto?> CentroCusto { get; set; }
        public ICollection<UnidadeDespesa?> UnidadeDespesas { get; set; }
        public Contato Contato { get; set; }
        public ContratoLocacao ContratoLocacao { get; set; }
        public ICollection<HistoricoOcorrencias?> HistoricoOcorrencias { get; set; }
        public int? EnderecoId { get; set; }
        public int? ContatoId { get; set; }
        public int? DadosBancarioId { get; set; }
        public int? ContratoLocacaoId { get; set; }
        public string Sigla { get; set; }
        public byte[] Foto { get; set; }
        public string Extensao { get; set; }
        public TipoUnidade TipoUnidade { get; set; }
    }
}
