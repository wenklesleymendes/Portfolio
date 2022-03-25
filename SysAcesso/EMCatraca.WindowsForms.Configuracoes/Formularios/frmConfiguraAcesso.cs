using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Dtos;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Negocio.Enumeradores;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using EMCatraca.WindowsForms.Configuracoes.Helpers;
using EMCatraca.WindowsForms.Configuracoes.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    public partial class FrmConfiguraAcesso : FrmBase
    {
        private readonly IRepositorioAuditoria repositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();
        private readonly IRepositorioAluno repositorioAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAluno();
        private readonly IRepositorioProfessor repositorioProfessor = FabricaDeRepositorios.Instancia.CrieRepositorioProfessor();
        private readonly IRepositorioColaborador repositorioProfissionais = FabricaDeRepositorios.Instancia.CrieRepositorioColaborador();
        private readonly IRepositorioResponsavel repositorioResponsavel = FabricaDeRepositorios.Instancia.CrieRepositorioResponsavel();
        private readonly IRepositorioAutorizadoBuscarAluno repositorioAutorizadoBuscarAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAutorizadoBuscarAluno();
        private readonly IRepositorioOcorrencias repositorioOcorrencia = FabricaDeRepositorios.Instancia.CrieRepositorioOcorrencias();
        private readonly IRepositorioAtributosAdicionais repositorioAtributosAdicionais = FabricaDeRepositorios.Instancia.CrieRepositorioAtributosAdicionais();

        private readonly ConfiguracoesDto _paramentros = new ConfiguracoesDto();
        private bool _ehAdministrador;
        private string _addCatraca = "Adcionar Dispositivo";
        private string _editarCatraca = "Editar Dispositivo";

        public List<Dispositivo> Catracas = new List<Dispositivo>();

        public FrmConfiguraAcesso(bool ehAdministrador = false)
        {
            InitializeComponent();
            AjusteOperador(ehAdministrador);
        }

        private void AjusteOperador(bool ehAdministrador) => _ehAdministrador = ehAdministrador ? ehAdministrador : SessaoDoUsuario.Instancia.OperadorLogado.EhAdministrador;

        private void FrmConfiguracaoCatracas_Load(object sender, EventArgs e)
        {
            AjustaDadosArquivosCfgs();
            AjustaConfiguracoes();

            AjustaGridIntervaloAcesso();
            AjustaGridCatraca();

            NomeFuncao = "Configurações Acesso";

            if (!_ehAdministrador)
            {
                RemoverPaginaTbcConteudo(PaginaCatracas);
                RemoverPaginaTbcConteudo(PaginaServidor);
            }
        }

        private void AjustaGridCatraca()
        {
            MonteGridCatraca();
            AjustaDadosFormualrioCatraca();
            HabilitarCamposCatraca(false);

        }

        private void MonteGridCatraca()
        {
            dgvCatracas.Limpe();

            var helperCatraca = new DataGridViewHelper(dgvCatracas.DataGridView);
            helperCatraca.RemovaColunas();
            helperCatraca.AddColumn("Id", "Codigo", 35);
            helperCatraca.AddColumn("Descrição", "Descricao", 94);
            helperCatraca.AddColumn("IP", "IpCatraca", 95);
            helperCatraca.AddColumn("Porta", "PortaCatraca", 60);
            helperCatraca.AddColumn("Ambos", "EhGiroNormal", 60);
            helperCatraca.AddColumn("Entrada", "EhGiroEntrada", 60);
            helperCatraca.AddColumn("Saída", "EhGiroSaida", 60);
            helperCatraca.AddColumn("Invertido", "EhGiroInvertido", 65);

            _paramentros.TodosDispositivos = MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("emcatraca.catracas.cfg");
            var dispositivosOrdenados = _paramentros.TodosDispositivos.OrderBy(p => p.Codigo);

            dgvCatracas.Exiba(dispositivosOrdenados);
        }

        private void AjustaGridIntervaloAcesso()
        {
            dgvSelecaoIntervalos.Limpe();

            List<DtoIntervalo> intervalos = _paramentros.RegrasAcesso.IntervalosAcesso;
            if (Equals(intervalos, null))
            {
                CriaIntervalosPadrao();
            }

            MonteGridIntervalosAcesso();
            dgvSelecaoIntervalos.Exiba(intervalos);

            AjustaBtnsIntevalosAcessoPadrao();
        }

        private void AjustaBtnsIntevalosAcessoPadrao()
        {
            btnIntervaloNovo.Enabled = true;
            btnIntervaloAlterar.Enabled = true;
            btnIntervaloRemover.Enabled = true;
        }

        private void MonteGridIntervalosAcesso()
        {
            var helperCatraca = new DataGridViewHelper(dgvSelecaoIntervalos.DataGridView);
            helperCatraca.RemovaColunas();
            helperCatraca.AddColumn("Dia Semana", "SemanaDescricao", 115);
            helperCatraca.AddColumn("Inicio", "HoraInicial", 55);
            helperCatraca.AddColumn("Fim", "HoraFinal", 55);
            helperCatraca.AddColumn("Tipo Acesso", "TipoAcesso", 111);
        }

        private void CriaIntervalosPadrao()
        {
            var intervalos = new List<DtoIntervalo>();
            for (int i = 1; i <= 5; i++)
            {
                var intervaloEntrada = ObtenhaIntervaloEntrada(i);
                intervalos.Add(intervaloEntrada);

                var intervaloSaida = ObtenhaIntervaloSaida(i);
                intervalos.Add(intervaloSaida);
            }

            _paramentros.RegrasAcesso.IntervalosAcesso = intervalos;

            GravaRegraAcesso();
        }

        private DtoIntervalo ObtenhaIntervaloSaida(int i)
        {
            var dtoIntervelo = new DtoIntervalo
            {
                NumeroDia = i,
                HoraInicial = "12:00",
                HoraFinal = "23:59",
                TipoAcesso = "SAIDA"
            };

            dtoIntervelo.SemanaDescricao = dtoIntervelo.ObtenhaDiaSemanaPeloNumero(i);
            return dtoIntervelo;
        }

        private DtoIntervalo ObtenhaIntervaloEntrada(int i)
        {
            var dtoIntervelo = new DtoIntervalo
            {
                NumeroDia = i,
                HoraInicial = "06:00",
                HoraFinal = "11:59",
                TipoAcesso = "ENTRADA"
            };

            dtoIntervelo.SemanaDescricao = dtoIntervelo.ObtenhaDiaSemanaPeloNumero(i);
            return dtoIntervelo;
        }

        private void AdicionaPaginaTbAnaliseCFG(TabPage pagina) => tbcAnaliseCFG.TabPages.Add(pagina);

        private void AdicionaPaginaTbcConteudo(TabPage pagina) => tbcConteudo.TabPages.Add(pagina);

        private void RemoverPaginaTbcConteudo(TabPage pagina) => tbcConteudo.TabPages.Remove(pagina);

        private bool ExisteBloqueioDeAcesso(string tipoBloqueio) => tipoBloqueio == "SIM";

        private void AjustaConfiguracoes()
        {
            AjustaServidorDados();
            AjustaRegrasAcessoDados();
            AjustaCustomizacaoTipoPessoa();

            cboDiaSemana.SelectedIndex = ObtenhaDiaSemana();

            ConfigurarAlunoMensagensMonitor();
            ConfiguraAlunoLiberacaoAcesso();
            ConfiguraOutros();
            ConfiguraTeclado();
        }

        private void AjustaDadosArquivosCfgs()
        {
            var conexao = EnumeradorTipoCFG.Conexao.Descricao;
            _paramentros.InformacaoConexao = MapeadorArquivoJson.CarreguerJson<InformacaoConexao>(conexao, _paramentros);

            var loader = EnumeradorTipoCFG.Loader.Descricao;
            _paramentros.TipoIntegracao = MapeadorArquivoJson.CarreguerJson<CatracaLoader>(loader, _paramentros).TipoIntegracao;

            var acesso = EnumeradorTipoCFG.Acesso.Descricao;
            _paramentros.RegrasAcesso = MapeadorArquivoJson.CarreguerJson<RegrasAcesso>(acesso, _paramentros);

            var customizacaoTipoPessoa = EnumeradorTipoCFG.CTipoPessoa.Descricao;
            _paramentros.CutomizacaoTipoPessoa = MapeadorArquivoJson.CarreguerJson<CustomizacaoTipoPessoa>(customizacaoTipoPessoa, _paramentros);
        }

        private void BtnIntervaloAdicionar_Click(object sender, EventArgs e)
        {
            AdicionaNovoIntervaloGird();
            AtualizaCfgRegraAcessoIntervalos();
            AjustaBtnsIntevalosAcessoPadrao();
        }

        private void BtnIntervaloRemover_Click(object sender, EventArgs e)
        {
            dgvSelecaoIntervalos.RemovaItensSelecionados();
            AtualizaCfgRegraAcessoIntervalos();
            GravaRegraAcesso();
        }

        private void AtualizaCfgRegraAcessoIntervalos()
        {
            _paramentros.RegrasAcesso.IntervalosAcesso = dgvSelecaoIntervalos.ObtenhaTodosObjetos<DtoIntervalo>();
            GravaRegraAcesso();
        }

        private void GravaRegraAcesso() => MapeadorArquivoJson.Gravar(
            EnumeradorTipoCFG.Acesso.Descricao, _paramentros.RegrasAcesso);

        private void CboTecladoTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkTecladoTodos.Checked = false;
            chkTecladoIverter.Checked = false;
            CarregaListaPorTipoPessoa((ListaGenerica)cboTecladoTipoPessoa.SelectedItem);
        }

        private void TxtTecladoFiltrar_TextChanged(object sender, EventArgs e)
        {
            chkTecladoTodos.Checked = false;
            chkTecladoIverter.Checked = false;
            FiltreRegistrosPessoas();
        }

        private void ChkTecladoTodos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cklTecladoPessoas.Items.Count; i++)
            {
                cklTecladoPessoas.SetItemChecked(i, chkTecladoTodos.Checked);
            }

            ArmazeneSelecaoDePermissaoDoTecladoParaTipoPessoa();
        }

        private void ChkTecladoIverter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cklTecladoPessoas.Items.Count; i++)
            {
                cklTecladoPessoas.SetItemChecked(i, !cklTecladoPessoas.GetItemChecked(i));
            }

            ArmazeneSelecaoDePermissaoDoTecladoParaTipoPessoa();
        }

        private void CklTecladoPessoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArmazeneSelecaoDePermissaoDoTecladoParaTipoPessoa();
        }

        private void TxtServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == '.') && !char.IsControl(e.KeyChar);
        }

        private void TxtPorta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == '.') && !char.IsControl(e.KeyChar);
        }

        private static Auditoria MonteAuditoria()
        {
            var log = $"O Operador: " +
                      $"{SessaoDoUsuario.Instancia.OperadorLogado.Codigo} - " +
                      $"{SessaoDoUsuario.Instancia.OperadorLogado.Nome}, " +
                      $"realizou alterações nas configurações de acesso.";

            return new Auditoria(FuncaoAcesso.ConfiguracaoDeAcesso, "Salvar", log);
        }

        private int ObtenhaDiaSemana()
        {
            var dia = DateTime.Now;
            var dateValue = new DateTime(dia.Year, dia.Month, dia.Day);
            return (int)dateValue.DayOfWeek;
        }

        private void ConfiguraTeclado()
        {
            cboTecladoTipoPessoa.ValueMember = "Id";
            cboTecladoTipoPessoa.DisplayMember = "Descricao";
            cboTecladoTipoPessoa.Items.Clear();
            cboTecladoTipoPessoa.Items.Add(new ListaGenerica { Id = 1, Descricao = "Alunos" });
            cboTecladoTipoPessoa.Items.Add(new ListaGenerica { Id = 2, Descricao = "Professores" });
            cboTecladoTipoPessoa.Items.Add(new ListaGenerica { Id = 3, Descricao = "Colaboradores" });
            cboTecladoTipoPessoa.Items.Add(new ListaGenerica { Id = 5, Descricao = "Responsáveis pelo Aluno" });
            cboTecladoTipoPessoa.Items.Add(new ListaGenerica { Id = 6, Descricao = "Autorizados Buscar Alunos" });
        }

        private void ConfiguraOutros()
        {
            chkBloquearAcessoResponsavelSemMatricula.Checked = _paramentros.RegrasAcesso.BloquearAcessoResponsavelSemMatricula == "SIM";
            chkBloquearAcessoAutorizadoSemMatricula.Checked = _paramentros.RegrasAcesso.BloquearAcessoAutorizadoSemMatricula == "SIM";
            chkBloquearAcessoProfessorInativo.Checked = _paramentros.RegrasAcesso.BloquearAcessoProfessorInativo == "SIM";
            chkBloquearAcessoColaboradorInativo.Checked = _paramentros.RegrasAcesso.BloquearAcessoColaboradorInativo == "SIM";

            if (_paramentros.InformacaoConexao.Conexao != null)
            {
                var professores = cklBloquearAcessoProfessorComOcorrencias;
                var bloquearAcessoProfessorOcorrencias = _paramentros.RegrasAcesso.BloquearAcessoProfessorComOcorrencias;
                var ocorrencias = repositorioOcorrencia.ConsulteTodosOcorrencias().Select(o => new ListaGenerica
                {
                    Id = o.Id,
                    Descricao = o.Descricao

                }).ToList();

                MostreOcorrencias(professores, bloquearAcessoProfessorOcorrencias, ocorrencias);

                var colaboradores = cklNegarOcorrenciasColaborador;
                var bloquearAcessoColaboradorOcorrencias = _paramentros.RegrasAcesso.BloquearAcessoColaboradorComOcorrencias;
                MostreOcorrencias(colaboradores, bloquearAcessoColaboradorOcorrencias, ocorrencias);
            }
        }

        private void ConfiguraAlunoLiberacaoAcesso()
        {
            numTempoPassagemAutorizado.Value = _paramentros.RegrasAcesso.TempoParaAcessoLiberadoSegundos;
            chkFormLiberacao.Checked = _paramentros.RegrasAcesso.FormularioLiberaAcessoAluno == "SIM";
            chkResponsavelLibera.Checked = _paramentros.RegrasAcesso.ResponsaveisPodemLiberarAcessoAluno == "SIM";
            chkAutorizadoLibera.Checked = _paramentros.RegrasAcesso.AutorizadosPodemLiberamAcessoAluno == "SIM";

            if (_paramentros.InformacaoConexao.Conexao != null)
            {
                var professores = cklProfessores;
                var professoresPodemLiberarAlunos = _paramentros.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno;
                var professoresAtivos = repositorioProfessor.ConsulteTodosProfessorAtivos().Select(o => new ListaGenerica { Id = o.Id, Descricao = o.Nome }).ToList();
                MostreOcorrencias(professores, professoresPodemLiberarAlunos, professoresAtivos);

                var colaboradores = cklColaboradores;
                var colaboradoresPodeLiberarAcessoAluno = _paramentros.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno;
                var colaboradoresAtivos = repositorioProfissionais.ConsulteTodosColaboradorAtivos().Select(o => new ListaGenerica { Id = o.Id, Descricao = o.Nome }).ToList();
                MostreOcorrencias(colaboradores, colaboradoresPodeLiberarAcessoAluno, colaboradoresAtivos);
            }
        }

        private void ConfigurarAlunoMensagensMonitor()
        {
            chkMsgInadimplentes.Checked = _paramentros.RegrasAcesso.MensagemAlunoComInadimplenciaDeDuplicatas == "SIM"
                                          || _paramentros.RegrasAcesso.MensagemAlunoComInadimplenciaDeCheques == "SIM";

            chkMsgPendenteDocumento.Checked = _paramentros.RegrasAcesso.MensagemAlunoComPendenciaDeDocumentos == "SIM";
            chkMsgPendenteMateriais.Checked = _paramentros.RegrasAcesso.MensagemAlunoComPendenciaDeMateriais == "SIM";

            if (_paramentros.InformacaoConexao.Conexao != null)
            {
                var ocorrencias = cklMsgOcorrencias;
                var mensagemAlunoComOcorrencias = _paramentros.RegrasAcesso.MensagemAlunoComOcorrencias;
                var todasOcorrencias = repositorioOcorrencia.ConsulteTodosOcorrencias().Select(o =>
                new ListaGenerica
                {
                    Id = o.Id,
                    Descricao = o.Descricao
                }).ToList();

                MostreOcorrencias(ocorrencias, mensagemAlunoComOcorrencias, todasOcorrencias);
            }
        }

        private void AjustaRegrasAcessoDados()
        {
            chkBloquearAcessoAlunoSemMatricula.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoSemMatricula);
            chkBloquearAcessoAlunoInadimplente.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoInadimplente);
            chkBloquearAcessoAlunoComPendenciaMaterial.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoComPendenciaMaterial);
            chkNegarAlunoPendenteDocumento.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoComPendenciaDocumentos);
            chkUmAcessoPorIntervalo.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.IntervalosParaAcessoUnico);

            GraveTempoMinimoEmSegundosAcesso();

            if (_paramentros.InformacaoConexao.Conexao != null)
            {
                var ocorrenciasCadastrada = ObtenhaTodosOcorrencias();
                var ocorrenciasRegraAcesso = _paramentros.RegrasAcesso.BloquearAcessoAlunoComOcorrencias;
                MostreOcorrencias(cklOcorrenciasAluno, ocorrenciasRegraAcesso, ocorrenciasCadastrada);

                var atributosAdcionaisCadastrado = ObtenhaTodosAtributosAdcionais();
                var atributoRegraAcessoSairSozinho = _paramentros.RegrasAcesso.AtributoPodeSairSozinho;
                var atributosBloqueios = _paramentros.RegrasAcesso.AtributoBloqueado;
                MostreAtributosAdcionais(cboAtributoPodeSairSozinho, atributoRegraAcessoSairSozinho, atributosAdcionaisCadastrado);
                MostreAtributosAdcionais(cboAtributoBloqueado, atributosBloqueios, atributosAdcionaisCadastrado);
            }
        }

        private void AjustaTempoMinimoParaNovoAcesso() => _paramentros.RegrasAcesso.TempoMinimoParaNovoAcessoSegundos = (int)nmSegundoMinimoParaNovoAcesso.Value;

        private void GraveTempoMinimoEmSegundosAcesso() => nmSegundoMinimoParaNovoAcesso.Value = _paramentros.RegrasAcesso.TempoMinimoParaNovoAcessoSegundos;

        private List<ListaGenerica> ObtenhaTodosAtributosAdcionais()
        {
            return repositorioAtributosAdicionais.ConsulteTodosAtributosAdcionais()
                                                 .Select(o => new ListaGenerica
                                                 {
                                                     Id = o.Id,
                                                     Descricao = o.Descricao
                                                 }).ToList();
        }

        private List<ListaGenerica> ObtenhaTodosOcorrencias()
        {
            return repositorioOcorrencia.ConsulteTodosOcorrencias()
                                        .Select(o => new ListaGenerica
                                        {
                                            Id = o.Id,
                                            Descricao = o.Descricao
                                        }).ToList();
        }

        private void AjustaServidorDados()
        {
            txtServerIP.Text = _paramentros.InformacaoConexao.IP;
            txtPortaServidor.Text = _paramentros.InformacaoConexao.PortaTcpIp;
            rdbtConexaoWebApi.Checked = _paramentros.InformacaoConexao.EhWebAPI;
            rdbtConexaoBD.Checked = !_paramentros.InformacaoConexao.EhWebAPI;
            txtTipoIntegracao.Text = _paramentros.TipoIntegracao;
            txtServidor.Text = _paramentros.InformacaoConexao.Conexao;
        }

        private void AjustaCustomizacaoTipoPessoa()
        {
            bool existeConfiguracoesCustomizadaTipoPessoa = _paramentros.RegrasAcesso.ExisteConfiguracoesCustomizadaTipoPessoa == "SIM";

            if (existeConfiguracoesCustomizadaTipoPessoa)
            {
                ckbExisteCustomizacaoTipoPessoa.Checked = true;
                txtTipoPessoaAluno.Text = _paramentros.CutomizacaoTipoPessoa.Aluno.ToString();
                txtTipoPessoaProfessor.Text = _paramentros.CutomizacaoTipoPessoa.Professor.ToString();
                txtTipoPessoaProfissional.Text = _paramentros.CutomizacaoTipoPessoa.Profissional.ToString();
                txtTipoPessoaResponsavel.Text = _paramentros.CutomizacaoTipoPessoa.Responsavel.ToString();
                txtTipoPessoaAutorizadoBuscarAluno.Text = _paramentros.CutomizacaoTipoPessoa.AutorizadoBuscarAluno.ToString();
            }
        }

        private List<DtoIntervalo> ObtenhaIntervalosSelecionadoGrid()
         => dgvSelecaoIntervalos.ObtenhaObjetosSelecionados<DtoIntervalo>();

        private void MostreOcorrencias(CheckedListBox listBox, string ocorrenciasRegraAcesso, List<ListaGenerica> ocorrenciasCadastrada)
        {
            listBox.ValueMember = "Id";
            listBox.DisplayMember = "Descricao";
            listBox.Items.Clear();

            ocorrenciasRegraAcesso = ocorrenciasRegraAcesso ?? "";

            var i = 0;
            foreach (var dado in ocorrenciasCadastrada)
            {
                listBox.Items.Add(dado, false);
                foreach (string itemSelecionado in ocorrenciasRegraAcesso.Split(','))
                {
                    if (dado.Id.ToString() == itemSelecionado)
                    {
                        listBox.SetItemChecked(i, true);
                    }
                }

                i++;
            }
        }

        private void MostreAtributosAdcionais(ComboBox comboBox, int atributoRegraAcesso, List<ListaGenerica> atributosAdcionaisCadastrado)
        {
            comboBox.ValueMember = "Id";
            comboBox.DisplayMember = "Descricao";

            comboBox.Items.Clear();
            comboBox.Items.Add(new ListaGenerica { Id = 0, Descricao = "* Não Bloquear" });
            if (atributoRegraAcesso == 0)
            {
                comboBox.SelectedIndex = 0;
            }

            var i = 1;
            foreach (var dado in atributosAdcionaisCadastrado)
            {
                comboBox.Items.Add(dado);
                if (dado.Id == atributoRegraAcesso)
                {
                    comboBox.SelectedIndex = i;
                }

                i++;
            }
        }

        private string RecupereItensSelecionadosDaListaParaString(List<object> itens)
        {
            var itensLista = new List<int>();
            foreach (ListaGenerica item in itens)
            {
                itensLista.Add(item.Id);
            }

            return string.Join(",", Array.ConvertAll(itensLista.ToArray(), x => x.ToString()));
        }

        private void AjusteParaCriarNovoIntervalo()
        {
            cboDiaSemana.SelectedIndex = (int)DateTime.Now.DayOfWeek;
            mtbIntervaloHoraInicial.Text = "";
            mtbIntervaloHoraFinal.Text = "";
            rdbIntervaloEntrada.Checked = false;
            rdbIntervaloSaida.Checked = false;

            btnIntervaloAdicionar.Text = "Adicionar";
            btnIntervaloAdicionar.Enabled = true;
            btnIntervaloNovo.Enabled = false;
            btnIntervaloRemover.Enabled = false;
            btnIntervaloAlterar.Enabled = false;
        }

        private void AdicionaNovoIntervaloGird()
        {
            if (PodeAdicionarIntervaloAcesso())
            {
                var novoIntervalo = new DtoIntervalo()
                {
                    NumeroDia = cboDiaSemana.SelectedIndex,
                    HoraInicial = mtbIntervaloHoraInicial.Text,
                    HoraFinal = mtbIntervaloHoraFinal.Text,
                    TipoAcesso = (rdbIntervaloEntrada.Checked ? "ENTRADA" : "SAIDA")
                };

                novoIntervalo.SemanaDescricao = novoIntervalo.ObtenhaDiaSemanaPeloNumero(cboDiaSemana.SelectedIndex);
                dgvSelecaoIntervalos.AdicioneItem(novoIntervalo);
            }
        }

        private bool PodeAdicionarIntervaloAcesso()
        {
            if (string.IsNullOrEmpty(mtbIntervaloHoraInicial.Text))
            {
                MessageBox.Show("Informe o horário inicial do intervalo!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbIntervaloHoraInicial.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(mtbIntervaloHoraFinal.Text))
            {
                MessageBox.Show("Informe o horário final do intervalo!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbIntervaloHoraFinal.Focus();
                return false;
            }

            if (!(rdbIntervaloEntrada.Checked || rdbIntervaloSaida.Checked))
            {
                MessageBox.Show("Selecione se o período é de Entrada ou Saída!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rdbIntervaloEntrada.Focus();
                return false;
            }

            if (!DateTime.TryParse($"01/01/2000 {mtbIntervaloHoraInicial.Text}", out DateTime horaInicial))
            {
                MessageBox.Show("Hora inicial está inválida!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbIntervaloHoraInicial.Text = "";
                mtbIntervaloHoraInicial.Focus();
                return false;
            }

            if (!DateTime.TryParse($"01/01/2000 {mtbIntervaloHoraFinal.Text}", out DateTime horaFinal))
            {
                MessageBox.Show("Hora final está inválida!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbIntervaloHoraFinal.Text = "";
                mtbIntervaloHoraFinal.Focus();
                return false;
            }

            if (horaInicial >= horaFinal)
            {
                MessageBox.Show("A hora de início do período não pode ser igual ou maior que a hora final!",
                                "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                rdbIntervaloEntrada.Focus();
                return false;
            }

            return true;
        }

        private void CarregaListaPorTipoPessoa(ListaGenerica tipoPessoaSelecionada)
        {
            cklTecladoPessoas.ValueMember = "Id";
            cklTecladoPessoas.DisplayMember = "Descricao";

            pessoas = ObtenhaPessoas(Enumeradores.ObtenhaTipoPessoaPadrao(tipoPessoaSelecionada.Id));

            FiltreRegistrosPessoas();
        }

        private void FiltreRegistrosPessoas()
        {
            cklTecladoPessoas.Items.Clear();

            if (pessoas == null)
            {
                return;
            }

            IEnumerable<Pessoa> registrosFiltrados = pessoas;
            if (txtTecladoFiltrar.Text != "")
            {
                registrosFiltrados = pessoas.Where(p => p.Nome.IndexOf(txtTecladoFiltrar.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            var tipoPessoa = Enumeradores.ObtenhaTipoPessoaPadrao(((ListaGenerica)cboTecladoTipoPessoa.SelectedItem).Id);
            switch (tipoPessoa)
            {
                case TipoPessoa.Aluno:
                    itensSelecionados = _paramentros.RegrasAcesso.AlunosPodemDigitar;
                    break;

                case TipoPessoa.Professor:
                    itensSelecionados = _paramentros.RegrasAcesso.ProfessoresPodemDigitar;
                    break;

                case TipoPessoa.Profissional:
                    itensSelecionados = _paramentros.RegrasAcesso.ProfissionaisPodemDigitar;
                    break;

                case TipoPessoa.Responsavel:
                    itensSelecionados = _paramentros.RegrasAcesso.ResponsaveisPodemDigitar;
                    break;

                case TipoPessoa.AutorizadoBuscarAluno:
                    itensSelecionados = _paramentros.RegrasAcesso.AutorizadosPodemDigitar;
                    break;
            }

            var selecionados = new List<string>();
            if (!string.IsNullOrEmpty(itensSelecionados))
            {
                selecionados = itensSelecionados.Split(',').ToList();
            }

            foreach (Pessoa pessoa in registrosFiltrados)
            {
                bool chk = selecionados.Any(x => x == pessoa.Id.ToString());
                cklTecladoPessoas.Items.Add(new ListaGenerica { Id = pessoa.Id, Descricao = pessoa.Nome }, chk);
            }
        }

        private IEnumerable<Pessoa> ObtenhaPessoas(TipoPessoa tipoPessoa)
        {
            switch (tipoPessoa)
            {
                case TipoPessoa.Aluno:

                    return (IEnumerable<Pessoa>)repositorioAluno.ConsulteAlunosAtivos();

                case TipoPessoa.Professor:

                    return (IEnumerable<Pessoa>)repositorioProfessor.ConsulteTodosProfessorAtivos();

                case TipoPessoa.Profissional:

                    return (IEnumerable<Pessoa>)repositorioProfissionais.ConsulteTodosColaboradorAtivos();

                case TipoPessoa.Responsavel:

                    return (IEnumerable<Pessoa>)repositorioResponsavel.ConsulteTodosResponsavelAtivos();

                case TipoPessoa.AutorizadoBuscarAluno:

                    return (IEnumerable<Pessoa>)repositorioAutorizadoBuscarAluno.ConsulteAutorizadoBuscarAlunoAtivos();

                default:

                    return null;
            }
        }

        private void ArmazeneSelecaoDePermissaoDoTecladoParaTipoPessoa()
        {
            if (cboTecladoTipoPessoa.SelectedItem == null)
            {
                return;
            }

            var selecionados = new List<string>();
            if (!string.IsNullOrEmpty(itensSelecionados))
            {
                selecionados = itensSelecionados.Split(',').ToList();
            }

            for (int i = 0; i < cklTecladoPessoas.Items.Count; i++)
            {
                var existe = selecionados.Any(x => x == ((ListaGenerica)cklTecladoPessoas.Items[i]).Id.ToString());
                if (existe)
                {
                    if (!cklTecladoPessoas.GetItemChecked(i))
                    {
                        selecionados.Remove(((ListaGenerica)cklTecladoPessoas.Items[i]).Id.ToString());
                    }
                }
                else
                {
                    if (cklTecladoPessoas.GetItemChecked(i))
                    {
                        selecionados.Add(((ListaGenerica)cklTecladoPessoas.Items[i]).Id.ToString());
                    }
                }

                selecionados.Remove("");
            }

            itensSelecionados = string.Join(",", Array.ConvertAll(selecionados.ToArray(), x => x.ToString()));

            var tipoPessoa = Enumeradores.ObtenhaTipoPessoaPadrao(((ListaGenerica)cboTecladoTipoPessoa.SelectedItem).Id);

            switch (tipoPessoa)
            {
                case TipoPessoa.Aluno:
                    _paramentros.RegrasAcesso.AlunosPodemDigitar = itensSelecionados;
                    break;

                case TipoPessoa.Professor:
                    _paramentros.RegrasAcesso.ProfessoresPodemDigitar = itensSelecionados;
                    break;

                case TipoPessoa.Profissional:
                    _paramentros.RegrasAcesso.ProfissionaisPodemDigitar = itensSelecionados;
                    break;

                case TipoPessoa.Responsavel:
                    _paramentros.RegrasAcesso.ResponsaveisPodemDigitar = itensSelecionados;
                    break;

                case TipoPessoa.AutorizadoBuscarAluno:
                    _paramentros.RegrasAcesso.AutorizadosPodemDigitar = itensSelecionados;
                    break;
            }
        }

        private void GraveConfiguracao()
        {
            MapeadorArquivoJson.BackUpConfiguracao();

            RegraAcessoAluno();
            RegraAcessoMensagens();
            RegraAcessoLiberacao();
            RegrasAcessoOutros();
            RegraAcessoOcorrencias();

            var cfgAcesso = EnumeradorTipoCFG.Acesso.Descricao;
            MapeadorArquivoJson.Gravar(cfgAcesso,_paramentros.RegrasAcesso);

            if (ckbExisteCustomizacaoTipoPessoa.Checked)
            {
                CustomizacaoTipoPessoa();
                var cfgTipoPessoa = EnumeradorTipoCFG.CTipoPessoa.Descricao;
                MapeadorArquivoJson.Gravar(cfgTipoPessoa,_paramentros.CutomizacaoTipoPessoa);                
            }

            if (!IPAddress.TryParse(txtServerIP.Text, out IPAddress ip) || txtServerIP.Text.Count(c => c == '.') != 3)
            {
                tbcConteudo.SelectedTab = tbcConteudo.TabPages["tpBD"];
                txtServerIP.Focus();
                MessageBox.Show("O IP do servidor de banco de dados, está inválido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServerIP.Text = "";
            }

            if (txtPortaServidor.Text.Length < 4)
            {
                tbcConteudo.SelectedTab = tbcConteudo.TabPages["tpBD"];
                txtServerIP.Focus();
                MessageBox.Show("A porta de comunicação TCP-IP, está inválida!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServerIP.Text = "";
            }

            MapeadorArquivoJson.Gravar("EmCatraca.Loader.cfg", new CatracaLoader() 
            {
                TipoIntegracao = txtTipoIntegracao.Text 
            });

            MapeadorArquivoJson.Gravar("Emcatraca.Servidor.cfg", new InformacaoConexao() 
            { 
                Conexao = txtServidor.Text, 
                PortaTcpIp = txtPortaServidor.Text, 
                IP = txtServerIP.Text, 
                EhWebAPI = rdbtConexaoWebApi.Checked 
            });

        }

        private void RegraAcessoOcorrencias()
        {
            _paramentros.RegrasAcesso.BloquearAcessoProfessorComOcorrencias = RecupereItensSelecionadosDaListaParaString(
                cklBloquearAcessoProfessorComOcorrencias.CheckedItems.OfType<object>().ToList());

            _paramentros.RegrasAcesso.BloquearAcessoColaboradorComOcorrencias = RecupereItensSelecionadosDaListaParaString(
                cklNegarOcorrenciasColaborador.CheckedItems.OfType<object>().ToList());
        }

        private void RegrasAcessoOutros()
        {
            _paramentros.RegrasAcesso.BloquearAcessoResponsavelSemMatricula = chkBloquearAcessoResponsavelSemMatricula.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoAutorizadoSemMatricula = chkBloquearAcessoAutorizadoSemMatricula.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoProfessorInativo = chkBloquearAcessoProfessorInativo.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoColaboradorInativo = chkBloquearAcessoColaboradorInativo.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.ExisteConfiguracoesCustomizadaTipoPessoa = ckbExisteCustomizacaoTipoPessoa.Checked ? "SIM" : "NAO";
        }

        private void RegraAcessoLiberacao()
        {
            _paramentros.RegrasAcesso.TempoParaAcessoLiberadoSegundos = (int)numTempoPassagemAutorizado.Value;
            _paramentros.RegrasAcesso.FormularioLiberaAcessoAluno = chkFormLiberacao.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.ResponsaveisPodemLiberarAcessoAluno = chkResponsavelLibera.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.AutorizadosPodemLiberamAcessoAluno = chkAutorizadoLibera.Checked ? "SIM" : "NAO";

            _paramentros.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno = RecupereItensSelecionadosDaListaParaString(
                cklProfessores.CheckedItems.OfType<object>().ToList());

            _paramentros.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno = RecupereItensSelecionadosDaListaParaString(
                cklColaboradores.CheckedItems.OfType<object>().ToList());
        }

        private void CustomizacaoTipoPessoa()
        {
            _paramentros.CutomizacaoTipoPessoa = new CustomizacaoTipoPessoa
            {
                Aluno = Convert.ToInt32(txtTipoPessoaAluno.Text),
                Professor = Convert.ToInt32(txtTipoPessoaProfessor.Text),
                Profissional = Convert.ToInt32(txtTipoPessoaProfissional.Text),
                Responsavel = Convert.ToInt32(txtTipoPessoaResponsavel.Text),
                AutorizadoBuscarAluno = Convert.ToInt32(txtTipoPessoaAutorizadoBuscarAluno.Text)
            };
        }

        private void RegraAcessoMensagens()
        {
            _paramentros.RegrasAcesso.MensagemAlunoComInadimplenciaDeDuplicatas = chkMsgInadimplentes.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.MensagemAlunoComInadimplenciaDeCheques = chkMsgInadimplentes.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.MensagemAlunoComPendenciaDeDocumentos = chkMsgPendenteDocumento.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.MensagemAlunoComPendenciaDeMateriais = chkMsgPendenteMateriais.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.MensagemAlunoComOcorrencias = RecupereItensSelecionadosDaListaParaString(cklMsgOcorrencias.CheckedItems.OfType<object>().ToList());
        }

        private void RegraAcessoAluno()
        {
            _paramentros.RegrasAcesso.BloquearAcessoAlunoSemMatricula = chkBloquearAcessoAlunoSemMatricula.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoAlunoInadimplente = chkBloquearAcessoAlunoInadimplente.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoAlunoComPendenciaDocumentos = chkNegarAlunoPendenteDocumento.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoAlunoComPendenciaMaterial = chkBloquearAcessoAlunoComPendenciaMaterial.Checked ? "SIM" : "NAO";
            _paramentros.RegrasAcesso.BloquearAcessoAlunoComOcorrencias = RecupereItensSelecionadosDaListaParaString(cklOcorrenciasAluno.CheckedItems.OfType<object>().ToList());

            _paramentros.RegrasAcesso.AtributoPodeSairSozinho = 0;
            _paramentros.RegrasAcesso.AtributoBloqueado = 0;

            if (cboAtributoPodeSairSozinho.SelectedItem != null)
            {
                _paramentros.RegrasAcesso.AtributoPodeSairSozinho = ((ListaGenerica)cboAtributoPodeSairSozinho.SelectedItem).Id;
            }

            if (cboAtributoBloqueado.SelectedItem != null)
            {
                _paramentros.RegrasAcesso.AtributoBloqueado = ((ListaGenerica)cboAtributoBloqueado.SelectedItem).Id;
            }

            AjustaTempoMinimoParaNovoAcesso();

            _paramentros.RegrasAcesso.IntervalosParaAcessoUnico = chkUmAcessoPorIntervalo.Checked ? "SIM" : "NAO";
        }

        private IEnumerable<Pessoa> pessoas;
        private string itensSelecionados = "";

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_ehAdministrador)
                {
                    repositorioAuditoria.RegistreAuditoria(MonteAuditoria());
                }

                GraveConfiguracao();

                MessageBox.Show("Configurações gravadas com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorrou um erro durante a gravação das configurações:\n\r" + ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                AuditoriaLog.EscrevaErro(nameof(FrmConfiguraAcesso), ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AjustaConfiguracoes();
        }

        private void pnlConteudo_MouseMove(object sender, MouseEventArgs e)
        {
            MauseMouveFormulario(e);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIntervaloNovo_Click(object sender, EventArgs e)
        {
            AjusteParaCriarNovoIntervalo();
            mtbIntervaloHoraInicial.Focus();
        }
        private string[] AnalisaPadroes(string string_padrao)
        {
            if (string_padrao.Contains("("))
            {
                string_padrao = TextoEntre(string_padrao, "(", ")");
            }

            string[] resultado = string_padrao.Split(';');

            for (int i = 0; i < resultado.Length; i++)
            {
                resultado[i] = resultado[i].Trim();
            }

            return resultado;
        }

        private void ProcuraDiretorio(ListBox lstb, DirectoryInfo dir_info, string[] padroes)
        {
            foreach (string padrao in padroes)
            {
                foreach (FileInfo Arq_info in dir_info.GetFiles(padrao))
                {
                    ProcessaArquivo(lstb, Arq_info);
                }
            }

            foreach (DirectoryInfo subdir_info in dir_info.GetDirectories())
            {
                ProcuraDiretorio(lstb, subdir_info, padroes);
            }
        }

        private string TextoEntre(string txt, string delimitador1, string delimitador2)
        {
            int pos1 = txt.IndexOf(delimitador1);
            int texto_inicio = pos1 + delimitador1.Length;
            int pos2 = txt.IndexOf(delimitador2, texto_inicio);
            return txt.Substring(texto_inicio, pos2 - texto_inicio);
        }

        private void ProcessaArquivo(ListBox lstb, FileInfo arq_info)
        {
            try
            {
                lstb.Items.Add(arq_info.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar o arquivo " + arq_info.FullName + "\n" + ex.Message);
            }
        }

        private void pnlConteudo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtAtalhos.Select();
            txtAtalhos.Focus();
        }

        private void chkUmAcessoPorIntervalo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUmAcessoPorIntervalo.Checked)
            {
                nmSegundoMinimoParaNovoAcesso.Value = 0;
                nmSegundoMinimoParaNovoAcesso.Enabled = false;

                return;
            }

            nmSegundoMinimoParaNovoAcesso.Value = 1;
            nmSegundoMinimoParaNovoAcesso.Enabled = true;
        }

        private void dgvSelecaoIntervalos_Load(object sender, EventArgs e)
        {
            PreenchaCamposIntervaloSelecionado(ObtenhaIntervalosSelecionadoGrid().First());
        }

        private void btnCancelarIntervalo_Click(object sender, EventArgs e)
        {
            AjustaBtnsIntevalosAcessoPadrao();
            PreenchaCamposIntervaloSelecionado(ObtenhaIntervalosSelecionadoGrid().First());
        }

        private void PreenchaCamposIntervaloSelecionado(DtoIntervalo interveloSelecionado)
        {
            cboDiaSemana.SelectedIndex = interveloSelecionado.NumeroDia;
            mtbIntervaloHoraInicial.Text = interveloSelecionado.HoraInicial;
            mtbIntervaloHoraFinal.Text = interveloSelecionado.HoraFinal;
            rdbIntervaloEntrada.Checked = Equals(interveloSelecionado.TipoAcesso, "ENTRADA");
            rdbIntervaloSaida.Checked = !rdbIntervaloEntrada.Checked;
        }

        private void dgvSelecaoIntervalos_Click(object sender, EventArgs e)
        {
            PreenchaCamposIntervaloSelecionado(ObtenhaIntervalosSelecionadoGrid().First());
        }

        private void btnIncluirCatraca_Click(object sender, EventArgs e)
        {
            HabilitarCamposCatraca(true);

            txtCodigoCatraca.Value = _paramentros.TodosDispositivos.Any()
                                        ? _paramentros.TodosDispositivos.Last().Codigo + 1
                                        : 1;

            LimpaDadosCatraca();

            btnEditarCatraca.Text = _addCatraca;

            txtIPCatraca.Focus();
            txtIPCatraca.Select();
        }

        private void LimpaDadosCatraca()
        {
            txtDescricaoCatraca.Clear();
            txtIPCatraca.Clear();
            txtPortaCatraca.Clear();
        }

        private void HabilitarCamposCatraca(bool habilitar)
        {
            txtCodigoCatraca.Enabled = habilitar;
            txtDescricaoCatraca.Enabled = habilitar;
            txtIPCatraca.Enabled = habilitar;
            txtPortaCatraca.Enabled = habilitar;

            rdbGiroNormal.Enabled = habilitar;
            rdbGiroEntrada.Enabled = habilitar;
            rdbGiroSaida.Enabled = habilitar;
            rdbGiroInvertido.Enabled = habilitar;
        }

        private void btnEditarCatraca_Click(object sender, EventArgs e)
        {
            string atualizarCatraca = "Atualizar Dispositivo";

            if (Equals(_addCatraca, btnEditarCatraca.Text))
            {
                AdicionaNovaCatracaGird();
                GravaCatraca();
                AjustaBtnsIntevalosAcessoPadrao();

                btnEditarCatraca.Text = _editarCatraca;

                return;
            }

            if (Equals(_editarCatraca, btnEditarCatraca.Text))
            {
                AjustaDadosFormualrioCatraca();
                btnEditarCatraca.Text = atualizarCatraca;
                HabilitarCamposCatraca(true);

                return;
            }

            if (Equals(atualizarCatraca, btnEditarCatraca.Text))
            {
                AtualizaDadosCatraca();
                GravaCatraca();
                btnCancelarCatraca_Click(sender, e);
            }
        }

        private void GravaCatraca()
        {
            MapeadorArquivoJson.BackUpConfiguracao();
            MapeadorArquivoJson.Gravar("Emcatraca.Catracas.cfg", _paramentros.TodosDispositivos);
        }

        private void AdicionaNovaCatracaGird()
        {
            var todosDispositivos = _paramentros.TodosDispositivos;
            if (PodeAdcionarCatraca())
            {
                var catraca = new Dispositivo()
                {
                    Codigo = (int)txtCodigoCatraca.Value,
                    Descricao = txtDescricaoCatraca.Text,
                    IpCatraca = txtIPCatraca.Text,
                    PortaCatraca = txtPortaCatraca.Text,
                    EhGiroNormal = rdbGiroNormal.Checked,
                    EhGiroEntrada = rdbGiroEntrada.Checked,
                    EhGiroSaida = rdbGiroEntrada.Checked,
                    EhGiroInvertido = rdbGiroInvertido.Checked
                };

                todosDispositivos.Add(catraca);
                dgvCatracas.AdicioneItem(catraca);

                _paramentros.TodosDispositivos = todosDispositivos;
            }
        }

        private bool PodeAdcionarCatraca()
        {
            if (!CodigoCatracaEhValido())
            {
                MessageBox.Show("O numero da catraca ja existe um cadastro!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoCatraca.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescricaoCatraca.Text))
            {
                MessageBox.Show("Informe a descrição da catraca!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricaoCatraca.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtIPCatraca.Text))
            {
                MessageBox.Show("Informe o IP da catraca!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIPCatraca.Focus();
                return false;
            }

            return true;
        }

        private bool CodigoCatracaEhValido()
        {
            var existeDispositivo = _paramentros.TodosDispositivos.Exists(p => p.Codigo == txtCodigoCatraca.Value);
            if (existeDispositivo)
            {
                return false;
            }

            return true;
        }

        private void rdbGiroNormal_CheckedChanged(object sender, EventArgs e)
        {
            AjustaGiroCatraca();
        }

        private void AjustaGiroCatraca()
        {
            _paramentros.Dispositivo = dgvCatracas.ObtenhaObjetosSelecionados<Dispositivo>().First();
            _paramentros.Dispositivo.EhGiroNormal = rdbGiroNormal.Checked;
            _paramentros.Dispositivo.EhGiroEntrada = rdbGiroEntrada.Checked;
            _paramentros.Dispositivo.EhGiroSaida = rdbGiroSaida.Checked;
            _paramentros.Dispositivo.EhGiroInvertido = rdbGiroInvertido.Checked;

            MonteGridCatraca();

            var catracasAlteradas = new List<Dispositivo>();
            foreach (var item in _paramentros.TodosDispositivos)
            {
                if (item.Codigo == _paramentros.Dispositivo.Codigo)
                {
                    catracasAlteradas.Add(_paramentros.Dispositivo);
                    continue;
                }

                catracasAlteradas.Add(item);
            }

            _paramentros.TodosDispositivos = catracasAlteradas;
        }

        private void AtualizaDadosCatraca()
        {
            var idCatraca = (int)txtCodigoCatraca.Value;
            foreach (var catraca in _paramentros.TodosDispositivos)
            {
                if (idCatraca == catraca.Codigo)
                {
                    catraca.Codigo = (int)txtCodigoCatraca.Value;
                    catraca.Descricao = txtDescricaoCatraca.Text;
                    catraca.IpCatraca = txtIPCatraca.Text;
                    catraca.PortaCatraca = txtPortaCatraca.Text;
                    catraca.EhGiroNormal = rdbGiroNormal.Checked;
                    catraca.EhGiroEntrada = rdbGiroEntrada.Checked;
                    catraca.EhGiroSaida = rdbGiroSaida.Checked;
                    catraca.EhGiroInvertido = rdbGiroInvertido.Checked;
                }
            }

            dgvCatracas.Exiba(_paramentros.TodosDispositivos);
        }

        private void rdbGiroEntrada_CheckedChanged(object sender, EventArgs e)
        {
            AjustaGiroCatraca();
        }

        private void rdbGiroSaida_CheckedChanged(object sender, EventArgs e)
        {
            AjustaGiroCatraca();
        }

        private void rdbInverterGiro_CheckedChanged(object sender, EventArgs e)
        {
            AjustaGiroCatraca();
        }

        private void txtDescricaoCatraca_Leave(object sender, EventArgs e)
        {
            if (txtDescricaoCatraca.Text.Length > 0)
            {
                var texto = txtDescricaoCatraca.Text.ToUpper();
                txtDescricaoCatraca.Text = texto;
            }
        }

        private void AjustaDadosFormualrioCatraca()
        {
            Dispositivo catracaSelecionada = dgvCatracas.ObtenhaObjetosSelecionados<Dispositivo>().First();
            txtCodigoCatraca.Value = catracaSelecionada.Codigo;
            txtDescricaoCatraca.Text = catracaSelecionada.Descricao;
            txtIPCatraca.Text = catracaSelecionada.IpCatraca;
            txtPortaCatraca.Text = catracaSelecionada.PortaCatraca;

            if (catracaSelecionada.EhGiroNormal)
            {
                rdbGiroNormal.Checked = true;
            }

            if (catracaSelecionada.EhGiroEntrada)
            {
                rdbGiroEntrada.Checked = true;
            }

            if (catracaSelecionada.EhGiroSaida)
            {
                rdbGiroSaida.Checked = true;
            }

            if (catracaSelecionada.EhGiroInvertido)
            {
                rdbGiroInvertido.Checked = true;
            }
        }

        private void btnCancelarCatraca_Click(object sender, EventArgs e)
        {
            LimpaDadosCatraca();
            HabilitarCamposCatraca(false);
            AjustaDadosFormualrioCatraca();

            btnEditarCatraca.Text = _editarCatraca;
        }

        private void ckbExisteCustomizacaoTipoPessoa_CheckedChanged(object sender, EventArgs e) => 
            gbIndentificadorTipoPessoa.Enabled = ckbExisteCustomizacaoTipoPessoa.Checked;

    }
}