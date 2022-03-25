using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.PagamentosVO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.ControlePontoVO;
using EscolaPro.Core.Helpers;
using EscolaPro.Service.Dto.UnidadeVO;
using System.IO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using Microsoft.Extensions.Configuration;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ReguaContato;

namespace EscolaPro.Service.Helpers
{
    public static class CoreHelpers
    {
        public static bool AmbienteProducao => true;

        public static async Task<string> GetTransaction(object dtoPagamentoCartao, AcquirersEnum acquirers)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();

            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var httpClient = new HttpClient(clientHandler))
            {
                var configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .AddEnvironmentVariables()
                      .Build();

                httpClient.BaseAddress = new Uri(configuration.GetSection("AppSettings:ApiPayment").Value);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string stringPayload = string.Empty;

                stringPayload = JsonConvert.SerializeObject(dtoPagamentoCartao);

                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"api/Payment/{(int)acquirers}", httpContent))
                {
                    string apiResponse = "";

                    if (response.IsSuccessStatusCode)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        apiResponse = System.Text.Json.JsonSerializer.Serialize(response);
                    }

                    return apiResponse;
                }
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

        public static StringBuilder MontarConteudoEmail(List<DtoPagamento> pagamentos, DtoAlunoFoto foto, string nomeAluno)
        {
            try
            {

                StringBuilder conteudoEmail = new StringBuilder();

                string filePath = string.Empty;

                conteudoEmail.Append("<br />");

                if (foto.Foto != null)
                {
                    if (foto.Foto.Length > 0)
                    {
                        string aspas = @"""";

                        string imgbyteArray = Convert.ToBase64String(foto.Foto);

                    }

                    conteudoEmail.Append("");
                    conteudoEmail.Append("<br />");
                    conteudoEmail.Append("<br />");
                }

                conteudoEmail.Append($"Olá {nomeAluno.Split()[0]}! ");

                conteudoEmail.Append("");
                conteudoEmail.Append("");

                if (pagamentos.Count == 1)
                {
                    conteudoEmail.Append($"<p>O seu boleto está disponível em anexo.<p>");
                }
                else
                {
                    conteudoEmail.Append($"<p>Os seus boletos estão disponíveis em anexo.</p>");
                }

                conteudoEmail.Append("");
                conteudoEmail.Append("");

                bool existeDesconto = false;

                if (pagamentos.Where(x => x.Desconto.HasValue).Count() > 0)
                {
                    if (pagamentos.Where(x => x.Desconto.Value > 0).Count() > 0)
                    {
                        existeDesconto = true;
                    }
                }

                if (existeDesconto)
                {
                    if (pagamentos.Count == 1)
                    {
                        conteudoEmail.Append($"<p>Aproveite o desconto e pague seu boleto até o vencimento.</p>\n\n");
                    }
                    else
                    {
                        conteudoEmail.Append($"<p>Aproveite o desconto e pague seus boletos até o vencimento.</p>\n\n");
                    }
                }

                conteudoEmail.Append("");
                conteudoEmail.Append("");
                conteudoEmail.Append($"<p>Você pode pagar em qualquer banco ou lotérica.</p>\n\n");
                conteudoEmail.Append("");
                conteudoEmail.Append("");
                conteudoEmail.Append("");
                conteudoEmail.Append("");

                string style = "style='border: 1px solid black;text-align:center'";
                string styleTable = "style='width:100%;border: 1px solid black;text-align:center'";

                conteudoEmail.Append($"<table " + styleTable + ">");
                conteudoEmail.Append($"<tr>");
                conteudoEmail.Append($"<th {style}>Descrição</th>");
                conteudoEmail.Append($"<th {style}>Valor</th>");

                if (existeDesconto)
                {
                    conteudoEmail.Append($"<th {style}>Desconto pontualidade</th>");
                }

                bool existeBolsaConvenio = false;

                if (pagamentos.Where(x => x.PromocaoBolsaConvenio.HasValue && x.PromocaoBolsaConvenio.Value > 0).Count() > 0)
                {
                    existeBolsaConvenio = true;
                }

                if (existeBolsaConvenio)
                {
                    conteudoEmail.Append($"<th {style}>Promoção, Bolsa ou Convênio</th>");
                }

                conteudoEmail.Append($"<th {style}>Valor até o vencimento</th>");
                conteudoEmail.Append($"<th {style}>Data de Vencimento</th>");
                conteudoEmail.Append("</tr>");

                pagamentos = pagamentos.OrderBy(x => x.DataVencimento).ToList();

                List<DtoPagamento> pagamentosLista = new List<DtoPagamento>();

                if (pagamentos.Any(x => x.Descricao.Contains("Taxa de Matricula")))
                    pagamentosLista.AddRange(pagamentos.Where(x => x.Descricao.Contains("Taxa de Matricula") &&
                                                                   !pagamentosLista.Any(y => y.Id == x.Id))
                                                       .OrderBy(x => x.DataVencimento));

                if (pagamentos.Any(x => x.Descricao.Contains("Apostila")))
                    pagamentosLista.AddRange(pagamentos.Where(x => x.Descricao.Contains("Apostila") &&
                                                                   !pagamentosLista.Any(y => y.Id == x.Id))
                                                       .OrderBy(x => x.DataVencimento));

                if (pagamentos.Any(x => x.Descricao.Contains("Parcela")))
                    pagamentosLista.AddRange(pagamentos.Where(x => x.Descricao.Contains("Parcela") &&
                                                                   !pagamentosLista.Any(y => y.Id == x.Id))
                                                       .OrderBy(x => x.DataVencimento));

                if (pagamentos.Any(x => x.Descricao.Contains("Taxa de Inscrição")))
                    pagamentosLista.AddRange(pagamentos.Where(x => x.Descricao.Contains("Taxa de Inscrição") &&
                                                                   !pagamentosLista.Any(y => y.Id == x.Id))
                                                       .OrderBy(x => x.DataVencimento));

                pagamentosLista.AddRange(pagamentos.Where(x => !pagamentosLista.Any(y => y.Id == x.Id)));

                foreach (var item in pagamentosLista)
                {
                    conteudoEmail.Append("<tr>");
                    conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"{item.Descricao}"); // Descrição
                    conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"R$ {item.Valor.ToString("N2")}"); // Valor

                    if (existeDesconto)
                    {
                        conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"{ Convert.ToDouble(item.Desconto) }%"); // Desconto pontualidade
                    }

                    if (existeBolsaConvenio)
                    {
                        conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"{ Convert.ToDouble(item.PromocaoBolsaConvenio.Value)}%"); // Promoção, Bolsa ou Convênio
                    }

                    var valorAteVencimento = item.Valor - ((item.Valor * item.Desconto) / 100);

                    if (valorAteVencimento.HasValue)
                    {
                        conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"R$ {valorAteVencimento.Value.ToString("N2")}"); // Valor até o vencimento
                    }
                    else
                    {
                        conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $""); // Valor até o vencimento
                    }

                    if (item.DataVencimento.HasValue)
                    {
                        conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"{item.DataVencimento.Value.ToString("dd/MM/yyyy")}");
                    }
                    else
                    {
                        conteudoEmail.AppendFormat("<td " + style + ">{0}</td>", $"");
                    }

                    // Data de Vencimento
                    conteudoEmail.Append("</tr>");
                }
                conteudoEmail.Append("</table>");
                conteudoEmail.Append("");
                conteudoEmail.Append("");
                conteudoEmail.Append($"<h3>O certificado escolar é o maior patrimônio do trabalhador. </h3>");
                conteudoEmail.Append("");

                return conteudoEmail;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string MontarEmailInscricaoProva(DtoProvaAlunoRequest provaAluno, DtoAluno aluno, Unidade unidade, bool nacionalTec, Dto.AgendaProvaVO.DtoColegioAutorizado colegioAutorizado = null)
        {
            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\templateEmailSemBotao.html"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            string logoUrl;

            switch (unidade.RazaoSocial)
            {
                case "Centro de Treinamento e Cursos Preparatórios Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_campinas.png";
                    break;
                case "Crescer Cursos Livres e Profissionalizantes Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_escola_modelo.png";
                    break;
                case "Cursos Preparatórios Piracicaba Eireli":
                case "Cursos Preparatórios Paulista Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_paulista.png";
                    break;
                case "Cursos Preparatórios Sorocaba Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_sorocaba.png";
                    break;
                case "Cursos Preparatórios Nacional Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_nacional.png";
                    break;
                case "Cursos Preparatórios E Pro. Educasp Ltda":
                case "Cursos Preparatórios Global Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/novo_logo_supletivo_urgente_centro.png";
                    break;
                // case "GCO Administração e Participações LTDA":
                //     logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                //     break;
                case "Cursos Preparatórios E Profissionalizantes Educasp Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                    break;
                case "Cursos Preparatórios E Profissionalizantes Abc Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_supletivo_abc.png";
                    break;
                default:
                    logoUrl = "";
                    break;
            }

            char[] trimCep = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', ' ' };

            rawHtml.Replace("{logo-unidade}", $"{logoUrl}");
            rawHtml.Replace("{saudacao}", $"Olá, <strong>{aluno.Nome}</strong>.");
            if (provaAluno.TipoTransporte == Core.Model.PainelMatricula.TipoTransporteEnum.Escola && provaAluno.UnidadeTransporteProva != null)
            {
                rawHtml.Replace("{primeiro-bloco}", "Sua inscrição para prova em colégio autorizado foi realizada com sucesso.<br><br>" +
                    $"A data da sua prova é: {provaAluno.DataProva.Value:dd/MM/yyyy}<br>" +
                    $"O local de saída do seu ônibus é: <span class=\"endereco\">{provaAluno.UnidadeTransporteProva.UnidadeParticipanteProva.LocalSaida.TrimEnd(trimCep)}</span><br>" +
                    $"O número do seu Ônibus é: {provaAluno.UnidadeTransporteProva.NumeroOnibus}<br>" +
                    $"Horário de embarque às: {provaAluno.UnidadeTransporteProva.UnidadeParticipanteProva.HoraSaida.Substring(0, 2)}:{provaAluno.UnidadeTransporteProva.UnidadeParticipanteProva.HoraSaida.Substring(2, 2)} horas<br><br>" +
                    $"Comparecer no local de embarque com 15 minutos de antecedência.");
            }
            else
            {
                rawHtml.Replace("{primeiro-bloco}", "Sua inscrição para prova em colégio autorizado foi realizada com sucesso;<br><br>" +
                    $"A data da sua prova é: {provaAluno.DataProva.Value:dd/MM/yyyy}<br>" +
                    $"O endereço para realizar sua prova fica na: {colegioAutorizado.Endereco.Rua}, {colegioAutorizado.Endereco.Numero}, {colegioAutorizado.Endereco.Bairro}, {colegioAutorizado.Endereco.Cidade} - {colegioAutorizado.Endereco.Estado}");
            }
            rawHtml.Replace("{segundo-bloco}", "<strong>Lembre de levar:</strong>" +
                "<ul><li>Lápis</li>" +
                "<li>Borracha</li>" +
                "<li>Caneta Azul</li>" +
                "<li>Documento com foto</li></ul><br>" +
                "<strong>Importante:</strong>" +
                "<ul><li>Celular deve ser mantido desligado durante a prova</li>" +
                "<li>A alimentação é por conta do aluno</li>" +
                "<li>Não faltar na prova</li></ul>");

            var urlGooglePlay = string.Empty;

            if (unidade.Id == 10)
            {
                urlGooglePlay = "cursosnacionaltec";
            }
            else
            {
                urlGooglePlay = "supletivoportal";
            }

            rawHtml.Replace("{urlGooglePlay}", urlGooglePlay);
            rawHtml.Replace("{facebook-unidade}", unidade.Contato.FaceBook);
            rawHtml.Replace("{instagram-unidade}", unidade.Contato.Instagram);
            rawHtml.Replace("{youtube-unidade}", nacionalTec ? "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw" : "https://www.youtube.com/channel/UCPE1FPk-zRY9sCso1SWgRVA");
            rawHtml.Replace("{frase-efeito}", "O certificado escolar é o maior patrimônio do trabalhador.");
            rawHtml.Replace("{nome-unidade}", unidade.NomeFantasia);
            rawHtml.Replace("{dados-unidade}", $"<span class=\"endereco\">{unidade.Endereco.Rua}, {unidade.Endereco.Numero}, {unidade.Endereco.Bairro}, {unidade.Endereco.Cidade} - {unidade.Endereco.Estado}</span><br>Ligue ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}");

            return rawHtml.ToString();
        }

        public static string MontarEmailInscricaoProvaOnline(DtoAluno aluno, Unidade unidade, bool nacionalTec)
        {
            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\templateEmailSemBotao.html"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            string logoUrl;

            switch (unidade.RazaoSocial)
            {
                case "Centro de Treinamento e Cursos Preparatórios Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_campinas.png";
                    break;
                case "Crescer Cursos Livres e Profissionalizantes Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_escola_modelo.png";
                    break;
                case "Cursos Preparatórios Piracicaba Eireli":
                case "Cursos Preparatórios Paulista Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_paulista.png";
                    break;
                case "Cursos Preparatórios Sorocaba Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_sorocaba.png";
                    break;
                case "Cursos Preparatórios Nacional Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_nacional.png";
                    break;
                case "Cursos Preparatórios E Pro. Educasp Ltda":
                case "Cursos Preparatórios Global Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/novo_logo_supletivo_urgente_centro.png";
                    break;
                // case "GCO Administração e Participações LTDA":
                //     logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                //     break;
                case "Cursos Preparatórios E Profissionalizantes Educasp Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                    break;
                case "Cursos Preparatórios E Profissionalizantes Abc Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_supletivo_abc.png";
                    break;
                default:
                    logoUrl = "";
                    break;
            }

            var urlGooglePlay = string.Empty;

            if (unidade.Id == 10)
            {
                urlGooglePlay = "cursosnacionaltec";
            }
            else
            {
                urlGooglePlay = "supletivoportal";
            }

            rawHtml.Replace("{logo-unidade}", $"{logoUrl}");
            rawHtml.Replace("{saudacao}", $"Olá, <strong>{aluno.Nome}</strong>.");
            rawHtml.Replace("{primeiro-bloco}", "");
            rawHtml.Replace("{segundo-bloco}", "Sua inscrição para prova em colégio autorizado foi realizada com sucesso.<br><br>" +
                "Em até 45 dias úteis você receberá todas as informações necessárias para acessar sua prova.<br><br>");
            rawHtml.Replace("{urlGooglePlay}", urlGooglePlay);
            rawHtml.Replace("{facebook-unidade}", unidade.Contato.FaceBook);
            rawHtml.Replace("{instagram-unidade}", unidade.Contato.Instagram);
            rawHtml.Replace("{youtube-unidade}", nacionalTec ? "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw" : "https://www.youtube.com/channel/UCPE1FPk-zRY9sCso1SWgRVA");
            rawHtml.Replace("{frase-efeito}", "O certificado escolar é o maior patrimônio do trabalhador.");
            rawHtml.Replace("{nome-unidade}", unidade.NomeFantasia);
            rawHtml.Replace("{dados-unidade}", $"<span class=\"endereco\">{unidade.Endereco.Rua}<z>, {unidade.Endereco.Numero}, {unidade.Endereco.Bairro}, {unidade.Endereco.Cidade} - {unidade.Endereco.Estado}</span><br>Ligue ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}");

            return rawHtml.ToString();
        }

        public static string MontarEmailBaixaManual(DtoMatriculaAlunoResponse dtoMatricula, DtoUnidadeResponse unidade, bool nacionalTec, List<Pagamento> pagamentos)
        {

            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\templateEmailSemBotao.html"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            string logoUrl;

            switch (unidade.RazaoSocial)
            {
                case "Centro de Treinamento e Cursos Preparatórios Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_campinas.png";
                    break;
                case "Crescer Cursos Livres e Profissionalizantes Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_escola_modelo.png";
                    break;
                case "Cursos Preparatórios Piracicaba Eireli":
                case "Cursos Preparatórios Paulista Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_paulista.png";
                    break;
                case "Cursos Preparatórios Sorocaba Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_sorocaba.png";
                    break;
                case "Cursos Preparatórios Nacional Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_nacional.png";
                    break;
                case "Cursos Preparatórios E Pro. Educasp Ltda":
                case "Cursos Preparatórios Global Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/novo_logo_supletivo_urgente_centro.png";
                    break;
                // case "GCO Administração e Participações LTDA":
                //     logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                //     break;
                case "Cursos Preparatórios E Profissionalizantes Educasp Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                    break;
                case "Cursos Preparatórios E Profissionalizantes Abc Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_supletivo_abc.png";
                    break;
                default:
                    logoUrl = "";
                    break;
            }

            var urlGooglePlay = string.Empty;

            if (unidade.Id == 10)
            {
                urlGooglePlay = "cursosnacionaltec";
            }
            else
            {
                urlGooglePlay = "supletivoportal";
            }

            rawHtml.Replace("{logo-unidade}", $"{logoUrl}");
            rawHtml.Replace("{saudacao}", $".");
            rawHtml.Replace("{primeiro-bloco}", "");

            var segundoBloco = new StringBuilder();
            segundoBloco.Append($"Baixa manual realizada com sucesso. <br> Aluno: {dtoMatricula.Aluno.Nome} <br> Rm: {dtoMatricula.NumeroMatricula} <br> Unidade: {unidade.NomeFantasia}.<br><br>");

            string style = "style='border: 1px solid black;text-align:center'";
            string styleTable = "style='width:100%;border: 1px solid black;text-align:center'";

            segundoBloco.Append($"<table " + styleTable + ">");
            segundoBloco.Append($"<tr>");
            segundoBloco.Append($"<th {style}>Descrição</th>");
            segundoBloco.Append($"<th {style}>Valor</th>");
            segundoBloco.Append($"<th {style}>Data de Vencimento</th>");
            segundoBloco.Append("</tr>");

            pagamentos = pagamentos.OrderBy(x => x.DataVencimento).ToList();            

            foreach (var item in pagamentos)
            {
                segundoBloco.Append("<tr>");
                segundoBloco.AppendFormat("<td " + style + ">{0}</td>", $"{item.Descricao}"); // Descrição
                segundoBloco.AppendFormat("<td " + style + ">{0}</td>", $"R$ {item.Valor.ToString("N2")}"); // Valor


                if(item.DataVencimento.HasValue)
                segundoBloco.AppendFormat("<td " + style + ">{0}</td>", $"{item.DataVencimento.Value.ToString("dd/MM/yyyy")}");
                else
                segundoBloco.AppendFormat("<td " + style + ">{0}</td>", $"");
                segundoBloco.Append("</tr>");
            }

            segundoBloco.Append("</table>");


            rawHtml.Replace("{segundo-bloco}", segundoBloco.ToString());

            rawHtml.Replace("{urlGooglePlay}", urlGooglePlay);
            rawHtml.Replace("{facebook-unidade}", unidade.Contato.FaceBook);
            rawHtml.Replace("{instagram-unidade}", unidade.Contato.Instagram);
            rawHtml.Replace("{youtube-unidade}", nacionalTec ? "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw" : "https://www.youtube.com/channel/UCPE1FPk-zRY9sCso1SWgRVA");
            rawHtml.Replace("{frase-efeito}", "");
            rawHtml.Replace("{nome-unidade}", unidade.NomeFantasia);
            rawHtml.Replace("{dados-unidade}", $"<span class=\"endereco\">{unidade.Endereco.Rua}<z>, {unidade.Endereco.Numero}, {unidade.Endereco.Bairro}, {unidade.Endereco.Cidade} - {unidade.Endereco.Estado}</span><br>Ligue ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}");

            return rawHtml.ToString();
        }

        public static string MontarEmailInformacoesProvaOnline(Core.Model.PainelMatricula.ProvaAluno provaAluno, DtoAluno aluno, Unidade unidade, bool nacionalTec)
        {
            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\templateEmailSemBotao.html"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            string logoUrl;
            string urlGooglePlay;

            switch (unidade.RazaoSocial)
            {
                case "Centro de Treinamento e Cursos Preparatórios Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_campinas.png";
                    break;
                case "Crescer Cursos Livres e Profissionalizantes Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_escola_modelo.png";
                    break;
                case "Cursos Preparatórios Piracicaba Eireli":
                case "Cursos Preparatórios Paulista Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_paulista.png";
                    break;
                case "Cursos Preparatórios Sorocaba Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_sorocaba.png";
                    break;
                case "Cursos Preparatórios Nacional Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_nacional.png";
                    break;
                case "Cursos Preparatórios E Pro. Educasp Ltda":
                case "Cursos Preparatórios Global Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/novo_logo_supletivo_urgente_centro.png";
                    break;
                // case "GCO Administração e Participações LTDA":
                //     logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                //     break;
                case "Cursos Preparatórios E Profissionalizantes Educasp Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                    break;
                case "Cursos Preparatórios E Profissionalizantes Abc Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_supletivo_abc.png";
                    break;
                default:
                    logoUrl = "";
                    break;
            }
            rawHtml.Replace("{logo-unidade}", $"{logoUrl}");
            rawHtml.Replace("{saudacao}", $"Olá, <strong>{aluno.Nome}</strong>.");
            rawHtml.Replace("{primeiro-bloco}", "");
            if (nacionalTec)
            {
                urlGooglePlay = "cursosnacionaltec";
                rawHtml.Replace("{segundo-bloco}", "<strong>ATENÇÃO, sua prova on-line já está disponível.</strong><br><br>" +
                    "<strong>Leia com atenção, caso contrário poderá ser reprovado.</strong><br><br>" +
                    "<ul><li>A partir de agora você tem 15 dias para concluir <strong>cada etapa.</strong></li>" +
                    "<li>Iniciando a prova você terá até <strong>1h30</strong> para finalizar cada disciplina.</li>" +
                    "<li>Você pode realizar a prova on-line apenas <strong>2 vezes.</strong> Caso ocorra uma 2ª reprova NÃO terá uma 3ª prova on - line, apenas presencial.</li>" +
                    "<li>Caso ocorra reprova, na primeira tentativa do Exame de Avaliação Final, será liberado na hora uma nova chance.</li></ul><br><br>" +
                    "<strong>Siga o passo a passo para acessar sua prova:</strong><br><br>" +
                    "1. Acesse o Portal do Aluno com <strong>Login</strong> (seu cpf) e sua <strong>Senha</strong> (4 primeiros dígitos do seu cpf).<br>" +
                    "2. Clique no botão EAD<br>" +
                    "3. Assista o vídeo que consta na página e em seguida clique no botão laranja <strong>\"Instituições autorizadas\".</strong><br>" +
                    $"4. <strong>Identificação do usuário:</strong> {provaAluno.IdentificacaoUsuario} <strong>Senha:</strong> {provaAluno.SenhaProva}<br><br>" +
                    "<strong>Boa prova.</strong><br><br>" +
                    $"Esta mensagem é automática, dúvidas entre em contato através do telefone ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}.");
            }
            else
            {
                urlGooglePlay = "supletivoportal";
                rawHtml.Replace("{segundo-bloco}", "ATENÇÃO, sua prova on-line já está disponível!<br><br>" +
                    "<strong>Leia com atenção, caso contrário poderá ser reprovado.</strong><br><br>" +
                    "A prova está dividida em duas etapas:" +
                    "<ol><li>Exames de Reclassificação</li>" +
                    "<li>Exames de Certificação</li></ol>" +
                    "<ul><li>A partir de agora você tem 15 dias para concluir cada etapa.</li>" +
                    "<li>Sendo aprovado em todos os exames de reclassificação, após 10 dias úteis os exames certificadores serão liberados no sistema automaticamente.</li>" +
                    "<li>Iniciando a prova você terá até 2 horas para finalizar cada disciplina.</li>" +
                    "<li>A redação deve ser feita de forma on-line. Não copie textos da internet, pois caso ocorra será reprovado.</li>" +
                    "<li>Você pode realizar a prova on-line apenas 2 vezes. Caso ocorra uma 2ª reprova NÃO terá uma 3ª prova on-line, apenas presencial.</li></ul><br><br>" +
                    "<strong>Seguem dados para acessar sua prova:</strong>" +
                    $"<ol style=\"text-align:left\"><li>Acesse o portal do aluno através do link: <a target=\"_blank\" href=\"https://www.portaldoalunocurso.com.br/\">https://www.portaldoalunocurso.com.br/</a></li>" +
                    "<li>Entre com o seu login (seu cpf) e sua senha (4 primeiros dígitos do seu cpf)</li>" +
                    "<li>Clique no botão EJA - ENCCEJA</li>" +
                    "<li>Assista o vídeo que consta na página e em seguida clique no botão laranja \"EJA - Instituições autorizadas\"</li>" +
                    $"<li>Identificação do usuário: {provaAluno.IdentificacaoUsuario} Senha: {provaAluno.SenhaProva}</li></ol>" +
                    "Boa prova.<br><br>" +
                    $"Esta mensagem é automática, dúvidas entre em contato através do telefone ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}.");
            }
            rawHtml.Replace("{urlGooglePlay}", urlGooglePlay);
            rawHtml.Replace("{facebook-unidade}", unidade.Contato.FaceBook);
            rawHtml.Replace("{instagram-unidade}", unidade.Contato.Instagram);
            rawHtml.Replace("{youtube-unidade}", nacionalTec ? "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw" : "https://www.youtube.com/channel/UCPE1FPk-zRY9sCso1SWgRVA");
            rawHtml.Replace("{frase-efeito}", "O certificado escolar é o maior patrimônio do trabalhador.");
            rawHtml.Replace("{nome-unidade}", unidade.NomeFantasia);
            rawHtml.Replace("{dados-unidade}", $"<span class=\"endereco\">{unidade.Endereco.Rua}<z>, {unidade.Endereco.Numero}<z>, {unidade.Endereco.Bairro}, {unidade.Endereco.Cidade} - {unidade.Endereco.Estado}</span><br>Ligue ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}");

            return rawHtml.ToString();
        }

        public static string[] Abreviatte(string nome)
        {
            try
            {
                var meio = " ";
                var nomes = nome.Split(' ');

                for (var i = 1; i < nomes.Length - 1; i++)
                {
                    if (!nomes[i].Equals("de", StringComparison.OrdinalIgnoreCase) &&
                        !nomes[i].Equals("da", StringComparison.OrdinalIgnoreCase) &&
                        !nomes[i].Equals("do", StringComparison.OrdinalIgnoreCase) &&
                        !nomes[i].Equals("das", StringComparison.OrdinalIgnoreCase) &&
                        !nomes[i].Equals("dos", StringComparison.OrdinalIgnoreCase))
                        meio += nomes[i][0] + ". ";
                }

                return nomes;
                //return nomes[0] + meio + nomes[nomes.Length - 1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ValidarSaldo(bool consultaSaldoPonto, List<DtoControlePontoHorario> controlePontoLista)
        {
            try
            {
                List<TimeSpan> valorTotal = new List<TimeSpan>();

                if (!consultaSaldoPonto)
                {
                    foreach (var item in controlePontoLista.Where(x => !x.Pago))
                    {
                        if (!string.IsNullOrEmpty(item.SaldoDevedor) && item.SaldoDevedor != "Período de férias")
                        {
                            TimeSpan time;

                            if (!TimeSpan.TryParse(item.SaldoDevedor, out time))
                            {

                            }
                            else
                            {
                                time = TimeSpan.Parse(item.SaldoDevedor);

                                valorTotal.Add(time);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in controlePontoLista)
                    {
                        if (!string.IsNullOrEmpty(item.SaldoDevedor) && item.SaldoDevedor != "Período de férias")
                        {
                            TimeSpan time;

                            if (!TimeSpan.TryParse(item.SaldoDevedor, out time))
                            {

                            }
                            else
                            {
                                time = TimeSpan.Parse(item.SaldoDevedor);

                                valorTotal.Add(time);
                            }
                        }
                    }
                }

                var totalSpan = StatisticExtensions.Sum(valorTotal);

                var saldoFormatado = (int)totalSpan.TotalHours + totalSpan.ToString(@"\:mm");

                string SaldoDevedorTotal;

                if (saldoFormatado == "-00:00")
                {
                    SaldoDevedorTotal = "00:00";
                }
                else
                {
                    TimeSpan horarioNegativo = new TimeSpan(0, 0, 0);

                    if (totalSpan < horarioNegativo)
                    {
                        SaldoDevedorTotal = saldoFormatado;
                    }
                    else
                    {
                        SaldoDevedorTotal = saldoFormatado;
                    }
                }

                return SaldoDevedorTotal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string MontarEmailMatriculaAluno(Aluno aluno, Unidade unidade, bool nacionalTec)
        {
            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\templateEmailSemBotao.html"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            string logoUrl;

            switch (unidade.RazaoSocial)
            {
                case "Centro de Treinamento e Cursos Preparatórios Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_campinas.png";
                    break;
                case "Crescer Cursos Livres e Profissionalizantes Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_escola_modelo.png";
                    break;
                case "Cursos Preparatórios Piracicaba Eireli":
                case "Cursos Preparatórios Paulista Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_paulista.png";
                    break;
                case "Cursos Preparatórios Sorocaba Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_sorocaba.png";
                    break;
                case "Cursos Preparatórios Nacional Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_nacional.png";
                    break;
                case "Cursos Preparatórios E Pro. Educasp Ltda":
                case "Cursos Preparatórios Global Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/novo_logo_supletivo_urgente_centro.png";
                    break;
                // case "GCO Administração e Participações LTDA":
                //     logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                //     break;
                case "Cursos Preparatórios E Profissionalizantes Educasp Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                    break;
                case "Cursos Preparatórios E Profissionalizantes Abc Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_supletivo_abc.png";
                    break;
                default:
                    logoUrl = "";
                    break;
            }

            var tipoUnidadeCurso = string.Empty;
            var nomeApp = string.Empty;
            var urlApp = string.Empty;
            var urlGooglePlay = string.Empty;

            if (unidade.Id == 10)
            {
                tipoUnidadeCurso = nomeApp = "Cursos NacionalTec";
                urlGooglePlay = "cursosnacionaltec";
            }
            else
            {
                tipoUnidadeCurso = "Curso Supletivo preparatório";
                nomeApp = "Supletivo Portal";
                urlGooglePlay = "supletivoportal";
            }

            rawHtml.Replace("{logo-unidade}", $"{logoUrl}");
            rawHtml.Replace("{saudacao}", $"Olá, <strong>{aluno.Nome}</strong>.");
            rawHtml.Replace("{primeiro-bloco}", "<img align='center' alt='' src='https://mcusercontent.com/448e1c7e0dbed5850a4dcde5f/images/625b7f2b-98ed-43c6-b17a-10260af454bb.jpg' width='100%' height=auto>");
            rawHtml.Replace("{segundo-bloco}",
                            $"<div style='font-size: 15px !important;'><strong>Seja bem-vindo(a) ao {tipoUnidadeCurso}.</strong><br><br>" +
                            $"<strong>Baixe nosso aplicativo '{nomeApp}' na Google Play e aproveite todo conteúdo:</strong><br>" +
                            "<ul><li style='font-size: 15px !important;'>Videoaulas</li>" +
                            "<li style='font-size: 15px !important;'>Simulados</li>" +
                            "<li style='font-size: 15px !important;'>Solicitações</li>" +
                            "<li style='font-size: 15px !important;'>Pagamentos</li>" +
                            "<li style='font-size: 15px !important;'>Envio de Documentos</li></ul><br>" +
                            "<strong>Tudo isso na palma da sua mão!</strong><br>" +
                            "Ou acesse o Portal do Aluno através do site:<br>" +
                            "<a style='font-size: 15px !important;' href='http://www.portaldoalunocurso.com.br' target='_blank'>www.portaldoalunocurso.com.br</a><br><br>" +
                            "Para acessar informe:<br>" +
                            $"<strong>Login:</strong> <a style='color: #000000 !important; text-decoration:none !important; font-size: 15px !important;'>{aluno.CPF}</a><br>" +
                            $"<strong>Senha:</strong> {aluno.CPF.Substring(0, 4)}<br></div>");
            rawHtml.Replace("{urlGooglePlay}", urlGooglePlay);
            rawHtml.Replace("{facebook-unidade}", unidade.Contato.FaceBook);
            rawHtml.Replace("{instagram-unidade}", unidade.Contato.Instagram);
            rawHtml.Replace("{youtube-unidade}", nacionalTec ? "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw" : "https://www.youtube.com/channel/UCPE1FPk-zRY9sCso1SWgRVA");
            rawHtml.Replace("{frase-efeito}", "O certificado escolar é o maior patrimônio do trabalhador.");
            rawHtml.Replace("{nome-unidade}", unidade.NomeFantasia);
            rawHtml.Replace("{dados-unidade}", $"<span class=\"endereco\">{unidade.Endereco.Rua}<z>, {unidade.Endereco.Numero}, {unidade.Endereco.Bairro}, {unidade.Endereco.Cidade} - {unidade.Endereco.Estado}</span><br>Ligue ({unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{unidade.Contato.TelefoneFixoPrincipal.Substring(6)}");

            return rawHtml.ToString();
        }

        public static string MontarEmailCobrancaReguaContato(ReguaContatoFila reguaContatoFila)
        {
            var rawHtml = new StringBuilder();
            using (var sr = new StreamReader($"{Directory.GetCurrentDirectory()}\\App_Data\\{reguaContatoFila.ReguaContatoRegras.Texto}"))
            {
                rawHtml.Append(sr.ReadToEnd());
            }

            string logoUrl;

            switch (reguaContatoFila.Unidade.RazaoSocial)
            {
                case "Centro de Treinamento e Cursos Preparatórios Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_campinas.png";
                    break;
                case "Crescer Cursos Livres e Profissionalizantes Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_escola_modelo.png";
                    break;
                case "Cursos Preparatórios Piracicaba Eireli":
                case "Cursos Preparatórios Paulista Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_paulista.png";
                    break;
                case "Cursos Preparatórios Sorocaba Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_sorocaba.png";
                    break;
                case "Cursos Preparatórios Nacional Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_nacional.png";
                    break;
                case "Cursos Preparatórios E Pro. Educasp Ltda":
                case "Cursos Preparatórios Global Eireli":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/novo_logo_supletivo_urgente_centro.png";
                    break;
                // case "GCO Administração e Participações LTDA":
                //     logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                //     break;
                case "Cursos Preparatórios E Profissionalizantes Educasp Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo-nacionaltec.png";
                    break;
                case "Cursos Preparatórios E Profissionalizantes Abc Ltda":
                    logoUrl = "https://www.portaldoalunocurso.com.br/assets/logo_supletivo_abc.png";
                    break;
                default:
                    logoUrl = "";
                    break;
            }

            var urlGooglePlay = string.Empty;
            bool nacionalTec;
            if (reguaContatoFila.Unidade.Id == 10)
            {
                urlGooglePlay = "cursosnacionaltec";
                nacionalTec = true;
            }
            else
            {
                urlGooglePlay = "supletivoportal";
                nacionalTec = false;
            }

            string primeiroNome = string.Empty;
            if (!string.IsNullOrEmpty(reguaContatoFila?.Aluno?.Nome))
            {
                var nomes = reguaContatoFila.Aluno.Nome.Split(" ");
                primeiroNome = nomes[0];
            }

            int dias = DateTime.Now.Date.Subtract(reguaContatoFila.Pagamento.DataVencimento.Value.Date).Days;
            dias *= -1;
            rawHtml.Replace("{logo-unidade}", $"{logoUrl}");
            rawHtml.Replace("{nome-aluno}", $"{primeiroNome}");
            rawHtml.Replace("{dias}", $"{dias}");
            rawHtml.Replace("{tipo-curso}", nacionalTec ? "NacionalTec" : "Supletivo");
            rawHtml.Replace("{assunto-email}", reguaContatoFila.ReguaContatoRegras.Titulo.Replace("{nome-aluno}", reguaContatoFila.Unidade.NomeFantasia).Replace("{dias}", dias.ToString()));
            rawHtml.Replace("{saudacao}", $"Olá, <strong>{reguaContatoFila.Aluno.Nome}</strong>.");
            rawHtml.Replace("{data-vencimento}", reguaContatoFila.Pagamento.DataVencimento.Value.ToString("dd/MM/yy"));
            rawHtml.Replace("{codigo-barras}", reguaContatoFila.Pagamento.NumeroLinhaDigitavel);
            rawHtml.Replace("{urlGooglePlay}", urlGooglePlay);
            rawHtml.Replace("{facebook-unidade}", reguaContatoFila.Unidade.Contato.FaceBook);
            rawHtml.Replace("{youtube-unidade}", nacionalTec ? "https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw" : "https://www.youtube.com/channel/UCPE1FPk-zRY9sCso1SWgRVA");
            rawHtml.Replace("{instagram-unidade}", reguaContatoFila.Unidade.Contato.Instagram);
            rawHtml.Replace("{frase-efeito}", "O certificado escolar é o maior patrimônio do trabalhador.");
            rawHtml.Replace("{nome-unidade}", reguaContatoFila.Unidade.NomeFantasia);
            rawHtml.Replace("{endereco-unidade}", $"<span class=\"endereco\">{reguaContatoFila.Unidade.Endereco.Rua}<z>, {reguaContatoFila.Unidade.Endereco.Numero}, {reguaContatoFila.Unidade.Endereco.Bairro}, {reguaContatoFila.Unidade.Endereco.Cidade} - {reguaContatoFila.Unidade.Endereco.Estado}</span><br>");
            rawHtml.Replace("{telefone-unidade}", $"({reguaContatoFila.Unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2)}) {reguaContatoFila.Unidade.Contato.TelefoneFixoPrincipal.Substring(2, 4)}-{reguaContatoFila.Unidade.Contato.TelefoneFixoPrincipal.Substring(6)}");

            return rawHtml.ToString();
        }

        public static string MontarWhatsAppCobrancaReguaContato(ReguaContatoFila reguaContatoFila)
        {
            var rawHtml = new StringBuilder();

            rawHtml.Append(reguaContatoFila.ReguaContatoRegras.Texto);

            bool nacionalTec;
            if (reguaContatoFila.Unidade.Id == 10)
            {
                nacionalTec = true;
            }
            else
            {
                nacionalTec = false;
            }

            string primeiroNome = string.Empty;
            if (!string.IsNullOrEmpty(reguaContatoFila?.Aluno?.Nome))
            {
                var nomes = reguaContatoFila.Aluno.Nome.Split(" ");
                primeiroNome = nomes[0];
            }

            int dias = DateTime.Now.Date.Subtract(reguaContatoFila.Pagamento.DataVencimento.Value.Date).Days;
            dias *= -1;
            rawHtml.Replace("{dias}", $"{dias}");
            rawHtml.Replace("{tipo-curso}", nacionalTec ? "NacionalTec" : "Supletivo");
            rawHtml.Replace("{nome-aluno}", $"{primeiroNome}");
            rawHtml.Replace("{data-vencimento}", reguaContatoFila.Pagamento.DataVencimento.Value.ToString("dd/MM/yy"));
            rawHtml.Replace("{codigo-barras}", reguaContatoFila.Pagamento.NumeroLinhaDigitavel);
            rawHtml.Replace("{assinatura}", nacionalTec ? "Cursos NacionalTec" : $"Supletivo – {reguaContatoFila.Unidade.NomeFantasia}");
            rawHtml.Replace("{new-line}", Environment.NewLine);
            return rawHtml.ToString();
        }

        public static string MontarSMSCobrancaReguaContato(ReguaContatoFila reguaContatoFila)
        {
            var rawHtml = new StringBuilder();

            rawHtml.Append(reguaContatoFila.ReguaContatoRegras.Texto);

            bool nacionalTec;
            if (reguaContatoFila.Unidade.Id == 10)
            {
                nacionalTec = true;
            }
            else
            {
                nacionalTec = false;
            }

            string primeiroNome = string.Empty;
            if (!string.IsNullOrEmpty(reguaContatoFila?.Aluno?.Nome))
            {
                var nomes = reguaContatoFila.Aluno.Nome.Split(" ");
                primeiroNome = nomes[0];
            }

            rawHtml.Replace("{tipo-curso}", nacionalTec ? "NacionalTec" : "Supletivo");
            rawHtml.Replace("{nome-aluno}", $"{primeiroNome}");
            rawHtml.Replace("{data-vencimento}", reguaContatoFila.Pagamento.DataVencimento.Value.ToString("dd/MM/yy"));
            rawHtml.Replace("{assinatura}", nacionalTec ? "Cursos NacionalTec" : $"Supletivo – {reguaContatoFila.Unidade.NomeFantasia}");
            rawHtml.Replace("{new-line}", Environment.NewLine);
            return rawHtml.ToString();
        }
    }
}
