using EscolaPro.Core.Model.Pagamentos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Core.Helpers
{
    public class CoreHelpers
    {
        public static string AplicarMascaraTelefone(string strNumero)
        {
            // por omissão tem 10 ou menos dígitos
            string strMascara = "{0:(00)0000-0000}";
            // converter o texto em número
            long lngNumero = Convert.ToInt64(strNumero);

            if (strNumero.Length == 11)
                strMascara = "{0:(00)00000-0000}";

            return string.Format(strMascara, lngNumero);
        }

        public static string ExibirLogo(string cnpj)
        {
            cnpj = Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");

            string logo = "logo_jundiai.jpeg";

            switch (cnpj)
            {
                case "09.435.113/0001-94": // Curso Campinas:
                    logo = "logo_campinas.jpeg";
                    break;
                case "30.966.440/0001-69": // Supletivo Urgente, Centro e Santo Amaro
                    logo = "supletivo_urgente_centro.jpeg";
                    break;
                case "17.509.394/0001-00": // Curso Nacional (Guarulhos)
                    logo = "logo_guarulhos.jpeg";
                    break;
                case "07.299.439/0001-06": // Curso Paulista (Jundiai)
                    logo = "logo_jundiai.jpeg";
                    break;
                case "07.341.116/0001-33": // Escola Modelo, Suporte Central
                    logo = "logo_campinas_modelo.jpeg";
                    break;
                case "12.641.105/0001-09": // Curso Paulista
                    logo = "logo_jundiai.jpeg";
                    break;
                case "97.541.480/0001-30": // Curso Sorocaba
                    logo = "logo_sorocaba.jpeg";
                    break;
                // case "39.854.472/0001-47": // Supletivo Urgente, Centro e Santo Amaro
                //     logo = "supletivo_urgente_centro.jpeg";
                //     break;
                case "39.854.472/0001-47": // NacionalTec
                    logo = "logo_nacionaltec.jpeg";
                    break;
                case "43.368.428/0001-02": // Supletivo ABC
                    logo = "logo_supletivo_abc.jpeg";
                    break;
                default:
                    break;
            }

            return logo;
        }

        public static TipoSituacaoEnum Situacao(TipoSituacaoEnum tipoSituacao)
        {
            throw new NotImplementedException();
        }

        public static string FormatCNPJouCPF(string value)
        {
            return value.Length == 11 ? Convert.ToUInt64(value).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(value).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string ComplementarZeroEsquerda(string valor)
        {
            for (int i = 0; valor.Length < 17; i++)
            {
                valor = "0" + valor;
            }
            return valor;
        }

        public static string FormataCep(string cep)
        {
            try
            {
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }
            catch
            {
                return "";
            }
        }

        public static async Task<string[]> ConverterBoletoPDF(List<string> listHTML, string servicePath)
        {
            try
            {
                HttpClient client = new HttpClient();
                
                var json = JsonConvert.SerializeObject(listHTML);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(servicePath, content);

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.ToString());
                }

                var retorno = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<string[]>(retorno);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        ///  Metodo responsavel por gerar o DAC, nosso numero
        ///  Sequência para cálculo:
        ///   0 9 4 0 0 1 4 3 6 9 6 1 2 0 2 0 1 1 2 1
        /// - 1 2 1 2 1 2 1 2 1 2 1 2 1 2 1 2 1 2 1 2 
        ///   0 9 4 0 0 2 4 6 6 9 6 2 2 0 2 0 1 2 2 2 = 59
        ///  
        ///  depois de somar, vc divide o resultado por 10, exemplo (59 / 10 = 5,9), pegue o restante 9
        ///  DAC = 10 - 9 = 1 (Caso resto da divisão igual a zero, considerar DAC = 0)
        /// </summary>
        /// <returns>
        /// O resultado do DAC é 1
        /// </returns>
        public static string GerarDAC(string nossoNumeroOld)
        {
            try
            {
                var numero = $"094001436961{nossoNumeroOld}";

                //if (numero.Length > 16)
                //    throw new Exception("Número não suportado pela função!");

                //Iniciando a variável da soma
                int soma = 0;

                //Loop para verficar cada numero do valor
                //x inicia com zero e o loop continua enquanto o x é menor que tamanho de caracteres
                for (int x = 0; x < numero.Length; x++)
                {
                    //Iniciando a variável peso com o valor 1
                    var peso = 1;

                    //Se x +1 for um numero par, peso = 2
                    if ((x + 1) % 2 == 0)
                    {
                        peso = 2;
                    }

                    //Convertendo a string do numero para int e multiplica pelo peso
                    int valor = Convert.ToInt32(numero[x].ToString()) * peso;

                    //Convertendo novamente o valor para string
                    string strValor = valor.ToString();

                    //Caso a quantidade de casas do numero seja maior que 1
                    if (strValor.Length > 1)
                    {
                        //Soma o valor de cada casa e adiciona a variavel soma
                        soma += Convert.ToInt32(strValor[0].ToString()) + Convert.ToInt32(strValor[1].ToString());
                    }
                    else
                    {
                        //Adicionar o valor a variavel soma
                        soma += valor;
                    }

                }

                //Resto é o resultado da divisão da soma por 10
                int resto = soma % 10;

                //Se o resto for igual a 0, retorna 0. Se não retorna a o resultado de 10 - resto
                if (resto == 0)
                {
                    return "0";
                }
                else
                {
                    var resultadoFinal = 10 - resto;

                    return resultadoFinal.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string BandeiraCartao(string numeroCartao)
        {
            string bandeira = "--";

            switch (numeroCartao.Substring(0, 1))
            {
                case "4":
                    bandeira = "Visa";
                    break;
                case "5":
                    bandeira = "Master";
                    break;
                case "3":
                    bandeira = "Dinners Club";
                    break;
                default:
                    bandeira = "Visa";
                    break;
            }

            return bandeira;
        }

        public static void AlterarArquivo(string strPathFile)
        {
            try
            {
                //Verifico se o arquivo que desejo abrir existe e passo como parâmetro a variável respectiva

                if (File.Exists(strPathFile))
                {

                    //Instancio o FileStream passando como parâmetro a variável padrão, o FileMode que será

                    //o modo Open e o FileAccess, que será Read(somente leitura). Este método é diferente dos

                    //demais: primeiro irei abrir o arquivo, depois criar um FileStream temporário que irá

                    //armazenar os novos dados e depois criarei outro FileStream para fazer a junção dos dois

                    using (FileStream fs = new FileStream(strPathFile, FileMode.Open, FileAccess.Read))
                    {

                        //Aqui instancio o StreamReader passando como parâmetro o FileStream criado acima.

                        //Uso o StreamReader já que faço 1º a leitura do arquivo. Irei percorrer o arquivo e

                        //quando encontrar uma string qualquer farei a alteração por outra string qualquer

                        using (StreamReader sr = new StreamReader(fs))
                        {

                            //Crio o FileStream temporário onde irei gravar as informações

                            using (FileStream fsTmp = new FileStream(strPathFile + ".tmp",

                                                       FileMode.Create, FileAccess.Write))

                            {

                                //Instancio o StreamWriter para escrever os dados no arquivo temporário,

                                //passando como parâmetro a variável fsTmp, referente ao FileStream criado

                                using (StreamWriter sw = new StreamWriter(fsTmp))

                                {

                                    //Crio uma variável e a atribuo como nula. Faço um while que percorrerá

                                    //meu arquivo original e enquanto ele estiver diferente de nulo...

                                    string strLinha = null;

                                    while ((strLinha = sr.ReadLine()) != null)
                                    {

                                        //faço um indexof para verificar se existe a palavra adicionado,

                                        //se ela existir, a substituo pela palavra alterado

                                        if (strLinha.IndexOf("adicionado") > -1)

                                        {

                                            //uso o método Replace que espera o valor antigo e valor novo

                                            strLinha = strLinha.Replace("adicionado", "alterado");
                                        }

                                        //Chamo o método Write do StreamWriter passando o strLinha como parâmetro

                                        sw.Write(strLinha);

                                    }
                                }
                            }
                        }
                    }

                    //Ao final excluo o arquivo anterior e movo o temporário no lugar do original

                    //Dessa forma não perco os dados de modificação de meu arquivo

                    File.Delete(strPathFile);
                    //No método Move passo o arquivo de origem, o temporário, e o de destino, o original

                    File.Move(strPathFile + ".tmp", strPathFile);
                    //Exibo a mensagem ao usuário
                }
                else
                {

                    //Se não existir exibo a mensagem


                }
            }
            catch (Exception)
            {

            }
        }

        public static string TransacoesNaoPermitidas(string code)
        {
            string codeReturn = "";

            return codeReturn;
        }
    }
}
