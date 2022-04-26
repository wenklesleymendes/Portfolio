using EscolaPro.Core.Interfaces;
using System;

namespace EscolaPro.Core.Model.ReguaContato
{
    public class ReguaContatoHistorico : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int? ReguaContatoRegrasId { get; set; }
        //public ReguaContatoRegras ReguaContatoRegras { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int? ReguaContatoFilaId { get; set; }
        //public ReguaContatoFila ReguaContatoFila { get; set; }
        public TipoMensagemEnum TipoMensagem { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
