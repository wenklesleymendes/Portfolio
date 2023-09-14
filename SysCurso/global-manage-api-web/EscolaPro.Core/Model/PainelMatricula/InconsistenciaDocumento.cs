using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class InconsistenciaDocumento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int DocumentoEnum { get; set; }
        public int MatriculaAlunoId { get; set; }
    }
}
