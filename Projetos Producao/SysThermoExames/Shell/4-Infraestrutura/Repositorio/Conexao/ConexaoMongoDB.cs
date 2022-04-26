using MongoDB.Driver;

namespace Repositorio.Conexao
{
    public class ConexaoMongoDB<T> : IConexaoMongoDB<T> where T : class
    {
        private IMongoDatabase db { get; set; }
        private MongoClient cliente = new MongoClient();

        private void ConfigureBD()
        {   
            if (cliente != null)
            {
                db = cliente.GetDatabase(ConstantesDeConexao.StringBD);
            }
        }

        public IMongoCollection<T> GetCollection(string name)
        {
            ConfigureBD();
            return db.GetCollection<T>(name);
        }
    }
}
