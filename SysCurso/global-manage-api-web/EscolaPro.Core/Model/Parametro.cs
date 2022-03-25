﻿using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Parametro : IIdentityEntity 
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}
