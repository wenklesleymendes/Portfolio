using EscolaPro.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UnidadeVO
{
    public class DtoContato
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool ReceberEmail { get; set; }
        public string Celular { get; set; }
        public bool RecebeSMS { get; set; }
        public string TelefoneFixo { get; set; }
        public ComoConheceuEnum ComoConheceuEnum { get; set; }
        public string TelefoneFixoPrincipal { get; set; }
        public string TelefoneFixo1 { get; set; }
        public string TelefoneFixo2 { get; set; }
        public string TelefoneFixo3 { get; set; }
        public string TelefoneFixo4 { get; set; }
        public string TelefoneFixo5 { get; set; }
        public string WhatsApp { get; set; }
        public string FaceBook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
        public string Ramal { get; set; }
        public string Fax { get; set; }
        public string Token { get; set; }
        public bool ReceberWhatsApp { get; set; }
        public bool ReceberFacebook { get; set; }
        public bool ReceberInstagram { get; set; }
    }
}
