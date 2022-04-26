using System;
using System.Collections.Concurrent;

namespace EMCatraca.Core.RemoteServices
{
    internal class LocalServiceFactory : IServiceFactory
    {
        public static LocalServiceFactory Instance = new LocalServiceFactory();

        private ConcurrentDictionary<Type, Instanciador> _instanciators = new ConcurrentDictionary<Type, Instanciador>();

        private LocalServiceFactory()
        {
        }

        public T Create<T>(params object[] args) where T : IService
        {
            if (!_instanciators.TryGetValue(typeof(T), out var instanciator))
            {
                instanciator = new Instanciador(typeof(T));
                _instanciators.TryAdd(typeof(T), instanciator);
            }
            return instanciator.Create<T>(args);
        }
    }
}
