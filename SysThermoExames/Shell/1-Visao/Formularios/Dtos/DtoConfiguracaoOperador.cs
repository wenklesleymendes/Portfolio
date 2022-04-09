using System.Windows.Forms;

namespace Formularios.Dtos
{
    public class DtoConfiguracaoOperador
    {
        public Panel PnUm { get; set; }

        public Panel PnDois { get; set; }

        public int CodigoOperador { get; set; }

        public bool ExisteCardSelecionadoOperador { get; set; }
    }
}
