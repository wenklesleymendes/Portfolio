using System;

namespace EMCatraca.Core.Dominio
{
    [Serializable]
    public class SerieTurma
    {
        public Serie Serie { get; set; }
        public Turma Turma { get; set; }
    }
}
