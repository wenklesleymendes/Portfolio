using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using ServidorDePontos.Aplication.Interface;
using Xunit;

namespace ServidorDePotos.Teste
{
    public class TesteConsultaDoRank
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGameResultService service;

        public TesteConsultaDoRank(IMemoryCache memoryCache, IGameResultService service)
        {
            this.memoryCache = memoryCache;
            this.service = service;
        }

        [Fact]
        public void RealizaConsultaDoRank()
        {
            var result = service.ConsultaDadosPersistidos();

            Assert.NotNull(result);
        }
    }
}