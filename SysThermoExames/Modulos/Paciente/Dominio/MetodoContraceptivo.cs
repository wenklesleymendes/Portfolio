using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente.Dominio
{
    public class MetodoContraceptivo
    {
        public Guid Id { get; set; }
        public int TempoUsoAnos { get; set; }
        public int TempoUsoMeses { get; set; }
        public string NomeContraceptivo { get; set; }
    }
}
