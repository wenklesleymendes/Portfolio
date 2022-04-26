using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PortalAlunoProfessor
{
    public class MensagemAlunoProfessor : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public IEnumerable<Anexo> Anexo { get; set; }
        public int AlunoId { get; set; }
        public int ProfessorId { get; set; }
        public DateTime DataEnvio { get; set; }
        public TipoMensagemEnum TipoMensagem { get; set; }
    }
}
