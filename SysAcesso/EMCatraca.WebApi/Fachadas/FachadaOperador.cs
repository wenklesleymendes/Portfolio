using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaOperador
    {
        private readonly IRepositorioOperador _repositorioDeOperador = FabricaDeRepositorios.Instancia.CrieRepositorioOperador();

        public IEnumerable<Operador> ConsulteTodosOperadorAtivos()
        {
            return _repositorioDeOperador.ConsulteTodosOperadorAtivos();
        }

        public bool ValideOperador(int codigo, string senha)
        {
            return _repositorioDeOperador.ValideOperador(codigo, senha);
        }
    }
}