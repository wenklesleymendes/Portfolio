using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoMateriaProfessor
    {
        public int Id { get; set; }
        public bool LinguaPortuguesa { get; set; }
        public bool Artes { get; set; }
        public bool Matematica { get; set; }
        public bool Biologia { get; set; }
        public bool Quimica { get; set; }
        public bool Historia { get; set; }
        public bool Geografia { get; set; }
        public bool Filosofia { get; set; }
        public bool Sociologia { get; set; }
        public bool Ingles { get; set; }
    }
}
