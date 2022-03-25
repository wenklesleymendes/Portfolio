using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace TEF.ImpressaoCupom
{
    public class CupomImpressao
    {
        public static void Imprimir(string texto, string porta)
        {
            try
            {
                int modeloImpressora = 0;

                var retorno = mp2032.ConfiguraModeloImpressora(modeloImpressora);
                retorno = mp2032.IniciaPorta(porta);
                retorno = mp2032.ComandoTX(texto, texto.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
   
}
