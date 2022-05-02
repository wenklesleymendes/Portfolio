using ServidorDePontos.Core.Model;
using ServidorDePontos.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorDePontos.Aplication.Interface
{
    public interface IGameResultService
    {
        Task PersisteDadosNaMemoria(List<GameResult> gameResults);
        Task<List<LeaderBoardDto>> ConsultaDadosPersistidos();
    }
}
