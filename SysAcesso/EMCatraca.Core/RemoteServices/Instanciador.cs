using System;

namespace EMCatraca.Core.RemoteServices
{
    internal class Instanciador
    {
        private object _singleInstance;
        private Type _interfaceType;
        private Type _concretType;

        public Instanciador(Type interfaceType)
        {
            _interfaceType = interfaceType;
            _concretType = interfaceType.Assembly.GetType(ObtenhaNomeDoTipoConcreto(interfaceType));
        }

        private static string ObtenhaNomeDoTipoConcreto(Type interfaceType)
        {
            return $"{interfaceType.Namespace}.{interfaceType.Name.Substring(1)}";
        }

        public T Create<T>(params object[] args) where T : IService
        {
            if (_interfaceType.IsDefined(typeof(SingletonServiceAttribute), false))
            {
                return (T)(_singleInstance ?? (_singleInstance = CreateInstance<T>(args)));
            }
            return CreateInstance<T>(args);
        }

        private T CreateInstance<T>(object[] args) where T : IService
        {
            return (T)Activator.CreateInstance(_concretType, args);
        }
    }
}
