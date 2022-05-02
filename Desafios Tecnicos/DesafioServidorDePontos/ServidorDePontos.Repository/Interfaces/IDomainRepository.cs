using ServidorDePontos.Core.Interfaces;

namespace ServidorDePontos.Repository.Interfaces
{
    public interface IDomainRepository<TEntidade> : IRepositoryBase<TEntidade> where TEntidade : class, IIdentityEntity
    {
    }
}
