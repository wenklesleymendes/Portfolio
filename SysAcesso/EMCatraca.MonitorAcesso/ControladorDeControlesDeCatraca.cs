using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.MonitorAcesso.ControlesDeUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.MonitorAcesso
{
    public class ControladorDeControlesDeCatraca
    {

        public int NumeroLinhas { get; internal set; }
        public int NumeroColunasPorLinha { get; internal set; }
        public int NumeroColunasLinha2 { get; internal set; }
        public TableLayoutPanel Panel { get; internal set; }
        public ucCatraca[] Catracas { get; internal set; }
        private const int NumeroLinhasGrade = 2;
        private const int NumeroColunasGrade = 5;

        public void MonteCatracas(IEnumerable<Dispositivo> catracas)
        {
            var numeroCatracas = catracas.Count();

            if (numeroCatracas == 0)
            {
                throw new ArgumentException("Não há catracas configuradas!");
            }
            if (numeroCatracas > 10)
            {
                throw new ArgumentException("Quantidade de catracas acima do limite máximo de 10 catracas por monitor!");
            }

            NumeroLinhas = (((float)numeroCatracas / NumeroLinhasGrade) <= 2) ? 1 : 2;
            NumeroColunasPorLinha = numeroCatracas;
            if (NumeroLinhas == 2)
            {
                NumeroColunasPorLinha = numeroCatracas - (numeroCatracas / 2);
                NumeroColunasLinha2 = numeroCatracas / 2;
            }

            if (NumeroLinhas == 1)
            {
                Panel.RowStyles[1].SizeType = SizeType.Absolute;
                Panel.RowStyles[1].Height = 0;
            }

            var j = 0;
            //Primeira linha, relaciona UserControl com ModelView ou invisibiliza UserControl
            for (int i = 0; i < NumeroColunasGrade; i++)
            {
                if (i >= NumeroColunasPorLinha)
                {
                    Panel.ColumnStyles[i].SizeType = SizeType.Absolute;
                    Panel.ColumnStyles[i].Width = 0;
                    Catracas[i].Visible = false;
                }
                else
                {
                    var catraca = catracas.ElementAt(j);
                    Catracas[j].AjusteModelView(new CatracaModelView(catraca));
                    Catracas[j].Visible = true;
                    j++;
                }
            }

            //Segunda linha, relaciona UserControl com ModelView ou invisibiliza UserControl
            for (int i = NumeroColunasGrade; i < Catracas.Length; i++)
            {
                if (i >= (NumeroColunasGrade + NumeroColunasLinha2))
                {
                    Catracas[i].Visible = false;
                }
                else
                {
                    var catraca = catracas.ElementAt(j);
                    Catracas[i].AjusteModelView(new CatracaModelView(catraca));
                    Catracas[j].Visible = true;
                    j++;
                }
            }
        }

        public void DispareEvento(EventoCatraca evento)
        {
            LogAuditoria.Escreva("Atribuindo os dados aos objetos ", 
                nameof(ControladorDeControlesDeCatraca));

            foreach (var userControls in Catracas)
            {
                if (userControls.CatracaModelView != null && userControls.CatracaModelView.CodigoCatraca == evento.Dispositivo.Codigo)
                {
                    userControls.DispareEvento(evento);
                }
            }
            LogAuditoria.Escreva("Atribuição dos dados aos objetos concluído", 
                nameof(ControladorDeControlesDeCatraca));
        }
    }
}
