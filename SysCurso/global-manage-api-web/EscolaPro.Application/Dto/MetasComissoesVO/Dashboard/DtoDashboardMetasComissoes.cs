using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MetasComissoesVO.Dashboard
{
    public class DtoDashboardMetasComissoes
    {
        public List<DtoVisaoDiaria> VisaoDiaria { get; set; }
        public List<DtoVisaoMensal> VisaoMensal { get; set; }
        public List<DtoMinhasComissoes> MinhasComissoes { get; set; }
        public int MetaTotal { get; set; }
        public int TotalMatriculasRealizadas { get; set; }
        public decimal ValorComissaoEquipe { get; set; }
        public decimal ValorComissaoIndividual { get; set; }
        public decimal TotalMinhasComissoes { get; set; }
    }
}
