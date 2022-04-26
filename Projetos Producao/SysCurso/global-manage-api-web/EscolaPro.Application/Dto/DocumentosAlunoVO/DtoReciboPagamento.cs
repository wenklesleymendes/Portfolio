using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.DocumentosAlunoVO
{
    public class DtoReciboPagamento
    {
        public int[] PagamentoIds { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int MatriculaId { get; set; }
    }
}
