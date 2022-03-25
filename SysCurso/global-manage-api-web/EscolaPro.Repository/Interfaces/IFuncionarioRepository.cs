using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IFuncionarioRepository : IDomainRepository<Funcionario>
    {
        Task<List<SalarioUnidade>> InserirSalarioUnidade(List<SalarioUnidade> salariosUnidade);
        Task<Funcionario> BuscarPorId(int idFuncionario);
        Task<Funcionario> BuscarPorCPF(string cpf);
        Task<IEnumerable<Funcionario>> BuscarTodosPorFiltro(int? idUnidade, string nome, bool? ativo, string cpf, DateTime? datainicioTerminoContrato, DateTime? dataFimTerminoContrato);
        Task<List<FuncionarioRobo>> ListaPIS();
        Task<Funcionario> Atualizar(Funcionario funcionario);
        Task<Funcionario> BuscarPorMateria(int materiaId);
    }
}
