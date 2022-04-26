using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IInstituicaoBancariaService
    {
        Task<IEnumerable<InstituicaoBancaria>> BuscarTodos();
    }
}
