using System.Collections.Generic;

namespace EMCatraca.Simuladores.Entity
{
    public class Usuarios
    {
        public int CodigoUsuario { get; set; }
        public string Usuario { get; set; }
        public int Faixa { get; set; }

        private List<Usuarios> listaUsuario = new List<Usuarios>();
        public List<Usuarios> ListaUsuario
        {
            get { return listaUsuario; }
            set { listaUsuario = value; }
        }
    }
}
