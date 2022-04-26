using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Conexao
{
    public class ContextoMongoDB
    {
        private readonly IMongoDatabase _dataBase = null;

        public ContextoMongoDB()
        {
            var cliente = new MongoClient(ConstantesDeConexao.StringConexao);
            if (cliente != null)
            {
                _dataBase = cliente.GetDatabase(ConstantesDeConexao.StringBD);
            }
            else
            {
                throw new Exception("Não foi possível conectar ao banco de dados, verifique as configurações do servidor");
            }
        }
    }
}
