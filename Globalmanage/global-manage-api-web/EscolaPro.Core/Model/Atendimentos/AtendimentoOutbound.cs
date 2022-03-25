using EscolaPro.Core.Interfaces;
using System;

namespace EscolaPro.Core.Model.Atendimentos
{
    public class AtendimentoOutbound : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int AtendimentoId { get; set; }
        public DateTime DataHoraContato { get; set; }
        public int MatriculaAgendada { get; set; }
        public string Observacao { get; set; }
        public int UsuarioLogado { get; set; }
        public int UsuarioCadastro { get; set; }
        public int ScoreInicial { get; set; }
        public int ScoreAplicado { get; set; }
        public int NumeroOutbound { get; set; }


        public DateTime? DataeHoradoAgendamento { get; set; }

        public int? MotivodoNaoAgendamento { get; set; }

        public int AgendamentodaMatricula { get; set; }
    }
}
