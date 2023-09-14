using EscolaPro.Core.Model.ControlePontoEletronico;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoFeriasFuncionario
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public ICollection<DtoAnexo?> Anexo { get; set; }
        public TipoFeriasFolgaFalta TipoFeriasFolgaFalta { get; set; }
        //public bool ApenasFerias { get; set; }
        public string Observacao { get; set; }
        public int? FuncionarioId { get; set; }
    }
}
