using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using System;
using System.Collections.Generic;

namespace EMCatraca.Core.Services
{
    internal class ServicoMonitorAcesso : MarshalByRefObject, IServicoMonitorAcesso
    {
        public event EventoCatracaEvent AoDispararEvento;

        public ServicoMonitorAcesso()
        {

        }

        public void AdicioneEvento(EventoCatraca evento)
        {
            SafeInvokeMessageArrived(evento);
        }

        public IEnumerable<Dispositivo> ObtenhaCatracas()
        {
            return MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("Emcatraca.Catracas.cfg");
        }

        private void SafeInvokeMessageArrived(EventoCatraca evento)
        {
            if (AoDispararEvento == null)
                return;

            LogAuditoria.Escreva(nameof(ServicoMonitorAcesso),
                $"Server enviando mensagem de evento para os Clients - Dispositivo ({evento.Dispositivo.Codigo})");

            EventoCatracaEvent listener = null;
            Delegate[] dels = AoDispararEvento.GetInvocationList();

            foreach (var del in dels)
            {
                try
                {
                    listener = (EventoCatracaEvent)del;
                    listener.Invoke(evento);
                }
                catch (Exception ex)
                {
                    //Could not reach the destination, so remove it
                    //from the list
                    Console.WriteLine(ex);
                    AoDispararEvento -= listener;
                }
            }

            LogAuditoria.Escreva(nameof(ServicoMonitorAcesso),
                $"Server o envio de evento para os Clientes - Dispositivo ({evento.Dispositivo.Codigo})");
        }

    }
}
