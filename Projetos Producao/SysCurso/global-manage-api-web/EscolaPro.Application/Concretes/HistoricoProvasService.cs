using AutoMapper;
using EscolaPro.API.Dto;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Repository.Repository;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class HistoricoProvasService : IHistoricoProvasService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IProvaAlunoRepository _provaAlunoRepository;
        private readonly IMapper _mapper;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnidadeTransporteProvaRepository _unidadeTransporteProvaRepository;
        private readonly IAgendaProvaRepository _agendaProvaRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMensagemTicketRepository _mensagemTicketRepository;

        public HistoricoProvasService(IAlunoRepository alunoRepository,
                                      IMapper mapper,
                                      IProvaAlunoRepository provaAlunoRepository,
                                      IMatriculaAlunoRepository matriculaAlunoRepository,
                                      IUsuarioRepository usuarioRepository,
                                      IUnidadeTransporteProvaRepository unidadeTransporteProvaRepository,
                                      IAgendaProvaRepository agendaProvaRepository,
                                      ICursoRepository cursoRepository,
                                      ITicketRepository ticketRepository,
                                      IMensagemTicketRepository mensagemTicketRepository)
        {
            _alunoRepository = alunoRepository;
            _provaAlunoRepository = provaAlunoRepository;
            _mapper = mapper;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _usuarioRepository = usuarioRepository;
            _unidadeTransporteProvaRepository = unidadeTransporteProvaRepository;
            _agendaProvaRepository = agendaProvaRepository;
            _cursoRepository = cursoRepository;
            _ticketRepository = ticketRepository;
            _mensagemTicketRepository = mensagemTicketRepository;
        }

        public async Task<List<HistoricoProvas>> ListaColegioAutorizadoExcel(FiltroHistoricoProvas filtro)
        {
            try
            {
                var alunos = await _alunoRepository.GetAllAsync();

                var provaAlunos = await _provaAlunoRepository.BuscarTodas();
                var provaAluno = filtrarProvaAluno(provaAlunos, filtro);
                //var provaAluno = _provaAlunoRepository.BuscarFiltro(filtro);

                List<HistoricoProvas> _historicoProvas = new List<HistoricoProvas>();

                foreach (var item in provaAluno)
                {
                    //var alunoMatricula = _alunoRepository.BuscarPorMatriculaId(item.MatriculaAlunoId);
                    var alunoMatricula = await _matriculaAlunoRepository.GetByIdAsync(item.MatriculaAlunoId);

                    var alunoMatriculaMap = _mapper.Map<DtoMatriculaAluno>(alunoMatricula);

                    var dadosCurso = await _cursoRepository.GetByIdAsync(alunoMatriculaMap.CursoId);

                    var dadosAluno = await _alunoRepository.BuscarPorId(alunoMatricula.AlunoId);

                    var dadosAlunoMap = _mapper.Map<DtoAluno>(dadosAluno);

                    string curso = "";
                    if (dadosAlunoMap != null)
                    {
                        if (dadosCurso.Descricao == "Ensino Fundamental")
                        {
                            curso = "000001-EJA ENSINO FUNDAMENTAL";
                        }
                        else if (dadosCurso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                                    dadosCurso.Descricao == "Ensino Fundamental e Médio" ||
                                    dadosCurso.Descricao == "Ensino Médio")
                        {
                            curso = "000002-EJA ENSINO MÉDIO";
                        }
                        else
                        {
                            curso = dadosCurso.Descricao;
                        }

                        HistoricoProvas historicoProvas = new HistoricoProvas
                        {
                            parceiro = "00032-André Coutinho",
                            nomecompleto = dadosAlunoMap.Nome,
                            datadenascimento = dadosAlunoMap.DataNascimento.ToString("dd/MM/yyyy"),
                            sexo = dadosAlunoMap.Sexo.ToString(),
                            estadocivil = dadosAlunoMap.EstadoCivil.ToString(),
                            naturalidade = dadosAlunoMap.Naturalidade != null ? dadosAlunoMap.Naturalidade.Descricao : "",
                            nacionalidade = dadosAlunoMap.Nacionalidade.Descricao,
                            nomedamae = dadosAlunoMap.NomeMae,
                            nomedopai = "",
                            email = "inedsuporte@gmail.com",
                            rg = dadosAlunoMap.RG,
                            orgaoexpedidor = dadosAlunoMap.OrgaoExpedicao,
                            ufrg = "",
                            cpf = dadosAlunoMap.CPF,
                            titulodeeleitor = dadosAlunoMap.TituloEleitoral,
                            zonaeleitoral = dadosAlunoMap.Zona,
                            secaoeleitoral = dadosAlunoMap.Secao,
                            enderecoresidencial = dadosAlunoMap.Endereco.Rua,
                            numeroresidencial = dadosAlunoMap.Endereco.Numero,
                            complementoresidencial = dadosAlunoMap.Endereco.Complemento,
                            bairroresidencial = dadosAlunoMap.Endereco.Bairro,
                            cidaderesidencial = dadosAlunoMap.Endereco.Cidade,
                            ufresidencial = dadosAlunoMap.Endereco.Estado,
                            cepresidencial = dadosAlunoMap.Endereco.CEP,
                            dddfixoresidencial = (dadosAlunoMap.Contato.TelefoneFixo != null && dadosAlunoMap.Contato.TelefoneFixo.Trim() != "") ? dadosAlunoMap.Contato.TelefoneFixo.Substring(0, 2) : "",
                            telefonefixoresidencial = (dadosAlunoMap.Contato.TelefoneFixo != null && dadosAlunoMap.Contato.TelefoneFixo.Trim() != "") ? dadosAlunoMap.Contato.TelefoneFixo.Remove(0, 2) : "",
                            dddcelularresidencial = (dadosAlunoMap.Contato.Celular != null && dadosAlunoMap.Contato.Celular.Trim() != "") ? dadosAlunoMap.Contato.Celular.Substring(0, 2) : "",
                            celularresidencial = (dadosAlunoMap.Contato.Celular != null && dadosAlunoMap.Contato.Celular.Trim() != "") ? dadosAlunoMap.Contato.Celular.Remove(0, 2) : "",
                            curso = curso,
                        };

                        _historicoProvas.Add(historicoProvas);
                    }
                }

                return _historicoProvas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<HistoricoProvas>> ListaGeralDeInscritosParaProvaExcel(FiltroHistoricoProvas filtro)
        {
            try
            {
                var alunos = await _alunoRepository.GetAllAsync();

                var provaAlunos = await _provaAlunoRepository.BuscarTodas();
                var provaAluno = filtrarProvaAluno(provaAlunos, filtro);
                //var provaAlunos = _provaAlunoRepository.BuscarFiltro(filtro);

                List<HistoricoProvas> _historicoProvas = new List<HistoricoProvas>();

                foreach (var item in provaAluno)
                {
                    var quant = provaAlunos.Where(x => x.MatriculaAlunoId == item.MatriculaAlunoId).Count();
                    
                    //var provas = _provaAlunoRepository.BuscarInscritoPorMatricula(item.MatriculaAlunoId);

                    var tickets = await _ticketRepository.BuscarPorMatriculaId(item.MatriculaAlunoId);
                    var analiseDocumentacaooProva = tickets.Where(x => x.AssuntoTicketId == 5);
                    var ultimoTicket = analiseDocumentacaooProva.LastOrDefault();
                    //Finalizado = 4

                    var alunoMatricula = await _matriculaAlunoRepository.GetByIdAsync(item.MatriculaAlunoId);

                    var alunoMatriculaMap = _mapper.Map<DtoMatriculaAluno>(alunoMatricula);

                    var dadosCurso = await _cursoRepository.GetByIdAsync(alunoMatriculaMap.CursoId);

                    var dadosAluno = await _alunoRepository.BuscarPorId(alunoMatricula.AlunoId);

                    var dadosAlunoMap = _mapper.Map<DtoAluno>(dadosAluno);

                    string nome = "", dataTicket = "";
                    if (ultimoTicket != null)
                    {
                        var mensagemTicketLista = await _mensagemTicketRepository.BuscarPorTicket(ultimoTicket.Id);
                        var mensagemTicket = mensagemTicketLista.Where(x => (int)x.StatusTicket == 4).FirstOrDefault();
                        if (mensagemTicket != null)
                        {
                            var usuario = await _usuarioRepository.BuscarPorId(mensagemTicket.UsuarioLogadoId);

                            if ((usuario != null) && ((int)ultimoTicket.Status == 4))
                            {
                                if (usuario.Funcionario != null)
                                    nome = usuario.Funcionario.Nome;

                                dataTicket = ultimoTicket.DestinatarioTicket.Last().DataAtendimento.ToString("dd/MM/yyyy");
                            }
                        }
                    }

                    if (dadosAlunoMap != null)
                    {
                        HistoricoProvas historicoProvas = new HistoricoProvas
                        {
                            nomecompleto = dadosAlunoMap.Nome,
                            rg = dadosAlunoMap.RG,
                            curso = dadosCurso.Descricao,
                            statusprova = ((int)item.StatusProva == 2 && quant == 1) ? "OK" : "REPROVA",
                            unidade = dadosAlunoMap.Unidade.Nome,
                            //docconferidopor = usuario != null ? usuario.UserName : "",
                            //docconferidopor = usuario != null ? ((int)ultimoTicket.Status == 4 ? usuario.UserName : "") : "",
                            docconferidopor = nome,
                            //dataconferenciadoc = item.DataProva?.ToString("dd/MM/yyyy"),
                            dataconferenciadoc = dataTicket,
                            emailporcpf = dadosAlunoMap.RG.Replace(".", "").Substring(0, 3) + 
                                          dadosAlunoMap.CPF.Substring(0, 3) +
                                          "@gmail.com"
                        };

                        _historicoProvas.Add(historicoProvas);
                    }
                }

                return _historicoProvas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<ChamadaOnibus> ListaDeChamadaOnibusExcel(FiltroHistoricoProvas filtro)
        {
            try
            {
                ChamadaOnibus chamadaOnibus = new ChamadaOnibus();
                List<HistoricoProvas> _historicoProvas = new List<HistoricoProvas>();
                //chamadaOnibus.HistoricoProvas = new ICollection<HistoricoProvas>();

                var alunos = await _alunoRepository.GetAllAsync();

                var provaAlunos = await _provaAlunoRepository.BuscarTodas();
                var provaAluno = filtrarProvaAluno(provaAlunos, filtro);
                //var provaAluno = _provaAlunoRepository.BuscarFiltro(filtro);

                var unidadeTransporteProva = await _unidadeTransporteProvaRepository.GetByIdAsync(filtro.onibus);
                var unidadeParticipanteProva = _agendaProvaRepository.BuscarUnidadeParticipante(unidadeTransporteProva.AgendaProvaId).FirstOrDefault();
                var agendaProva = await _agendaProvaRepository.BuscarPorId(unidadeTransporteProva.AgendaProvaId);

                chamadaOnibus.numeroonibus = unidadeTransporteProva.NumeroOnibus.ToString();
                chamadaOnibus.localsaida = unidadeParticipanteProva.LocalSaida;
                //chamadaOnibus.destino = agendaProva.ColegioAutorizado.NomeColegioAutorizado;
                chamadaOnibus.destino = agendaProva.ColegioAutorizado.Endereco.Rua + ", "
                                      + agendaProva.ColegioAutorizado.Endereco.Numero + " - "
                                      + agendaProva.ColegioAutorizado.Endereco.Bairro + " - "
                                      + agendaProva.ColegioAutorizado.Endereco.Cidade + " - "
                                      + agendaProva.ColegioAutorizado.Endereco.Estado + " - CEP: "
                                      + agendaProva.ColegioAutorizado.Endereco.CEP;
                chamadaOnibus.dataprova = agendaProva.DataProva?.ToString("dd/MM/yyyy");
                chamadaOnibus.horariosaidaonibus = unidadeParticipanteProva.HoraSaida;

                foreach (var item in provaAluno)
                {
                    var alunoMatricula = await _matriculaAlunoRepository.GetByIdAsync(item.MatriculaAlunoId);
                    var alunoMatriculaMap = _mapper.Map<DtoMatriculaAluno>(alunoMatricula);

                    var dadosCurso = await _cursoRepository.GetByIdAsync(alunoMatriculaMap.CursoId);

                    var dadosAluno = await _alunoRepository.BuscarPorId(alunoMatricula.AlunoId);
                    var dadosAlunoMap = _mapper.Map<DtoAluno>(dadosAluno);

                    if (dadosAlunoMap != null)
                    {
                        //chamadaOnibus.HistoricoProvas = new HistoricoProvas { 
                        HistoricoProvas historicoProvas = new HistoricoProvas
                        {
                            nomecompleto = dadosAlunoMap.Nome,
                            rg = dadosAlunoMap.RG,
                            curso = dadosCurso.Descricao,
                            //numeroonibus = item.UnidadeTransporteProva != null ? item.UnidadeTransporteProva.NumeroOnibus.ToString() : "",
                            //localsaida = item.UnidadeTransporteProva != null ? item.UnidadeTransporteProva.UnidadeParticipanteProva.LocalSaida : "",
                            //destino = item.LocalProva,
                            //dataprova = item.DataProva.ToString(),
                            //horariosaidaonibus = item.UnidadeTransporteProva != null ? item.UnidadeTransporteProva.UnidadeParticipanteProva.HoraSaida : ""
                        };

                        _historicoProvas.Add(historicoProvas);
                        
                    }
                }
                chamadaOnibus.HistoricoProvas = new List<HistoricoProvas>(_historicoProvas.ToList());

                return chamadaOnibus;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnidadeTransporteProva>> NumeroOnibus(int idUnidade, string dataInicioMatricula, string dataFimMatricula)
        {
            try
            {
                var provaAluno = await _provaAlunoRepository.BuscarTodas();

                List<UnidadeTransporteProva> unidadeTransporteProvas = new List<UnidadeTransporteProva>();
                foreach (var item in provaAluno)
                {
                    if(dataInicioMatricula != null && dataInicioMatricula != "null" &&
                          dataFimMatricula != null && dataFimMatricula != "null")
                    {
                        string format = "ddd MMM dd yyyy HH:mm:ss 'GMT'K";

                        var dataInicioMatriculaValidFormat = DateTime.ParseExact(dataInicioMatricula, format, CultureInfo.InvariantCulture);
                        var dataFimMatriculaValidFormat = DateTime.ParseExact(dataFimMatricula, format, CultureInfo.InvariantCulture);

                        if (dataInicioMatriculaValidFormat != null && dataFimMatriculaValidFormat != null)
                        {
                            if (item.DataProva != null)
                            {
                                if (item.DataProva?.Date >= dataInicioMatriculaValidFormat &&
                                    item.DataProva?.Date <= dataFimMatriculaValidFormat)
                                {
                                    if (item.UnidadeTransporteProvaId != null && item.AgendaProvaId != null)
                                    {
                                        var unidadeTransporteProva = _unidadeTransporteProvaRepository.BuscarPorUnidadeEAgendaProvaEUnidadeTransporteProva((int)item.AgendaProvaId, idUnidade, (int)item.UnidadeTransporteProvaId);

                                        foreach (var item2 in unidadeTransporteProva)
                                        {
                                            item2.UnidadeParticipanteProva = null;
                                        }
                                        unidadeTransporteProvas.AddRange(unidadeTransporteProva);
                                    }
                                }
                            }
                        }
                    }
                }

                if (unidadeTransporteProvas.Count > 0)
                {
                    unidadeTransporteProvas = unidadeTransporteProvas.DistinctBy(x => x.Id).ToList();
                    //unidadeTransporteProvas = unidadeTransporteProvas.DistinctBy(x => x.NumeroOnibus).ToList();
                }

                return unidadeTransporteProvas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProvaAluno> filtrarProvaAluno(IEnumerable<ProvaAluno> provaAlunos, FiltroHistoricoProvas filtro)
        {
            try
            {
                var alunoMatricula = _matriculaAlunoRepository.GetAllAsync().Result;

                var filtrado = false;
                List<ProvaAluno> provaAlunoLocal = new List<ProvaAluno>();
                if (filtro.unidadeSelect != null)
                {
                    foreach (var item in provaAlunos)
                    {
                        var matriculaAluno = alunoMatricula.Where(x => x.Id == item.MatriculaAlunoId).FirstOrDefault();

                        foreach (var item2 in filtro.unidadeSelect)
                        {
                            if(item2 == matriculaAluno.UnidadeId)
                            {
                                provaAlunoLocal.Add(item);
                            }
                        }
                    }
                    filtrado = true;
                }
                if (filtro.onibus != null)
                {
                    if (filtrado)
                        provaAlunoLocal = provaAlunoLocal.Where(x => x.UnidadeTransporteProvaId == filtro.onibus).ToList();
                    else
                        provaAlunoLocal = provaAlunos.Where(x => x.UnidadeTransporteProvaId == filtro.onibus).ToList();
                    filtrado = true;
                }
                if (filtro.colegioSelect != null)
                {
                    if (filtrado)
                        provaAlunoLocal = provaAlunoLocal.Where(x => x.LocalProva == filtro.colegioSelect).ToList();
                    else
                        provaAlunoLocal = provaAlunos.Where(x => x.LocalProva == filtro.colegioSelect).ToList();
                    filtrado = true;
                }
                if (filtro.tipoProva != null)
                {
                    if (filtrado)
                        provaAlunoLocal = provaAlunoLocal.Where(x => x.TipoProva == (TipoProvaEnum)filtro.tipoProva).ToList();
                    else
                        provaAlunoLocal = provaAlunos.Where(x => x.TipoProva == (TipoProvaEnum)filtro.tipoProva).ToList();
                    filtrado = true;
                }
                if (filtro.statusProva != null)
                {
                    if (filtrado)
                        provaAlunoLocal = provaAlunoLocal.Where(x => x.StatusProva == (StatusProvaEnum)filtro.statusProva).ToList();
                    else
                        provaAlunoLocal = provaAlunos.Where(x => x.StatusProva == (StatusProvaEnum)filtro.statusProva).ToList();
                    filtrado = true;
                }
                if (filtro.dataInicioMatricula != null)
                {
                    if (filtro.tipoProva != 2)
                    {
                        if (filtrado)
                            provaAlunoLocal = provaAlunoLocal.Where(x => x.DataProva >= filtro.dataInicioMatricula).ToList();
                        else
                            provaAlunoLocal = provaAlunos.Where(x => x.DataProva >= filtro.dataInicioMatricula).ToList();
                    }
                    else
                    {
                        if (filtrado)
                            provaAlunoLocal = provaAlunoLocal.Where(x => x.DataInscricao >= filtro.dataInicioMatricula).ToList();
                        else
                            provaAlunoLocal = provaAlunos.Where(x => x.DataInscricao >= filtro.dataInicioMatricula).ToList();
                    }
                    filtrado = true;
                }
                if (filtro.dataFimMatricula != null)
                {
                    if (filtro.tipoProva != 2)
                    {
                        if (filtrado)
                            provaAlunoLocal = provaAlunoLocal.Where(x => x.DataProva <= filtro.dataFimMatricula).ToList();
                        else
                            provaAlunoLocal = provaAlunos.Where(x => x.DataProva <= filtro.dataFimMatricula).ToList();
                    }
                    else
                    {
                        if (filtrado)
                            provaAlunoLocal = provaAlunoLocal.Where(x => x.DataInscricao <= filtro.dataFimMatricula).ToList();
                        else
                            provaAlunoLocal = provaAlunos.Where(x => x.DataInscricao <= filtro.dataFimMatricula).ToList();
                    }
                    filtrado = true;
                }

                if(provaAlunoLocal.Count > 0)
                {
                    provaAlunoLocal = provaAlunoLocal.DistinctBy(x=>x.Id).ToList();
                }

                if (filtro.unidadeSelect == null &&
                    filtro.onibus == null &&
                    filtro.colegioSelect == null &&
                    filtro.tipoProva == null &&
                    filtro.dataInicioMatricula == null &&
                    filtro.dataFimMatricula == null &&
                    filtro.statusProva == null)
                {
                    provaAlunoLocal = provaAlunos.ToList();
                }

                return provaAlunoLocal.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
