namespace Acesso.Interfaces.Servicos
{
    public interface IServiceFactory
    {
        T Create<T>(params object[] args) where T : IService;
    }
}
