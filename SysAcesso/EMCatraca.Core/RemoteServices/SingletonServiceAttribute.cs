using System;

namespace EMCatraca.Core.RemoteServices
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class SingletonServiceAttribute : Attribute
    {
    }
}
