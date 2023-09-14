using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoPergunta
    {
        public int Id { get; set; }
        public string DescricaoPergunta { get; set; }
        //public byte[] Imagem { get; set; }
        public string Extensao { get; set; }
        public int? AnexoId { get; set; }
        public string ArquivoString { get; set; }
        //public IEnumerable<DtoAnexo> Imagem { get; set; }
        public IEnumerable<DtoResposta> Resposta { get; set; }
        public int VideoAulaId { get; set; }
        public bool IsDelete { get; set; }
    }
}
