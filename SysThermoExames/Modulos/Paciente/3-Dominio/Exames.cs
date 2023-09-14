using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente._3_Dominio
{
    class Exames
    {
        public Guid Id { get; set; }

        public int CodigoPaciente { get; set; }

        public DateTime Data { get; set; }

        public string TipoExame { get; set; }

        public decimal Peso { get; set; }

        public string Anamnese { get; set; }

        public string Laudo { get; set; }

        public EnumeradorDeStatus Status { get; set; }
    }
}
