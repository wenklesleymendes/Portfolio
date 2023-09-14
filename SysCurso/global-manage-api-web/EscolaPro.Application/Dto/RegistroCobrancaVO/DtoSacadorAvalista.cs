using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.RegistroCobrancaVO
{
    public class DtoSacadorAvalista
    {
        public string cpf_cnpj_sacador_avalista { get; set; }
        public string nome_sacador_avalista { get; set; }
        public string logradouro_sacador_avalista { get; set; }
        public string bairro_sacador_avalista { get; set; }
        public string cidade_sacador_avalista { get; set; }
        public string uf_sacador_avalista { get; set; }
        public string cep_sacador_avalista { get; set; }
    }
}
