using System;
using System.Runtime.Remoting.Messaging;

namespace EMCatraca.Core.Dominio
{
    [Serializable]
    public class SessaoDoUsuario
    {
        public Operador OperadorLogado { get; set; }

        public static bool ApiSession { get; set; }

        private static SessaoDoUsuario _instancia;

        public static SessaoDoUsuario Instancia
        {
            get { return _instancia ?? (_instancia = ObtenhaInstanciaContextoChamada()); }
        }

        private static SessaoDoUsuario ObtenhaInstanciaContextoChamada()
        {
            var contexto = (SessaoDoUsuario)CallContext.GetData(Constantes.CHAVE_CONTEXTO_SESSAOUSUARIO);
            if (contexto == null)
            {
                contexto = new SessaoDoUsuario();
                CallContext.SetData(Constantes.CHAVE_CONTEXTO_SESSAOUSUARIO, contexto);
            }
            return contexto;
        }

    }

}
