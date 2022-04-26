using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.CursoTurma;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AulasOnline
{
    public class VideoAula : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string TituloAula { get; set; }
        public string URLVideo { get; set; }
        public IEnumerable<Pergunta> Pergunta { get; set; }
        public int MateriaId { get; set; }
    }
}
