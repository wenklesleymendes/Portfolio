using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.CadastroAluno
{
    public class FiltrarAluno
    {
        public int? UnidadeId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string NumeroMatricula { get; set; }
        public ComoConheceuEnum? ComoConheceuEnum { get; set; }
        public string DataInicioMatricula { get; set; }
        public string DataFimMatricula { get; set; }
        public int? CursoId { get; set; }
        public bool? Presencial { get; set; }
        public string Ano { get; set; }
        public int? Semestre { get; set; }
        public bool? Segunda { get; set; }
        public bool? Terca { get; set; }
        public bool? Quarta { get; set; }
        public bool? Quinta { get; set; }
        public bool? Sexta { get; set; }
        public bool? Sabado { get; set; }
        public bool? Domingo { get; set; }
        public int? Periodo { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public int? Sala { get; set; }
        public bool? StatusMatricula { get; set; }
        public bool VerTodosUnidades { get; set; }
        public bool? StatusDocumento { get; set; }
        //public CertificadoProva Status { get; set; }

    }
}
