using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeSerieTurma
    {
        private readonly IRepositorioSerieTurma _repositorioDeSerieTurma = FabricaDeRepositorios.Instancia.CrieRepositorioSerieTurma();

        public List<SerieTurma> ConsulteTodasSeriesTurmas()
        {
            return _repositorioDeSerieTurma.ConsulteTodasSeriesTurmas();
        }
    }
}