using MdPaciente.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente.Dominio
{
    public class MedicacaoFarmacos
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public EnumTipoMedicacao EnumTipoMedicacao { get; set; }
    }
}
