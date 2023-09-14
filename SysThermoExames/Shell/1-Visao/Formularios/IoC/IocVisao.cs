using Autofac;
using Formularios.Telas.Principal;
using Formularios.Telas.Login;
using Formularios.Telas.Controles;

namespace Formularios.IoC
{
    public class IocVisao : Module
    {
        public static void Registre(ContainerBuilder builder)
        {
            //builder.RegisterType<frmLogin>().SingleInstance();
            builder.RegisterType<frmMessageBox>().SingleInstance();
            builder.RegisterType<frmMain>().SingleInstance();
            builder.RegisterType<frmLoginOperador>().SingleInstance();
        }
    }
}
