using Autofac;
using MdPaciente.Visao;
using MaterialSkin.Controls;
using Repositorio.IoC;
using Shell.Modulos;
using System;
using System.Windows.Forms;

namespace MdPaciente.Infraestrutura.IoC
{
    public class Modulo : IModulo
    {
        public Form frmPrimario { get; private set; }

        public Modulo()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<frmCard>().SingleInstance();
            var container = builder.Build();

            frmPrimario = container.Resolve<frmCard>();
        }

        public Form FrmMainPaciente()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<frmMainMdPaciente>().SingleInstance();
            var container = builder.Build();

            return container.Resolve<frmMainMdPaciente>();
        }

        public Form FrmListaExames()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<frmListaExames>().SingleInstance();
            var container = builder.Build();

            return container.Resolve<frmListaExames>();
        }

        public void Show()
        {
            try
            {
                frmPrimario.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível abrir o formulário pelo seguinte motivo: \n" + ex.Message);
            }

        }
    }

    public class RegistroDeFormulario : Module
    {
        public void RegistreServicos(ContainerBuilder builder)
        {
            builder.RegisterType<frmCard>().SingleInstance();
        }
    }
    public class IocMdPacientes : Module
    {
        public void RegistreServicos(ContainerBuilder builder)
        {
            builder.RegisterType<Modulo>().As<IModulo>().Named<IModulo>("MdPacientes").FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
