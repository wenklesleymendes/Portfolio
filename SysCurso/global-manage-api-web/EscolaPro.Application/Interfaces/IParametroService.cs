using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IParametroService
    {
        Task<bool> Atualizar(Parametro parametro);
        Task<string> BuscarParametroPorId(int id);
        Task<string> BuscarParametroPorChave(string chave);
    }
}
