using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoFuncionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DtoContatoFuncionario Contato { get; set; }
        public DtoEndereco Endereco { get; set; }
        public DtoDadosContratacao DadosContratacao { get; set; }
        public DtoDadosBancario DadosBancario { get; set; }
        //public List<DtoAnexo> Documentos { get; set; }
        public DtoAgenteIntegracao AgenteIntegracao { get; set; }
        public DtoJornadaTrabalho JornadaTrabalho { get; set; }
        //public DtoMateriaProfessor MateriaProfessor { get; set; }
        public ICollection<DtoCursoProfessor?> CursoProfessor { get; set; }
        public IEnumerable<DtoSalarioUnidade> SalarioUnidade { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
    }
}
