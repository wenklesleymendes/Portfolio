using Acesso.Dominio;
using Acesso.Interfaces;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioAuditoriaAPI : IRepositorioAuditoria
    {
        public void RegistreAuditoria(Auditoria auditoria)
        {
            APIHelper.Instancia.Post("Auditoria", "RegistreAuditoria", auditoria);
        }

        public void RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso)
        {
            APIHelper.Instancia.Post("Auditoria", "RegistreAuditoriaDeAcesso", auditoriaAcesso);
        }
    }
}
