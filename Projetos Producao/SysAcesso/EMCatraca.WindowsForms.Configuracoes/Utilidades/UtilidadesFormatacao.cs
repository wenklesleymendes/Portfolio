using EMCatraca.WindowsForms.Configuracoes.ObjetosValorados;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EMCatraca.WindowsForms.Configuracoes.Utilidades
{
    public static class UtilidadesFormatacao
    {
        public const string MatchEmailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-\w])*)(?<=[0-9a-z]|_)@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static Encoding EncodingPadrao = Encoding.GetEncoding(1252);
        public static Encoding EncodingISO_8859 = Encoding.GetEncoding("ISO-8859-1");

        private static readonly Regex _regEx = new Regex(MatchEmailPattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly CultureInfo _culturaPtBr = new CultureInfo("pt-BR");


        public static CultureInfo ObtenhaCulturaPtBr()
        {
            return _culturaPtBr;
        }

        public static string FormateDataCurta(DateTime data)
        {
            return data.ToString("dd/MM/yyyy");
        }

        public static string FormateMoeda(Flutuante valor)
        {
            return valor == 0 ? string.Empty : valor.ToStringCurrency();
        }

        public static bool EhTelefoneCelular(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                return false;
            }
            telefone = SomenteNumeros(telefone);
            if (telefone.Length == 10 || telefone.Length == 11)
            {
                return telefone[2] == '9';
            }
            return false;
        }

        public static string FormateTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                return string.Empty;
            }

            telefone = SomenteNumeros(telefone);
            if (telefone.Length == 11)
            {
                return telefone.Insert(7, "-").Insert(2, ")").Insert(0, "(");
            }

            if (telefone.Length == 10)
            {
                return telefone.Insert(6, "-").Insert(2, ")").Insert(0, "(");
            }

            if (telefone.Length == 8)
            {
                return telefone.Insert(4, "-");
            }

            return telefone;
        }

        public static string ProperCase(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput))
            {
                return string.Empty;
            }

            string[] prep = { "DA", "DAS", "DE", "DO", "DOS", "E" };
            var romanNumerals = @"^((?=[MDCLXVI])((I?V?)(M*)((C[DM])|(D?C*))?((X[LC])|(L?X*))?(I[XVC]|V?I*)?))(?=[MDCLXVI,.\)]).?$";

            var sb = new StringBuilder();
            var tokens = stringInput.ToUpperInvariant().Split(' ');
            foreach (var token in tokens)
            {
                var matchedToken = Regex.Match(token, romanNumerals, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                if (prep.Contains(token))
                {
                    sb.Append(token.ToLower());
                }
                else if (string.Equals(token, matchedToken.Value))
                {
                    sb.Append(matchedToken.ToString().ToUpperInvariant());
                }
                else
                {
                    sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(token.ToLowerInvariant()));
                }

                sb.Append(" ");
            }
            return sb.ToString().TrimEnd();
        }

        public static string UnicodeToAnsi(string text)
        {
            var ansi = EncodingPadrao.GetBytes(GetStringNoAccents(text));
            return Encoding.ASCII.GetString(ansi);
        }

        public static string GetStringNoAccents(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var acentos = "çÇáéíóúýÁÉÍÓÚÝàèìòùÀÈÌÒÙãõñäëïöüÿÄËÏÖÜÃÕÑâêîôûÂÊÎÔÛºª°ø§";
            var semAcento = "cCaeiouyAEIOUYaeiouAEIOUaonaeiouyAEIOUAONaeiouAEIOU     ";

            for (var i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }

            return str;
        }

        public static string SomenteNumeros(string str)
        {
            return new string((from c in str ?? string.Empty where char.IsDigit(c) select c).ToArray());
        }

        public static string SomenteNumerosOuLetras(string str)
        {
            return new string((from c in str ?? string.Empty where char.IsLetterOrDigit(c) select c).ToArray());
        }

        public static string SomenteNumerosOuLetrasOuEspacos(string str)
        {
            return new string((from c in str ?? string.Empty where EhCaracterAceito(c) select c).ToArray());
        }

        public static string SomenteNumerosOuLetrasOuEspacosOuVirgulas(string str, bool preservarVirgula)
        {
            return new string((from c in str ?? string.Empty where EhCaracterAceito(c, preservarVirgula) select c).ToArray());
        }

        private static string SomenteNumerosOuLetrasOuEspacosOuVirgulasOuCaractereEspecialNotaFiscal(string texto)
        {
            return new string((from chr in texto ?? string.Empty where EhCaracterAceito(chr) || EhCaracterEspecialAceitoNotaFiscal(chr) select chr).ToArray());
        }

        private static bool EhCaracterAceito(char caracter, bool preservarVirgula = false)
        {
            return char.IsLetterOrDigit(caracter) || char.IsWhiteSpace(caracter) || (preservarVirgula && caracter == ',');
        }

        private static bool EhCaracterEspecialAceitoNotaFiscal(char caracter)
        {
            return caracter == '&' || caracter == ';' || caracter == '-' || caracter == '/' || caracter == '\\' || caracter == '.';
        }

        public static string FormateCPFCNPJSomenteNumerosComZerosAEsquerda(string cpfcnpj)
        {
            cpfcnpj = SomenteNumeros(cpfcnpj);
            cpfcnpj = cpfcnpj.PadLeft(14, '0');
            return cpfcnpj;
        }

        public static bool EhEmailValido(string eMail)
        {
            return !string.IsNullOrWhiteSpace(eMail) && _regEx.IsMatch(eMail);
        }

        public static bool EhEmailValidoQuandoInformado(string eMail)
        {
            return string.IsNullOrWhiteSpace(eMail) || _regEx.IsMatch(eMail);
        }

        public static string AjustarNomeArquivo(string filename)
        {
            var file = filename.Replace(" ", string.Empty);
            return Path.GetInvalidFileNameChars().Aggregate(file, (current, c) => current.Replace(c, '_'));
        }

        public static string AjustarNomeDiretorio(string filename)
        {
            return Path.GetInvalidPathChars().Aggregate(filename, (current, c) => current.Replace(c, '_'));
        }

        public static string[] ParticioneString(string valor, int tamanhoMaximo)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return new string[] { };
            }

            var posicao = 0;
            var cont = 0;
            var comprimento = valor.Length;
            var parts = new string[comprimento / tamanhoMaximo + Math.Min(1, comprimento % tamanhoMaximo)];

            while (posicao < comprimento)
            {
                var comprimentoRestante = Math.Min(tamanhoMaximo, comprimento - posicao);
                var part = new char[comprimentoRestante];
                valor.CopyTo(posicao, part, 0, comprimentoRestante);
                parts[cont++] = new string(part);
                posicao += comprimentoRestante;
            }

            return parts;
        }

        public static string ObtenhaPrimeiraEUltimaPalavra(string palavra)
        {
            if (string.IsNullOrEmpty(palavra))
            {
                return string.Empty;
            }

            var nomeVetor = palavra.Split(' ');
            var ultimo = string.Empty;

            var lista = nomeVetor.ToList();

            var primeiro = lista[0];
            if (lista.Count > 1)
            {
                ultimo = lista.Last();
            }

            return string.Concat(primeiro, lista.Count > 1 ? $" {ultimo}" : string.Empty);
        }

        public static string ObtenhaPrimeiraPalavra(string frase)
        {
            return ObtenhaPalavra(frase, 1);
        }

        public static string ObtenhaSegundaPalavra(string frase)
        {
            return ObtenhaPalavra(frase, 2);
        }

        public static string ObtenhaPalavra(string frase, int ordem)
        {
            if (string.IsNullOrEmpty(frase))
            {
                return string.Empty;
            }

            var vetorFrase = frase.Split(' ');
            if (vetorFrase.Length == 0 || vetorFrase.Length < ordem)
            {
                return string.Empty;
            }

            return vetorFrase[ordem - 1].Trim();
        }

        public static bool ContemSimilar(string texto, string padrao)
        {
            return GetStringNoAccents(texto).ToUpper().Contains(GetStringNoAccents(padrao).ToUpper());
        }

        public static string ObtenhaPrimeiroNome(string primeiroNome)
        {
            if (string.IsNullOrEmpty(primeiroNome))
            {
                return string.Empty;
            }

            var nomeVetor = primeiroNome.Split(' ');
            return nomeVetor[0];
        }

        public static bool PesquisaPartes(string a, string b)
        {
            a = GetStringNoAccents(a).ToLower();
            b = GetStringNoAccents(b).ToLower();

            var partes = b.Split(' ');
            return partes.All(parte => GetStringNoAccents(a).Contains(parte.Trim()));
        }

        public static string LeiaArquivoEncodingPadrao(string caminhoArquivo)
        {
            return File.ReadAllText(caminhoArquivo, EncodingPadrao);
        }

        public static string RemovaCaracteresNaoAceitosNotaFiscal(string str)
        {
            return RemovaCaracteresNaoAceitosNotaFiscal(str, false);
        }
        public static string RemovaCaracteresNaoAceitosNotaFiscal(string str, bool preservarVirgula)
        {
            str = SomenteNumerosOuLetrasOuEspacosOuVirgulas(str, preservarVirgula);
            str = GetStringNoAccents(str);
            str = Regex.Replace(str, @"[\u00A0]", "\u0020");
            str = Regex.Replace(str, @"[^\u0000-\u007F]", "");
            return str;
        }

        public static string RemovaCaracteresDeControle(string texto)
        {
            return new string(texto?.Where(c => !char.IsControl(c)).ToArray() ?? string.Empty.ToArray());
        }

        public static string ConvertaCaracteresParaEscritaXmlNotaFiscal(string texto)
        {
            texto = texto
                .Replace("&", "&amp;");
            texto = texto
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;");

            texto = SomenteNumerosOuLetrasOuEspacosOuVirgulasOuCaractereEspecialNotaFiscal(texto);
            texto = GetStringNoAccents(texto);
            texto = Regex.Replace(texto, @"[\u00A0]", "\u0020");
            texto = Regex.Replace(texto, @"[^\u0000-\u007F]", "");

            return texto;
        }

        public static string ConvertaCaracteresParaLeituraXmlNotaFiscal(string texto)
        {
            texto = texto
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&#39;", "'");
            texto = texto
                .Replace("&amp;", "&");

            return texto;
        }

        public static string ValideListaDeEmails(List<string> emails)
        {
            var emailsInconsistentes = string.Empty;
            foreach (var email in emails)
            {
                if (EhEmailValido(email))
                {
                    emailsInconsistentes += emailsInconsistentes == string.Empty ? email : $"\n{email}";
                }
            }

            return emailsInconsistentes;
        }

        public static string PreparaTermoPesquisa(string termoPesquisa)
        {
            return GetStringNoAccents(termoPesquisa)
                .ToUpperInvariant()
                .Replace($"{(char)92}", $"\\{(char)92}") // Na tabela ASCII, o decimal 92 "(char)92" representa o caracter "\". Adicionado aqui para evitar o erro "Invalid SIMILAR TO pattern"
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace("|", "\\|")
                .Replace("^", "\\^")
                .Replace("+", "\\+")
                .Replace("-", "\\-")
                .Replace("*", "\\*")
                .Replace("%", "\\%")
                .Replace("_", "\\_")
                .Replace("?", "\\?")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("}", "\\}")
                .Replace(",", "")
                .Replace("  ", " ")
                .Replace("'", "''")
                .Replace(" ", "%")
                .Replace("A", "[AÃÂÁÀÄaãâáàä]")
                .Replace("E", "[EÊÉÈËeêéèë]")
                .Replace("I", "[IÎÍÌÏiîíìï]")
                .Replace("O", "[OÕÔÓÒÖoõôóòö]")
                .Replace("U", "[UÛÚÙÜuûúùü]")
                .Replace("C", "[CÇcç]");
        }
    }
}
