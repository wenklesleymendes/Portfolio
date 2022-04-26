using EMCatraca.TopData;
using System;

namespace EMCatraca.Simuladores.Entity
{
    public class Inner
    {
        public EnumeradoresDllInner.EstadosInner EstadoAtual { get; set; }
        public EnumeradoresDllInner.EstadosTeclado EstadoTeclado { get; set; }
        public EnumeradoresDllInner.EstadosInner EstadoSolicitacaoPingOnLine { get; set; }
        public Bilhete BilheteOnline { get; set; } = new Bilhete();

        public bool Biometrico { get; set; }
        public bool HabilitaTeclado { get; set; }
        public bool DoisLeitores { get; set; }
        public bool HabilitaListaOffLine { get; set; }
        public bool HabilitaListaBioSemDigital { get; set; }
        public bool InnerNetAcesso { get; set; }
        public bool CatracaInvertida { get; set; }
        public bool HabilitaCartaoMaster { get; set; }

        public int QtdDigitos { get; set; }
        public int CntDoEvents { get; set; }
        public int Numero { get; set; }
        public int TipoLeitor { get; set; }
        public int PadraoCartao { get; set; }
        public int CountPingFail { get; set; }
        public int CountRepeatPingOnline { get; set; }
        public int TentativasColeta { get; set; }
        public int BilhetesAReceber { get; set; }
        public int Tentativas { get; set; }
        public int TipoEquipamento { get; set; }
        public int TipoConexao { get; set; }
        public int Porta { get; set; }
        public int TentativasUrna { get; set; }
        public int VersaoFW { get; set; }

        public byte HabilitaVerificacao { get; set; }
        public byte HabilitaIdentificacao { get; set; }

        public string LinhaInner { get; set; }
        public string VersaoInner { get; set; }
        public string VersaoBio { get; set; }
        public string ModeloBioInner { get; set; }
        public string CartaoMaster { get; set; }

        public DateTime Temporizador { get; set; }
        public DateTime TempoInicialMensagem { get; set; }
        public DateTime TempoInicialPingOnLine { get; set; }
        public DateTime TempoColeta { get; set; }

        public short VariacaoInner { get; set; }
        public byte TipoComBio { get; internal set; }

        public override string ToString()
        {
            var ehComBiometria = Biometrico ? "Com bio" : "Sem bio";
            var ehParaVerificarBiometria = HabilitaVerificacao == 0 ? "Não" : "Sim";
            var ehParaIndetificarComBiometria = HabilitaIdentificacao == 0 ? "Não" : "Sim";
            var tipoLeitor = ObtenhaOhTipoLeitor(this);
            var ehDoisLeitores = DoisLeitores ? "Dois Leitores" : "Um Leitor";

            var ehListaOff = HabilitaListaOffLine ? "Sim" : "Não";

            var texto = $"Inner: {Numero} " +
                        $"| Dígitos: {QtdDigitos} " +
                        $"| Dispositivo " +
                        $"| {ehComBiometria} " +
                        $"| Verificação: {ehParaVerificarBiometria} " +
                        $"| Identificação: {ehParaIndetificarComBiometria} " +
                        $"| Leitor: {tipoLeitor} | {ehDoisLeitores} " +
                        $"| Lista Off: {ehListaOff} ";

            return texto.Trim();
        }

        private string ObtenhaOhTipoLeitor(Inner inner)
        {
            var resposta = string.Empty;
            switch (inner.TipoLeitor)
            {
                case 0:
                    resposta = "Barras";
                    break;

                case 1:
                    resposta = "Magnético";
                    break;

                case 2:
                    resposta = "Abatrack";
                    break;

                case 3:
                    resposta = "Wiegand";
                    break;

                default:
                    break;
            }

            return resposta;
        }
    }
}
