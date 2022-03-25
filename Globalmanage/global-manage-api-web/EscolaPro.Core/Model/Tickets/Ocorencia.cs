using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class Ocorrencia : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int UsuarioLogadoId { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string NumeroProtocolo { get; set; }
        public string Status { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public int? MatriculaId { get; set; }
        public MatriculaAluno Matricula { get; set; }

    }
}
