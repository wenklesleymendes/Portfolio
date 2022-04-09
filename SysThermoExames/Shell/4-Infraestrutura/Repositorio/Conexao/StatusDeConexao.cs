using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Conexao
{
    public class StatusDeConexao
    {
        private MongoClient cliente = new MongoClient();
        public void VerifiqueConexao()
        {
            cliente.ListDatabases();
        }
    }
}
