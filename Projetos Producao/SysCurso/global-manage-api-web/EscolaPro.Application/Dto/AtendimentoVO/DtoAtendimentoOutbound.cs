using System;

namespace EscolaPro.Service.Dto.AtendimentoVO
{
    public class DtoAtendimentoOutbound
    {
        public int Id { get; set; }
        public int AtendimentoId { get; set; }
        public int MatriculaAgendada { get; set; }
        public int ScoreInicial { get; set; }
        public int ScoreAplicado { get; set; }
        public string Observacoes { get; set; }
        public int NumeroTentativa { get; set; }
        public int UsuarioLogado { get; set; }
        public int UsuarioCadastrado { get; set; }

        public DateTime DataHoraContato { get; set; }

        public int AgendamentodaMatricula { get; set; }
        public string DiadoAgendamento { get; set; }
        public string HoradoAgendamento { get; set; }
        public string DataeHoradoAgendamento { get; set; }
        public int? MotivodoNaoAgendamento { get; set; }

    }
}
