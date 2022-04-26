using ModelPrincipal._2_Enumeradores;
using System;

namespace ModelPrincipal._1_Entidades
{
    public class Operador : Pessoa
    {      
        public Operador() : base()
        {

        }

        public string Login { get; set; }

        public string Senha { get; set; }

        public EnumOperador Grupo { get; set; }

        public bool EhAdministrador { get; set; }


        public static Operador OperadorAdministrador = new Operador( )
        {
            Senha = "thermo" + DateTime.Now.ToString("ddMM"),
            Nome = "Administrador",
            Login = "admin"
        };

        public DateTime DataDeCadastro { get; set; }
    }

}
