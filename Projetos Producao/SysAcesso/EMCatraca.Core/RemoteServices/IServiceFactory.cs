namespace EMCatraca.Core.RemoteServices
{
    public interface IServiceFactory
    {
        T Create<T>(params object[] args) where T : IService;
    }
}
