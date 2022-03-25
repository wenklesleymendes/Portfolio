using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.AlunoQuestionarioProva;
using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class Aluno : BaseEntity, IIdentityEntity
    {
        private string nome;
        private string nomeMae;
        private string nomeResponsavel;

        public int Id { get; set; }
        public string Nome { get => nome?.ToTitleCase(); set => nome = value?.ToTitleCase(); }
        public DateTime DataNascimento { get; set; }
        public string NomeMae { get => nomeMae?.ToTitleCase(); set => nomeMae = value?.ToTitleCase(); }
        public SexoEnum Sexo { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string OrgaoExpedicao { get; set; }
        public EstadoCivilEnum EstadoCivil { get; set; }
        //public string Nascionalidade { get; set; }
        //public string Naturalidade { get; set; }
        public string TituloEleitoral { get; set; }
        public string Zona { get; set; }
        public string Secao { get; set; }
        public string NomeResponsavel { get => nomeResponsavel?.ToTitleCase(); set => nomeResponsavel = value?.ToTitleCase(); }
        public string RGResponsavel { get; set; }
        public string CPFResponsavel { get; set; }
        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }
        public Unidade Unidade { get; set; }
        public ICollection<Anexo> Anexo { get; set; }
        public int UnidadeId { get; set; }
        public int ContatoId { get; set; }
        public int EnderecoId { get; set; }
        public byte[] Foto { get; set; }
        public IEnumerable<AlunoQuestionario> AlunoQuestionario { get; set; }
        public string Extensao { get; set; }
        public Nacionalidade Nacionalidade { get; set; }
        public Naturalidade Naturalidade { get; set; }
        public int NacionalidadeId { get; set; }
        public int? NaturalidadeId { get; set; }
        public ICollection<MatriculaAluno> Matriculas { get; set; }
    }
}
