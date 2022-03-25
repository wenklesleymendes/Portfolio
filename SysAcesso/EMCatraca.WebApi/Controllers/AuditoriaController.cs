using EMCatraca.Core.Dominio;
using EMCatraca.WebApi.Fachadas;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class AuditoriaController : ApiControllerAbstract
    {
        private readonly FachadaDeAuditoria _fachadaDeAuditoria = new FachadaDeAuditoria();

        [HttpPost]
        public async Task<HttpResponseMessage> RegistreAuditoria([FromBody] Auditoria auditoria)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAuditoria.RegistreAuditoria(auditoria));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> RegistreAuditoriaDeAcesso(AuditoriaAcesso auditoriaAcesso)
        {
            return await AcaoDeProcessoAsync(() => _fachadaDeAuditoria.RegistreAuditoriaDeAcesso(auditoriaAcesso));
        }
    }
}
