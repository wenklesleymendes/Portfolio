//******************************************************************************
//A Topdata Sistemas de Automação Ltda não se responsabiliza por qualquer
//tipo de dano que este software possa causar, este exemplo deve ser utilizado
//apenas para demonstrar a comunicação com os equipamentos da linha Inner.
//
//Exemplo Off-Line
//Desenvolvido em C#.
//                                           Topdata Sistemas de Automação Ltda.
//******************************************************************************

//Referências Projeto
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyInnerSDK.Entity;
using EasyInnerSDK.UI;
using System.Threading;
using ExemploOnline.Entity;
using EasyInnerSDK.DAO;

namespace EasyInnerSDK.UI
{
    public class FrmOffLineController
    {

        private static Boolean InnerNetAcesso;
        private static byte InnerAcessoBio;

        private byte VersaoAlta = 0;
        
        
        #region Linha
        private static byte _linha;
        public static byte Linha
        {
            get { return _linha; }
            set { _linha = value; }
        }
        #endregion

        private static FrmOffLine UiOffline;

        public FrmOffLineController(FrmOffLine frmOffLine)
        {
            UiOffline = frmOffLine;
        }

        #region Metodos

        #region Enviar
        /// <summary>
        /// Envia as configurações, relogio, mensagem, horarios, lista de acesso, horario
        /// da sirene, lista dos inners.
        /// </summary>
        /// <param name="UiMainOffline"></param>
        public void Enviar()
        {
            //Campo obrigatório
            if (UiOffline.cboTipoLeitor.SelectedIndex == -1)
            {
                MessageBox.Show("Favor selecionar um tipo de leitor !", "Atenção");
                return;
            }

            //Se catraca deve informar o lado que está instalada
            if ((UiOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Coletor) || ((UiOffline.optDireita.Checked) ||
                (UiOffline.optEsquerda.Checked)))
            {
            }
            else
            {
                MessageBox.Show("Favor informar o lado de instalação da catraca !", "Atenção");
                return;
            }

            if (UiOffline.chkLista.Checked == true || UiOffline.chkListaBio.Checked == true || UiOffline.chkHorarios.Checked == true)
            {
                DAOUsuariosBio acessoBio = new DAOUsuariosBio();
                acessoBio.ConsultarUsuariosBio(0);
            }

            //Mensagem Status
            UiOffline.lblEnvia.Text = "Conectando com o inner...";
            UiOffline.btnEnviar.Enabled = false;
            UiOffline.btnReceber.Enabled = false;
            Application.DoEvents();
            UiOffline.lblVersao.Text = "";          
            Linha = 0;

            if (Conectar() == false)
            {
                UiOffline.lblEnvia.Text = "Erro ao conectar no inner!";
                EasyInner.FecharPortaComunicacao();
                UiOffline.btnEnviar.Enabled = true;
                UiOffline.btnReceber.Enabled = true;
                Application.DoEvents();
                return;
            }

            //Define versão
            DefineVersao();

            //Se selecionado Biometria, valida se o equipamento é compatível
            if (UiOffline.chkBio.Checked)
            {
                if ((Linha != 6 && Linha != 14) || (Linha == 14 && InnerAcessoBio == 0))
                {
                    MessageBox.Show("Equipamento não compatível com Biometria.", "Atenção");
                }
            }

            //Mensagem Status
            UiOffline.lblEnvia.Text = "Enviando configurações...";
            EnviarConfiguracoes();
            EnviarRelogio();
            EnviarMensagemOffLine();
            EnviarSirene();
            EnviarHorarios();
            EnviarListaOff();
            ConfigurarBio();
            EnviarListaSemDigital();
            
            UiOffline.btnEnviar.Enabled = true;
            UiOffline.btnReceber.Enabled = true;

            //Após procedimentos, fecha porta de comunicação
            EasyInner.FecharPortaComunicacao();
        }

        private void EnviarListaSemDigital()
        {
            int Ret = -1;
            //Envia lista biometrica
            if (UiOffline.chkListaBio.Checked)
            {
                //Mensagem Status
                UiOffline.lblEnvia.Text = "Enviando lista do InnerBio...";

                //Chama rotina que monta o buffer de cartoes que nao irao precisar da digital
                MontarBufferListaSemDigital();

                Application.DoEvents();
                if (InnerNetAcesso)
                {
                    Ret = EasyInner.EnviarListaUsuariosSemDigitalBioVariavel(int.Parse(UiOffline.txtNumInner.Text), int.Parse(UiOffline.txtDigitos.Text));
                }
                else
                {
                    //Envia o buffer com a lista de usuarios sem digital
                    Ret = EasyInner.EnviarListaUsuariosSemDigitalBio(int.Parse(UiOffline.txtNumInner.Text));
                }

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblEnvia.Text = "Lista do InnerBio enviada com sucesso!";
                }
                else
                {
                    UiOffline.lblEnvia.Text = "Erro ao enviar lista do InnerBio!";
                    UiOffline.btnEnviar.Enabled = true;
                    UiOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
        }
        private void ConfigurarBio()
        {
            int Ret = -1;
            //Equipamento Biométrico
            if ((Linha == 6 || (Linha == 14)) && (UiOffline.chkBio.Checked))
            {
                if (VersaoAlta < 6)
                {
                    Ret = HabilitarIdentVerifAVer5xx();
                    Ret = ReceberVersaoBioAVer5xx();
                    Ret = ReceberModeloBioAVer5xx();
                }
                else
                {
                    Ret = HabilitarIdentVerif6xx();
                    Ret = ReceberVersaoBio6xx();
                    Ret = ReceberModeloBio6xx();
                }
            
                if (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK)
                UiOffline.lblEnvia.Text = "Não foi possível realizar a configuração bio";
            }
        }

        private int HabilitarIdentVerif6xx()
        {
            int Ret = -1;
            Ret = EasyInner.RequisitarHabilitarIdentificacaoVerificacao(int.Parse(UiOffline.txtNumInner.Text), (UiOffline.chkModuloLC.Checked ? 1 : 0), (UiOffline.chkIdentificacao.Checked ? 1 : 0), (UiOffline.chkVerificacao.Checked ? 1 : 0));
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                Ret = EasyInner.RespostaHabilitarIdentificacaoVerificacao(int.Parse(UiOffline.txtNumInner.Text));
            }
            return Ret;
        }

        private int HabilitarIdentVerifAVer5xx()
        {
            int Ret = -1;
            //Habilita/Desabilita a identificação biométrica e/ou a verificação
            //biométrica do Inner bio.
            EasyInner.ConfigurarBio(int.Parse(UiOffline.txtNumInner.Text), (byte)(UiOffline.chkIdentificacao.Checked ? 1 : 0), (byte)(UiOffline.chkVerificacao.Checked ? 1 : 0));

            //Retorna o resultado da configuração do Inner Bio, função ConfigurarBio.
            //Se o retorno for igual a 0 é porque o Inner bio foi configurado com
            //sucesso.
            Ret = EasyInner.ResultadoConfiguracaoBio(int.Parse(UiOffline.txtNumInner.Text), 0);
            return Ret;
        }
        private int ReceberModeloBio6xx()
        {
            int Ret = -1;
            Ret = EasyInner.RequisitarModeloBio(int.Parse(UiOffline.txtNumInner.Text), (UiOffline.chkModuloLC.Checked ? 1 : 0));
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                byte[] ModeloBio = new byte[4];
                Ret = EasyInner.RespostaModeloBio(int.Parse(UiOffline.txtNumInner.Text), ModeloBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblVersao.Text += "Modelo bio " + Encoding.ASCII.GetString(ModeloBio);
                }
            }
            return Ret;
        }
        private int ReceberModeloBioAVer5xx()
        {
            int Ret = -1;
            int Modelo = 0;
            //Solicita o modelo do Inner bio.
            Ret = EasyInner.SolicitarModeloBio(int.Parse(UiOffline.txtNumInner.Text));

            do
            {
                Thread.Sleep(1);

                //Retorna o resultado do comando SolicitarModeloBio, o modelo
                //do Inner Bio é retornado por referência no parâmetro da função.
                Ret = EasyInner.ReceberModeloBio(int.Parse(UiOffline.txtNumInner.Text), 0, ref Modelo);
                Application.DoEvents();
            }
            while (Ret == (int)Enumeradores.Retorno.RET_BIO_PROCESSANDO);

            //Define o modelo do Inner Bio
            switch (Modelo)
            {
                case 1:
                    UiOffline.lblVersao.Text += "Modelo do bio: Light 100 usuários FIM10";
                    break;
                case 4:
                    UiOffline.lblVersao.Text += "Modelo do bio: 1000/4000 usuários FIM01";
                    break;
                case 51:
                    UiOffline.lblVersao.Text += "Modelo do bio: 1000/4000 usuários FIM2030";
                    break;
                case 52:
                    UiOffline.lblVersao.Text += "Modelo do bio: 1000/4000 usuários FIM2040";
                    break;
                case 48:
                    UiOffline.lblVersao.Text += "Modelo do bio: Light 100 usuários FIM3030";
                    break;
                case 64:
                    UiOffline.lblVersao.Text += "Modelo do bio: Light 100 usuários FIM3040";
                    break;
                case 80:
                    UiOffline.lblVersao.Text += "Modelo do bio: FIM5060";
                    break;
                case 82:
                    UiOffline.lblVersao.Text += "Modelo do bio: FIM5260";
                    break;
                case 83:
                    UiOffline.lblVersao.Text += "Modelo do bio: FIM5360";
                    break;
                case 96:
                    UiOffline.lblVersao.Text += "Modelo do bio: FIM6060";
                    break;
                case 255:
                    UiOffline.lblVersao.Text += "Modelo do bio: Desconhecido";
                    break;
            }
            return Ret;
        }

        private int ReceberVersaoBioAVer5xx()
        {
            int Ret = -1;
            int VersaoAltaBio = 0;
            int VersaoBaixaBio = 0;
            //Solicita a versão do Inner bio.
            Ret = EasyInner.SolicitarVersaoBio(int.Parse(UiOffline.txtNumInner.Text));
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Retorna o resultado do comando SolicitarVersaoBio, a versão
                //do Inner Bio é retornado por referência nos parâmetros da
                //função.

                Ret = EasyInner.ReceberVersaoBio(int.Parse(UiOffline.txtNumInner.Text), 0, ref VersaoAltaBio, ref VersaoBaixaBio);
                Application.DoEvents();
            }
            UiOffline.lblVersao.Text += VersaoAltaBio + "." + VersaoBaixaBio;
            return Ret;
        }
        private int ReceberVersaoBio6xx()
        {
            int Ret = -1;
            Ret = EasyInner.RequisitarVersaoBio(int.Parse(UiOffline.txtNumInner.Text), (UiOffline.chkModuloLC.Checked ? 1 : 0));
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                byte[] VersaoBio = new byte[4];
                Ret = EasyInner.RespostaVersaoBio(int.Parse(UiOffline.txtNumInner.Text), VersaoBio);
                if(Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblVersao.Text += "Versão bio " + Encoding.ASCII.GetString(VersaoBio);
                }
            }
            return Ret;
        }

        private void EnviarListaOff()
        {
            int Ret = -1;
            //Envia lista
            if (UiOffline.chkLista.Checked)
            {
                //Mensagem Status
                UiOffline.lblEnvia.Text = "Enviando lista...";
                System.Threading.Thread.Sleep(100);

                //Verifica qual lista enviar
                if (UiOffline.rdbPadraoTopdata.Checked)
                {
                    //Chama rotina que monta lista do tipo TOPDATA
                    MontarListaTopdata();
                }
                else
                {
                    //Chama rotina que monta lista do tipo LIVRE
                    MontarListaLivre(UiOffline);

                }
                Application.DoEvents();
                //Envia o Buffer com os usuarios da lista
                Ret = EasyInner.EnviarListaAcesso(int.Parse(UiOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblEnvia.Text = "Lista enviada com sucesso!";
                }
                else
                {
                    UiOffline.lblEnvia.Text = "Erro ao enviar lista!";
                    UiOffline.btnEnviar.Enabled = true;
                    UiOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
        }
        private void EnviarHorarios()
        {
            int Ret = -1;
            //Envia para o Inner o buffer com a lista de horários de acesso, após executar
            //o comando o buffer é limpo tomaticamente pela dll
            if (UiOffline.chkHorarios.Checked)
            {
                //Mensagem Status
                UiOffline.lblEnvia.Text = "Enviando horários...";

                //chama a rotina que monta horarios
                MontarHorarios();
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

                //Envia buffer com lista de horarios de acesso
                Ret = EasyInner.EnviarHorariosAcesso(int.Parse(UiOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblEnvia.Text = "Horários enviados com sucesso!";
                }
                else
                {
                    UiOffline.lblEnvia.Text = "Erro ao enviar os horários!";
                    UiOffline.btnEnviar.Enabled = true;
                    UiOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
        }
        private void EnviarSirene()
        {
            int Ret = -1;
            //Envia o buffer com os horário de sirene cadastrados para o Inner.
            if (UiOffline.chkSirene.Checked)
            {
                //Mensagem Status
                UiOffline.lblEnvia.Text = "Enviando horários sirene...";
                System.Threading.Thread.Sleep(100);

                //Chama rotina que monta os horarios
                MontarHorariosSirene();
                Application.DoEvents();
                Ret = EasyInner.EnviarHorariosSirene(int.Parse(UiOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblEnvia.Text = "Horários da Sirene enviados com sucesso!";
                }
                else
                {
                    UiOffline.lblEnvia.Text = "Erro ao enviar os horários da sirene!";
                    UiOffline.btnEnviar.Enabled = true;
                    UiOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
        }
        private void EnviarMensagemOffLine()
        {
            //Envia o buffer com todas as mensagens off line configuradas anteriormente,
            //para o Inner.
            if (UiOffline.chkMensagem.Checked)
            {
                int Ret = -1;
                //Mensagem Status
                UiOffline.lblEnvia.Text = "Enviando mensagem...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

                //Chama rotina de envio de mensagem
                MontarMensagem();

                //Envia Buffer com todas as mensagens offline
                Ret = EasyInner.EnviarMensagensOffLine(int.Parse(UiOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblEnvia.Text = "Mensagem enviada com sucesso!";
                }
                else
                {
                    UiOffline.lblEnvia.Text = "Erro ao enviar Mensagem!";
                    UiOffline.btnEnviar.Enabled = true;
                    UiOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
        }
        private void EnviarConfiguracoes()
        {
            int Ret = -1;
            //Chama rotina que monta as configurações
            MontarConfiguracao(UiOffline);


            //Envia buffer com as configurações, buffer interno da dll que contém todas as
            //configurações das funções anteriores para o Inner, após o envio esse buffer
            //é limpo sendo necessário chamar novamente as funções acima para reconfigurá-lo.
            Ret = EasyInner.EnviarConfiguracoes(int.Parse(UiOffline.txtNumInner.Text)); //(nº do Inner)
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                UiOffline.lblEnvia.Text = "Configurações enviadas com sucesso!";
            }
            else
            {
                UiOffline.lblEnvia.Text = "Erro ao enviar configurações!";
                UiOffline.btnEnviar.Enabled = true;
                UiOffline.btnReceber.Enabled = true;
                return;
            }

            Application.DoEvents();
        }
        private void EnviarRelogio()
        {
            int Ret = -1;
            //Envia relógio
            //Configura o relógio(data/hora) do Inner.
            if (UiOffline.chkRelogio.Checked)
            {
                //Mensagem Status
                UiOffline.lblEnvia.Text = "Enviando relógio...";
                Application.DoEvents();
                Thread.Sleep(100);

                //Formato o ano, pega apenas os dois ultimos digitos
                int Ano = int.Parse(System.DateTime.Now.ToString("yy"));
                int Mes = System.DateTime.Now.Month;
                int Dia = System.DateTime.Now.Day;
                int Hora = System.DateTime.Now.Hour;
                int Minuto = System.DateTime.Now.Minute;
                int Segundo = System.DateTime.Now.Second;

                //Envia relogio
                Ret = EasyInner.EnviarRelogio(int.Parse(UiOffline.txtNumInner.Text), (byte)Dia, (byte)Mes, (byte)Ano, (byte)Hora, (byte)Minuto, (byte)Segundo);

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiOffline.lblEnvia.Text = "Relógio enviado com sucesso!";
                }
                else
                {
                    UiOffline.lblEnvia.Text = "Erro ao enviar relógio!";
                    UiOffline.btnEnviar.Enabled = true;
                    UiOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
        }
        #endregion

        #region Receber
        /// <summary>
        /// Esta rotina é responsável por efetuar a coleta dos bilhetes, verificando
        /// qual o padrão do cartão
        /// </summary>
        /// <param name="UiMainOffline"></param>
        public void Receber()
        {
            //Campo obrigatório
            if (UiOffline.cboTipoLeitor.SelectedIndex == -1)
            {
                MessageBox.Show("Favor selecionar um tipo de leitor !", "Atenção");
                return;
            }

            //Desabilita os botões durante a coleta
            UiOffline.btnEnviar.Enabled = false;
            UiOffline.btnReceber.Enabled = false;

            //Define qual será o tipo de conexão(meio de comunicação) que será utilizada
            //pela dll para comunicar com os Inners. Essa função deverá ser chamada antes
            //de iniciar o processo de comunicação e antes da função AbrirPortaComunicacao.
            EasyInner.DefinirTipoConexao((byte)UiOffline.cboTipoConexao.SelectedIndex);

            //Define qual padrão de cartão será utilizado pelos Inners
            //padrão Topdata ou padrão livre.
            if (UiOffline.rdbPadraoTopdata.Checked)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                if (UiOffline.rdbPadraoLivre.Checked)
                {
                    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
                }
            }

            if (Conectar())
            {
                DefineVersao();

                //Mensagem Status
                UiOffline.lblBilhetes.Text = "Coletando bilhetes...";
                Application.DoEvents();

                //Chama rotina que realiza a coleta dos bilhetes offline
                if (InnerNetAcesso)
                {
                    ColetarBilhetesInnerAcesso(UiOffline);
                }
                else
                {
                    ColetarBilhetesInnerNet(UiOffline);
                }
            }

            //Após realizar a coleta, habilita novamente os botões
            UiOffline.btnEnviar.Enabled = true;
            UiOffline.btnReceber.Enabled = true;
        }

        #endregion

        #endregion

        #region Metodos Auxiliares

        #region DefineVersao
        /// <summary>
        /// Esta rotina é responsável por identificar a versão do inner
        /// </summary>
        /// <param name="UiMainOffline"></param>
        /// <returns></returns>
        private bool DefineVersao()
        {
            //Declaração de variáveis
            short VariacaoInner = 0;
            byte VersaoBaixa = 0;
            byte VersaoSufixo = 0;
            byte Linha_ = 0;
            string LinhaInner = "";
            string VersaoInner = "";
            byte Ret2 = 0;
            string StrVersao = "";
            byte TipoModBio = 0;

           
            Application.DoEvents();

            //Chama rotina que realiza a conexão
            Ret2 = 255;
            
            while(Ret2 != 0)
            {
                //Solicita a versão do firmware do Inner e dados como o Idioma, se é
                //uma versão especial.
                Ret2 = EasyInner.ReceberVersaoFirmware6xx(int.Parse(UiOffline.txtNumInner.Text), ref Linha_, ref VariacaoInner, ref VersaoAlta, 
                                                        ref VersaoBaixa, ref VersaoSufixo, ref InnerAcessoBio, ref TipoModBio);
                System.Threading.Thread.Sleep(100);
                Linha = Linha_;
            }

            if (Ret2 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Define a linha do Inner
                switch (Linha)
                {
                    case 1:
                        LinhaInner = "Inner Plus";
                        InnerNetAcesso = false;
                        break;
                    case 2:
                        LinhaInner = "Inner Disk";
                        InnerNetAcesso = false;
                        break;
                    case 3:
                        LinhaInner = "Inner Verid";
                        InnerNetAcesso = false;
                        break;
                    case 6:
                        LinhaInner = "Inner Bio";
                        InnerNetAcesso = false;
                        break;
                    case 7:
                        LinhaInner = "Inner NET";
                        InnerNetAcesso = false;
                        break;
                    case 14:
                        LinhaInner = "Inner Acesso";
                        InnerNetAcesso = true;
                        break;
                }

                VersaoInner = VersaoAlta.ToString() + "." + VersaoBaixa.ToString() + "." + VersaoSufixo.ToString();
                StrVersao = LinhaInner;

                if (VariacaoInner > 0)
                    StrVersao = StrVersao + " - Variação: " + VariacaoInner.ToString();

                StrVersao = StrVersao + " - Versão: " + VersaoInner;
                return true;
            } 
            else 
            {
                //Mensagens Status
                UiOffline.lblEnvia.Text = "Erro ao conectar no inner!";
                EasyInner.FecharPortaComunicacao();
                Application.DoEvents();
                return false; 
            }
        }
        #endregion

        #region testaConexaoInner
        /// <summary>
        /// Metodo responsável por realizar um comando simples com o equipamento para detectar
        /// se esta conectado.
        /// </summary>
        /// <param name="UiMainOffline"></param>
        /// <returns></returns>
        private static int testaConexaoInner(FrmOffLine UiMainOffline)
        {
            int RetRelogio = -1;
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;

           RetRelogio = EasyInner.ReceberRelogio(System.Convert.ToInt16(UiMainOffline.txtNumInner.Text), ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, ref Segundo);
           return RetRelogio;

        }
        #endregion

        #region Conectar
        /// <summary>
        /// Rotina responsável por efetuar a conexão com o Inner
        /// </summary>
        /// <param name="UiMainOffline"></param>
        /// <returns></returns>
        private bool Conectar()
        {
            int Fim = 0;
            int Ret = -1;

            //Define qual será o tipo de conexão(meio de comunicação) que será utilizada
            //pela dll para comunicar com os Inners. Essa função deverá ser chamada antes
            //de iniciar o processo de comunicação e antes da função AbrirPortaComunicacao.
            EasyInner.DefinirTipoConexao((byte)UiOffline.cboTipoConexao.SelectedIndex);

            //Define qual padrão de cartão será utilizado pelos Inners
            //padrão Topdata ou padrão livre.
            if (UiOffline.rdbPadraoTopdata.Checked)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                if (UiOffline.rdbPadraoLivre.Checked)
                {
                    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
                }
            }

            //Fecha a porta de comunicação previamente aberta, seja ela serial, Modem ou
            //TCP/IP.
            EasyInner.FecharPortaComunicacao();

            //Abre a porta de comunicação desejada, essa função deverá ser chamada antes
            //de iniciar qualquer processo de transmissão ou recepção de dados com o Inner.
            Ret = EasyInner.AbrirPortaComunicacao(int.Parse(UiOffline.txtPorta.Text));
       
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                Fim = (int) EasyInner.RetornarSegundosSys() + 15;
                do
                {
                    //Testa a comunicação com o Inner, também utilizado para efetuar a conexão
                    //com o Inner. Para efetuar a conexão com o Inner, essa função deve ser
                    //executada em um loop até retornar 0(zero), executado com sucesso.
                    
                    Ret = testaConexaoInner(UiOffline);

                } while (((int) EasyInner.RetornarSegundosSys() <= Fim) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));
                 
                return (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK);
            }
            else
            {
                return (false);
            }
        }
        #endregion

        #region MontarConfiguracao
        /// <summary>
        /// Esta rotina monta o buffer para enviar a configuração do Inner
        /// </summary>
        /// <param name="UiMainOffline"></param>
        private void MontarConfiguracao(FrmOffLine UiMainOffline)
        {
            //Antes de realizar a configuração precisa definir o Padrão do cartão
            //Topdata ou padrão livre.
            if (UiMainOffline.rdbPadraoTopdata.Checked)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                if (UiMainOffline.rdbPadraoLivre.Checked)
                {
                    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
                }
            }

            //Modo de comunicação
            //Configurações para Modo Offline.
            //Prepara o Inner para trabalhar no modo Off-Line, porém essa função ainda
            //não envia essa informação para o equipamento.
            EasyInner.ConfigurarInnerOffLine(); 

            //Verificar
            //Acionamentos 1 e 2
            //Configura como irá funcionar o acionamento(rele) 1 e 2 do Inner, e por
            //quanto tempo ele será acionado.
            switch (UiMainOffline.cboEquipamento.SelectedIndex)
            {
                //Coletor
                case (byte)Enumeradores.Acionamento.Acionamento_Coletor:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 5);

                    if (UiMainOffline.chkDoisLeitores.Checked)
                    {
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    break;

                //Catraca
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    if (UiMainOffline.chkDoisLeitores.Checked)
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    }
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada:
                    if (UiMainOffline.optDireita.Checked)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida:
                    if (UiMainOffline.optDireita.Checked)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;


                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada:
                    if (UiMainOffline.optEsquerda.Checked)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;


                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_Liberada:
                    if (UiMainOffline.optEsquerda.Checked)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    if (UiMainOffline.chkDoisLeitores.Checked)
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    }
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS_MARCACAO_REGISTRO, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    if (UiMainOffline.chkDoisLeitores.Checked)
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    }
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    break;
            }

            //Configura o tipo do leitor que o Inner está utilizando, se é um leitor
            //de código de barras, magnético ou proximidade.
            switch (UiMainOffline.cboTipoLeitor.SelectedIndex)
            {
                case (byte) Enumeradores.TipoLeitor.CODIGO_DE_BARRAS:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS);
                    break;

                case (byte) Enumeradores.TipoLeitor.MAGNETICO:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.MAGNETICO);
                    break;

                case (byte) Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2);
                    break;

                case (byte) Enumeradores.TipoLeitor.WIEGAND:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND);
                    break;
                case (byte) Enumeradores.TipoLeitor.PROXIMIDADE_SMART_CARD_SERIAL:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_SMART_CARD_SERIAL);
                    break;
                case (byte)Enumeradores.TipoLeitor.CODIGO_BARRAS_SERIAL:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_BARRAS_SERIAL);
                    break;
                case (byte)Enumeradores.TipoLeitor.WIEGAND_FC_SEM_ZERO:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND_FC_SEM_ZERO);
                    break;
            }

            //Define a quantidade de dígitos dos cartões a serem lidos pelo Inner.
            EasyInner.DefinirQuantidadeDigitosCartao((byte)int.Parse(UiMainOffline.txtDigitos.Text));

            if(UiMainOffline.chkCartaoMaster.Checked)
            {
                EasyInner.DefinirNumeroCartaoMaster(UiMainOffline.txtCartaoMaster.Text);
            }
            //Habilitar teclado
            //Permite que os dados sejam inseridos no Inner através do teclado do
            //equipamento. Habilitando o parâmetro ecoar, o teclado irá ecoar asteriscos
            //no display do Inner.
            EasyInner.HabilitarTeclado((byte)(UiMainOffline.chkTeclado.Checked ? 1 : 0), 0);

            //ConfigurarLeitor: Configura as operações que o leitor irá executar. Se irá
            //registrar os dados somente como entrada independente do sentido em que o
            //cartão for passado, somente como saída ou como entrada e saída.
            if (UiMainOffline.chkDoisLeitores.Checked)
            {
               
                //Habilita os leitores wiegand para o primeiro leitor e o segundo leitor
                //do Inner, e configura se o segundo leitor irá exibir as mensagens
                //configuradas.
                EasyInner.ConfigurarWiegandDoisLeitores(0, 1);
            }       
            
            //Define qual tipo de lista(controle) de acesso o Inner vai utilizar
            if (UiMainOffline.chkLista.Checked)
                EasyInner.DefinirTipoListaAcesso(1);
            else
                EasyInner.DefinirTipoListaAcesso(0);

            //Configura o Inner para registrar as tentativas de acesso negado.
            EasyInner.RegistrarAcessoNegado(1);

            //Catraca
            //Define qual será o tipo do registro realizado pelo Inner ao aproximar um
            //cartão do tipo proximidade no leitor do Inner, sem que o usuário tenha
            //pressionado a tecla entrada, saída ou função.
            if ((UiMainOffline.cboEquipamento.SelectedIndex ==
                (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida)
                || (UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos)
                || (UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro))
            {
                //Configura o tipo de registro que será associado a uma marcação
                EasyInner.DefinirFuncaoDefaultLeitoresProximidade(12); // 12 – Libera a catraca nos dois sentidos e registra o bilhete conforme o sentido giro.

                if (UiMainOffline.chkBio.Checked)
                {
                    EasyInner.DefinirFuncaoDefaultSensorBiometria(12);
                }
                else
                {
                    EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                }
            }
            else
            {
                if ((UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada)
                    || (UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada))
                {
                    if (UiMainOffline.optDireita.Checked)
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // 10 – Registrar sempre como entrada.

                        if (UiMainOffline.chkBio.Checked)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(10);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }
                    }
                    else
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // Inverte o sentido de entrada.

                        if (UiMainOffline.chkBio.Checked)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(11);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }
                    }

                }
                else
                {
                    if (UiMainOffline.optDireita.Checked)
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // 10 – Registrar sempre como entrada.

                        if (UiMainOffline.chkBio.Checked)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(11);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }

                    }
                    else
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // Inverte o sentido de entrada.

                        if (UiMainOffline.chkBio.Checked)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(10);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }
                    }
                }
            }

            if (VersaoAlta>=5)
            {
                EasyInner.SetarBioVariavel(1);
                EasyInner.ConfigurarBioVariavel(1);
            } 

        }
        #endregion

        #region ColetarBilhetesInnerAcesso
        /// <summary>
        /// Esta rotina efetua a coleta de bilhetes que foram registrados em offline
        /// </summary>
        /// <param name="UiMainOffline"></param>
        private void ColetarBilhetesInnerAcesso(FrmOffLine UiMainOffline)
        {

            //Declaração das variáveis
            Bilhete Bilhete = new Bilhete();
            Bilhete.Ano = 0;
            Bilhete.Cartao = new StringBuilder();   
            Bilhete.Dia = 0;
            Bilhete.Hora = 0;
            Bilhete.Mes = 0;
            Bilhete.Minuto = 0;
            Bilhete.Tipo = 0;

            int Fim = 0;
            int Ret2 = -1;
            String strCartao = "";
            int Count;
            int nBilhetes = 0;
            int QtdeBilhetes;
            int[] receber = new int[2];

                nBilhetes = 0;
                QtdeBilhetes = 0;
                Ret2 = EasyInner.ReceberQuantidadeBilhetes(int.Parse(UiMainOffline.txtNumInner.Text), receber);
                QtdeBilhetes = receber[0];

                do
                {                
                    if (QtdeBilhetes > 0)
                    {
                        do
                        {
                            System.Threading.Thread.Sleep(100);

                            //Coleta um bilhete Off-Line que está armazenado na memória do Inner
                            Ret2 = EasyInner.ColetarBilhete(int.Parse(UiMainOffline.txtNumInner.Text), ref Bilhete.Tipo, ref Bilhete.Dia, ref Bilhete.Mes, ref Bilhete.Ano, ref Bilhete.Hora, ref Bilhete.Minuto, Bilhete.Cartao);

                            if(Ret2 == (int)Enumeradores.Retorno.RET_COMANDO_OK){
                                strCartao = "";

                                //Atribui o nro do Cartão..
                                for (Count = 0; Count < 16; Count++)
                                {
                                    strCartao += System.Convert.ToString(System.Convert.ToChar(Bilhete.Cartao[Count]));
                                }

                                //Armazena os dados do bilhete no list, pode ser utilizado com
                                //banco de dados ou outro meio de armazenamento compatível
                                UiMainOffline.lstBilhetes.Items.Add("Tipo:" + Bilhete.Tipo + "  Cartão:" + strCartao + "  Data:" + Bilhete.Dia.ToString("00") + "/" + Bilhete.Mes.ToString("00") + "/" + Bilhete.Ano.ToString("00") + "  Hora:" + Bilhete.Hora.ToString("00") + ":" + Bilhete.Minuto.ToString("00"));
                                Fim = (int)EasyInner.RetornarSegundosSys() + 15;
                                nBilhetes++;
                                QtdeBilhetes--;
                                Application.DoEvents();                        
                            }
                        } while (QtdeBilhetes > 0);

                        UiMainOffline.lblBilhetes.Text = "Foram coletados "+ nBilhetes +" bilhete(s) offline !";
                        Ret2 = EasyInner.ReceberQuantidadeBilhetes(int.Parse(UiMainOffline.txtNumInner.Text), receber);
                        QtdeBilhetes = receber[0];
                    }
                } while (QtdeBilhetes > 0);
                UiMainOffline.lblBilhetes.Text = "Foram coletados 0 bilhete(s)!";
           
            Application.DoEvents();
        }
        #endregion

        #region ColetarBilhetesInnerNet
        /// <summary>
        /// Esta rotina efetua a coleta de bilhetes que foram registrados em offline
        /// </summary>
        /// <param name="UiMainOffline"></param>
        private void ColetarBilhetesInnerNet(FrmOffLine UiMainOffline)
        {

            //Declaração das variáveis
            Bilhete Bilhete;
            Bilhete = new Bilhete();
            Bilhete.Ano = 0;
            Bilhete.Cartao = null;
            Bilhete.Cartao = new StringBuilder();
            Bilhete.Dia = 0;
            Bilhete.Hora = 0;
            Bilhete.Mes = 0;
            Bilhete.Minuto = 0;
            Bilhete.Tipo = 0;

            int Fim = 0;
            int Ret = -1;
            String strCartao = "";
            int Count;
            int nBilhetes = 0;

          
                //Contador tempo de coleta
                Fim = (int)EasyInner.RetornarSegundosSys() + 30;
                do
                {
                    System.Threading.Thread.Sleep(100);

                    //Coleta um bilhete Off-Line que está armazenado na memória do Inner
                    Ret = EasyInner.ColetarBilhete(int.Parse(UiMainOffline.txtNumInner.Text), ref Bilhete.Tipo, ref Bilhete.Dia, ref Bilhete.Mes, ref Bilhete.Ano, ref Bilhete.Hora, ref Bilhete.Minuto, Bilhete.Cartao);

                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        strCartao = "";

                        //Atribui o nro do Cartão..
                        for (Count = 0; Count < 16; Count++)
                        {
                            strCartao += System.Convert.ToString(System.Convert.ToChar(Bilhete.Cartao[Count]));
                        }

                        //Armazena os dados do bilhete no list, pode ser utilizado com
                        //banco de dados ou outro meio de armazenamento compatível
                        UiMainOffline.lstBilhetes.Items.Add("Tipo:" + Bilhete.Tipo + "  Cartão:" + strCartao + "  Data:" + Bilhete.Dia.ToString("00") + "/" + Bilhete.Mes.ToString("00") + "/" + Bilhete.Ano.ToString("00") + "  Hora:" + Bilhete.Hora.ToString("00") + ":" + Bilhete.Minuto.ToString("00"));
                        Fim = (int)EasyInner.RetornarSegundosSys() + 15;
                        nBilhetes++;
                        Application.DoEvents();
                    }
                } while ((Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK) || ((int)EasyInner.RetornarSegundosSys() <= Fim));

                EasyInner.FecharPortaComunicacao();

                //Mensagens Status
                UiMainOffline.lblBilhetes.Text = "Foram coletados " + nBilhetes + " bilhete(s) offline !";
            
           
            Application.DoEvents();
        }
        #endregion

        #region MontarBufferListaSemDigital
        //***********************************************************************************
        //APENAS PARA O INNER BIO
        //Monta o buffer da lista de cartões dos usuários sem digital no Inner bio
        //***********************************************************************************
        private void MontarBufferListaSemDigital()
        {
            DAOUsuariosBio usuariosSD = new DAOUsuariosBio();

            List<UsuarioSemDigital> ListaSD = new List<UsuarioSemDigital>(); 
            ListaSD = usuariosSD.ConsultarUsuariosSD();
            for (int index = 0; index < ListaSD.Count; index++)
            {
                if (InnerNetAcesso)
                {
                    EasyInner.IncluirUsuarioSemDigitalBioInnerAcesso(ListaSD[index].Usuario);
                }
                else
                {
                    EasyInner.IncluirUsuarioSemDigitalBio(ListaSD[index].Usuario);
                }
            }
        }
        #endregion

        #region MontarHorarios
        /// <summary>
        /// Monta o buffer para enviar os horários de acesso
        /// Tabela de horarios numero 1
        /// </summary>
        private void MontarHorarios()
        {
            List<Horarios> ListaHorarios = Horarios.MontarListaHorarios();
            for (int index = 0; index < ListaHorarios.Count; index++)
            {
                
                //Insere no buffer da DLL horario de acesso
                EasyInner.InserirHorarioAcesso(ListaHorarios[index].Horario, ListaHorarios[index].Dia, ListaHorarios[index].Faixa, 
                                               ListaHorarios[index].Hora, ListaHorarios[index].Minuto); //(1 - nº da tabela horario, 1 - dia da semana, 1 - faixa de horario, 8 - hora, 0 - minuto)
            }

        }
        #endregion

        #region MontarMensagem
        /// <summary>
        /// Esta rotina é responsável por montar o buffer para o envio de mensagens
        /// </summary>
        private void MontarMensagem()
        {
            if (!UiOffline.optEsquerda.Checked)
            {
                //Mensagem Entrada e Saida Offline Liberado!
                EasyInner.DefinirMensagemEntradaOffLine(1, "ENTRADA LIBERADA.");
                EasyInner.DefinirMensagemSaidaOffLine(1, "SAIDA LIBERADA.");


            }
            else
            { 

                    //Mensagem Entrada e Saida Offline Liberado!
                    EasyInner.DefinirMensagemEntradaOffLine(1, "SAIDA LIBERADA.");
                    EasyInner.DefinirMensagemSaidaOffLine(1, "ENTRADA LIBERADA.");
            }
            EasyInner.DefinirMensagemPadraoOffLine(1, "    OFF LINE    "); //(1 - Exibe data/hora, string com a mensagem a ser exibida quando o Inner estiver ocioso)
       }

        #endregion

        #region MontarHorariosSirene
        /// <summary>
        /// Esta rotina monta os horários de toque da sirene e quais dias da semana irão tocar
        /// </summary>
        private void MontarHorariosSirene(){

            List<string> ListaSirene = new List<string>();
            ListaSirene.Add("07;59;1;1;1;1;1;1;0");
            ListaSirene.Add("11;31;1;1;1;1;1;1;0");
            ListaSirene.Add("14;30;1;1;1;1;1;0;0");
            ListaSirene.Add("18;10;1;1;1;1;1;0;0");
            string[] Mhs;
            byte Hora, Min, Seg, Ter, Quar, Quin, Sex, Sab, DomFer;
            for (int index = 0; index < ListaSirene.Count; index++)
            {
                Mhs = ListaSirene[index].Split(';');
                Hora = Convert.ToByte(Mhs[0]);
                Min = Convert.ToByte(Mhs[1]);
                Seg = Convert.ToByte(Mhs[2]);
                Ter = Convert.ToByte(Mhs[3]);
                Quar = Convert.ToByte(Mhs[4]);
                Quin = Convert.ToByte(Mhs[5]);
                Sex = Convert.ToByte(Mhs[6]);
                Sab = Convert.ToByte(Mhs[7]);
                DomFer = Convert.ToByte(Mhs[8]);
                EasyInner.InserirHorarioSirene(Hora, Min, Seg, Ter, Quar, Quin, Sex, Sab, DomFer); //( Hora, Minuto, Segunda, Terca, Quarta, Quinta, Sexta, Sabado,DomingoFeriado)
            }
        }
        #endregion

        #region MontarListaTopdata
        //***********************************************************************************
        //MONTAR LISTA TOPDATA
        //Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão Topdata
        //***********************************************************************************
        private void MontarListaTopdata(){

            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);

            //Insere usuario da lista no buffer da DLL
            for (int i = 0; i < 5; i++)
            {
                //Insere usuário da lista no buffer da DLL
                EasyInner.InserirUsuarioListaAcesso(i.ToString(), 101);
            }
        }
        #endregion

        #region MontarListaLivre
        /// <summary>
        /// Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão livre 14 dígitos
        /// </summary>
        /// <param name="UiMainOffline"></param>
        private void MontarListaLivre(FrmOffLine UiMainOffline)
        {
            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte) Enumeradores.PadraoCartao.PADRAO_LIVRE); //(1 - Padrao Livre(Default))
            
            //Quantidade de digitos que o cartao usará
            EasyInner.DefinirQuantidadeDigitosCartao((byte) int.Parse(UiMainOffline.txtDigitos.Text)); //(qtde de digitos)
            
            //Insere usuario da lista no buffer da DLL
            EasyInner.InserirUsuarioListaAcesso("1", 101); //(1 - depende do padrao do cartao, 1 - nº do horario ja cadastrado)
            EasyInner.InserirUsuarioListaAcesso("187", 101);
            EasyInner.InserirUsuarioListaAcesso("123456", 101);
            EasyInner.InserirUsuarioListaAcesso("27105070", 101);
            EasyInner.InserirUsuarioListaAcesso("103086639459", 101);
            EasyInner.InserirUsuarioListaAcesso("10", 2);
            EasyInner.InserirUsuarioListaAcesso("10", 3);

        }
        #endregion

        #endregion
    }
}
