using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class DadosBancario : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public TipoContaBancariaEnum? TipoContaBancaria { get; set; }
    }
}
