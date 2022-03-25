using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpIp
{
    public class TcpIpCliente
    {
        public string resultado { get; set; }

        private TcpClient tcpCliente { get; set; }

        private bool executarCliente { get; set; }

        public event EventHandler Cliente_Change;

        private int controleErros;

        private List<Dispositivo> _catracas { get; set; }

        private void AtualizaStatus(int x)
        {
            AoOcorrerEvento(EventArgs.Empty);
        }

        protected virtual void AoOcorrerEvento(EventArgs e)
        {
            EventHandler handler = Cliente_Change;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Iniciar(string ip, string porta, List<Dispositivo> catracas)
        {
            executarCliente = true;
            _catracas = catracas;
            Thread tcpThread = new Thread(() => conectar(ip, porta));
            tcpThread.IsBackground = true;
            tcpThread.Start();
        }

        private void Pausa(int segundos)
        {
            var tempoInicial = DateTime.Now;
            while ((DateTime.Now - tempoInicial).Seconds <= segundos)
            {
                Thread.Sleep(3);
            }
        }

        public bool EstaOnline(string ip)
        {
            var retorno = true;
            try
            {
                var resultadoPing = new Ping().Send(ip, 60 * 1000);
            }
            catch
            {
                retorno = false;
            }
            return retorno;
        }

        public void EncerrarCliente()
        {
            executarCliente = false;
        }

        public void EnviarMensagem(string mensagem)
        {
            try
            {
                if (tcpCliente != null && tcpCliente.Client != null && tcpCliente.Connected)
                {
                    StreamWriter writer = new StreamWriter(tcpCliente.GetStream());
                    writer.WriteLine(mensagem);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(nameof(TcpClient),ex);
                AtualizaStatus(0);
            }
        }

        private void conectar(string ip, string porta)
        {
            while (executarCliente)
            {
                try
                {
                    if (tcpCliente == null)
                    {
                        tcpCliente = new TcpClient(ip, int.Parse(porta));
                        tcpCliente.Client.ReceiveTimeout = 3000;
                        tcpCliente.Client.SendTimeout = 4000;
                        controleErros = 0;
                    }

                    while (true)
                    {
                        StringBuilder texto = new StringBuilder();
                        StreamReader reader = new StreamReader(tcpCliente.GetStream());
                        try
                        {
                            texto.Append(reader.ReadLine());
                        }
                        catch (IOException ioex)
                        {
                            if (ioex.InnerException is SocketException socketException && socketException.ErrorCode == 10060)
                            {
                                var ping = new Ping().Send(ip, 1000);
                                if (ping.Status == IPStatus.Success)
                                {
                                    continue;
                                }
                            }
                            throw ioex;
                        }
                        resultado = texto.ToString();
                        AtualizaStatus(0);
                    }
                }
                catch (Exception ex)
                {
                    if (tcpCliente != null)
                    {
                        tcpCliente.Close();
                        tcpCliente.Dispose();
                        tcpCliente = null;
                    }
                    controleErros++;
                    if (controleErros >= 3)
                    {
                        foreach (var catraca in _catracas)
                        {
                            resultado = JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(catraca, "Conectando"));
                            AtualizaStatus(0);
                        }

                        AuditoriaLog.EscrevaErro(nameof(TcpClient), ex);
                        
                        tcpCliente = null;
                    }
                    Pausa(3);
                }
            }

            if (!(tcpCliente == null)) tcpCliente.Close();
            if (!(tcpCliente == null)) tcpCliente.Dispose();
            EncerrarCliente();
        }
    }
}


