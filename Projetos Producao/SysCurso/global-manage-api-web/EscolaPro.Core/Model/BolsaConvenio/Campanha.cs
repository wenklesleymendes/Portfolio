using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.BolsaConvenio
{
    public class Campanha : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeCampanha { get; set; }
        public string CodigoPromocao { get; set; }
        public int Parcela { get; set; }
        public double DescontoPlanoPagamento { get; set; }
        public double DescontoTaxaMatricula { get; set; }
        public double DescontoTaxaMateriaDidatico { get; set; }
        public double DescontoTaxaInscricaoProvas { get; set; }
        public bool ExigeComprovante { get; set; }
        public int? CursoId { get; set; }
        public string DescricaoCurso { get; set; }
        public DateTime InicioCampanha { get; set; }
        public DateTime TerminoCampanha { get; set; }
        //public CampanhaCurso CampanhaCurso { get; set; }
        public ICollection<CampanhaTipoPagamento> CampanhaTipoPagamento { get; set; }
        //public ICollection<CampanhaFormaPagamento> CampanhaFormaPagamento { get; set; }
        public ICollection<CampanhaUnidade> CampanhaUnidade { get; set; }
    }
}
