using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExemploOnline.Entity
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
