using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AulasOnline
{
    public class Resposta : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Pergunta Pergunta { get; set; }
        public string Opcao { get; set; }
        public ICollection<Anexo> Imagem { get; set; }
        //public byte[] Imagem { get; set; }
        //public string Extensao { get; set; }
        public int? PerguntaId { get; set; }
        public bool Correta { get; set; }
    }
}
