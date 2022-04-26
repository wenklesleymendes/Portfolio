using MongoDB.Driver;

namespace Repositorio.Conexao
{
    public interface IConexaoMongoDB<T> where T : class
    {
        IMongoCollection<T> GetCollection(string name);
    }
}
