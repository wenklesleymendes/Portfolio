using EscolaPro.Core.Model.PortalAlunoProfessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PortalAlunoProfessorVO
{
    public class DtoMensagemAlunoProfessor
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public IEnumerable<DtoAnexo> Anexo { get; set; }
        public int AlunoId { get; set; }
        public int ProfessorId { get; set; }
        public DateTime DataEnvio { get; set; }
        public TipoMensagemEnum TipoMensagem { get; set; }

    }
}
