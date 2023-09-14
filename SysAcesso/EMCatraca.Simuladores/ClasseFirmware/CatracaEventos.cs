using System;

namespace EMCatraca.Simuladores.ClasseFirmware
{
    public class CatracaEventos
    {
        public CatracaEventos()
        {

        }

        public void AtualizaStatus(int x)
        {
            EventoAtualizaStatus(EventArgs.Empty);
        }

        protected virtual void EventoAtualizaStatus(EventArgs e)
        {
            EventHandler handler = AoAlterarStatus;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler AoAlterarStatus;



        public void AtualizaDadosRecebidos(int x)
        {
            EventoDadosRecebidos(EventArgs.Empty);
        }

        protected virtual void EventoDadosRecebidos(EventArgs e)
        {
            EventHandler handler = AoReceberDados;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler AoReceberDados;
    }
}