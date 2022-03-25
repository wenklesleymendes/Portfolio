using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeColaborador
    {
        private readonly IRepositorioColaborador _repositorioDeColaborador = FabricaDeRepositorios.Instancia.CrieRepositorioColaborador();

        public Colaborador ConsulteColaborador(int idColaborador)
        {
            return _repositorioDeColaborador.ConsulteColaborador(idColaborador);
        }

        public IEnumerable<Colaborador> ConsulteTodosColaboradorAtivos()
        {
            return _repositorioDeColaborador.ConsulteTodosColaboradorAtivos();
        }

        public bool ColaboradorEstaAtivo(int idColaborador)
        {
            return _repositorioDeColaborador.ColaboradorEstaAtivo(idColaborador);
        }
    }
}