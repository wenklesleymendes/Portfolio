using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AlunoQuestionarioProva
{
    public class AlunoQuestionarioReposta : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Opcao { get; set; }
    }
}
