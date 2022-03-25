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

        public int Credito(StringBuilder pValueTransaction, StringBuilder pNumberCupon,
        StringBuilder pNumberControl)
        {
            try
            {
                return gCredito.Invoke(pValueTransaction, pNumberCupon, pNumberControl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EfetuarPagamentoDebito(decimal valorTransacao)
        {
            try
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

                int iNumeroControle = 0;

                if (iRes == 0)
                {
                    ///iNumeroControle = Conversor.ToIntDef(pNumeroControle.ToString(), 0);
                }

                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtemLog(ref string sLogUltimaTransacao, ref string sLogUltimaTransacaoEstendido)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
