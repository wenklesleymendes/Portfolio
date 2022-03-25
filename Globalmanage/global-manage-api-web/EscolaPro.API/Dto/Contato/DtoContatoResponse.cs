using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Contato
{
    public class DtoContatoResponse
    {
        public int Id { get; set; }
        public int IdUnidade { get; set; }
        public string Email { get; set; }
        public bool ReceberEmail { get; set; }
        public string Celular { get; set; }
        public string RecebeSMS { get; set; }
        public string TelefoneFixo { get; set; }
        public ComoConheceuEnum ComoConheceuEnum { get; set; }
        public string TelefoneFixoPrincipal { get; set; }
        public string TelefoneFixo1 { get; set; }
        public string TelefoneFixo2 { get; set; }
        public string TelefoneFixo3 { get; set; }
        public string TelefoneFixo4 { get; set; }
        public string WhatsApp { get; set; }
        public string FaceBook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
    }
}
