using Acesso.Dominio.Pessoas.Tipo;
using System;

namespace Acesso.Dominio
{
    [Serializable]
    public class Liberacao
    {
        public Aluno Aluno { get; set; }
        public Operador Operador { get; set; }
        public string Motivo { get; set; }
        public DateTime DataHoraLiberou { get; set; }
        public bool Acessou { get; set; }
        public DateTime DataHoraAcessou { get; set; }
        public decimal TempoParaAcessso { get; set; }

        public Liberacao()
        {
            Aluno = new Aluno();
            Operador = new Operador();
        }

    }
}
