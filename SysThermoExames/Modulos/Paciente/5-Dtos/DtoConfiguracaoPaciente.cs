using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MdPaciente._5_Dtos
{
    public class DtoConfiguracaoPaciente
    {
        public Panel PnUm { get; set; }

        public Panel PnDois { get; set; }

        public int CodigoPaciente { get; set; }

        public bool ExisteCardSelecionadoPaciente { get; set; }
    }
}
