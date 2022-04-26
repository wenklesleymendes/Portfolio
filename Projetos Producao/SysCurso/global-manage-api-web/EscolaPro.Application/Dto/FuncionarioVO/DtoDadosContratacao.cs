using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoDadosContratacao
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public RegimeContratacaoEnum TipoRegimeContratacao { get; set; }
        public DateTime? DataAtestadoAdmissao { get; set; }
        public DateTime? DataAtestadoDemissao { get; set; }
        public DateTime? DataRecisao { get; set; }
        public string TempoAlmoco { get; set; }
        //public decimal Salario { get; set; }
        public decimal? ValeTransporte { get; set; }
        public decimal? ValeAlimentacao { get; set; }
        public string NumeroCT { get; set; }
        public string SerieCT { get; set; }
        public DateTime? DataEmissaoCT { get; set; }
        public string CargaHorarioSemanalCT { get; set; }
        public string NumeroPIS { get; set; }
        public string NumeroTituloEleitor { get; set; }
        public string ZonaTituloEleitor { get; set; }
        public string SecaoTituloEleitor { get; set; }

        public RegimeContratacaoEnum? TipoRegimeContratacaoAnterior { get; set; }
        public DateTime? DataAlteracaoRegime { get; set; }
    }
}
