using System;
using System.Collections.Generic;
using System.Text;
using TEF.ImpressaoCupom;

namespace TEF.Core.Library
{
    public class Impressao
    {
        public static void Imprimir(string texto, string porta)
        {
            CupomImpressao.Imprimir(texto, porta);
        }
    }
}
