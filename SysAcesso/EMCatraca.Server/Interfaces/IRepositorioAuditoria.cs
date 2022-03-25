using EMCatraca.Core.Dominio;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioAuditoria
    {
        void RegistreAuditoria(Auditoria auditoria);

        void RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso);
    }
}
