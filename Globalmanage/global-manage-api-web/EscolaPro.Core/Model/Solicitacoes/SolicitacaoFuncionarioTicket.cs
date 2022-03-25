using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class SolicitacaoFuncionarioTicket : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DadosFuncionario.Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
