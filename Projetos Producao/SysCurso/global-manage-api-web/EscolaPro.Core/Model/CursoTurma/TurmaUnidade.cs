using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class TurmaUnidade : BaseEntity, IIdentityEntity
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UnidadeId { get; set; }
        public int? TurmaId { get; set; }
        //public ICollection<Turma?> Turma { get; set; }
        //public ICollection<Unidade?> Unidade { get; set; }
    }
}
