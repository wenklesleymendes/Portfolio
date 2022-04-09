using Autofac;
using Repositorio.Persistencia.Operadores;


namespace Repositorio.IoC
{
    public class IocRepositorio : Module
    {
        public void RegistreServicos(ContainerBuilder builder)
        {
            builder.RegisterType<RepositorioOperadores>().As<IRepositorioOperadores>();
        }
    }
}