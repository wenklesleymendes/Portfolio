using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AulasOnline
{
    public class Pergunta : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string DescricaoPergunta { get; set; }
        public ICollection<Anexo> Imagem { get; set; }
        public IEnumerable<Resposta> Resposta { get; set; }
        public int VideoAulaId { get; set; }
    }
}
