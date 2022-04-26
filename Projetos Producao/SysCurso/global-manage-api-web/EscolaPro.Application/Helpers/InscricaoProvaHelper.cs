using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EscolaPro.Service.Helpers
{
    public static class InscricaoProvaHelper
    {
        public static string InscricaoProvaFundamental (InscricaoProvaModel model)
        {
            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\inscricao-provas-fundamental.html"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            rawHtml.Replace("{nome-completo}", model.NomeCompleto);
            rawHtml.Replace("{data-nascimento}", model.DataNascimento);
            rawHtml.Replace("{estado-civil}", model.EstadoCivil);
            rawHtml.Replace("{sexo}", model.Sexo);
            rawHtml.Replace("{rg}", model.Rg);
            rawHtml.Replace("{orgao-expeditor}", model.OrgaoExpeditor);
            rawHtml.Replace("{uf}", model.UF);
            rawHtml.Replace("{cpf}", model.CPF);
            rawHtml.Replace("{titulo-eleitoral}", model.TituloEleitoral);
            rawHtml.Replace("{zona}", model.ZonaEleitoral);
            rawHtml.Replace("{secao}", model.SecaoEleitoral);
            rawHtml.Replace("{nacionalidade}", model.Nacionalidade);
            rawHtml.Replace("{naturalidade}", model.Naturalidade);
            rawHtml.Replace("{nome-responsavel}", model.NomeResponsavel);
            rawHtml.Replace("{nome-mae}", model.NomeMae);
            rawHtml.Replace("{endereco}", model.Endereco);
            rawHtml.Replace("{end-numero}", model.EnderecoNumero);
            rawHtml.Replace("{end-complemento}", model.EnderecoComplemento);
            rawHtml.Replace("{end-bairro}", model.EnderecoBairro);
            rawHtml.Replace("{end-cidade}", model.EnderecoCidade);
            rawHtml.Replace("{end-uf}", model.EnderecoUF);
            rawHtml.Replace("{end-cep}", model.EnderecoCEP);

            return rawHtml.ToString();
        }

        public static string InscricaoProva(InscricaoProvaModel model, string Curso)
        {
            var rawHtml = new StringBuilder();

            string nomeFormulario;

            switch (Curso.Trim())
            {
                case "Ensino Fundamental":
                    nomeFormulario = "inscricao-provas-fundamental.html";
                    break;
                case "Ensino Médio":
                case "Alfabetização, Ensino Fundamental e Médio":
                case "Ensino Fundamental e Médio":
                    nomeFormulario = "inscricao-provas-medio.html";
                    break;
                case "Analista Contábil":
                    nomeFormulario = "Analista_Contabil.html";
                    break;
                case "Assistente Administrativo":
                    nomeFormulario = "Assistente_Administrativo.html";
                    break;
                case "Assistente Comercial":
                    nomeFormulario = "Assistente_Comercial.html";
                    break;
                case "Assistente Contábil":
                    nomeFormulario = "Assistente_Contabil.html";
                    break;
                case "Assistente de Organização, Métodos e Processos":
                    nomeFormulario = "Assistente_de_Organizacao_Metodos_e_Processos.html";
                    break;
                case "Assistente de Produção e Compras":
                    nomeFormulario = "Assistente_de_Producao_e_Compras.html";
                    break;
                case "Assistente Financeiro":
                    nomeFormulario = "Assistente_Financeiro.html";
                    break;
                case "Técnico em Administração":
                    nomeFormulario = "Tecnico_em_Administracao.html";
                    break;
                case "Técnico em Contabilidade":
                    nomeFormulario = "Tecnico_em_Contabilidade.html";
                    break;
                case "Técnico em Transações Imobiliárias":
                    nomeFormulario = "Tecnico_em_Transacoes_Imobiliarias.html";
                    break;
                case "Assistente de Recursos Humanos":
                    nomeFormulario = "Assistente_de_Recursos_Humanos.html";
                    break;
                default:
                    return "";
            }

            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\{nomeFormulario}"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            rawHtml.Replace("{nome-completo}", model.NomeCompleto);
            rawHtml.Replace("{data-nascimento}", model.DataNascimento);
            rawHtml.Replace("{estado-civil}", model.EstadoCivil);
            rawHtml.Replace("{sexo}", model.Sexo);
            rawHtml.Replace("{rg}", model.Rg);
            rawHtml.Replace("{orgao-expeditor}", model.OrgaoExpeditor);
            rawHtml.Replace("{uf}", model.UF);
            rawHtml.Replace("{cpf}", model.CPF);
            rawHtml.Replace("{titulo-eleitoral}", model.TituloEleitoral);
            rawHtml.Replace("{zona}", model.ZonaEleitoral);
            rawHtml.Replace("{secao}", model.SecaoEleitoral);
            rawHtml.Replace("{nacionalidade}", model.Nacionalidade);
            rawHtml.Replace("{naturalidade}", model.Naturalidade);
            rawHtml.Replace("{nome-responsavel}", model.NomeResponsavel);
            rawHtml.Replace("{nome-mae}", model.NomeMae);
            rawHtml.Replace("{endereco}", model.Endereco);
            rawHtml.Replace("{end-numero}", model.EnderecoNumero);
            rawHtml.Replace("{end-complemento}", model.EnderecoComplemento);
            rawHtml.Replace("{end-bairro}", model.EnderecoBairro);
            rawHtml.Replace("{end-cidade}", model.EnderecoCidade);
            rawHtml.Replace("{end-uf}", model.EnderecoUF);
            rawHtml.Replace("{end-cep}", model.EnderecoCEP);

            return rawHtml.ToString();
        }

    }

    public class InscricaoProvaModel
    {
        public string NomeCompleto { get; set; }
        public string DataNascimento { get; set; }
        public string EstadoCivil { get; set; }
        public string Sexo { get; set; }
        public string Rg { get; set; }
        public string OrgaoExpeditor { get; set; }
        public string UF { get; set; }
        public string CPF { get; set; }
        public string TituloEleitoral { get; set; }
        public string ZonaEleitoral { get; set; }
        public string SecaoEleitoral { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public string NomeResponsavel { get; set; }
        public string NomeMae { get; set; }
        public string Endereco { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public string EnderecoBairro { get; set; }
        public string EnderecoCidade { get; set; }
        public string EnderecoUF { get; set; }
        public string EnderecoCEP { get; set; }
    }
}
