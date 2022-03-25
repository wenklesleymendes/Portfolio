using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Anexos
{
    public class DtoAnexoResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte[] Arquivo { get; set; }
        public string ArquivoString { get; set; }
        public DateTime DataAnexo { get; set; }
        public TipoAnexoEnum TipoAnexo { get; set; }
        public string Extensao { get; set; }
        public int UnidadeId { get; set; }
    }
}
