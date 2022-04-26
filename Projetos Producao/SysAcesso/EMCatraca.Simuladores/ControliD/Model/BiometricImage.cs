using System.Net;

namespace EMCatraca.Simuladores.ControliD.Model
{
    public class BiometricImageResult
    {
        public BiometricImage result;
    }
    public class BiometricImage
    {
        public int @event;
        public int user_id;
        public string user_name;
        public int portal_id;
        public Actions[] actions;
        public bool user_image;
    }
    public class Actions
    {
        public string action;
        public string parameters;
    }
    public class DeviceIsAlive
    {
        public HttpStatusCode @event;
    }
    public class DeviceIsAliveResult
    {
        public DeviceIsAlive result;
    }
}
