using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class InstituicaoBancaria : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeBanco { get; set; }
        public string CodigoBanco { get; set; }
    }
}
