using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace Acesso.Dominio
{
    public class TcpIpServidor
    {
        private bool _executarServer { get; set; }
        private static TcpListener _tcpServidor = null;
        private IniciaComunicacaoTcpIp _comunicacaoCliente;

        public static Hashtable nomeIdentificador;
        public static Hashtable nomeIdentificadorDaConexao;
        public static Hashtable nomeClienteInstancia;

        public TcpIpServidor()
        {
            nomeIdentificador = new Hashtable(100);
            nomeIdentificadorDaConexao = new Hashtable(100);
        }

        public void EncerrarServidor()
        {
            _executarServer = false;
        }

        private void EncerraClientes()
        {
            RemoveClientesInativos();
            TcpClient[] tcpCliente = new TcpClient[nomeIdentificador.Count];
            try
            {
                tcpCliente[nomeIdentificador.Count].Client.ReceiveTimeout = 3000;
                tcpCliente[nomeIdentificador.Count].Client.SendTimeout = 4000;
                for (int i = 0; i < tcpCliente.Length; i++)
                {
                    tcpCliente[i].Close();
                }
            }
            catch (Exception ex)
            {
                AuditoriaLog.WriteError(ex);
                AuditoriaLog.WriteLog("Conexão com o Cliente do Monitor foi encerrada");
            }
        }

        private static void RemoveClientesInativos()
        {
            try
            {
                if (nomeIdentificador.Count < 0)
                {
                    return;
                }

                TcpClient[] tcpCliente = new TcpClient[nomeIdentificador.Count];
                nomeIdentificador.Values.CopyTo(tcpCliente, 0);

                for (int i = 0; i < tcpCliente.Length; i++)
                {
                    if (!tcpCliente[i].Client.Connected)
                    {
                        var chave = nomeIdentificadorDaConexao[tcpCliente[i]];
                        nomeIdentificador.Remove(chave);
                        nomeIdentificadorDaConexao.Remove(tcpCliente[i]);
                        var instanciaCliente = nomeClienteInstancia[tcpCliente[i]];
                    }
                }
                tcpCliente = new TcpClient[nomeIdentificador.Count];
                nomeIdentificador.Values.CopyTo(tcpCliente, 0);
            }
            catch (Exception ex)
            {
                AuditoriaLog.WriteError(ex);
                AuditoriaLog.WriteLog("Cliente do Monitor encerrou comunicação");
            }
        }

        public void IniciarServidorTcpIp(string ip, string porta, string tipoServidor, bool EhParaMudarCaminho = false)
        {
            AuditoriaLog.WriteLog($"Inicando {nameof(IniciarServidorTcpIp)}({ip},{porta},{tipoServidor},{EhParaMudarCaminho})");

            if (_tcpServidor != null)
            {
                return;
            }

            _executarServer = true;

            var tcpThread = new Thread(() => ExecutarMonitoramento(ip, Int32.Parse(porta)))
            {
                IsBackground = true
            };
            tcpThread.Start();
        }

        private void ExecutarMonitoramento(string ip, int porta)
        {
            _tcpServidor = new TcpListener(IPAddress.Parse(ip), porta);
            while (_executarServer)
            {
                try
                {
                    var resultadoPing = new Ping().Send(ip, 60 * 1000);
                    _tcpServidor.Start();
                    if (_tcpServidor.Pending())
                    {
                        RemoveClientesInativos();
                        TcpClient cliente = _tcpServidor.AcceptTcpClient();
                        _comunicacaoCliente = new IniciaComunicacaoTcpIp(cliente, ip, porta);
                    }
                }
                catch (Exception ex)
                {
                    RemoveClientesInativos();
                    Pausa(10);
                }
            }

            if (_tcpServidor != null)
            {
                _tcpServidor.Stop();
            }

            EncerraClientes();
        }

        private void Pausa(int segundos)
        {
            var tempoInicial = DateTime.Now;
            while ((DateTime.Now - tempoInicial).Seconds <= segundos)
            {
                Thread.Sleep(3);
            }
        }

        private static int QuantidadeConexoesAtivas()
        {
            RemoveClientesInativos();
            return nomeIdentificador.Count;
        }

        public void EnviarMensagem(string msg)
        {
            EnviaMensagemParaTodosClientes(msg);
        }

        private static void EnviaMensagemParaTodosClientes(string msg)
        {
            if (_tcpServidor == null) return;

            StreamWriter writer;
            ArrayList ToRemove = new ArrayList(0);
            RemoveClientesInativos();
            TcpClient[] tcpCliente = new TcpClient[nomeIdentificador.Count];
            nomeIdentificador.Values.CopyTo(tcpCliente, 0);

            for (int i = 0; i < tcpCliente.Length; i++)
            {
                if (tcpCliente[i].Client.Connected)
                {
                    try
                    {
                        if (msg.Trim() == "" || tcpCliente[i] == null) continue;
                        writer = new StreamWriter(tcpCliente[i].GetStream());
                        writer.WriteLine(msg);
                        writer.Flush();
                        writer = null;
                    }
                    catch (Exception ex)
                    {
                        nomeIdentificador.Remove(nomeIdentificadorDaConexao[tcpCliente[i]]);
                        nomeIdentificadorDaConexao.Remove(tcpCliente[i]);
                        AuditoriaLog.WriteError(ex);
                        AuditoriaLog.WriteLog("Falha ao enviar dados ao Cliente do Monitor");
                    }
                }
            }
        }

        private class IniciaComunicacaoTcpIp
        {
            private bool executarCliente { get; set; }

            private StreamReader reader;
            private StreamWriter writer;

            public IniciaComunicacaoTcpIp(TcpClient tcpCliente, string ip, int porta)
            {
                AuditoriaLog.WriteLog("Iniciando comunicação com novo Cliente do Monitor");
                string instanciaConeccao = Guid.NewGuid().ToString();
                nomeIdentificador.Add(instanciaConeccao, tcpCliente);
                nomeIdentificadorDaConexao.Add(tcpCliente, instanciaConeccao);

                reader = new StreamReader(tcpCliente.GetStream());
                writer = new StreamWriter(tcpCliente.GetStream());

                executarCliente = true;

                Thread clientTrhead = new Thread(() => MonitoraMensagens(tcpCliente, ip, porta))
                {
                    IsBackground = true
                };

                try
                {
                    clientTrhead.Start();
                }
                catch (Exception e)
                {

                    throw;
                }

                clientTrhead.Start();
                nomeClienteInstancia.Add(tcpCliente, clientTrhead);
            }

            public void MonitoraMensagens(TcpClient tcpCliente, string ip, int porta)
            {
                NetworkStream stream = null;

                try
                {
                    while (executarCliente)
                    {
                        var resultadoPing = new Ping().Send(ip, 60 * 1000);
                        if (tcpCliente != null && tcpCliente.Connected)
                        {
                            using (stream = tcpCliente.GetStream())
                            {
                                resultadoPing = new Ping().Send(ip, 60 * 1000);
                                string texto = "";
                                while (executarCliente)
                                {
                                    if (TcpIpServidor.QuantidadeConexoesAtivas() <= 0) return;
                                    texto = reader.ReadLine();
                                    if (!String.IsNullOrEmpty(texto))
                                    {
                                        TcpIpServidor.EnviaMensagemParaTodosClientes(texto);
                                    }
                                }
                            }
                        }
                        else
                        {
                            executarCliente = false;
                        }
                    }
                }
                catch (IOException ioex)
                {
                    if (ioex.InnerException is SocketException socketException && socketException.ErrorCode == 10060)
                    {
                        var ping = new Ping().Send(ip, 1000);
                        if (ping.Status != IPStatus.Success)
                        {
                            throw ioex;
                        }
                    }
                }
                catch (SocketException)
                {
                    AuditoriaLog.WriteLog("Tentativa de conexão sem sucesso");
                }
                catch (Exception ex)
                {
                    executarCliente = false;
                    AuditoriaLog.WriteError(ex);
                    AuditoriaLog.WriteLog("Cliente do Monitor desconectou");
                }
                finally
                {
                    if (stream != null) stream.Close();
                    if (stream != null) stream.Dispose();
                    if (tcpCliente != null) tcpCliente.Close();
                    if (tcpCliente != null) tcpCliente.Dispose();
                }
            }

        }

        private static string ObtenhaLinhaLog()
        {
            var rastreamentoDePilhas = new System.Diagnostics.StackTrace(1, true);
            System.Diagnostics.StackFrame[] quadroPilha = rastreamentoDePilhas.GetFrames();

            int linhaCorrententeDoChamado = quadroPilha[0].GetFileLineNumber();
            var projetoMetodoCorrente = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();

            return $"Ln:{linhaCorrententeDoChamado}/{projetoMetodoCorrente}";
        }
    }
}



