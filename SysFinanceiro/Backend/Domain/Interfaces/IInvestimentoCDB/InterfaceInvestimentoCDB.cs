using Domain.Interfaces.Generics;
using Entities.Entidades;

namespace Domain.Interfaces.IInvestimentoCDB
{
    public interface InterfaceInvestimentoCDB : InterfaceGeneric<Investimento>
    {
        Task<IList<Investimento>> ListarInvestimentosUsuario(string emailUsuario);
    }
}
