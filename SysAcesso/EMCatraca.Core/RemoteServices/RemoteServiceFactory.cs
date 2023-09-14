using EMCatraca.Core.Logs;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;

namespace EMCatraca.Core.RemoteServices
{
    internal class RemoteServiceFactory : MarshalByRefObject, IServiceFactory
    {
        public T Create<T>(params object[] args) where T : IService
        {
            var marshalByRefObject =
                LocalServiceFactory.Instance.Create<T>(args) as MarshalByRefObject;

            if (marshalByRefObject == null)
            {
                throw new ApplicationException("Serviço não encontrado ou então não herda de MarshalByRefObject: " + typeof(T).Name);
            }

            return (T)MarshalByRefWrapper.Wrap(marshalByRefObject);
        }

        internal class MarshalByRefWrapper : RealProxy
        {
            private MarshalByRefWrapper(MarshalByRefObject wrappedObject)
                : base(wrappedObject.GetType())
            {
                WrappedObject = wrappedObject;
            }

            internal static object Wrap(object objectToWrap)
            {
                if (RemotingServices.IsTransparentProxy(objectToWrap))
                {
                    var Proxy = RemotingServices.GetRealProxy(objectToWrap);
                    if (Proxy is MarshalByRefWrapper)
                        return ((MarshalByRefWrapper)Proxy).GetTransparentProxy();
                }

                if (objectToWrap is MarshalByRefWrapper)
                {
                    return ((MarshalByRefWrapper)objectToWrap).GetTransparentProxy();
                }

                return new MarshalByRefWrapper((MarshalByRefObject)objectToWrap).GetTransparentProxy();
            }

            protected MarshalByRefObject WrappedObject { get; }

            public override IMessage Invoke(IMessage msg)
            {
                if (WrappedObject == null)
                {
                    throw new ObjectDisposedException("MarshalByRefWrapper disposed.");
                }

                var methodCallMessage = (msg as IMethodCallMessage);
                if (methodCallMessage == null)
                {
                    return new ReturnMessage(null, null, 0, null, null);
                }

                var returnMessage = RealInvoke(methodCallMessage);

                if (returnMessage.ReturnValue is MarshalByRefObject)
                {
                    var ReturnWrapper = new MethodReturnMessageWrapper(returnMessage);
                    ReturnWrapper.ReturnValue = Wrap(returnMessage.ReturnValue);
                    returnMessage = ReturnWrapper;
                }

                //Este tipo de exceção possui atributos que não podem ser serializados, portanto, estamos trocando o tipo para retornar uma exceção serializável
                if (returnMessage.Exception is FaultException)
                {
                    PubliqueExcecao(returnMessage.Exception);
                    var fx = (FaultException)returnMessage.Exception;
                    throw new CommunicationException(fx.Message);
                }
                else if (returnMessage.Exception != null)
                {
                    PubliqueExcecao(returnMessage.Exception);
                }

                return returnMessage;
            }

            private static void PubliqueExcecao(Exception exception)
            {
                if (exception.InnerException != null)
                    AuditoriaLog.EscrevaErro(nameof(RemoteServiceFactory),exception);
                else
                    AuditoriaLog.EscrevaErro(nameof(RemoteServiceFactory), exception); 
            }

            protected virtual IMethodReturnMessage RealInvoke(IMethodCallMessage methodCallMessage)
            {
                return RemotingServices.ExecuteMessage(WrappedObject, methodCallMessage);
            }
        }
    }
}
