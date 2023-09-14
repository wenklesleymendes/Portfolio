using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class ContratoLocacao : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeProprietario { get; set; }
        public string TelefoneProprietario { get; set; }
        public string NomeImobiliaria { get; set; }
        public string TelefoneFixo { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime? VigenciaInicio { get; set; }
        public DateTime? VigenciaTermino { get; set; }
        public decimal? ValorAluguel { get; set; }
        public decimal? ValorCondominio { get; set; }
        public decimal? ValorIPTU { get; set; }
    }
}
