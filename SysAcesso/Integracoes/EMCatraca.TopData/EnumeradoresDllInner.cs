namespace EMCatraca.TopData
{
    public class EnumeradoresDllInner
    {
        #region Estado do Equipamento

        public enum EstadoEquipamento
        {
            Conectando,
            Conectado,
            ValidandoAcesso,
            ExcedeuTempoDeGiro,
            AguardandoGiro,
            GirouCatraca,
            Parado
        }

        #endregion

        #region Habilita

        public enum Habilita
        {
            Desabilita = 0,
            Habilita = 1
        }

        #endregion

        #region Ecoar 

        public enum Ecoar
        {
            //Constantes de Eco de digitos no teclado
            EcoaDigitado = 0,
            EcoaAsterisco = 1
        }

        #endregion

        #region TipoConexao

        public enum TipoConexao
        {
            SerialRs232_485,
            TcpIpComPortaVariaavel,
            TcpIpComPortaFixa,
            Modem,
            TopPendrive
        }

        #endregion

        #region TipoLeitor

        public enum TipoLeitor
        {
            //Constantes de Tipo de Leitor..
            CodigoDeBarras = 0,
            Magnetico = 1,
            ProximidadeAbatrack2 = 2,
            Wiegand = 3,
            ProximidadeSmartCardSertial = 4,
            CodigoBarrasSerial = 5,
            WiegandFcSemSeparador = 6,
            WiegandFcComSeparador = 7,
            QRCodeLetras = 8,
            BarrasProxQRCode = 7
        }

        #endregion

        #region Operacao

        public enum Operacao
        {
            //Constantes de Operação
            Desativado = 0,
            SomenteEntrada = 1,
            SomenteSaida = 2,
            EntradaEhSaida = 3,
            EntradaEhSaidaInvertidas = 4
        }

        #endregion

        #region Opcao

        public enum Opcao
        {
            //Constantes de Opção
            Nao = 0,
            Sim = 1
        }

        #endregion

        #region ConfiguracaoLeitor

        public enum ConfiguracaoLeitor
        {
            //Constantes de Configuração de Leitor
            Desativado = 0,
            Eentrada = 1,
            Saida = 2,
            EntradaSaida = 3,
            EentradaSaidaIinvertida = 4
        }

        #endregion

        #region FuncaoAcionamento

        public enum FuncaoAcionamento
        {
            NaoUtilizado = 0,
            AcionaRegistroEntradaOuSaida = 1,
            AcionaRegistroEntrada = 2,
            AcionaRegistroSaida = 3,
            ConectadoSirene = 4,
            RevistaUsuarios = 5,
            CatracaSaidaLiberada = 6,
            CatracaEntradaLiberada = 7,
            CatracaLiberadaDoisSentidos = 8,
            CatacaLiberadaDoisSantiddosMarcacaoRegistro = 9
        }

        #endregion

        #region RetornoBIO

        public enum RetornoBIO
        {
            //Constantes retorno Bio
            Sucesso = 0,
            FalhaNaComunicao = 1,
            ProcessandoUltimoComando = 128,
            FalhaNaComunicacaoComPlacaBio = 129,
            InnerBioNaoEstaEmModoMaster = 130,
            UsuarioJaCadastroNoBancoDeDadosInnerBio = 131,
            UsuarioNaoCadastroNoBancoDeDadosInnerBio = 132,
            BaseDeDadosDeUsuariosEstaCheia = 133,
            ErroNoSegindoDedoDoUsuario = 134,
            SolicitacaoParaInnerBioIinvalida = 135,
            RetBioTemplateIinvalido = 136,
            RetBioParamentroInvalido = 137,
            RetBioModuloIincorreto = 250
        }

        #endregion

        #region EnviarFormasEntradasOnLine

        public enum EnviarFormasEntradasOnLine
        {
            EntradasOnNaoAcaitaEntradaDados = 0,
            EntradasOnAceitaTeclado = 1,
            EntradasOnAceitaLeituraLeitor1 = 2,
            EntradasOnAceitaLeituraLeitor2 = 3,
            EntradasOnTecladoEhLeitor1 = 4,
            EntradasOnTecladoEhLeitor2 = 5,
            EntradasOnLeitor1EhLeitor2 = 6,
            EntradasOnTecladoEhLeitor1EhLeitor2 = 7,
            EntradasOnTecladoEhVerifBiometrica = 10,
            EntradasOnLeitor1EhVerifBiometrica = 11,
            EntradasOnTecladoEhLeitor1EhVerifBiometrica = 12,
            EntradasON_LEITOR1_COM_VERI_BIO_E_LEITOR2_SEM_VERI_BIO = 13,
            EntradasON_LEITOR1_COM_VERI_BIO_E_LEITOR2_SEM_VERI_BIO_E_TECLADO_SEM_VERI_BIO = 14,
            EntradasON_LEITOR1_E_IDENTIFICACAO_BIO = 100,
            EntradasON_LEITOR1_E_TECLADO_E_IDENTIFICACAO_BIO = 101,
            EntradasON_LEITOR1_E_LEITOR2_E_IDENTIFICACAO_BIO = 102,
            EntradasON_LEITOR1_E_LEITOR2_E_TECLADO_E_IDENTIFICACAO_BIO = 103,
            EntradasON_LEITOR1_INVERTIDO_E_IDENTIFICACAO_BIO = 104,
            EntradasON_LEITOR1_INVERTIDO_E_TECLADO_E_IDENTIFICACAO_BIO = 105
        }

        #endregion

        #region EstadosTeclado
        public enum EstadosTeclado
        {
            //Enumeradores Estados Teclado
            TecladoEmBranco,
            AguardadoTeclado
        }
        #endregion

        #region EstadosInner

        public enum EstadosInner
        {
            //Enumeradores Estados Inner
            EstadoConectar,
            EstadoColetarBilhetes,
            EstadoConfigurarEntradasOnline,
            EstadosPolling,
            EstadoLiberarCatraca,
            EstadoMonitorarGiroCatraca,
            EstadoPingOnline,
            EstadoReconectar,
            EstadoAguardaTempoMensagem,
            EstadoDefinicaoTeclado,
            EstadoAguardaDefinicaoTeclado,
            EstadoMonitoramentoUrna,
            EstadoEnviarCFGOnLineOffLine,
            EstadoEnviarMSGUrna,
            EstadoEnviarMSGAcessoNegado,
            EstadoEnviarMSGOffLineCatraca,
            EstadoEnviarMSGPadrao,
            EstadoEnviarMSGUrnaCheia,
            EstadoEnviarListaOffLine,
            EstadoEnviarListaSemDigital,
            ESTADO_ENVIAR_BIPCURTO,
            EstadoEnviarCFGOnline,
            EstadoEnviarDataHora,
            EstadoValidarAcesso,
            EstadoValidaUrnaCheia,
            EstadoErnviarCFGOffLine,
            EstadoDesligaRele,
            EstadoReceberVersaoBio,
            EstadoReceberModeloBio,
            EstadoReceberQTDBilhetesOFF,
            EstadoReceberFirmWare,
            EstadoAcionarRele1,
            EstadoAcionarRele2
        }

        #endregion

        #region Modo de comunicacao
        public enum modoComunicacao
        {
            //Constantes de Modo
            ModoOFFLINE = 0,
            ModoOnLine = 1
        }
        #endregion

        #region Retorno

        public enum Retorno
        {
            //Constantes de Retorno
            RetornoComandoOk = 0,
            RetornoComandoErro = 1,
            RetornoPortaNaoAberta = 2,
            RetornoPortaJaAberta = 3,
            RetornoDllInner2KNaoEncontrada = 4,
            RetornoDllInnerTCPNaoEncontrada = 5,
            RetornoDllInnerTCP2NaoEncontrada = 6,
            RetornoErroGpf = 8,
            RetornoTipoConexaoInvalida = 9,
            RetornoBioProcessando = 128,
            RetornoBioFalhaComunicacao = 129,
            RetornoBioUsrJaCadastrado = 131,
            RetornoBioUsrNaoCadastrado = 132,
            RetornoBioBaseCheia = 133,
            RetornoBioDIGNaoConfere = 134,
            RetornoBioInvalida = 135,
            RetornoBioTempleanteInvalido = 136,
            RetornoBioParametroInvalido = 137
        }

        #endregion

        #region PadraoCartao

        public enum PadraoCartao
        {
            //Constantes de Tipo de Cartão..
            PadraoLivre = 1
        }

        #endregion

        #region EntradasMudancaOnline
        public enum EntradasMudancaOnline
        {
            //Constantes do Método EntradasMudancaOnline
            NAO_ACEITA_ENTRADA_DADOS = 0,
            ACEITA_TECLADO = 1,
            ACEITA_LEITURA_LEITOR1 = 2,
            ACEITA_LEITURA_LEITOR2 = 3,
            TECLADO_E_LEITOR1 = 4,
            TECLADO_E_LEITOR2 = 5,
            LEITOR1_E_LEITOR2 = 6,
            TECLADO_LEITOR1_LEITOR2 = 7,
            TECLADO_E_VERI_BIOMETRICA = 10,
            LEITOR1_E_VERI_BIOMETRICA = 11,
            TECLADO_E_LEITOR1_E_VERI_BIOMETRICA = 12,
            LEITOR1_E_VERI_BIOMETRICA_LEITOR2_SEM_VERI_BIOMETRICA = 13,
            LEITOR1_E_VERI_BIOMETRICA_LEITOR2_SEM_VERI_BIOMETRICA_E_TECLADO_SEM_VERI_BIOMETRICA = 14,
            LEITOR1_E_IDENTIFICACAO_BIOMETRICA = 100,
            LEITOR1_E_TECLADO_IDENTIFICACAO_BIOMETRICA = 101,
            LEITOR1_E_LEITOR2_E_IDENTIFICACAO_BIOMETRICA = 102,
            LEITOR1_E_LEITOR2_E_TECLADO_E_IDENTIFICACAO_BIOMETRICA = 103,
            LEITOR1_INVERTIDO_IDENTIFICACAO_BIOMETRICA = 104,
            LEITOR1_INVERTIDO_E_TECLADO_E_IDENTIFICACAO_BIOMETRICA = 105
        }
        #endregion

        #region Origem
        public enum Origem
        {
            //Constantes de Origem do Método Receber Dados Online
            VIA_TECLADO = 1,
            VIA_LEITOR1 = 2,
            VIA_LEITOR2 = 3,
            SENSOR_DA_CATRACA_OBSOLETO_ = 4,
            FIM_TEMPO_ACIONAMENTO = 5,
            GIRO_DA_CATRACA_TOPDATA = 6,
            URNA = 7,
            EVENTO_SENSOR1 = 8,
            EVENTO_SENSOR2 = 9,
            EVENTO_SENSOR3 = 10,
            SENSOR_BIOMETRICO = 12,
            TECLA_FUNCAO = 65,
            TECLA_ANULA = 42,
            TECLA_ENTRADA = 66,
            TECLA_SAIDA = 67,
            TECLA_CONFIRMA = 35,
            PRESENCA_DEDO = 37,
            URNA_CHEIA = 20

        }
        #endregion

        #region MudancaOnlineOffline
        public enum MudancaOnlineOffline
        {
            DESABILITA = 0,
            SEM_PING_ONLINE = 1,
            COM_PING_ONLINE = 2
        }
        #endregion

        #region LeitorEntrada
        public enum LeitorEntrada
        {
            LEITOR1_DESABILITADO = 0,
            LEITOR1_SOMENTE_ENTRADA = 1,
            LEITOR1_SOMENTE_SAIDA = 2,
            LEITOR1_ENTRADA_SAIDA = 3,
            LEITOR1_SAIDA_ENTRADA = 4,
            LEITOR2_DESABILITADO = 0,
            LEITOR2_SOMENTE_ENTRADA = 1,
            LEITOR2_SOMENTE_SAIDA = 2,
            LEITOR2_ENTRADA_SAIDA = 3,
            LEITOR2_SAIDA_ENTRADA = 4
        }
        #endregion

        #region Acionamento
        public enum Acionamento
        {
            //Constantes de Função de Acionamento..
            AcionamentoColetor = 0,
            AcionamentoCatracaEntradaEhSaida = 1,
            AcionamentoCatracaEntrada = 2,
            AcionamentoCatracaSaida = 3,
            AcionamentoCatracaSaidaLiberada = 4,
            AcionamentoCatracaEntradaLiberada = 5,
            AcionamentoCatracaLiberadaDoisSentidos = 6,
            AcionamentoCatracaSentidoGiro = 7,
            AcionamentoCatracaUrna = 8
        }
        #endregion

        /// <summary>
        /// Enumerador para formas de entrada sem uso da biometria
        /// </summary>
        public enum FormaDeEntradaSemBiometria : int
        {
            /// <summary>
            /// Leitores 1 e 2 Desabilitados, Teclado Desabilitado 
            /// </summary>
            Byte128 = 10000000,

            /// <summary>
            /// Leitores 1 e 2 Desabilitados, Teclado Habilitado 
            /// </summary>
            Byte129 = 10000001,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Só Entrada, Teclado Desabilitado 
            /// </summary>
            Byte130 = 10000010,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Só Entrada, Teclado Habilitado 
            /// </summary>
            Byte131 = 10000011,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Só Saída, Teclado Desabilitado 
            /// </summary>
            Byte132 = 10000100,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Só Saída, Teclado Habilitado
            /// </summary>
            Byte133 = 10000101,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Entrada e Saída, Teclado Desabilitado 
            /// </summary>
            Byte134 = 10000110,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Entrada e Saída, Teclado Habilitado
            /// </summary>
            Byte135 = 10000111,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Entrada e Saída Invertido, Teclado Desabilitado 
            /// </summary>
            Byte136 = 10001000,

            /// <summary>
            /// Leitor 1 Desabilitado, Leitor 2 Entrada e Saída Invertido, Teclado Habilitado
            /// </summary>
            Byte137 = 10001001,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Desabilitado, Teclado Desabilitado 
            /// </summary>
            Byte144 = 10010000,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Desabilitado, Teclado Habilitado 
            /// </summary>
            Byte145 = 10010001,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Só Entrada, Teclado Desabilitado 
            /// </summary>
            Byte146 = 10010010,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Só Entrada, Teclado Habilitado 
            /// </summary>
            Byte147 = 10010011,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Só Saída, Teclado Desabilitado 
            /// </summary>
            Byte148 = 10010100,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Só Saída, Teclado Habilitado
            /// </summary>
            Byte149 = 10010101,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Entrada e Saída, Teclado Desabilitado
            /// </summary>
            Byte150 = 10010110,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Entrada e Saída, Teclado Habilitado
            /// </summary>
            Byte151 = 10010111,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Entrada e Saída Invertidos, Teclado Desabilitado
            /// </summary>
            Byte152 = 10011000,

            /// <summary>
            /// Leitor 1 Só Entrada, Leitor 2 Entrada e Saída Invertidos, Teclado Habilitado
            /// </summary>
            Byte153 = 10011001,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Desabilitado, Teclado Desabilitado
            /// </summary>
            Byte160 = 10100000,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Desabilitado, Teclado Habilitado
            /// </summary>
            Byte161 = 10100001,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Só Entrada, Teclado Desabilitado
            /// </summary>
            Byte162 = 10100010,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Só Entrada, Teclado Habilitado
            /// </summary>
            Byte163 = 10100011,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Só Saída, Teclado Desabilitado
            /// </summary>
            Byte164 = 10100100,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Só Saída, Teclado Habilitado
            /// </summary>
            Byte165 = 10100101,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Entrada e Saída, Teclado Desabilitado
            /// </summary>
            Byte166 = 10100110,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Entrada e Saída, Teclado Habilitado
            /// </summary>
            Byte167 = 10100111,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Entrada e Saída Invertido, Teclado Desabilitado
            /// </summary>
            Byte168 = 10101000,

            /// <summary>
            /// Leitor 1 Só Saída, Leitor 2 Entrada e Saída Invertido, Teclado Habilitado
            /// </summary>
            Byte169 = 10101001,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Desabilitado, Teclado Desabilitado 
            /// </summary>
            Byte176 = 10110000,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Desabilitado, Teclado Habilitado
            /// </summary>
            Byte177 = 10110001,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Só Entrada, Teclado Desabilitado
            /// </summary>
            Byte178 = 10110010,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Só Entrada, Teclado Habilitado
            /// </summary>
            Byte179 = 10110011,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Só Saída, Teclado Desabilitado 
            /// </summary>
            Byte180 = 10110100,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Só Saída, Teclado Habilitado
            /// </summary>
            Byte181 = 10110101,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Entrada e Saída, Teclado Desabilitado
            /// </summary>
            Byte182 = 10110110,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Entrada e Saída, Teclado Habilitado
            /// </summary>
            Byte183 = 10110111,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Entrada e Saída Invertido, Teclado Desabilitado
            /// </summary>
            Byte184 = 10111000,

            /// <summary>
            /// Leitor 1 Entrada e Saída, Leitor 2 Entrada e Saída Invertido, Teclado Habilitado
            /// </summary>
            Byte185 = 10111001,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Desabilitado, Teclado Desabilitado 
            /// </summary>
            Byte192 = 11000000,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Desabilitado, Teclado Habilitado
            /// </summary>
            Byte193 = 11000001,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Só Entrada, Teclado Desabilitado
            /// </summary>
            Byte194 = 11000010,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Só Entrada, Teclado Habilitado 
            /// </summary>
            Byte195 = 11000011,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Só Saída, Teclado Desabilitado
            /// </summary>
            Byte196 = 11000100,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Só Saída, Teclado Habilitado
            /// </summary>
            Byte197 = 11000101,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Entrada e Saída, Teclado Desabilitado
            /// </summary>
            Byte198 = 11000110,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Entrada e Saída, Teclado Habilitado
            /// </summary>
            Byte199 = 11000111,
        }

        /// <summary>
        /// Enumerador para formas de entrada com uso da biometria
        /// </summary>
        public enum FormaDeEntradasComBiometria : int
        {
            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Leitores 1 e 2 Desabilitados, Teclado Desabilitado
            /// </summary>
            Byte64 = 1000000,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Leitores 1 e 2 Desabilitados, Teclado Habilitado
            /// </summary>
            Byte65 = 1000001,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Somente Leitor 2 Habilitado, Teclado Desabilitado
            /// </summary>            
            Byte66 = 1000010,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Somente Leitor 2 Habilitado, Teclado Habilitado
            /// </summary>
            Byte67 = 1000011,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Somente Leitor 1 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte68 = 1000100,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Somente Leitor 1 Habilitado, Teclado Habilitado
            /// </summary>
            Byte69 = 1000101,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Leitores 1 e 2 Habilitados, Teclado Desabilitado
            /// </summary>
            Byte70 = 1000110,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Leitores 1 e 2 Habilitados, Teclado Habilitado
            /// </summary>
            Byte71 = 1000111,

            /// <summary>
            /// Leitor 1 Entrada e Saída Invertido, Leitor 2 Entrada e Saída, Teclado Habilitado
            /// </summary>
            Byte72 = 1001000,

            /// <summary>
            /// Identificação Desabilitada, Verificação Desabilitada, Leitor 1 Entrada/Saída invertidas, Teclado Habilitado
            /// </summary>
            Byte73 = 1001001,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Leitores 1 e 2 Desabilitados, Teclado Desabilitado
            /// </summary>
            Byte80 = 1010000,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Leitores 1 e 2 Desabilitados, Teclado Habilitado
            /// </summary>
            Byte81 = 1010001,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Somente Leitor 2 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte82 = 1010010,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Somente Leitor 2 Habilitado, Teclado Habilitado
            /// </summary>
            Byte83 = 1010011,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Somente Leitor 1 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte84 = 1010100,

            /// <summary>
            ///  Identificação Desabilitada, Verificação Habilitada, Somente Leitor 1 Habilitado, Teclado Habilitado
            /// </summary>
            Byte85 = 1010101,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Leitores 1 e 2 Habilitados, Teclado Desabilitado
            /// </summary>
            Byte86 = 1010110,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Leitores 1 e 2 Habilitados, Teclado Habilitado
            /// </summary>
            Byte87 = 1010111,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Leitor 1 Entrada/Saída invertidas, Teclado Desabilitado
            /// </summary>
            Byte88 = 1011000,

            /// <summary>
            /// Identificação Desabilitada, Verificação Habilitada, Leitor 1 Entrada/Saída invertidas, Teclado Habilitado
            /// </summary>
            Byte89 = 1011001,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Leitores 1 e 2 Desabilitados, Teclado Desabilitado
            /// </summary>
            Byte96 = 1100000,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Leitores 1 e 2 Desabilitados, Teclado Habilitado
            /// </summary>
            Byte97 = 1100001,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Somente Leitor 2 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte98 = 1100010,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Somente Leitor 2 Habilitado, Teclado Habilitado
            /// </summary>
            Byte99 = 1100011,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Somente Leitor 1 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte100 = 1100100,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Somente Leitor 1 Habilitado, Teclado Habilitado
            /// </summary>
            Byte101 = 1100101,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Leitores 1 e 2 Habilitados, Teclado Desabilitado
            /// </summary>
            Byte102 = 1100110,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Leitores 1 e 2 Habilitados, Teclado Habilitado
            /// </summary>
            Byte103 = 1100111,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Leitor 1 Entrada/Saída invertidas, Teclado Desabilitado
            /// </summary>
            Byte104 = 1101000,

            /// <summary>
            /// Identificação Habilitada, Verificação Desabilitada, Leitor 1 Entrada/Saída invertidas, Teclado Habilitado
            /// </summary>
            Byte105 = 1101001,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Leitores 1 e 2 Desabilitados, Teclado Desabilitado
            /// </summary>
            Byte112 = 1110000,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Leitores 1 e 2 Desabilitados, Teclado Habilitado
            /// </summary>
            Byte113 = 1110001,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Somente Leitor 2 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte114 = 1110010,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Somente Leitor 2 Habilitado, Teclado Habilitado
            /// </summary>
            Byte115 = 1110011,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Somente Leitor 1 Habilitado, Teclado Desabilitado
            /// </summary>
            Byte116 = 1110100,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Somente Leitor 1 Habilitado, Teclado Habilitado
            /// </summary>
            Byte117 = 1110101,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Leitores 1 e 2 Habilitados, Teclado Desabilitado
            /// </summary>
            Byte118 = 1110110,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Leitores 1 e 2 Habilitados, Teclado Habilitado
            /// </summary>
            Byte119 = 1110111,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Leitor 1 Entrada/Saída invertidas, Teclado Desabilitado
            /// </summary>
            Byte120 = 1111000,

            /// <summary>
            /// Identificação Habilitada, Verificação Habilitada, Leitor 1 Entrada/Saída invertidas, Teclado Habilitado
            /// </summary>
            Byte121 = 1111001,
        }
    }
}
