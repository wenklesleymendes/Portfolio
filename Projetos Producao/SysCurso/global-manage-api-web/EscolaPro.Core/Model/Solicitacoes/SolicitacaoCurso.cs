using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class SolicitacaoCurso : IIdentityEntity
    {
        public int Id { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
