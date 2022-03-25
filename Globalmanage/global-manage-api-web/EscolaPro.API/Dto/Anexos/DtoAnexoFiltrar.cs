using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Anexos
{
    public class DtoAnexoFiltrar
    {
        public int IdUnidade { get; set; }
        public int IdFerias { get; set; }
        public int IdPontoEletronico { get; set; }
        public int IdFuncionario { get; set; }
        public int IdAluno { get; set; }
        public int MensagemTicketId { get; set; }
        public int FornecedorId { get; set; }
    }
}
