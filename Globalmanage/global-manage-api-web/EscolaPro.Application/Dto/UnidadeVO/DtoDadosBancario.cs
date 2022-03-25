using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoDadosBancario
    {
        public int Id { get; set; }
        public string CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public TipoContaBancariaEnum? TipoContaBancaria { get; set; }
    }
}
