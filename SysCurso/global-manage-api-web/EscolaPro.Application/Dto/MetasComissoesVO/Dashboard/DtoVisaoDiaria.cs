﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MetasComissoesVO.Dashboard
{
    public class DtoVisaoDiaria
    {
        public DateTime DataDiaria { get; set; }
        public int QuantidadeMeta { get; set; }
        public int QuantidadeMatriculasRealizadas { get; set; }
    }
}
