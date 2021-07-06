using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Acesso.Core.Utilidades
{
    public static class CriptografiaHelper
    {
        public static byte[] Criptografa(this string str)
        {
            var chave = Encoding.ASCII.GetBytes("K—7æ\u0012ý\u008f#&ÉÕ¿1\u007f™\u001bù«u\f~ÙmðW\u001f-¿…\u000eÜ\u0001");
            var vetorInicial = Encoding.ASCII.GetBytes("\0žZîØ\u0001Ú-Ýø\u001fàvyœ£");

            if (str == null || str.Length <= 0)
                throw new ArgumentNullException("string");
            if (chave == null || chave.Length <= 0)
                throw new ArgumentNullException("key");
            if (vetorInicial == null || vetorInicial.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] retorno;

            using (Aes algoritmo = Aes.Create())
            {
                algoritmo.Key = chave;
                algoritmo.IV = vetorInicial;

                ICryptoTransform criptografador = algoritmo.CreateEncryptor(algoritmo.Key, algoritmo.IV);

                using (MemoryStream msCriptogradado = new MemoryStream())
                {
                    using (CryptoStream csCriptografado = new CryptoStream(msCriptogradado, criptografador, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swCriptografado = new StreamWriter(csCriptografado))
                        {
                            swCriptografado.Write(str);
                        }
                        retorno = msCriptogradado.ToArray();
                    }
                }
            }
            return retorno;
        }

        public static string Decriptografa(this byte[] cipher)
        {
            var chave = Encoding.ASCII.GetBytes("K—7æ\u0012ý\u008f#&ÉÕ¿1\u007f™\u001bù«u\f~ÙmðW\u001f-¿…\u000eÜ\u0001");
            var vetorInicial = Encoding.ASCII.GetBytes("\0žZîØ\u0001Ú-Ýø\u001fàvyœ£");

            if (cipher == null || cipher.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (chave == null || chave.Length <= 0)
                throw new ArgumentNullException("Key");
            if (vetorInicial == null || vetorInicial.Length <= 0)
                throw new ArgumentNullException("IV");

            string retorno = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = chave;
                aesAlg.IV = vetorInicial;

                ICryptoTransform decriptografador = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecriptografado = new MemoryStream(cipher))
                {
                    using (CryptoStream csDecriptografado = new CryptoStream(msDecriptografado, decriptografador, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecriptografado = new StreamReader(csDecriptografado))
                        {
                            retorno = srDecriptografado.ReadToEnd();
                        }
                    }
                }
            }
            return retorno;
        }

        public static string CriptografeMD5(this string textoNormal)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var sb = new StringBuilder(32);
                foreach (var var in md5.ComputeHash(Encoding.UTF8.GetBytes(textoNormal)))
                    sb.Append(var.ToString("x2"));
                return sb.ToString().ToUpper();
            }
        }
    }
}
