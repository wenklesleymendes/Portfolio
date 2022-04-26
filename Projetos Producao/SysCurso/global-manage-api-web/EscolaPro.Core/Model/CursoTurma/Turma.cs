using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.CursoTurma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Turma : BaseEntity, IIdentityEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Presencial { get; set; }
        public string Ano { get; set; }
        public string Semestre { get; set; }
        public PeriodoEnum Periodo { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioTermino { get; set; }
        public SalaEnum Sala { get; set; }
        public int QuantidadeVagas { get; set; }
        public bool Disponivel { get; set; }
        public ICollection<TurmaUnidade?> TurmaUnidade { get; set; }
        public ICollection<TurmaCurso?> TurmaCurso { get; set; }
        public bool Segunda { get; set; }
        public bool Terca { get; set; }
        public bool Quarta { get; set; }
        public bool Quinta { get; set; }
        public bool Sexta { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public DateTime? InicioTurma { get; set; }
        public DateTime? TerminoTurma { get; set; }
    }
}
