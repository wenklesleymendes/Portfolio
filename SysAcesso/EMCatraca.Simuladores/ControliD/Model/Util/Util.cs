using EMCatraca.Simuladores.ControliD.Controller;
using System;

namespace EMCatraca.Simuladores.ControliD.Model.Util
{
    class Util
    {

        public string ipServer;
        public string ipTerminal;

        public String GetIpServer()
        {
            return ipServer = Device.ServerIp;

        }
        public String GetIpTerminal()
        {

            return ipTerminal = Device.IPAddress;
        }
    }
}
