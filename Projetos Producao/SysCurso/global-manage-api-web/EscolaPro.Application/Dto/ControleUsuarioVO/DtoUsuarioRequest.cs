using EscolaPro.Service.Dto.ControlePontoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ControleUsuarioVO
{
    public class DtoUsuarioRequest
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string CPF { get; set; }
        public int? PerfilUsuarioId { get; set; }
        public int? FuncionarioId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? UnidadeId { get; set; }
        public bool IsActive { get; set; }

        //public DtoPerfilUsuarioRequest PerfilUsuario { get; set; }
        //public DtoFuncionarioRequest Funcionario { get; set; }
        //public DtoCentroCusto Departamento { get; set; }
        //public DtoUnidadeTurma Unidade { get; set; }
    }
}
