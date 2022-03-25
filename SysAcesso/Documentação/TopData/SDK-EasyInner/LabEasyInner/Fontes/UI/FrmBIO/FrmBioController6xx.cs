using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyInnerSDK.DAO;
using EasyInnerSDK.Entity;
using System.Windows.Forms;
using System.Threading;

namespace EasyInnerSDK.UI.FrmBIO
{
    class FrmBioController6xx
    {
        #region Propriedades
        private DAOUsuariosBio AcessoUsuariosBio;

        private List<UsuarioBio> ListaUsuariosBio;
        private frmBio6xx frmBiometrico;
        private Inner InnerAtual;
        private NitgenController ntgController;

        #endregion
        
        public FrmBioController6xx(frmBio6xx frmBio6xx, Inner inner)
        {
            InnerAtual = inner;
            frmBiometrico = frmBio6xx;
            AcessoUsuariosBio = new DAOUsuariosBio();
        }

        #region Atualizar form
        private void AtualizarInformacoes(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarInfoConfig(frmBiometrico.AtualizarInfoConfiguracao), new object[] {mensagem});
        }
        private void AtualizarInfoTelaManutencao(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarInfoTelaManut6xx(frmBiometrico.AtualizarInfoManutencao6xx), new object[] { mensagem });
        }
        private void AtualizarMouseCursor(Cursor cursor)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarCursorsMs(frmBiometrico.AtualizarCursorsMouse), new object[] {cursor });
        }
        private void AtualizarInfoTelaCadLeitorInner(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarInfoCad(frmBiometrico.AtualizarInfoCadInner), new object[] { mensagem });
        }
        private void LimparlstCadastroInner()
        {
            frmBiometrico.Invoke(new frmBio6xx.LimparlstCadInner(frmBiometrico.LimparListCadInner), null);
        }
        private void LimparListInfo()
        {
            frmBiometrico.Invoke(new frmBio6xx.LimparlstInfo(frmBiometrico.LimparListInfo), null);
        }
        private void LimparListManutencao()
        {
            frmBiometrico.Invoke(new frmBio6xx.LimparlstManut6xx(frmBiometrico.LimparListManut6xx), null);
        }
        private void AtualizarLabelStatusCap(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarlblStatusCap(frmBiometrico.AtualizarLabelStatusCap), new object[] { mensagem });
        }

        private void AtualizarLabelQualidadeDigital(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarlblQualidadeDigital(frmBiometrico.AtualizarLabelQualidadeDigital), new object[] { mensagem });
        }

        private void AtualizarLabelQualidadeImagem(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarlblQualidadeImagem(frmBiometrico.AtualizarLabelQualidadeImagem), new object[] { mensagem });
        }

        private void ControlarBotoesManutencao(bool Habilitar)
        {
            frmBiometrico.Invoke(new frmBio6xx.ControlarbtnManutencao6xx(frmBiometrico.ControlarBotoesManutencao6xx), new object[] { Habilitar});
        }
        #endregion

        public void ReceberQuantidadeUsuarios()
        {
            int Quantidade = 0;
            int Ret = -1;
            //Mensagem Status
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Recebendo quantidade de usuários cadastrados");
            AtualizarMouseCursor(Cursors.WaitCursor);
            Application.DoEvents();

            if (Conectar())
            {
                Application.DoEvents();
                Ret = EasyInner.RequisitarQuantidadeUsuariosBio(InnerAtual.Numero, InnerAtual.TipoComBio);
                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO;
                    Ret = EasyInner.RespostaQuantidadeUsuariosBio(InnerAtual.Numero, ref Quantidade);

                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Quantidade total de usuários: " + Quantidade);
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Modulo incorreto");
                    }
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
        }

        public void ListarUsuariosBio()
        {
            int Ret = -1;
            int NumPacote = 0;
            int QtdPacotes = 0;
            int Tamanho = 0;
            //Mensagem Status
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Listar usuários bio");
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            Application.DoEvents();

            if (Conectar())
            {
                ListaUsuariosBio = new List<UsuarioBio>();
                NumPacote = 0;
                while(NumPacote <= QtdPacotes){
                    Ret = EasyInner.RequisitarListarUsuariosBio(InnerAtual.Numero, InnerAtual.TipoComBio, NumPacote);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        Ret = EasyInner.RespostaListarUsuariosBio(InnerAtual.Numero, ref QtdPacotes, ref Tamanho);
                        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            byte[] ListaRecebida = new byte[Tamanho];
                            Ret = EasyInner.ReceberListaPacUsuariosBio(InnerAtual.Numero, ListaRecebida, Tamanho);
                            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                            {
                                for(int index = 4; index < ListaRecebida.Length; index+=11)
                                {
                                    byte[] user = new byte[11];
                                    Array.Copy(ListaRecebida, index, user, 0, 11);
                                    string usuario = Utils.BCDToDecimalSub(user);
                                    UsuarioBio usuarioPronto = new UsuarioBio();
                                    usuarioPronto.Usuario = usuario;
                                    ListaUsuariosBio.Add(usuarioPronto);
                                }
                            }
                        }
                    }
                    NumPacote++;
                }
                EasyInner.FecharPortaComunicacao();
                AtualizarMouseCursor(Cursors.Default);
                ControlarBotoesManutencao(true);
                LimparListManutencao();
                frmBiometrico.PreencherGridUsuariosInner6xx(ListaUsuariosBio);
                AtualizarInfoTelaManutencao("Total de usuários " + ListaUsuariosBio.Count);
            }
        }

        public void ExcluirUsuarioBD(List<string> ListaUsuariosExcluir, int TipoComBio)
        {
            //Consulta se o usuário existe
            if (ListaUsuariosExcluir.Count == 0)
            {
                return;
            }
            try
            {
                AtualizarMouseCursor(Cursors.WaitCursor);
                ControlarBotoesManutencao(false);
                for (int index = 0; index < ListaUsuariosExcluir.Count; index++)
                {
                    //Usuário encontrado
                    AtualizarInfoTelaManutencao("Apagar " + ListaUsuariosExcluir[index]);
                    if (AcessoUsuariosBio.ExcluirTemplateBD(ListaUsuariosExcluir[index], TipoComBio) )
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Usuario: " + ListaUsuariosExcluir[index] + " apagado");
                    }
                }
                frmBiometrico.CarregarDigitaisBD6xx();
                AtualizarMouseCursor(Cursors.Default);
                ControlarBotoesManutencao(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ExcluirUsuarioBioInner(List<string> ListaUsuariosExcluir)
        {
            if (ListaUsuariosExcluir.Count == 0)
            {
                return;
            }
            int Ret = -1;
            //Mensagem Status
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Excluir usuários bio");
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            Application.DoEvents();
            if (Conectar())
            {
                int Falha = 0;
                int excluidos = 0;
                for (int index = 0; index < ListaUsuariosExcluir.Count; index++)
                {
                    AtualizarInfoTelaManutencao("Excluir usuário " + ListaUsuariosExcluir[index]);
                    string usuario = Utils.RemZeroEsquerda(ListaUsuariosExcluir[index]);
                    Ret = EasyInner.RequisitarExcluirUsuarioBio(InnerAtual.Numero, InnerAtual.TipoComBio, usuario);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        Ret = EasyInner.RespostaExcluirUsuarioBio(InnerAtual.Numero);
                        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            excluidos++;
                            LimparListManutencao();
                        }
                        else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                        {
                            LimparListManutencao();
                            AtualizarInfoTelaManutencao("Modulo incorreto");
                            break;
                        }
                        else
                        {
                            Falha++;
                        }
                    }
                }
                LimparListManutencao();
                AtualizarInfoTelaManutencao("Foram excluídos " + excluidos);
                AtualizarInfoTelaManutencao("Não foram excluídos " + Falha);
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
            ControlarBotoesManutencao(true);
        }

        public void ExcluirTodosUsuariosInner()
        {
            int Ret = -1;
            //Mensagem Status
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Excluir todos usuários do Inner");
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            Application.DoEvents();
            if (Conectar())
            {
                Ret = EasyInner.RequisitarExcluirTodosUsuariosBio(InnerAtual.Numero, InnerAtual.TipoComBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = EasyInner.RespostaExcluirTodosUsuariosBio(InnerAtual.Numero);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Foram excluídos todos os usuários.");
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Modulo incorreto");
                    }
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
            ControlarBotoesManutencao(true);
        }

        public void EnviarDigitaisInner(List<UsuarioBio> ListaUsuariosEnviar)
        {
            int Ret = -1;
            int iOk = 0;
            int iJaCad = 0;
            int iFalha = 0;
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Enviar digitais para o Inner");
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            if (Conectar())
            {
                byte[] Digital1 = null;
                byte[] Digital2 = null;
                for (int index = 0; index < ListaUsuariosEnviar.Count; index++)
                {
                    if (InnerAtual.TipoComBio == 0)
                    {
                        Digital1 = Utils.HexToByteArray(ListaUsuariosEnviar[index].TemplateA, 404, 808);
                        Digital2 = Utils.HexToByteArray(ListaUsuariosEnviar[index].TemplateB, 404, 808);
                    }
                    else
                    {
                        Digital1 = Utils.HexToByteArray(ListaUsuariosEnviar[index].TemplateA, 502, 1004);
                        Digital2 = Utils.HexToByteArray(ListaUsuariosEnviar[index].TemplateB, 502, 1004);

                    }
                    Ret = EasyInner.EnviarDigitalUsuarioBio(InnerAtual.Numero, InnerAtual.TipoComBio, ListaUsuariosEnviar[index].Usuario,
                                                            Digital1, InnerAtual.DuasDigitais ? Digital2 : null);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        Ret = EasyInner.RespostaEnviarDigitalUsuarioBio(InnerAtual.Numero);
                        if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                        {
                            LimparListManutencao();
                            AtualizarInfoTelaManutencao("Modulo incorreto");
                            break;
                        }
                    }
                    AtualizaContadores(Ret, ref iOk, ref iJaCad, ref iFalha);
                }
            }
            AtualizarMouseCursor(Cursors.Default);
            ControlarBotoesManutencao(true);
            EasyInner.FecharPortaComunicacao();
        }

        public void ReceberModeloBio()
        {
            int Ret = -1;
            byte[] ModeloBio = null;
            LimparListInfo();
            AtualizarInformacoes("Receber modelo bio");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                Ret = EasyInner.RequisitarModeloBio(InnerAtual.Numero, InnerAtual.TipoComBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    ModeloBio = new byte[4];
                    Ret = EasyInner.RespostaModeloBio(InnerAtual.Numero, ModeloBio);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        AtualizarInformacoes("Modelo bio " + Encoding.ASCII.GetString(ModeloBio));
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparListInfo();
                        AtualizarInformacoes("Modulo incorreto");
                    }
                }
            }
            AtualizarMouseCursor(Cursors.Default);
            EasyInner.FecharPortaComunicacao();
        }

        public void ReceberVersaoBio()
        {
            int Ret = -1;
            byte[] VersaoBio = null;
            LimparListInfo();
            AtualizarInformacoes("Receber versão bio");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                Ret = EasyInner.RequisitarVersaoBio(InnerAtual.Numero, InnerAtual.TipoComBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    VersaoBio = new byte[4];
                    Ret = EasyInner.RespostaVersaoBio(InnerAtual.Numero, VersaoBio);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        AtualizarInformacoes("Versão bio " + Encoding.ASCII.GetString(VersaoBio));
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparListInfo();
                        AtualizarInformacoes("Modulo incorreto");
                    }
                }
            }
            AtualizarMouseCursor(Cursors.Default);
            EasyInner.FecharPortaComunicacao();
        }

        public void ReceberConfiguracoesInner()
        {
            int Ret = -1;
            LimparListInfo();
            AtualizarInformacoes("Receber configurações Inner");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                byte[] Configuracoes = new byte[50];
                Ret = EasyInner.ReceberConfiguracoesInner(InnerAtual.Numero, Configuracoes);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    LimparListInfo();
                    AtualizarInformacoes("Configurações recebidas com sucesso");
                }
            }
            AtualizarMouseCursor(Cursors.Default);
            EasyInner.FecharPortaComunicacao();
        }

        public void ReceberDigitaisBio(List<string> ListaUsuariosReceber)
        {
            int Ret = -1;
            int TamanhoReceber = 0;
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Receber digitais bio");
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            if (Conectar())
            {
                ListaUsuariosBio = new List<UsuarioBio>();
                for (int index = 0; index < ListaUsuariosReceber.Count; index++)
                {
                    if (AcessoUsuariosBio.ExisteUsuarioBio(ListaUsuariosReceber[index], InnerAtual.TipoComBio) == false)
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Receber usuário " + ListaUsuariosReceber[index]);
                        Ret = EasyInner.RequisitarUsuarioCadastradoBio(InnerAtual.Numero, InnerAtual.TipoComBio, ListaUsuariosReceber[index]);
                        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            Ret = EasyInner.RespostaUsuarioCadastradoBio(InnerAtual.Numero, ref TamanhoReceber);
                            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                            {
                                byte[] Buffertemplate = new byte[TamanhoReceber];
                                Ret = EasyInner.ReceberDigitalUsuarioBio(InnerAtual.Numero, Buffertemplate, TamanhoReceber);
                                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                                {
                                    UsuarioBio usuarioBio = new UsuarioBio();
                                    usuarioBio.Usuario = Utils.RemZeroEsquerda(ListaUsuariosReceber[index]);

                                    usuarioBio.TemplateA = Utils.ByteToHex(Buffertemplate, 68,
                                                                                    InnerAtual.TipoComBio == 0 ? 404 : 502);
                                    usuarioBio.TemplateB = TamanhoReceber == 472 || TamanhoReceber == 570 ? usuarioBio.TemplateA :
                                                                    Utils.ByteToHex(Buffertemplate, InnerAtual.TipoComBio == 0 ? 472 : 570,
                                                                InnerAtual.TipoComBio == 0 ? 404 : 502);
                                    ListaUsuariosBio.Add(usuarioBio);
                                }
                            }
                            else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                            {
                                LimparlstCadastroInner();
                                AtualizarInfoTelaCadLeitorInner("Modulo incorreto");
                                break;
                            }
                        }
                    }
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarInfoTelaManutencao("Gravando usuários recebidos na base.");
            GravarTemplatesRecebidos(ListaUsuariosBio);
            AtualizarMouseCursor(Cursors.Default);
            AtualizarInfoTelaManutencao("Total de usuários recebidos " + ListaUsuariosBio.Count);
            ControlarBotoesManutencao(true);
        }

        public void EnviarAjustesBiometricos()
        {
            int Ret = -1;
            LimparListInfo();
            AtualizarInformacoes("Enviar ajustes biométricos");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                byte [] AjustesBio = getAjustesBiometricos(InnerAtual.TipoComBio);
                Ret = EasyInner.RequisitarEnviarAjustesBio(InnerAtual.Numero, InnerAtual.TipoComBio, AjustesBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = EasyInner.RespostaEnviarAjustesBio(InnerAtual.Numero);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        LimparListInfo();
                        AtualizarInformacoes("Ajustes biométricos enviados");
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparListInfo();
                        AtualizarInformacoes("Modulo incorreto");
                    }
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
        }

        public void VerificarDigital(string Usuario)
        {
            int Ret = -1;
            LimparlstCadastroInner();
            AtualizarInfoTelaCadLeitorInner("Conectando ao Inner...");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                AtualizarInfoTelaCadLeitorInner("Verificar digital");
                Ret = EasyInner.RequisitarVerificarDigitalBio(InnerAtual.Numero, InnerAtual.TipoComBio, Usuario);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = EasyInner.RespostaVerificarDigitalBio(InnerAtual.Numero);
                    LimparlstCadastroInner();
                    AtualizarInfoTelaCadLeitorInner(Ret != 8 ? "Resposta bio " + ((Enumeradores.RetornoBIO)Ret).ToString() : "Erro");
                    AtualizarInfoTelaCadLeitorInner("Usuário: " + Usuario + " verificado");
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
        }

        public void IdentificarDigital()
        {
            int Ret = -1;
            LimparlstCadastroInner();
            AtualizarInfoTelaCadLeitorInner("Conectando ao Inner...");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                AtualizarInfoTelaCadLeitorInner("Indetificando usuário...");
                Ret = EasyInner.RequisitarIdentificarUsuarioLeitorBio(InnerAtual.Numero, InnerAtual.TipoComBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    byte[] Usuario = new byte[16];
                    Ret = EasyInner.RespostaIdentificarUsuarioLeitorBio(InnerAtual.Numero, Usuario);
                    AtualizarInfoTelaCadLeitorInner(Ret != 8 ? "Resposta bio " + ((Enumeradores.RetornoBIO)Ret).ToString() : "Erro");
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        AtualizarInfoTelaCadLeitorInner("Usuário: " + Utils.BCDToDecimalSub(Usuario) + " identificado");
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparlstCadastroInner();
                        AtualizarInfoTelaCadLeitorInner("Modulo incorreto");
                    }
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
        }

        public void ReceberTemplateLeitor(string UsuarioCad)
        {
            int Ret = -1;
            int TamanhoReceber = 0;
            LimparlstCadastroInner();
            AtualizarInfoTelaCadLeitorInner("Conectando ao Inner...");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                AtualizarInfoTelaCadLeitorInner("Receber template leitor");
                Ret = EasyInner.RequisitarReceberTemplateLeitorInnerBio(InnerAtual.Numero, InnerAtual.TipoComBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = EasyInner.RespostaReceberTemplateLeitorInnerBio(InnerAtual.Numero, ref TamanhoReceber);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        byte[] Buffertemplate = new byte[TamanhoReceber];
                        Ret = EasyInner.ReceberTemplateLeitorInnerBio(InnerAtual.Numero, Buffertemplate, TamanhoReceber);
                        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            if (MessageBox.Show("Template recebido referente ao usuário " + UsuarioCad + "\nDeseja gravar este usuário na base de dados?", "Receber Template leitor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                AtualizarInfoTelaCadLeitorInner("Gravando o template na base de dados, usuário " + UsuarioCad);
                                UsuarioBio user = new UsuarioBio();
                                user.Usuario = UsuarioCad;
                                user.TemplateA = Utils.ByteToHex(Buffertemplate, 0, Buffertemplate.Length);
                                user.TemplateB = Utils.ByteToHex(Buffertemplate, 0, Buffertemplate.Length); // coloca o mesmo template nos dois campos
                                GravarTemplatesRecebidos(new List<UsuarioBio>() { user });
                                AtualizarInfoTelaCadLeitorInner("Template gravado.");
                            }
                        }
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparlstCadastroInner();
                        AtualizarInfoTelaCadLeitorInner("Modulo incorreto");
                    }
                }
                EasyInner.FecharPortaComunicacao();
                AtualizarMouseCursor(Cursors.Default);
            }
        }
        public void VerificarCadastroBio(string usuario)
        {
            int Ret = -1;
            LimparlstCadastroInner();
            AtualizarInfoTelaCadLeitorInner("Conectando ao Inner...");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                AtualizarInfoTelaCadLeitorInner("Verificar cadastro usuário");
                Ret = EasyInner.RequisitarVerificarCadastroUsuarioBio(InnerAtual.Numero, InnerAtual.TipoComBio, usuario);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = EasyInner.RespostaVerificarCadastroUsuarioBio(InnerAtual.Numero);
                    AtualizarInfoTelaCadLeitorInner(Ret != 8 ? "Resposta bio " + ((Enumeradores.RetornoBIO)Ret).ToString() : "Erro");
                }
                EasyInner.FecharPortaComunicacao();
                AtualizarMouseCursor(Cursors.Default);
            }
        }
        public void ConfigurarIdentificacaoVerificacao()
        {
            int Ret = -1;
            LimparListInfo();
            AtualizarInformacoes("Conectando ao Inner...");
            AtualizarMouseCursor(Cursors.WaitCursor);
            if (Conectar())
            {
                AtualizarInformacoes("Configurar identificação e verificação");
                Ret = EasyInner.RequisitarHabilitarIdentificacaoVerificacao(InnerAtual.Numero, InnerAtual.TipoComBio, InnerAtual.Identificacao, InnerAtual.Verificacao);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = EasyInner.RespostaHabilitarIdentificacaoVerificacao(InnerAtual.Numero);
                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        LimparListInfo();
                        AtualizarInformacoes("Identificação e verificação configurados");
                    }
                    else if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                    {
                        LimparListInfo();
                        AtualizarInformacoes("Modulo incorreto");
                    }
                }
                EasyInner.FecharPortaComunicacao();
                AtualizarMouseCursor(Cursors.Default);
            }
        }

        private byte[] getAjustesBiometricos(int TipoModBio)
        {
            byte[] Ajustes = new byte[16];
            if (TipoModBio == 0)
            {
                Ajustes[0] = 2; //ganho
                Ajustes[1] = 40; //brilho
                Ajustes[2] = 20; //contraste
                Ajustes[3] = 8; //seg indet.
                Ajustes[4] = 5; //seg verif.
                Ajustes[5] = 40; //qualidade reg
                Ajustes[6] = 30; //qualidade verif
                Ajustes[7] = 0; //filtro dig latente
                Ajustes[8] = 0; //captura adapt
                Ajustes[9] = 5; //total capturas
                Ajustes[10] = 50; //tempo cap
                Ajustes[11] = (byte)InnerAtual.TimeOutAjustes; //timeout ident
                Ajustes[12] = (byte)InnerAtual.NivelLFD; //nivel lfd
            }
            else
            {
                Ajustes[0] = (byte)InnerAtual.NivelLFD; //seg ident
                Ajustes[1] = (byte)InnerAtual.TimeOutAjustes; //timeout ident
                Ajustes[2] = InnerAtual.DedoDuplicado == true ? (byte)1 : (byte)0;
            }
            return Ajustes;
        }

        private void GravarTemplatesRecebidos(List<UsuarioBio> ListaUsuariosBioRecebidos)
        {
            for (int index = 0; index < ListaUsuariosBioRecebidos.Count; index++)
            {
                AcessoUsuariosBio.InserirTemplateBD(ListaUsuariosBioRecebidos[index], InnerAtual.TipoComBio);
            }
        }

        /// <summary>
        /// Metodo responsável por realizar um comando simples com o equipamento para detectar
        /// se esta conectado.
        /// </summary>
        /// <param name="UiBIO"></param>
        /// <returns></returns>
        private int testaConexaoInner()
        {
            int RetRelogio = -1;
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;

            RetRelogio = EasyInner.ReceberRelogio(InnerAtual.Numero, ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, ref Segundo);
            return RetRelogio;
        }
        /// <summary>
        /// Rotina responsável por efetuar a conexão com o Inner
        /// </summary>
        /// <param name="UiMainBIO"></param>
        /// <returns></returns>
        /// 
        private bool Conectar()
        {
            int Fim;
            int Ret = -1;
            Application.DoEvents();
            //Define o tipo de conexão selecionada no Combo..
            EasyInner.DefinirTipoConexao((byte)InnerAtual.TipoConexao);

            //Fecha as conexões caso esteja aberta..
            EasyInner.FecharPortaComunicacao();

            //Abre a porta de Conexão conforme a Porta Indicada..
            Ret = EasyInner.AbrirPortaComunicacao(InnerAtual.Porta);

            System.DateTime Data;
            Data = System.DateTime.Now;

            //Tenta Realizar a Conexão
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Registra o tempo fim de conexão (tempo atual +15)
                Fim = (int)EasyInner.RetornarSegundosSys() + 15;

                //Realiza loop enquanto o tempo fim for menor que o tempo atual, e o comando retornado diferente de OK.
                do
                {
                    Ret = testaConexaoInner();
                    Thread.Sleep(100);
                }
                while ((EasyInner.RetornarSegundosSys() <= Fim) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));

                //Caso o retorno seja OK.. volta a função chamadora..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    return true;
                }
                else
                {
                    //Exibe mensagem de erro para o Usuário..
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Erro ao conectar com o Inner!");
                    LimparListManutencao();
                    Application.DoEvents();
                    return false;
                }
            }
            else
            {
                LimparListManutencao();
                AtualizarInfoTelaManutencao("Não conectou ao Inner!");
                Application.DoEvents();
                return false;
            }
        }

        private int AtualizaContadores(int Retorno, ref int iOk, ref int iJaCadast, ref int iFalha)
        {
            switch (Retorno)
            {
                case (int)Enumeradores.Retorno.RET_COMANDO_OK:
                    iOk++;
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO:
                    iJaCadast++;
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_BASE_CHEIA:
                    LimparListManutencao();
                    MessageBox.Show("Base bio cheia.");
                    AtualizarInfoTelaManutencao("ENVIADOS: " + iOk);
                    AtualizarInfoTelaManutencao("JÁ CADASTRADOS: " + iJaCadast);
                    AtualizarInfoTelaManutencao("FALHA ENVIO: " + iFalha);
                    break;
                default:
                    iFalha++;
                    break;
            }

            LimparListManutencao();
            AtualizarInfoTelaManutencao("ENVIADOS: " + iOk);
            AtualizarInfoTelaManutencao("\nJÁ CADASTRADOS: " + iJaCadast);
            AtualizarInfoTelaManutencao("\nFALHA ENVIO: " + iFalha);

            return Retorno;
        }

        public string CapturarHasmter()
        {
            string template = ""; 
            try
            {
                AtualizarLabelQualidadeImagem("0");
                AtualizarLabelQualidadeDigital("0");
                ntgController = new NitgenController(null, frmBiometrico);
                object Fir = ntgController.GetTemplateHamster(frmBiometrico.chkImagem.Checked, frmBiometrico.tcbVerify.Value, frmBiometrico.pcbImagemDigital);
                if (ntgController.GetQualidadeDigital(Fir) >= frmBiometrico.tcbQualidadeDigital.Value)
                {
                    AtualizarLabelQualidadeDigital(ntgController.GetQualidadeDigital(Fir).ToString());
                    template = ntgController.ExportarTemplate(Fir);
                }
                else
                {
                    AtualizarLabelStatusCap("Qualidade digital baixa!");
                    AtualizarLabelQualidadeDigital(ntgController.GetQualidadeDigital(Fir).ToString());
                    return "";
                }
                return template;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                return "";
            }
        }
    }
}
