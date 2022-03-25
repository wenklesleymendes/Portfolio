using EscolaPro.Core.Extensions;
using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
   public  class DtoCancelamentoAutorizacaoIsencao
    {
        public string Login { get; set; }
        private string senha;
        public string Senha
        {
            get
            {
                if (string.IsNullOrEmpty(senha))
                {
                    return "";
                }
                return Criptografia.CreateMD5(senha);
            }
            set
            {
                senha = value;
            }
        }
        public string Justificativa { get; set; }
        public DtoCancelamentoMatriculaRequest CancelamentoMatricula { get; set; }
        public List<DtoPagamento> Pagamentos { get; set; }
    }
}
