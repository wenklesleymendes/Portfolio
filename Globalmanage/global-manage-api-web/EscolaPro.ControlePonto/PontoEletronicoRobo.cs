using EscolaPro.Core.Model.ControlePontoEletronico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using EscolaPro.Repository.Interfaces;
using System.Reflection.Metadata;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace EscolaPro.ControlePonto
{
    public class PontoEletronicoRobo : IPontoEletronicoRobo
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public PontoEletronicoRobo(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<PontoEletronico>> MontarArquivo(string path, string nomeArquivo)
        {
            try
            {

                List<PontoEletronico> pontoEletronicos = new List<PontoEletronico>();

                RegistraLogPonto.Log(path, TipoResquisicao.Sucesso, "Controle de Ponto", "MontarArquivo");

                string[] readText = File.ReadAllLines(path);

                PontoEletronico pontoEletronico = null;

                List<ObjetoMarcacao> objetoMarcacaos = new List<ObjetoMarcacao>();

                foreach (var item in readText)
                {
                    if (item.Length == 38 || item.Length == 34)
                    {
                        var dateExtra = item.Substring(10, 12);

                        var diaMesAno = dateExtra.Substring(0, 2) + "/" + dateExtra.Substring(2, 2) + "/" + dateExtra.Substring(4, 4);
                        var horario = dateExtra.Substring(8, 2) + ":" + dateExtra.Substring(10, 2) + ":00";


                        var validarData = Regex.IsMatch(diaMesAno.Replace("/", ""), @"^\d+$");

                        if (diaMesAno != "00/00/0000" && validarData)
                        {
                            var dataConverter = $"{diaMesAno} {horario}";

                            var dataCompleta = DateTime.Parse(dataConverter, CultureInfo.CreateSpecificCulture("pt-BR"));

                            string pis = item.Substring(23, 11);

                            objetoMarcacaos.Add(new ObjetoMarcacao { DataMarcacao = dataCompleta, PIS = pis });
                        }
                    }
                }

                var old = DateTime.Now;
                bool inicio = false;
                int countData = 1;

                var listaPIS = await _funcionarioRepository.ListaPIS();

                foreach (var funcionarioRobo in listaPIS)
                {
                    var horariosRegistradoLista = objetoMarcacaos.Where(x => x.PIS == funcionarioRobo.NumeroPIS).ToList();

                    //foreach (var item in horariosRegistradoLista)
                    for (int i = 0; i < horariosRegistradoLista.Count; i++)
                    {
                        var item = horariosRegistradoLista[i];

                        if (old.Date != item.DataMarcacao.Date)
                        {
                            if (inicio)
                            {
                                pontoEletronico.NumeroPIS = funcionarioRobo.NumeroPIS;
                                pontoEletronico.FuncionarioId = funcionarioRobo.FuncionarioId;

                                if (funcionarioRobo.RegimeContratacaoAntigo.HasValue)
                                {
                                    if (pontoEletronico.Entrada1.HasValue)
                                    {
                                        if (funcionarioRobo.DataRegimeAnterior != null)
                                        {
                                            if (pontoEletronico.Entrada1.Value.Date <= funcionarioRobo.DataRegimeAnterior.Value.Date)
                                            {
                                                pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacaoAntigo.Value;
                                            }
                                            else
                                            {
                                                pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;
                                            }
                                        }
                                        else
                                        {
                                            pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;
                                        }
                                    }
                                }
                                else
                                {
                                    pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;
                                }


                                if (pontoEletronico.Entrada1.HasValue)
                                {
                                    pontoEletronico.NomeArquivo = nomeArquivo;
                                    pontoEletronicos.Add(pontoEletronico);
                                }

                                countData = 1;
                            }

                            pontoEletronico = new PontoEletronico();
                        }

                        switch (countData)
                        {
                            case 1:
                                pontoEletronico.Entrada1 = item.DataMarcacao;
                                break;
                            case 2:
                                pontoEletronico.Saida1 = item.DataMarcacao;
                                break;
                            case 3:
                                pontoEletronico.Entrada2 = item.DataMarcacao;
                                break;
                            case 4:
                                pontoEletronico.Saida2 = item.DataMarcacao;
                                break;
                            case 5:
                                pontoEletronico.Entrada3 = item.DataMarcacao;
                                break;
                            case 6:
                                pontoEletronico.Saida3 = item.DataMarcacao;
                                break;
                            case 7:
                                pontoEletronico.Entrada4 = item.DataMarcacao;
                                break;
                            case 8:
                                pontoEletronico.Saida4 = item.DataMarcacao;
                                break;
                            default:
                                break;
                        }

                        countData++;
                        old = item.DataMarcacao;
                        inicio = true;

                        if (horariosRegistradoLista.Last() == horariosRegistradoLista[i])
                        {
                            pontoEletronico.NumeroPIS = funcionarioRobo.NumeroPIS;
                            pontoEletronico.FuncionarioId = funcionarioRobo.FuncionarioId;
                            //pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;

                            if (funcionarioRobo.RegimeContratacaoAntigo.HasValue)
                            {
                                if (pontoEletronico.Entrada1.HasValue)
                                {
                                    if (funcionarioRobo.DataRegimeAnterior != null)
                                    {
                                        if (pontoEletronico.Entrada1.Value.Date <= funcionarioRobo.DataRegimeAnterior.Value.Date)
                                        {
                                            pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacaoAntigo.Value;
                                        }
                                        else
                                        {
                                            pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;
                                        }
                                    }
                                    else
                                    {
                                        pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;
                                    }
                                }
                            }
                            else
                            {
                                pontoEletronico.RegimeContratacao = funcionarioRobo.RegimeContratacao;
                            }

                            if (pontoEletronico.Entrada1.HasValue)
                            {
                                pontoEletronico.NomeArquivo = nomeArquivo;
                                pontoEletronicos.Add(pontoEletronico);
                            }

                            //pontoEletronicos.Add(pontoEletronico);
                            countData = 1;

                            pontoEletronico = new PontoEletronico();
                        }

                    }
                }

                return pontoEletronicos;
            }
            catch (Exception ex)
            {
                RegistraLogPonto.Log(ex.Message, TipoResquisicao.Error, "Controle de Ponto", "Montar arquivo");
                throw ex;
            }
        }
    }
}
