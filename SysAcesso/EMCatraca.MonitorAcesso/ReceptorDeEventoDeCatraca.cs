using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using System;

namespace EMCatraca.MonitorAcesso
{
    [Serializable]
    internal class ReceptorDeEventoDeCatraca : MarshalByRefObject, IReceptorDeEventoDeCatraca
    {
        [NonSerialized]
        private ControladorDeControlesDeCatraca _controladorDeControlesDeCatraca;

        public ReceptorDeEventoDeCatraca(ControladorDeControlesDeCatraca controladorDeControlesDeCatraca)
        {
            _controladorDeControlesDeCatraca = controladorDeControlesDeCatraca;
        }

        public void RecebaEvento(EventoCatraca evento)
        {
            _controladorDeControlesDeCatraca.DispareEvento(evento);
        }
    }
}
