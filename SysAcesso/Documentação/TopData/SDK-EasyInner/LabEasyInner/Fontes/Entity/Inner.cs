using System;
using System.Collections.Generic;
using System.Text;

namespace EasyInnerSDK.Entity
{
    public class Inner
    {
        #region Propriedades

        #region Enumeradores
        public Enumeradores.modoComunicacao ModoComunicacao { get; set; }

        public Enumeradores.EstadosInner EstadoAtual { get; set; }
        public Enumeradores.EstadosTeclado EstadoTeclado { get; set; }
        public Enumeradores.EstadosInner EstadoSolicitacaoPingOnLine { get; set; }
        #endregion

        private Bilhete bilheteOnline = new Bilhete();
        public Bilhete BilheteOnline
        {
            get { return bilheteOnline; }
            set { bilheteOnline = value; }
        }

        public bool Catraca { get; set; }
        public bool Biometrico { get; set; }
        public bool Urna { get; set; }
        public bool Box { get; set; }
        public bool Teclado { get; set; }
        public bool DoisLeitores { get; set; }
        public bool Lista { get; set; }
        public bool ListaBioSemDigital { get; set; }
        public bool InnerNetAcesso { get; set; }
        public bool CatInvertida { get; set; }
        public bool CartMaster { get; set; }
        public bool DuasDigitais { get; set; }
        public bool Bio16Digitos { get; set; }
        public bool DedoDuplicado { get; set; }

        public int QtdDigitos { get; set; }
        public int CntDoEvents { get; set; }
        public int Numero { get; set; }
        public int TipoLeitor { get; set; }
        public int AcionarReles { get; set; }
        public int PadraoCartao { get; set; }
        public int CountReconexao { get; set; }
        public int CountPingFail { get; set; }
        public int CountRepeatPingOnline { get; set; }
        public int TentativasColeta { get; set; }
        public int BilhetesAReceber { get; set; }
        public int Tentativas { get; set; }
        public int Acionamento { get; set; }
        public int TipoConexao { get; set; }
        public int Porta { get; set; }
        public int TentativasUrna { get; set; }
        public int TimeOutAjustes { get; set; }
        public int NivelLFD { get; set; }
        public int TipoComBio { get; set; }
        public int VersaoFW { get; set; }
        
        public byte ValorLeitor1 { get; set; }
        public byte ValorLeitor2 { get; set; }
        public byte Verificacao { get; set; }
        public byte Identificacao { get; set; }

        public string FInvertido { get; set; }
        public string CaminhoDados { get; set; }
        public string LinhaInner { get; set; }
        public string VersaoInner { get; set; }
        public string VersaoBio { get; set; }
        public string ModeloBioInner { get; set; }
        public string Master { get; set; }

        public DateTime Temporizador { get; set; }
        public DateTime TempoInicialMensagem { get; set; }
        public DateTime TempoInicialPingOnLine { get; set; }
        public DateTime TempoColeta { get; set; }

        public short VariacaoInner { get; set; }

        #endregion

        public override string ToString()
        {
            //return base.ToString();
            return "Número Inner: " + this.Numero.ToString() +
                   " | Número de Dígitos: " + this.QtdDigitos.ToString() +
                   " | Catraca: " + this.Catraca.ToString() +
                   " | Modulo Bio: " + (this.TipoComBio == 0 ? "LN" : "LC") +
                   " | Verificação: " + (this.Verificacao == 0 ? "False" : "True") +
                   " | Identificação: " + (this.Identificacao == 0 ? "False" : "True") +
                   " | Cartão: " + (this.PadraoCartao == 0 ? "Barras" : (this.PadraoCartao == 1 ? "Magnético" : 
                                    (this.PadraoCartao == 2 ? "Abatrack" : this.PadraoCartao == 3 ? "Wiegand" : "Smart Card"))) +
                   " | " + (this.DoisLeitores ? "Dois Leitores" : "Um Leitor") +
                   " | Lista: " + this.Lista.ToString() +
                   (this.LinhaInner != null ? " | Linha: " + this.LinhaInner : "") +
                   (this.VariacaoInner != 0 ? " | Variação: " + this.VariacaoInner : "") +
                   (this.VersaoInner != null ? " | Versão : " + this.VersaoInner : "") +
                   (this.Biometrico ? (this.ModeloBioInner != null ? " | " + this.ModeloBioInner : "") : "") +
                   (this.Biometrico ? (this.VersaoBio != null ? " | Versão Bio: " + this.VersaoBio : "") : "");

        }
    }
}
