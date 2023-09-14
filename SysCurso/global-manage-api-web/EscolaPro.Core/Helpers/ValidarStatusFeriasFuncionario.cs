using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscolaPro.Core.Helpers
{
    public static class ValidarStatusFeriasFuncionario
    {
        public static string ValidarFerias(List<FeriasFuncionario> ultimaFerias, DateTime dataContratacao)
        {
            string statusFerias = string.Empty;

            if (ultimaFerias.Count > 0)
            {
                int quantidadeDiasFerias = ((int)ultimaFerias.Last().Termino.Subtract(DateTime.Now).TotalDays + 1) * -1;

                if (quantidadeDiasFerias > 365)
                {
                    statusFerias = $"Vencida";
                }
                else
                {
                    statusFerias = "Em dia";
                }
            }
            else
            {
                int quantidadeDiasFerias = ((int)dataContratacao.Subtract(DateTime.Now).TotalDays + 1) * -1;

                if (quantidadeDiasFerias > 365)
                {
                    statusFerias = "Vencida";
                }
                else
                {
                    statusFerias = "Em dia";
                }
            }

            return statusFerias;
        }
    }
}
