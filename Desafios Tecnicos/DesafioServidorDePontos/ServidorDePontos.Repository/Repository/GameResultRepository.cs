using Microsoft.EntityFrameworkCore;
using ServidorDePontos.Core.Model;
using ServidorDePontos.Repository.Interfaces;
using System.Linq;

namespace ServidorDePontos.Repository.Repository
{
    public class GameResultRepository : DomainRepository<GameResult>, IGameResultRepository
    {
        public GameResultRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<GameResult>> Busca100Primeiros()
        {

            var query = dbSet.FromSqlRaw("Select Id, PlayerId, GameId, Win, MAX(Timestamp) As Timestamp From GameResult Group By PlayerId Order By Win Desc LIMIT 100");

            return query;
        }
    }
}
