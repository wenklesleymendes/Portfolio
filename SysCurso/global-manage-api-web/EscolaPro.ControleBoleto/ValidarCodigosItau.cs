using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.ControleBoleto
{
    public static class ValidarCodigosItau
    {

        public static int CalcularDAC(string agencia, string conta, string digito, string carteira, string nossoNumero)
        {
            int dac = 0;
            
            string numeroCompleto = String.Concat(agencia, conta, digito, carteira, nossoNumero);


          //  0 9 4 0 0 1 4 3 6 9 6 1 0 9 0 0 0 0 0 1
          //- 1 2 1 2 1 2 1 2 1 2 1 2 1 2 1 2 1 2 1 2
          //  0 9 5 2 0 2 5 6 6 9 6 3 0 9 0 0 0 0 0 2
                 
             

            return dac;
        }
    }
}
