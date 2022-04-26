using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.RegistroCobrancaVO
{
    public class DtoCorpoCobranca
    {
        public int tipo_ambiente { get; set; }
        public int tipo_registro { get; set; }
        public int tipo_cobranca { get; set; }
        public string tipo_produto { get; set; }
        public string subproduto { get; set; }
        public DtoBeneficiario beneficiario { get; set; }
        public DtoDebito debito { get; set; }
        public string identificador_titulo_empresa { get; set; }
        public string uso_banco { get; set; }
        public string titulo_aceite { get; set; }
        public DtoPagador pagador { get; set; }
        public DtoSacadorAvalista sacador_avalista { get; set; }
        public string tipo_carteira_titulo { get; set; }
        public DtoMoeda moeda { get; set; }
        public string nosso_numero { get; set; }
        public string digito_verificador_nosso_numero { get; set; }
        public string codigo_barras { get; set; }
        public string numero_linha_digitavel { get; set; }
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
        public DtoJuros juros { get; set; }
        public DtoMulta multa { get; set; }
        public List<DtoGrupoDesconto> grupo_desconto { get; set; }
        public DtoRecebimentoDivergente recebimento_divergente { get; set; }
        public List<DtoGrupoRateio> grupo_rateio { get; set; }
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
        public List<Campos> Campos { get; set; }
    }

    public class Campos
    {
        public string Campo { get; set; }
        public string Mensagem { get; set; }
    }
}
