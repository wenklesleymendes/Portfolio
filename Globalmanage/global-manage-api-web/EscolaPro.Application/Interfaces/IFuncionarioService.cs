using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Service.Dto.FuncionarioVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IFuncionarioService
    {
        Task<DtoFuncionario> Inserir(DtoFuncionario funcionario);
        Task<DtoFuncionario> BuscarPorId(int idFuncionario);
        Task<DtoFuncionario> BuscarPorCPF(string cpf);
        Task<IEnumerable<DtoFuncionarioGrid>> BuscarTodos();
        Task<IEnumerable<DtoFuncionarioGrid>> BuscarTodosPorFiltro(int? idUnidade, string nome, bool? ativo, string cpf, DateTime? dataTerminoContrato, DateTime? dataFimTerminoContrato); 
        public Task<DtoFuncionario> AtivarOuDesativar(int idFuncionario);
        public Task<bool> Deletar(int idFuncionario);
        Task<DtoFuncionario> BuscarPorMateria(int materiaId);
    }
}
