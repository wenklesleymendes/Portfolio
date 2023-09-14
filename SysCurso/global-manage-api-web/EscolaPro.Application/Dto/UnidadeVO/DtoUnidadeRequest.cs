using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoUnidadeRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public ICollection<DtoHorarioFuncionamento?> HorarioFuncionamento { get; set; }
        public DateTime? VigenciaInicioAVCB { get; set; }
        public DateTime? VigenciaTerminoAVCB { get; set; }
        public DateTime? VigenciaInicioAlvara { get; set; }
        public DateTime? VigenciaTerminoAlvara { get; set; }
        public DtoEndereco? Endereco { get; set; }
        public DtoDadosBancario? DadosBancario { get; set; }
        public ICollection<DtoUnidadeDespesa?> UnidadeDespesas { get; set; }
        //public ICollection<TurmaUnidade?> UnidadeTurma { get; set; }
        public DtoContato Contato { get; set; }
        public DtoContratoLocacao ContratoLocacao { get; set; }
        public ICollection<DtoHistoricoOcorrencias?> HistoricoOcorrencias { get; set; }
        public int? HorarioFuncionamentoId { get; set; }
        public int? EnderecoId { get; set; }
        public int? ContatoId { get; set; }
        public int? DadosBancarioId { get; set; }
        public int? ContratoLocacaoId { get; set; }
        public string Sigla { get; set; }
    }
}
