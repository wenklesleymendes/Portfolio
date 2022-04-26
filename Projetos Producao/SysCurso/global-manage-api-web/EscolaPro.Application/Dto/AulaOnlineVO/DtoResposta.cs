using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AulaOnlineVO
{
    public class DtoResposta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        //public DtoPergunta Pergunta { get; set; }
        public string Opcao { get; set; }
        //public byte[] Imagem { get; set; }
        //public IEnumerable<DtoAnexo> Imagem { get; set; }
        public string ArquivoString { get; set; }
        public string Extensao { get; set; }
        public int? AnexoId { get; set; }

        public int PerguntaId { get; set; }
        public bool Correta { get; set; }
        public bool IsDelete { get; set; }
    }
}
