using Acesso.Dominio;

namespace Acesso.Interfaces
{
    public interface IRepositorioAuditoria
    {
        void RegistreAuditoria(Auditoria auditoria);

        void RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso);
    }
}
