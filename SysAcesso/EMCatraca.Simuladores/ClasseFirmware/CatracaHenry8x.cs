
using EMCatraca.Core.Dominio;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EMCatraca.ClasseFirmware.Simuladores
{
    public class CatracaStatus
    {
        private int codigo = 0;
        private string status = "";
        private string mensagem = "";
        private CatracaHenry8xEvents eventos = new CatracaHenry8xEvents();
        public CatracaHenry8xEvents Eventos
        {
            get { return eventos; }
            set { eventos = value; }
        }
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                eventos.AtualizaStatus(0);
            }
        }
        public string Mensagem
        {
            get { return mensagem; }
            set { mensagem = value; }
        }
    }

    public class CatracaDadosRecebidos
    {
        private string dadosString = "";
        private byte[] dadosBytes;
        public CatracaHenry8xEvents Eventos { get; set; } = new CatracaHenry8xEvents();

        public byte[] DadosBytes
        {
            get { return dadosBytes; }
            set
            {
                dadosBytes = value;
            }
        }

        public string DadosString
        {
            get { return dadosString; }
            set
            {
                dadosString = value;
                Eventos.AtualizaDadosRecebidos(0);
            }
        }
    }

    public class CatracaHenry8xEvents
    {
        public CatracaHenry8xEvents()
        {

        }

        public void AtualizaStatus(int x)
        {
            EventoAtualizaStatus(EventArgs.Empty);
        }

        protected virtual void EventoAtualizaStatus(EventArgs e)
        {
            EventHandler handler = CatracaHenry8xStatus_RecebeStatus;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CatracaHenry8xStatus_RecebeStatus;



        public void AtualizaDadosRecebidos(int x)
        {
            EventoDadosRecebidos(EventArgs.Empty);
        }

        protected virtual void EventoDadosRecebidos(EventArgs e)
        {
            EventHandler handler = CatracaHenry8x_RecebeDados;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CatracaHenry8x_RecebeDados;
    }

    public class CatracaConfig
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Ip { get; set; }
        public string Porta { get; set; }
        public bool Log { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CatracaConfig catracaConfig &&
                   Codigo == catracaConfig.Codigo;
        }

        public override int GetHashCode()
        {
            return 1745598366 + Codigo.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Codigo}, {Descricao}, {Ip}, {Porta}, {Log}";
        }
    }

    public class CatracaHenry8x
    {
        public CatracaHenry8xEvents eventos = new CatracaHenry8xEvents();
        public CatracaConfig configuracao = new CatracaConfig();
        public CatracaStatus status = new CatracaStatus();
        public CatracaDadosRecebidos dadosRecebidos = new CatracaDadosRecebidos();

        private TcpListener tcpServidor;
        private Thread threadMonitoramento;
        private TcpClient tcpCliente;
        private NetworkStream stream;
        public bool executarServer { get; set; }
        public bool executarClient { get; set; }
        private string tipoConexao;

        public void IniciarEmuladorCatraca(Dispositivo _catraca)
        {
            try
            {
                status.Eventos = eventos;
                dadosRecebidos.Eventos = eventos;

                configuracao.Codigo = _catraca.Codigo;
                configuracao.Descricao = _catraca.Descricao;
                configuracao.Ip = _catraca.IpCatraca;
                configuracao.Porta = _catraca.PortaCatraca.ToString();
                executarServer = true;
                tipoConexao = "";

                threadMonitoramento = new Thread(() => EmulaCatraca())
                {
                    IsBackground = true
                };

                threadMonitoramento.Start();
            }
            catch
            {
                throw;
            }
        }

        private void EmulaCatraca()
        {
            Byte[] bytes = new Byte[1024];
            while (executarServer)
            {
                var ping = new Ping();
                tipoConexao = "SERVIDOR";
                try
                {
                    if (tcpServidor == null)
                    {
                        tcpServidor = new TcpListener(IPAddress.Parse(configuracao.Ip), int.Parse(configuracao.Porta));
                        tcpServidor.Start();
                        status.Mensagem = "Dispositivo está ativa";
                        status.Status = "Ativo";
                    }

                    using (tcpCliente = tcpServidor.AcceptTcpClient())
                    {
                        if (!(tcpCliente == null) && tcpCliente.Connected)
                        {
                            using (stream = tcpCliente.GetStream())
                            {
                                try
                                {
                                    var resultadoPing = ping.Send(configuracao.Ip, 60 * 1000);
                                    int length;
                                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                                    {
                                        var DadosDeRetorno = new byte[length];
                                        Array.Copy(bytes, 0, DadosDeRetorno, 0, length);
                                        status.Mensagem = "Recebeu dados";
                                        status.Status = "Recebeu";
                                        RecuperaTextoMensagem("Recebeu", DadosDeRetorno);
                                    }
                                }
                                catch
                                {
                                    status.Mensagem = "Sem comunicação pela rede";
                                    status.Status = "Desconectado";
                                    executarServer = false;
                                }
                            }
                        }
                        else
                        {
                            status.Mensagem = "Cliente desconectou";
                            status.Status = "Desconectado";
                            executarServer = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tcpServidor = null;

                    status.Mensagem = "Falha do Socket " + ex.ToString();
                    status.Status = "Falha";
                }
            }
        }

        public void IniciaMonitorCatraca(CatracaConfig _configuracao)
        {
            status.Eventos = eventos;
            dadosRecebidos.Eventos = eventos;
            configuracao = _configuracao;
            executarClient = true;
            //threadMonitoramento = new Thread(() => MonitoraCatraca());
            //threadMonitoramento.IsBackground = true;
            //threadMonitoramento.Start();
            MonitoraCatraca();
        }

        private void MonitoraCatraca()
        {
            var ping = new Ping();
            try
            {
                tcpCliente = new TcpClient(configuracao.Ip, int.Parse(this.configuracao.Porta));
                status.Mensagem = "Conectado à catraca em: IP: " + this.configuracao.Ip + ", Porta: " + configuracao.Porta;
                status.Status = "Conectado";
            }
            catch { }

            Byte[] bytes = new Byte[1024];
            while (executarClient)
            {
                try
                {
                    var resultadoPing = ping.Send(configuracao.Ip, 60 * 1000);
                    if (tcpCliente != null && tcpCliente.Client != null && tcpCliente.Connected && resultadoPing.Status == IPStatus.Success)
                    {
                        using (NetworkStream stream = tcpCliente.GetStream())
                        {
                            int length;
                            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                var DadosDeRetorno = new byte[length];
                                Array.Copy(bytes, 0, DadosDeRetorno, 0, length);
                                status.Mensagem = "Recebeu dados";
                                status.Status = "Recebeu";
                                RecuperaTextoMensagem("Recebeu", DadosDeRetorno);
                            }
                        }
                    }
                    else
                    {
                        if (tcpCliente != null) tcpCliente.Close();
                        if (tcpCliente != null) tcpCliente.Dispose();
                        tcpCliente = new TcpClient(configuracao.Ip, int.Parse(configuracao.Porta));
                        status.Mensagem = "Conectado à catraca em: IP: " + configuracao.Ip + ", Porta: " + configuracao.Porta;
                        status.Status = "Conectado";
                    }
                }
                catch
                {
                    status.Mensagem = "Reconectando com a catraca";
                    status.Status = "Reconectando";
                }
            }

            tcpCliente = null;
        }

        public void EnviaMensagemEmBytes(string mensagem)
        {
            if (tcpCliente == null)
            {
                status.Mensagem = "A conexão com a catraca não foi estabelecida";
                status.Status = "Desconectado";
                return;
            }

            try
            {
                if (tcpCliente != null && tcpCliente.Client != null && tcpCliente.Connected)
                {
                    NetworkStream stream = tcpCliente.GetStream();
                    byte[] byteMensagem = MontaProtocolo(mensagem);
                    stream.Write(byteMensagem, 0, byteMensagem.Length);
                    status.Mensagem = "Enviou dados";
                    status.Status = "Enviou";
                }
                else
                {
                    status.Mensagem = "Dispositivo inativa";
                    status.Status = "Dispositivo inativa";
                }
            }
            catch (Exception ex)
            {
                status.Mensagem = "Falha de Comunicacao: " + ex.Message;
                status.Status = "Falha";
            }
        }

        public string[][] FiltraMensagem(string mensagem)
        {
            string[] msg = mensagem.Split(']');
            string[][] resultado = new string[msg.Length][];

            for (int i = 0; i < msg.Length; i++)
            {
                resultado[i] = msg[i].Split('+');
            }
            return resultado;
        }

        public string RecuperaTextoMensagem(string tipo, byte[] mensagemEmBytes)
        {
            var utf8 = Encoding.UTF8;
            string clientMessage = Encoding.ASCII.GetString(mensagemEmBytes);
            byte[] utfBytes = utf8.GetBytes(Encoding.ASCII.GetString(mensagemEmBytes));

            byte[] resultadoBytesTratados = new byte[utfBytes.Length - 5];
            if (utfBytes.Length > 10)
                Array.Copy(utfBytes, 3, resultadoBytesTratados, 0, utfBytes.Length - 5);

            dadosRecebidos.DadosBytes = mensagemEmBytes;
            if (tipo.Equals("Recebeu")) dadosRecebidos.DadosString = status.Mensagem = System.Text.Encoding.UTF8.GetString(resultadoBytesTratados);

            string textoUtf8 = Encoding.UTF8.GetString(resultadoBytesTratados);

            if (configuracao.Log)
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.AppendLine("----------------------------------------------------------------------");
                sb.AppendLine(tipo + $": {tipoConexao} IP:{configuracao.Ip} Porta:{configuracao.Porta} { DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}");
                sb.AppendLine("Bytes:");
                sb.AppendLine(BitConverter.ToString(utfBytes).Replace("-", " "));
                sb.AppendLine("String:");
                sb.AppendLine(textoUtf8);

                Log(sb.ToString());
            }


            return textoUtf8;
        }



        public byte[] MontaProtocolo(string mensagem)
        {
            status.Mensagem = "Montando o protocolo";
            status.Status = "Protocolo";

            byte[] mensagemBytes = Encoding.ASCII.GetBytes(mensagem);
            short intValue = (Int16)mensagemBytes.Length;
            byte[] intBytes = BitConverter.GetBytes(intValue);
            Array.Reverse(intBytes);

            byte checkSum = CalculaCheckSum(mensagemBytes);

            mensagemBytes = AdicionaByteInicioArray(mensagemBytes, 0, intBytes[0]);
            mensagemBytes = AdicionaByteInicioArray(mensagemBytes, 0, intBytes[1]);
            mensagemBytes = AdicionaByteInicioArray(mensagemBytes, 0, 2);

            AdicionaByteFinalArray(ref mensagemBytes, checkSum);
            AdicionaByteFinalArray(ref mensagemBytes, 0x03);

            RecuperaTextoMensagem("Envio", mensagemBytes);
            return mensagemBytes;
        }

        private static byte CalculaCheckSum(byte[] bytes)
        {
            Byte chkSumByte = 0x00;
            for (int i = 0; i < bytes.Length; i++)
                chkSumByte ^= bytes[i];

            chkSumByte ^= (Byte)(bytes.Length % 256);
            chkSumByte ^= (Byte)(bytes.Length / 256);

            string h16 = int.Parse((Math.Floor((Decimal)chkSumByte / 16)).ToString()).ToString("X");
            string h1 = int.Parse((chkSumByte % 16).ToString()).ToString("X");

            return Convert.ToByte("0x" + h16 + h1, 16);
        }

        private void AdicionaByteFinalArray(ref byte[] dst, byte src)
        {
            Array.Resize(ref dst, dst.Length + 1);
            dst[dst.Length - 1] = src;
        }

        private byte[] AdicionaByteInicioArray(byte[] bArray, int pos, byte novoByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = novoByte;
            return newArray;
        }

        private void Log(string logMensagem)
        {
            //if (!Configuracao.Log) return;

            //StreamWriter w = File.AppendText("AuditoriaLog.txt");
            //w.Write(logMensagem);
            //w.Close();
        }
    }
}
