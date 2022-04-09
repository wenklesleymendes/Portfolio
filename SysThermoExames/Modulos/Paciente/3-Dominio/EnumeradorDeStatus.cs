using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente._3_Dominio
{
    public class EnumeradorDeStatus : EnumeradorBase
    {
        public EnumeradorDeStatus ImagesColetadas = new EnumeradorDeStatus(0, "Imagens Coletadas");

        public EnumeradorDeStatus RealizandoLaudo = new EnumeradorDeStatus(1, "Realizando laudo");

        public EnumeradorDeStatus LaudoPronto = new EnumeradorDeStatus(2, "Laudo Pronto");

        public EnumeradorDeStatus Entregue = new EnumeradorDeStatus(3, "Entregue");

        private EnumeradorDeStatus(int id, string name) : base(id, name)
        {
        }
    }
}
