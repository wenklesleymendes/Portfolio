using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.UsuarioVO
{
    public class DtoFiltrarUsuario
    {
        public int? FuncionarioId { get; set; }
        public int? CentroCustoId { get; set; }
        public int? UnidadeId { get; set; }
        public bool EhAtendimento { get; set; }
    }
}
