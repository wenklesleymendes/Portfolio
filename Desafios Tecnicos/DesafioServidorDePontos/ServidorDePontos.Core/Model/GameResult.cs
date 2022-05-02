using ServidorDePontos.Core.Interfaces;

namespace ServidorDePontos.Core.Model
{
    public class GameResult : IIdentityEntity
    {
        public int Id { get; set; }
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public long Win { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
