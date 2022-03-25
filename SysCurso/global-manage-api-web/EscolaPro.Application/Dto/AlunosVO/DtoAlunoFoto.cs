using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AlunosVO
{
    public class DtoAlunoFoto
    {
        public int? AlunoId { get; set; }
        public byte[] Foto { get; set; }
        public string Extensao { get; set; }
        public int? UnidadeId { get; set; }
        public int? SolicitacaoId { get; set; }
    }
}
