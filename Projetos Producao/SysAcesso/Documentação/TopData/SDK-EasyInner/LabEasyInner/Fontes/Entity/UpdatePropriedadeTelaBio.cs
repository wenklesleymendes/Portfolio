using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyInnerSDK.Entity
{
    public class UpdatePropriedadeTelaBio
    {
        public string txtCartaoCaptura { get; set; }
        public string txtCartaoCadInner { get; set; }
        public int QualidadeImagem { get; set; }
        public int QualidadeDigital { get; set; }
        public bool EnviarDigitalInner { get; set; }
        public bool CheckImagem { get; set; }

        public PictureBox ImagemDigital { get; set; }
    }
}
