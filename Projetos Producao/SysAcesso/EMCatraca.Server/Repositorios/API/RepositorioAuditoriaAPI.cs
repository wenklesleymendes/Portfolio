using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioAuditoriaAPI : IRepositorioAuditoria
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioAuditoriaAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public void RegistreAuditoria(Auditoria auditoria)
        {
            _apiConexao.Post("Auditoria", "RegistreAuditoria", auditoria);
        }

        public void RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso)
        {
            _apiConexao.Post("Auditoria", "RegistreAuditoriaDeAcesso", auditoriaAcesso);
        }
    }
}
