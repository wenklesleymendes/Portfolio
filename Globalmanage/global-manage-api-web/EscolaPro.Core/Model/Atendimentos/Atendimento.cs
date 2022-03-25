using EscolaPro.Core.Interfaces;
using System;

namespace EscolaPro.Core.Model.Atendimentos
{
    public class Atendimento : BaseEntity, IIdentityEntity
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
        public DateTime? DataeHoradoAgendamento { get; set; }
        public DateTime DataeHoradoAtendimento { get; set; }
        public bool ExisteMatricula { get; set; }        
        public int UsuarioLogado { get; set; }
        public int UsuarioCadastro { get; set; }
        public int UnidadeCadastro { get; set; }
        public DateTime StatusAlteracao { get; set; }
        public int AgendamentodaMatricula { get; set; }
        public int? MotivodoNaoAgendamento { get; set; }
        public string Observacoes { get; set; }
    }
}
