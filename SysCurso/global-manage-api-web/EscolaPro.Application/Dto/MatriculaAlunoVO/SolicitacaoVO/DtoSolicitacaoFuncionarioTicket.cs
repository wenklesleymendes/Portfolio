using EscolaPro.Service.Dto.FuncionarioVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO
{
    public class DtoSolicitacaoFuncionarioTicket
    {
        public int Id { get; set; }
        public DtoFuncionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
