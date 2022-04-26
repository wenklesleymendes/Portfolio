using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IContatoRepository : IDomainRepository<Contato>
    {
        Task<Contato> PorIdContato(int idContato);
        Task<Contato> BuscarPorCelular(string celular);
        Task<Contato> BuscarPorEmail(string email);
    }
}

