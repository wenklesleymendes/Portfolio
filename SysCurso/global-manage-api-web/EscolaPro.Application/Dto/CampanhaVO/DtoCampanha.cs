using EscolaPro.Core.Model.BolsaConvenio;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.CampanhaVO
{
    public class DtoCampanha
    {
        public int Id { get; set; }
        public string NomeCampanha { get; set; }
        public string CodigoPromocao { get; set; }
        public int Parcela { get; set; }
        public double DescontoPlanoPagamento { get; set; }
        public double DescontoTaxaMatricula { get; set; }
        public double DescontoTaxaMateriaDidatico { get; set; }
        public double DescontoTaxaInscricaoProvas { get; set; }
        public bool IsActive { get; set; }
        public bool ExigeComprovante { get; set; }
        public DateTime InicioCampanha { get; set; }
        public DateTime TerminoCampanha { get; set; }
        public int CursoId { get; set; }
        public string DescricaoCurso { get; set; }
        public ICollection<DtoCampanhaPlanoPagamento> CampanhaTipoPagamento { get; set; }
        //public ICollection<DtoCampanhaFormaPagamento> CampanhaFormaPagamento { get; set; }
        //public DtoCampanhaCurso CampanhaCurso { get; set; }
        public ICollection<DtoCampanhaUnidade> CampanhaUnidade { get; set; }
    }
}
