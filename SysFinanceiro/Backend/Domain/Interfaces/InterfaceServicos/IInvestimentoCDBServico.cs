using Entities.Entidades;

namespace Domain.Interfaces.InterfaceServicos
{
    public interface IInvestimentoCDBServico
    {
        DadosCDB CalculaCDB(DadosCDB dados);
    }
}
