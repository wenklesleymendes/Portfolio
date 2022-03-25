using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Usuario : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public PerfilUsuario PerfilUsuario { get; set; }
        public Model.DadosFuncionario.Funcionario Funcionario { get; set; }
        public CentroCusto Departamento { get; set; }
        public Unidade Unidade { get; set; }
        public int? PerfilUsuarioId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? FuncionarioId { get; set; }
        public int? UnidadeId { get; set; }
        public int? AlunoId { get; set; }
        public bool IsAluno { get; set; }
        public Aluno Aluno { get; set; }
        public bool IsPrimeiroAcesso { get; set; }
    }
}
