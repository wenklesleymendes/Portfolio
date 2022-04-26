using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class DtoMateria
    {
        public int Id { get; set; }
        public string NomeMateria { get; set; }
        public int Ordenacao { get; set; }
        public bool IsDelete { get; set; }
    }
}
