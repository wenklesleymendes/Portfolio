using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TEF.Core.Library
{
    public class IntegracaoDTEF
    {
        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string pFilename);
        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr pDll, string pFunction);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]

        public delegate int TransacaoCartaoCredito(StringBuilder ValueTransaction, StringBuilder
        NumberCupon, StringBuilder NumberControl);
        private static IntPtr gDllHandle;
        private static TransacaoCartaoCredito gCredito;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _TransacaoCartaoDebito(StringBuilder valorTransacao, StringBuilder numeroCupom, StringBuilder numeroControle);
        private _TransacaoCartaoDebito dllTransacaoCartaoDebito;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void _ObtemLogUltimaTransacao(byte[] logUltimaTransacao);
        private _ObtemLogUltimaTransacao dllObtemLogUltimaTransacao;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _TransacaoConsultaParcelas(StringBuilder numeroControle);
        private _TransacaoConsultaParcelas dllTransacaoConsultaParcelas;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _ConsultaTransacao(StringBuilder numeroEmpresa, StringBuilder numeroLoja, StringBuilder numeroPDV, StringBuilder solicitacao, byte[] resposta, byte[] mensagemErro);
        private _ConsultaTransacao dllConsultaTransacao;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _FinalizaTransacao();
        private _FinalizaTransacao dllFinalizaTransacao;

        #region Confirmar transações cartão
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _ConfirmaCartaoCredito(StringBuilder numeroControle);
        private _ConfirmaCartaoCredito dllConfirmaCartaoCredito;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _ConfirmaCartaoDebito(StringBuilder numeroControle);
        private _ConfirmaCartaoDebito dllConfirmaCartaoDebito;
        #endregion

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void _ObtemComprovanteTransacao(StringBuilder numeroControle, byte[] comprovante, byte[] comprovanteReduzido);
        private _ObtemComprovanteTransacao dllObtemComprovanteTransacao;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _TrataDesfazimento(int tratar);
        private _TrataDesfazimento dllTrataDesfazimento;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _DadosUltimaTransacaoNaoAprovada(byte[] dadosTransacaoNaoAprovada);
        private _DadosUltimaTransacaoNaoAprovada dllDadosUltimaTransacaoNaoAprovada;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int _DesfazCartao(StringBuilder numeroControle);
        private _DesfazCartao dllDesfazCartao;

        [DllImport("kernel32")]
        public static extern IntPtr FreeLibrary(IntPtr pDll);

        private IntPtr dllHandle;
        bool bManterDLLCarregada;
        String sNomeDLL;

        public int Credito(StringBuilder pValueTransaction, StringBuilder pNumberCupon,
        StringBuilder pNumberControl)
        {
            return gCredito.Invoke(pValueTransaction, pNumberCupon, pNumberControl);
        }

        public int Debito(StringBuilder pValueTransaction, StringBuilder pNumberCupon,
        StringBuilder pNumberControl)
        {
            try
            {
                return 1; // return gDebito.Invoke(pValueTransaction, pNumberCupon, pNumberControl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IntegracaoDTEF()
        {
            Load();
        }

        public static object GetDelegateFromDLL(string pFunction, Type pType)
        {
            IntPtr lTemp = GetProcAddress(gDllHandle, pFunction);
            object gTemp = Marshal.GetDelegateForFunctionPointer(lTemp, pType);
            return gTemp;
        }
        public static void Load()
        {
            gDllHandle = LoadLibrary("DPOSDRV.dll");
            gCredito = (TransacaoCartaoCredito)GetDelegateFromDLL("TransacaoCartaoCredito",
            typeof(TransacaoCartaoCredito));
        }

        public int EfetuarPagamentoCredito(decimal valorTransacao)
        {
            StringBuilder controle;
            StringBuilder valor;
            StringBuilder cupom;
            Load();
            StringBuilder sb = new StringBuilder();

            valor = new StringBuilder(valorTransacao.ToString().Replace(",", "").Replace(".", ""));
            //controle = new StringBuilder("123456");
            controle = new StringBuilder("");
            cupom = new StringBuilder("000000");

            int iRes = Credito(valor, cupom, controle);

            return iRes;
        }

        public int EfetuarPagamentoDebito(decimal valorTransacao)
        {
            StringBuilder controle;
            StringBuilder valor;
            StringBuilder cupom;
            Load();
            StringBuilder sb = new StringBuilder();


            valor = new StringBuilder(valorTransacao.ToString().Replace(",", "").Replace(".", ""));

            controle = new StringBuilder("");
            cupom = new StringBuilder("");

            dllTransacaoCartaoDebito = (_TransacaoCartaoDebito)GetDelegateFromDLL("TransacaoCartaoDebito", typeof(_TransacaoCartaoDebito));

            int iRes = dllTransacaoCartaoDebito(valor, cupom, controle);

            return iRes;
        }

        public void ObtemLog(ref string sLogUltimaTransacao, ref string sLogUltimaTransacaoEstendido)
        {
            sLogUltimaTransacaoEstendido = "LOGESTENDIDO";

            byte[] pLogUltimaTransacao = Encoding.ASCII.GetBytes(sLogUltimaTransacao.PadRight(256).ToCharArray());
            byte[] pLogUltimaTransacaoEstendido = Encoding.ASCII.GetBytes(sLogUltimaTransacaoEstendido.PadRight(256).ToCharArray());

            dllObtemLogUltimaTransacao = (_ObtemLogUltimaTransacao)GetDelegateFromDLL("ObtemLogUltimaTransacao", typeof(_ObtemLogUltimaTransacao));

            dllObtemLogUltimaTransacao(pLogUltimaTransacao);
            dllObtemLogUltimaTransacao(pLogUltimaTransacaoEstendido);

            sLogUltimaTransacao = Encoding.ASCII.GetString(pLogUltimaTransacao);
            sLogUltimaTransacaoEstendido = Encoding.ASCII.GetString(pLogUltimaTransacaoEstendido);
        }

        public int ConsultarParcelas(string iNumeroControle)
        {
            dllTransacaoConsultaParcelas = (_TransacaoConsultaParcelas)GetDelegateFromDLL("TransacaoConsultaParcelas", typeof(_TransacaoConsultaParcelas));
            StringBuilder pNumeroControle = new StringBuilder(iNumeroControle);
            int iRes = dllTransacaoConsultaParcelas(pNumeroControle);

            return iRes;
        }

        public int FinalizaTransacao()
        {
            //if (!CarregaDLL())
            //    return 11;

            dllFinalizaTransacao = (_FinalizaTransacao)GetDelegateFromDLL("FinalizaTransacao", typeof(_FinalizaTransacao));

            int iRes = dllFinalizaTransacao();

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return iRes;

        }

        public bool CarregaDLL()
        {
            if (dllHandle == (IntPtr)0)
                dllHandle = LoadLibrary(sNomeDLL);

            if (dllHandle == (IntPtr)0)
                return false;

            return true;
        }

        public bool DescarregaDLL()
        {
            FreeLibrary(dllHandle);
            dllHandle = (IntPtr)0;
            return true;
        }


        public int ConfirmaCartaoCredito(int iNumeroControle)
        {
            //if (!CarregaDLL())
            //    return 11;

            StringBuilder pNumeroControle = new StringBuilder(String.Format("{0:d6}", iNumeroControle));

            dllConfirmaCartaoCredito = (_ConfirmaCartaoCredito)GetDelegateFromDLL("ConfirmaCartaoCredito", typeof(_ConfirmaCartaoCredito));
            int iRes = dllConfirmaCartaoCredito(pNumeroControle);

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return iRes;
        }

        public int ConfirmaCartaoDebito(int iNumeroControle)
        {
            //if (!CarregaDLL())
            //    return 11;

            StringBuilder pNumeroControle = new StringBuilder(String.Format("{0:d6}", iNumeroControle));

            dllConfirmaCartaoDebito = (_ConfirmaCartaoDebito)GetDelegateFromDLL("ConfirmaCartaoDebito", typeof(_ConfirmaCartaoDebito));
            int iRes = dllConfirmaCartaoDebito(pNumeroControle);

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return iRes;
        }

        public string ObtemComprovanteTransacao(int iNumeroControle)
        {
            string sComprovante = "";
            string sComprovanteReduzido = "";

            byte[] pComprovante = new byte[2048];
            byte[] pComprovanteReduzido = new byte[320];
            StringBuilder pNumeroControle = new StringBuilder(String.Format("{0:d6}", iNumeroControle));

            dllObtemComprovanteTransacao = (_ObtemComprovanteTransacao)GetDelegateFromDLL("ObtemComprovanteTransacao", typeof(_ObtemComprovanteTransacao));

            dllObtemComprovanteTransacao(pNumeroControle, pComprovante, pComprovanteReduzido);

            sComprovante = ASCIIEncoding.ASCII.GetString(pComprovante);
            sComprovanteReduzido = ASCIIEncoding.ASCII.GetString(pComprovanteReduzido);

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return sComprovante;
        }

        public int TrataDesfazimento(int iTratar)
        {
            dllTrataDesfazimento = (_TrataDesfazimento)GetDelegateFromDLL("TrataDesfazimento", typeof(_TrataDesfazimento));

            int iRes = dllTrataDesfazimento(iTratar);

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return iRes;
        }

        public int DesfazCartao(int iNumeroControle)
        {
            StringBuilder pNumeroControle = new StringBuilder(String.Format("{0:d6}", iNumeroControle));

            dllDesfazCartao = (_DesfazCartao)GetDelegateFromDLL("DesfazCartao", typeof(_DesfazCartao));

            int iRes = dllDesfazCartao(pNumeroControle);

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return iRes;
        }

        public int ConsultaTransacao(int iNumeroEmpresa, int iNumeroLoja, int iNumeroPDV, string sSolicitacao, out string sResposta, out string sMensagemErro)
        {
            sResposta = "";
            sMensagemErro = "";

            StringBuilder pNumeroEmpresa = new StringBuilder(String.Format("{0:d4}", iNumeroEmpresa));
            StringBuilder pNumeroLoja = new StringBuilder(String.Format("{0:d4}", iNumeroLoja));
            StringBuilder pNumeroPDV = new StringBuilder(String.Format("{0:d3}", iNumeroPDV));
            StringBuilder pSolicitacao = new StringBuilder(String.Format("{0:-102}", sSolicitacao));
            byte[] pResposta = new byte[102];
            byte[] pMensagemErro = new byte[64];

            dllConsultaTransacao = (_ConsultaTransacao)GetDelegateFromDLL("ConsultaTransacao", typeof(_ConsultaTransacao));

            int iRes = dllConsultaTransacao(pNumeroEmpresa, pNumeroLoja, pNumeroPDV, pSolicitacao, pResposta, pMensagemErro);

            if (iRes == 0)
            {
                sResposta = ASCIIEncoding.ASCII.GetString(pResposta);
                sMensagemErro = ASCIIEncoding.ASCII.GetString(pMensagemErro);
            }

            if (!bManterDLLCarregada)
                DescarregaDLL();

            return iRes;
        }
    }
}