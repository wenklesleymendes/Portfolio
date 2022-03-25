using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ControlePontoEletronico
{
    public class ArquivoPonto : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
