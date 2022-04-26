using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AulasOnline
{
    public class CursoOnline : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int AulaOnlineId { get; set; }
    }
}
