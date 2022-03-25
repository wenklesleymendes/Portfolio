using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauCorpoCobranca
    {
        public int tipo_ambiente { get; set; }
        public int tipo_registro { get; set; }
        public int tipo_cobranca { get; set; }
        public string tipo_produto { get; set; }
        public string subproduto { get; set; }

        [JsonPropertyName("beneficiario")]
        public ItauBeneficiario beneficiario { get; set; }

        [JsonPropertyName("debito")]
        public ItauDebito debito { get; set; }

        public string identificador_titulo_empresa { get; set; }
        public string uso_banco { get; set; }
        public string titulo_aceite { get; set; }

        [JsonPropertyName("pagador")]
        public ItauPagador pagador { get; set; }


        [JsonPropertyName("sacador_avalista")]
        public ItauSacadorAvalista sacador_avalista { get; set; }

        public string tipo_carteira_titulo { get; set; }

        [JsonPropertyName("moeda")]
        public ItauMoeda moeda { get; set; }

        public string nosso_numero { get; set; }
        public string digito_verificador_nosso_numero { get; set; }
        public string codigo_barras { get; set; }
        public string data_vencimento { get; set; }
        public string valor_cobrado { get; set; }
        public string seu_numero { get; set; }
        public string especie { get; set; }
        public string data_emissao { get; set; }
        public string data_limite_pagamento { get; set; }
        public int tipo_pagamento { get; set; }
        public string indicador_pagamento_parcial { get; set; }
        public string quantidade_pagamento_parcial { get; set; }
        public string quantidade_parcelas { get; set; }
        public string instrucao_cobranca_1 { get; set; }
        public string quantidade_dias_1 { get; set; }
        public string data_instrucao_1 { get; set; }
        public string instrucao_cobranca_2 { get; set; }
        public string quantidade_dias_2 { get; set; }
        public string data_instrucao_2 { get; set; }
        public string instrucao_cobranca_3 { get; set; }
        public string quantidade_dias_3 { get; set; }
        public string data_instrucao_3 { get; set; }
        public string valor_abatimento { get; set; }

        [JsonPropertyName("juros")]
        public ItauJuros juros { get; set; }

        [JsonPropertyName("multa")]
        public ItauMulta multa { get; set; }

        [JsonPropertyName("grupo_desconto")]
        public List<ItauGrupoDesconto> grupo_desconto { get; set; }

        [JsonPropertyName("recebimento_divergente")]
        public ItauRecebimentoDivergente recebimento_divergente { get; set; }

        [JsonPropertyName("grupo_rateio")]
        public List<ItauGrupoRateio> grupo_rateio { get; set; }

    }
}
