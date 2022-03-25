using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Anexos
{
    public class DocumentoPendente
    {
        public IEnumerable<TipoAnexoEnum> DocumentosPendentes { get; set; }
        public bool DeclaracaoPendenciaDocumental { get; set; }
    }
}
