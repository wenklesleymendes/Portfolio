using EscolaPro.Core.Model;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Service.Dto.AlunoQuestionarioProvaVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AlunosVO
{
    public class DtoAluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomeMae { get; set; }
        public SexoEnum Sexo { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string OrgaoExpedicao { get; set; }
        public EstadoCivilEnum EstadoCivil { get; set; }
        public string TituloEleitoral { get; set; }
        public string Zona { get; set; }
        public string Secao { get; set; }
        public string NomeResponsavel { get; set; }
        public string RGResponsavel { get; set; }
        public string CPFResponsavel { get; set; }
        public DtoEndereco Endereco { get; set; }
        public DtoContato Contato { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
        public IEnumerable<DtoAnexo> Anexo { get; set; }
        public int UnidadeId { get; set; }
        public int ContatoId { get; set; }
        public int EnderecoId { get; set; }
        public IEnumerable<DtoAlunoQuestionario> AlunoQuestionario { get; set; }
        public DtoNaturalidade Naturalidade { get; set; }
        public DtoNacionalidade Nacionalidade { get; set; }
        public int NacionalidadeId { get; set; }
        public int? NaturalidadeId { get; set; }

    }
}
