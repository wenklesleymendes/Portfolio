using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples
{
    public class ItauSimplesCorpoCobranca
    {
        public int tipo_ambiente { get; set; }
        public int tipo_registro { get; set; }
        public int tipo_cobranca { get; set; }
        public string tipo_produto { get; set; }
        public string subproduto { get; set; }
        public ItauBeneficiario beneficiario { get; set; }
        public string titulo_aceite { get; set; }
        public ItauSimplesPagador pagador { get; set; }

        public string tipo_carteira_titulo { get; set; }
        public ItauSimplesMoeda moeda { get; set; }

        public string nosso_numero { get; set; }
        public string digito_verificador_nosso_numero { get; set; }
        public string data_vencimento { get; set; }
        public string valor_cobrado { get; set; }
        public string especie { get; set; }
        public string data_emissao { get; set; }
        public int tipo_pagamento { get; set; }
        public string indicador_pagamento_parcial { get; set; }

        public string seu_numero { get; set; }

        public ItauSimplesJuros juros { get; set; }

        public ItauSimplesMulta multa { get; set; }

        public List<ItauSimplesGrupoDesconto> grupo_desconto { get; set; }

        public ItauSimplesRecebimentoDivergente recebimento_divergente { get; set; }

        //Instrução 1
        public string instrucao_cobranca_1 { get; set; }
        public string quantidade_dias_1 { get; set; }
        public string data_instrucao_1 { get; set; }

    }
}
