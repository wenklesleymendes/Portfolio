using System;
using System.Collections.Generic;

namespace EscolaPro.Service.Dto.AtendimentoVO
{
    public class DtoAtendimento
    {
        public int Id { get; set; }
        public int CanaldeAtendimento { get; set; }
        public string NomedoCliente { get; set; }
        public int CursodeInteresse { get; set; }
        public string Celular { get; set; }
        public string TelefoneFixo { get; set; }
        public int Periodo { get; set; }
        public int MotivodeInteressenoCurso { get; set; }
        public int ComonosConheceu { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int Status { get; set; }
        public bool ExisteAgendamento { get; set; }
        public int AgendamentodaMatricula { get; set; }
        public string DiadoAgendamento { get; set; }
        public string DataeHoradoAtendimento { get; set; }
        public string HoradoAgendamento { get; set; }
        public string DataeHoradoAgendamento { get; set; }
        public int? MotivodoNaoAgendamento { get; set; }
        public bool ExisteMatricula { get; set; }
        public int UsuarioLogado { get; set; }
        public int UsuarioCadastro { get; set; }
        public int UnidadeCadastro { get; set; }

        public int UnidadeLogada { get; set; }
        public DateTime StatusAlteracao { get; set; }
    }
}
