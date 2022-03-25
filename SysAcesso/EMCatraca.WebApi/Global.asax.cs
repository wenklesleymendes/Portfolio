using System.Globalization;
using System.Web;
using System.Web.Http;

namespace EMCatraca.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
