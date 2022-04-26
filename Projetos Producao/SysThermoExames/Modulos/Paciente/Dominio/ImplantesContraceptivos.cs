using MdPaciente.Dominio.Enums;
using System;

namespace MdPaciente.Dominio
{
    public class ImplantesContraceptivos
    {
        public Guid Id { get; set; }
        public DateTime DataImplante { get; set; }
        public EnumImplantesContraceptivos EnumImplantesContraceptivos { get; set; }

    }
}
