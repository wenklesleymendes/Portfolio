using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AulasOnline
{
    public class MateriaOnline : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeMateriaOnline { get; set; }
        public int ProfessorId { get; set; }
        public int AulaOnlineId { get; set; }
        public int MateriaId { get; set; }
        public IEnumerable<VideoAula> VideoAula { get; set; }
    }
}
