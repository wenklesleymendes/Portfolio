using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.BolsaConvenio
{
    public class CampanhaCurso : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int CampanhaId { get; set; }
        public int CursoId { get; set; }
    }
}
