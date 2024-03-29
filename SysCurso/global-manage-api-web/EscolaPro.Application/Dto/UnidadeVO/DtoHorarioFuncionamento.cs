﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoHorarioFuncionamento
    {
        public int Id { get; set; }
        public bool FinalSemana { get; set; }
        public bool ComAula { get; set; }
        public string SemanaInicio { get; set; }
        public string SemanaTermino { get; set; }
        public string SabadoInicio { get; set; }
        public string SabadoTermino { get; set; }
        public int UnidadeId { get; set; }
    }
}
