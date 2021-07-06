using System;

namespace Acesso.Servico
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class SingletonServiceAttribute : Attribute
    {
    }
}
