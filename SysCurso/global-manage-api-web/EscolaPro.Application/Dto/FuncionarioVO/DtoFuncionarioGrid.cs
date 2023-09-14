using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoFuncionarioGrid
    {
        public int Id { get; set; }
        public List<string> Unidade { get; set; }
        public string NomeColaborador { get; set; }
        public string CPF { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public DateTime? DataContratacao { get; set; }
        public DateTime? DataRecisao { get; set; }
        public List<TipoAnexoEnum> Documentos { get; set; }
        public bool IsActive { get; set; }
        public CalculaDiferencaDatas TempoTrabalhado { get; set; }
    }
}
