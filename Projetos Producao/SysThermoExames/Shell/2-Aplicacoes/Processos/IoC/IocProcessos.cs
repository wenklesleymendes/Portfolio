using Autofac;
using Processos.Operadores;


namespace Processos.IoC
{
    public class IocProcessos : Module
    {
        public void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ProcessoOperadores>();
        }
    }
}
