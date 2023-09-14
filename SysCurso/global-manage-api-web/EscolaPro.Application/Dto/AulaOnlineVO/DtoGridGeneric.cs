using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoGridGeneric<T>
    {
        public string Titulo { get; set; }
        public IEnumerable<T> Lista { get; set; }
    }
}
