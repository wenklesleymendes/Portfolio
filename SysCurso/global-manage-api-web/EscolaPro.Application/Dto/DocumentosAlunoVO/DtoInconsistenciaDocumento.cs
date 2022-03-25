using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.DocumentosAlunoVO
{
    public class DtoInconsistenciaDocumento
    {
        public int Id { get; set; }
        public int DocumentoEnum { get; set; }
        public int MatriculaAlunoId { get; set; }
    }

    public class DtoInconsistenciaDocumentoRequest
    {
        public int MatriculaId { get; set; }
        public int[] TipoAnexoEnum { get; set; }
    }
}
