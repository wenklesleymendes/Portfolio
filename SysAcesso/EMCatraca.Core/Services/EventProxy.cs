using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

namespace EMCatraca.Core.Services
{
    public class EventProxy : MarshalByRefObject
    {
        public event EventoCatracaEvent AoDispararEvento;

        public override object InitializeLifetimeService()
        {
            //Returning null holds the object alive until it is explicitly destroyed
            return null;
        }

        public void LocallyHandleMessageArrived(EventoCatraca evento)
        {
            LogAuditoria.Escreva(nameof(LocallyHandleMessageArrived),$"Evento enviado para o Client - Dispositivo ({evento.Dispositivo})");
            AoDispararEvento?.Invoke(evento);
        }

        public void InicieCanal()
        {
            LogAuditoria.Escreva(nameof(EventProxy),"Iniciando canal de comunicação TCP Monitoring EventProxy.InicieCanal()");
            var clientProv = new BinaryClientFormatterSinkProvider();
            var serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = TypeFilterLevel.Full;

            var props = new Hashtable
            {
                ["name"] = "remotingClient",
                ["port"] = 0  //First available port
            };

            var tcpCanal = new TcpChannel(props, clientProv, serverProv);

            ChannelServices.RegisterChannel(tcpCanal, false);

            LogAuditoria.Escreva(nameof(EventProxy), "Registrando o tipo de cliente TCP Monitoring EventProxy.InicieCanal()");
            RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IServicoMonitorAcesso), "ServicoMonitorAcesso"));
            LogAuditoria.Escreva(nameof(EventProxy), "Concluiu o canal de comunicação");
        }
    }
}
