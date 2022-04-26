using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.ControlePontoEletronico;
using System;
using System.Collections.Generic;

namespace EscolaPro.Core.Model.DadosFuncionario
{
    public class Funcionario : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public Contato Contato { get; set; }
        public Endereco Endereco { get; set; }
        public DadosBancario DadosBancario { get; set; }
        public DadosContratacao DadosContratacao { get; set; }
        public ICollection<Anexo> Documentos { get; set; }
        public AgenteIntegracao? AgenteIntegracao { get; set; }
        public JornadaTrabalho JornadaTrabalho { get; set; }
        public MateriaProfessor? MateriaProfessor { get; set; }
        public ICollection<CursoProfessor?> CursoProfessor { get; set; }
        public ICollection<SalarioUnidade?> SalarioUnidade { get; set; }
        public int? ContatoId { get; set; }
        public int? EnderecoId { get; set; }
        public int? DadosBancarioId { get; set; }
        public int? AgenteIntegracaoId { get; set; }
        public int? DadosContratacaoId { get; set; }
        public int? JornadaTrabalhoId { get; set; }
        public int? MateriaProfessorId { get; set; }
        public ICollection<PontoEletronico> PontoEletronico { get; set; }
        public ICollection<FeriasFuncionario> Ferias { get; set; }
    }
}
