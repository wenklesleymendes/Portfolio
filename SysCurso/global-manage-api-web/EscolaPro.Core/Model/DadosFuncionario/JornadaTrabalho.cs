using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.DadosFuncionario
{
    public class JornadaTrabalho : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }

        public string SegundaFeiraInicio { get; set; }
        public string SegundaFeiraTermino { get; set; }

        public string TercaFeiraInicio { get; set; }
        public string TercaFeiraTermino { get; set; }

        public string QuartaFeiraInicio { get; set; }
        public string QuartaFeiraTermino { get; set; }

        public string QuintaFeiraInicio { get; set; }
        public string QuintaFeiraTermino { get; set; }

        public string SextaFeiraInicio { get; set; }
        public string SextaFeiraTermino { get; set; }

        public string SabadoInicio { get; set; }
        public string SabadoTermino { get; set; }

        public string DomingoInicio { get; set; }
        public string DomingoTermino { get; set; }
    }
}
