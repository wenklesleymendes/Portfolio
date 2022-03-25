using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioResponsavelAPI : IRepositorioResponsavel
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioResponsavelAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public Responsavel ConsulteResponsavel(int idResponsavel)
        {
            return _apiConexao.Get<Responsavel>("Responsavel", $"ConsulteResponsavel?idResponsavel={idResponsavel}");
        }

        public IEnumerable<Responsavel> ConsulteTodosResponsavelAtivos()
        {
            return _apiConexao.Get<List<Responsavel>>("Responsavel", "ConsulteTodosResponsavelAtivos");
        }

        public bool ResponsavelEstaAtivo(int idResponsavel)
        {
            return _apiConexao.Get<bool>("Responsavel", $"ResponsavelEstaAtivo?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaBloqueado(int idResponsavel)
        {
            return _apiConexao.Get<bool>("Responsavel", $"ResponsavelEstaBloqueado?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaInadimplenteDeCheques(int idResponsavel)
        {
            return _apiConexao.Get<bool>("Responsavel", $"ResponsavelEstaInadimplenteDeCheques?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaInadimplenteDuplicata(int idResponsavel)
        {
            return _apiConexao.Get<bool>("Responsavel", $"ResponsavelEstaInadimplenteDuplicata?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaPendenteDeDocumentos(int idResponsavel)
        {
            return _apiConexao.Get<bool>("Responsavel", $"ResponsavelEstaPendenteDeDocumentos?idResponsavel={idResponsavel}");
        }

        public bool ResponsavelEstaPendenteDeMateriais(int idResponsavel)
        {
            return _apiConexao.Get<bool>("Responsavel", $"ResponsavelEstaPendenteDeDocumentos?idResponsavel={idResponsavel}");
        }
    }
}
