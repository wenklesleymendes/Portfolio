using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.CursoTurma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Curso : BaseEntity, IIdentityEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        public List<Materia> Materia { get; set; }
        public bool NacionatalTec { get; set; }
    }
}
