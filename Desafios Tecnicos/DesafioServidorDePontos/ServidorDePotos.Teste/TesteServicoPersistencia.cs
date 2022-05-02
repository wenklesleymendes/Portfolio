using NUnit.Framework;
using ServidorDePontos.Core.Model;
using System;
using Xunit;
using ServidorDePontos.Aplication.Service;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using ServidorDePontos.Repository.Interfaces;
using ServidorDePontos.Aplication.Interface;

namespace ServidorDePotos.Teste
{
    public class TesteServicoPersistencia
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGameResultService service;

        private readonly string gameResultKey = "GameResult";

        public TesteServicoPersistencia(IMemoryCache memoryCache, IGameResultService service)
        {
            this.memoryCache = memoryCache;
            this.service = service;
        }

        [Fact]
        public void PersisteDadosNaMemoria()
        {
            List<GameResult> gameResults = new()
            {
                new GameResult()
                {
                    PlayerId = 1,
                    GameId = 1,
                    Win = 55,
                    Timestamp = DateTime.Now
                },
                new GameResult()
                {
                    PlayerId = 2,
                    GameId = 1,
                    Win = 55,
                    Timestamp = DateTime.Now
                },
                new GameResult()
                {
                    PlayerId = 3,
                    GameId = 1,
                    Win = 55,
                    Timestamp = DateTime.Now
                },
                new GameResult()
                {
                    PlayerId = 4,
                    GameId = 1,
                    Win = 55,
                    Timestamp = DateTime.Now
                }
            };

            service.PersisteDadosNaMemoria(gameResults);

            List<GameResult> value;

            memoryCache.TryGetValue(gameResultKey, out value);
            var teste = value.Count == gameResults.Count;

            Assert.IsTrue(teste);
        }
    }
}
