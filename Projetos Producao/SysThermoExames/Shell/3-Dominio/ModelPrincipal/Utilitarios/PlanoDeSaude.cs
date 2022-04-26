using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPrincipal.Utilitarios
{
    public class PlanoDeSaude
    {
        public string Nome { get; set; }

        public string RazaoSocial { get; set; }

        public int CodigoANS { get; set; }

        public CNPJ CNPJ { get; set; }

        public bool Situacao { get; set; }
    }
}
