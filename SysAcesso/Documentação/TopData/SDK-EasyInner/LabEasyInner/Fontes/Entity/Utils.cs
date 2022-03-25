using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyInnerSDK.Entity
{
    class Utils
    {
        public static string ByteToHex(byte[] Template, int indexBuff, int LenghtBuff)
        {
            StringBuilder TplText = new StringBuilder();
            for (int index = indexBuff; index < indexBuff + LenghtBuff; index++)
            {
                TplText.Append(Template[index].ToString("x").PadLeft(2, '0'));
            }
            return TplText.ToString();
        }

        public static string RemZeroEsquerda(String valor)
        {
            bool blnNum = false;
            String str = "";
            if (valor != null)
            {
                for (int i = 0; i < valor.Length; i++)
                {
                    if (!"0".Equals(valor.Substring(i, 1)))
                    {
                        blnNum = true;
                    }
                    if (blnNum)
                    {
                        str += valor.Substring(i, 1);
                    }
                }
            }
            return str;
        }

        public static string CompletarUsuario(string usuario)
        {
            int Count = 16 - usuario.Length;
            StringBuilder UsuarioCompleto = new StringBuilder();
            while (Count > 0)
            {
                UsuarioCompleto.Append("0");
                Count--;
            }
            UsuarioCompleto.Append(usuario);
            return UsuarioCompleto.ToString();
        }

        /// <summary>
        /// Verifica se a string é feita somente de números
        /// </summary>
        /// <param name="texto">string a ser verificada</param>
        /// <returns>Verdadeiro se contem somente números na string</returns>
        public static bool SomenteNumeros(string texto)
        {
            if (texto == null || texto.Equals(""))
            {
                throw new Exception("Texto inválido!");
            }

            for (int i = 0; i < texto.Length; i++)
            {
                if ((char.IsDigit(texto[i]) == false) && (texto[i] != ' '))
                {
                    return false;
                }
            }
            return true;
        }

        public static string BCDToDecimalSub(byte[] valorBCD)
        {
            StringBuilder Retorno = new StringBuilder();
            for (int index = 0; index < valorBCD.Length; index++)
            {
                if (valorBCD[index] != 0)
                {
                    byte bt = valorBCD[index];
                    bt = Convert.ToByte(int.Parse(bt.ToString()) - 1);
                    Retorno.Append(bt.ToString("x").PadLeft(2, '0'));
                }
            }
            return Retorno.ToString();
        }

        public static byte[] HexToByteArray(string ValorHex, int TamanhoArray, int Total)
        {
            byte[] ValorByte = new byte[TamanhoArray];
            int ibyte = 0;
            for (int index = 0; index < Total - 1; index += 2)
            {
                ValorByte[ibyte] = Convert.ToByte(Convert.ToUInt32(ValorHex.Substring(index, 2), 16));
                ibyte++;
            }
            return ValorByte;
        }

        public static string ByteArrayToString(byte[] buff, int indexBuff, int LenghtBuff, string CaracterEntreBytes)
        {
            try
            {
                if (indexBuff > buff.Length)
                {
                    return "";
                }
                StringBuilder BuffString = new StringBuilder();
                for (int iBuff = indexBuff; iBuff < indexBuff + LenghtBuff; iBuff++)
                {
                    BuffString.Append(iBuff == indexBuff ? buff[iBuff].ToString() : CaracterEntreBytes + buff[iBuff].ToString());
                }
                return BuffString.ToString();
            }
            catch (IndexOutOfRangeException)
            {
                return "";
            }
        }
        /// <summary>
        /// Retorna os numeros de uma string
        /// 32 em ascii = espaço
        /// 58 em ascii = :
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>string com os números</returns>
        public static string ReturnNumeros(string texto)
        {
            StringBuilder nums = new StringBuilder();
            foreach (char ch in texto)
            {
                if (char.IsDigit(ch))
                {
                    nums.Append(ch);
                }
            }
            return nums.ToString();
        }
    }
}
