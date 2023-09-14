using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeAuditoria
    {
        private readonly IRepositorioAuditoria _repositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();

        public void RegistreAuditoria(Auditoria auditoria)
        {
            _repositorioAuditoria.RegistreAuditoria(auditoria);
        }

        public void RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso)
        {
            _repositorioAuditoria.RegistreAuditoriaDeAcesso(auditoriaAcesso);
        }
    }
}