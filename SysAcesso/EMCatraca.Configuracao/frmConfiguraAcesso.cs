using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Negocio.Enumeradores;
using EMCatraca.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace EMCatraca.Configuracao
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

		private ConfiguracoesDto _paramentros = new ConfiguracoesDto();

		private bool _exibaListaPeriodos = false;
		private bool _ehAdministrador;

		public FrmConfiguraAcesso(bool ehModoDepuracao = false)
		{
			InitializeComponent();

			_ehAdministrador = !ehModoDepuracao
							   ? SessaoDoUsuario.Instancia.OperadorLogado.EhAdministrador
							   : ehModoDepuracao;
		}

		private void FrmConfiguracaoCatracas_Load(object sender, EventArgs e)
		{
			ObtenhaParametros();
			AjustaConfiguracoes();

			NomeFuncao = "Configurações Acesso";

			RemoverPaginaTbcConteudo(PaginaCFG);

			if (!_ehAdministrador)
			{
				RemoverPaginaTbcConteudo(PaginaCatracas);
				RemoverPaginaTbcConteudo(PaginaServidor);
			}
		}

		private void AdicionaPaginaTbAnaliseCFG(TabPage pagina) => tbcAnaliseCFG.TabPages.Add(pagina);

		private void AdicionaPaginaTbcConteudo(TabPage pagina) => tbcConteudo.TabPages.Add(pagina);

		private void RemoverPaginaTbcConteudo(TabPage pagina) => tbcConteudo.TabPages.Remove(pagina);

		private void txtAtalhos_KeyDown(object sender, KeyEventArgs e)
		{
			Atalhos(e);
		}

		private bool ExisteBloqueioDeAcesso(string tipoBloqueio) => tipoBloqueio == "SIM";

		private void AjustaConfiguracoes()
		{

			MostraDadosServidor();
			MostreDadosRegrasAcesso();

			cboDiaSemana.SelectedIndex = ObtenhaDiaSemana();

			ConfigurarAlunoMensagensMonitor();
			ConfiguraAlunoLiberacaoAcesso();
			ConfiguraOutros();
			ConfiguraTeclado();
		}

		private void ObtenhaParametros()
		{
			_paramentros.InformacaoConexao = MapeadorArquivoJson.CarreguerArquivoJsonParametros<InformacaoConexao>(EnumeradorTipoCFG.InformacaoConexao.Descricao, _paramentros);
			_paramentros.TipoIntegracao = MapeadorArquivoJson.CarreguerArquivoJson<CatracaLoader>("Emcatraca.Loader.cfg").TipoIntegracao;
			_paramentros.RegrasAcesso = MapeadorArquivoJson.CarreguerArquivoJson<RegrasAcesso>("Emcatraca.Acesso.cfg");
		}

		private void CboDiaSemana_SelectedIndexChanged(object sender, EventArgs e)
		{
			ExibaIntervalosDiaSemana();
		}

		private void LstIntervalos_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_exibaListaPeriodos)
			{
				CarreguePeriodoSelecionado();
			}

			_exibaListaPeriodos = true;
		}

		private void BtnIntervaloAdicionar_Click(object sender, EventArgs e)
		{
			AtualizeIntervalosNaLista();
		}

		private void BtnIntervaloRemover_Click(object sender, EventArgs e)
		{
			if (lstIntervalos.SelectedIndex > -1)
			{
				_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo.RemoveAt(lstIntervalos.SelectedIndex);
				ExibaIntervalosDiaSemana();
			}
		}

		private void BtnIntervaloCopiar_Click(object sender, EventArgs e)
		{
			//if (cboDiaSemana.SelectedIndex > -1)
			//{
			//    int diaAnterior = cboDiaSemana.SelectedIndex - 1;
			//    if (diaAnterior == -1)
			//    {
			//        diaAnterior = 6;
			//    }

			//    _regrasDeAcesso.BloquearAcessoPorIntervalos[cboDiaSemana.SelectedIndex] = _regrasDeAcesso.BloquearAcessoPorIntervalos[diaAnterior].Clone();
			//    _regrasDeAcesso.BloquearAcessoPorIntervalos[cboDiaSemana.SelectedIndex].DiaSemana = cboDiaSemana.SelectedIndex;
			//    ExibaIntervalosDiaSemana();
			//    cboDiaSemana.Focus();
			//}
		}

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

		private void MostreDadosRegrasAcesso()
		{
			chkBloquearAcessoAlunoSemMatricula.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoSemMatricula);
			chkBloquearAcessoAlunoInadimplente.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoInadimplente);
			chkBloquearAcessoAlunoComPendenciaMaterial.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoComPendenciaMaterial);
			chkNegarAlunoPendenteDocumento.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.BloquearAcessoAlunoComPendenciaDocumentos);
			chkIntervaloUnico.Checked = ExisteBloqueioDeAcesso(_paramentros.RegrasAcesso.IntervalosParaAcessoUnico);

			numNovoAcessoAluno.Value = _paramentros.RegrasAcesso.TempoMinimoParaNovoAcessoSegundos;

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

		private void MostraDadosServidor()
		{
			txtCaminhoBanco.Text = _paramentros.InformacaoConexao.Conexao;
			txtServerIP.Text = _paramentros.InformacaoConexao.IP;
			txtPorta.Text = _paramentros.InformacaoConexao.PortaTcpIp;
			rdbtConexaoWebApi.Checked = _paramentros.InformacaoConexao.EhWebAPI;
			rdbtConexaoBD.Checked = !_paramentros.InformacaoConexao.EhWebAPI;
			txtTipoIntegracao.Text = _paramentros.TipoIntegracao;
			txtServidor.Text = _paramentros.InformacaoConexao.Conexao;
		}

		private void CarreguePeriodoSelecionado()
		{
			if (lstIntervalos.SelectedIndex > -1)
			{
				var item = lstIntervalos.SelectedItem.ToString().Split(' ');
				mtbIntervaloHoraInicial.Text = item[0];
				mtbIntervaloHoraFinal.Text = item[2];
				rdoIntervaloEntrada.Checked = item[4] == "Entrada";
				rdoIntervaloSaida.Checked = item[4] == "Saída";
				btnIntervaloAdicionar.Text = "Alterar";
			}
		}

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
			lstIntervalos.SelectedIndex = -1;
			btnIntervaloAdicionar.Text = "Adicionar";
			mtbIntervaloHoraInicial.Text = "";
			mtbIntervaloHoraFinal.Text = "";
			rdoIntervaloEntrada.Checked = false;
			rdoIntervaloSaida.Checked = false;
		}

		private void ExibaIntervalosDiaSemana()
		{
			if (_paramentros.RegrasAcesso.IntervalosParaAcesso != null && cboDiaSemana.SelectedIndex != -1)
			{
				lstIntervalos.DataSource = _paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo
														  .OrderBy(x => x.HoraInicial)
														  .Select(a => a.HoraInicial + " até " + a.HoraFinal + " - " +
														  (a.Tipo == "ENTRADA" ? "Entrada" : "Saída")).ToList();

				AjusteParaCriarNovoIntervalo();
			}
		}

		private void AtualizeIntervalosNaLista()
		{
			_exibaListaPeriodos = false;
			if (ValideDadosIntevalo())
			{
				if (lstIntervalos.SelectedIndex == -1)
				{
					var intervalo = new Horarios
					{
						HoraInicial = mtbIntervaloHoraInicial.Text,
						HoraFinal = mtbIntervaloHoraFinal.Text,
						Tipo = (rdoIntervaloEntrada.Checked ? "ENTRADA" : "SAIDA")
					};

					_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo.Add(intervalo);
				}
				else
				{
					_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo[lstIntervalos.SelectedIndex].HoraInicial = mtbIntervaloHoraInicial.Text;
					_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo[lstIntervalos.SelectedIndex].HoraFinal = mtbIntervaloHoraFinal.Text;
					_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo[lstIntervalos.SelectedIndex].Tipo = rdoIntervaloEntrada.Checked ? "ENTRADA" : "SAIDA";
				}

				if (_paramentros.RegrasAcesso.IntervalosParaAcesso.Count <= 0)
				{
					return;
				}

				if (_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo.Count <= 0)
				{
					return;
				}

				_paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo = _paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo.OrderBy(x => x.HoraInicial).ToList();

				lstIntervalos.DataSource = _paramentros.RegrasAcesso.IntervalosParaAcesso[cboDiaSemana.SelectedIndex].Intervalo
					.OrderBy(x => x.HoraInicial)
					.Select(a => a.HoraInicial + " até " + a.HoraFinal + " - " + (a.Tipo == "ENTRADA" ? "Entrada" : "Saída"))
					.ToList();

				AjusteParaCriarNovoIntervalo();
				mtbIntervaloHoraInicial.Focus();
			}
		}

		private bool ValideDadosIntevalo()
		{
			if (string.IsNullOrEmpty(mtbIntervaloHoraInicial.Text))
			{
				MessageBox.Show("Informe o horário inicial do período!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				mtbIntervaloHoraInicial.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(mtbIntervaloHoraFinal.Text))
			{
				MessageBox.Show("Informe o horário final do período!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				mtbIntervaloHoraFinal.Focus();
				return false;
			}

			if (!(rdoIntervaloEntrada.Checked || rdoIntervaloSaida.Checked))
			{
				MessageBox.Show("Selecione se o período é de Entrada ou Saída!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				rdoIntervaloEntrada.Focus();
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

				rdoIntervaloEntrada.Focus();
				return false;
			}

			return true;
		}

		private void CarregaListaPorTipoPessoa(ListaGenerica tipoPessoaSelecionada)
		{
			cklTecladoPessoas.ValueMember = "Id";
			cklTecladoPessoas.DisplayMember = "Descricao";

			pessoas = ObtenhaPessoas(Enumeradores.ObtenhaTipoPessoa(tipoPessoaSelecionada.Id));

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

			var tipoPessoa = Enumeradores.ObtenhaTipoPessoa(((ListaGenerica)cboTecladoTipoPessoa.SelectedItem).Id);
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

			var tipoPessoa = Enumeradores.ObtenhaTipoPessoa(((ListaGenerica)cboTecladoTipoPessoa.SelectedItem).Id);

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

			MapeadorArquivoJson.Gravar<RegrasAcesso>(EnumeradorTipoCFG.RegrasDeAcesso.Descricao, _paramentros.RegrasAcesso);

			if (!IPAddress.TryParse(txtServerIP.Text, out IPAddress ip) || txtServerIP.Text.Count(c => c == '.') != 3)
			{
				tbcConteudo.SelectedTab = tbcConteudo.TabPages["tpBD"];
				txtServerIP.Focus();
				MessageBox.Show("O IP do servidor de banco de dados, está inválido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtServerIP.Text = "";
			}

			if (txtPorta.Text.Length < 4)
			{
				tbcConteudo.SelectedTab = tbcConteudo.TabPages["tpBD"];
				txtServerIP.Focus();
				MessageBox.Show("A porta de comunicação TCP-IP, está inválida!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtServerIP.Text = "";
			}

			MapeadorArquivoJson.Gravar<CatracaLoader>("EmCatraca.Loader.cfg", new CatracaLoader() { TipoIntegracao = txtTipoIntegracao.Text });
			MapeadorArquivoJson.Gravar<InformacaoConexao>("Emcatraca.Servidor.cfg", new InformacaoConexao() { Conexao = txtCaminhoBanco.Text, PortaTcpIp = txtPorta.Text, IP = txtServerIP.Text, EhWebAPI = rdbtConexaoWebApi.Checked });
		}

		private void RegraAcessoOcorrencias()
		{
			_paramentros.RegrasAcesso.BloquearAcessoProfessorComOcorrencias = RecupereItensSelecionadosDaListaParaString(cklBloquearAcessoProfessorComOcorrencias.CheckedItems.OfType<object>().ToList());
			_paramentros.RegrasAcesso.BloquearAcessoColaboradorComOcorrencias = RecupereItensSelecionadosDaListaParaString(cklNegarOcorrenciasColaborador.CheckedItems.OfType<object>().ToList());
		}

		private void RegrasAcessoOutros()
		{
			_paramentros.RegrasAcesso.BloquearAcessoResponsavelSemMatricula = chkBloquearAcessoResponsavelSemMatricula.Checked ? "SIM" : "NAO";
			_paramentros.RegrasAcesso.BloquearAcessoAutorizadoSemMatricula = chkBloquearAcessoAutorizadoSemMatricula.Checked ? "SIM" : "NAO";
			_paramentros.RegrasAcesso.BloquearAcessoProfessorInativo = chkBloquearAcessoProfessorInativo.Checked ? "SIM" : "NAO";
			_paramentros.RegrasAcesso.BloquearAcessoColaboradorInativo = chkBloquearAcessoColaboradorInativo.Checked ? "SIM" : "NAO";
		}

		private void RegraAcessoLiberacao()
		{
			_paramentros.RegrasAcesso.TempoParaAcessoLiberadoSegundos = (int)numTempoPassagemAutorizado.Value;
			_paramentros.RegrasAcesso.FormularioLiberaAcessoAluno = chkFormLiberacao.Checked ? "SIM" : "NAO";
			_paramentros.RegrasAcesso.ResponsaveisPodemLiberarAcessoAluno = chkResponsavelLibera.Checked ? "SIM" : "NAO";
			_paramentros.RegrasAcesso.AutorizadosPodemLiberamAcessoAluno = chkAutorizadoLibera.Checked ? "SIM" : "NAO";
			_paramentros.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno = RecupereItensSelecionadosDaListaParaString(cklProfessores.CheckedItems.OfType<object>().ToList());
			_paramentros.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno = RecupereItensSelecionadosDaListaParaString(cklColaboradores.CheckedItems.OfType<object>().ToList());
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

			_paramentros.RegrasAcesso.TempoMinimoParaNovoAcessoSegundos = (int)numNovoAcessoAluno.Value;
			_paramentros.RegrasAcesso.IntervalosParaAcessoUnico = chkIntervaloUnico.Checked ? "SIM" : "NAO";
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
				AuditoriaLog.WriteError(ex);
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

		private void AjusteBuscaDeAquivoPadrao() => cboPadraoArquivos.SelectedIndex = 0;

		private void ProcuraArquivos(ListBox listBox, string pastaInicial, string arquivoBuscar)
		{
			try
			{
				listBox.Items.Clear();
				string[] padroes = AnalisaPadroes(arquivoBuscar);
				var dir_info = new DirectoryInfo(pastaInicial);
				ProcuraDiretorio(listBox, dir_info, padroes);
				lbArquivosEncontrado.Text = $"Foram encontrados {listBox.Items.Count} arquivos.";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnDiretorioInicial_Click(object sender, EventArgs e)
		{
			try
			{
				fbd1.SelectedPath = txtPastaInicial.Text;
				if (fbd1.ShowDialog() == DialogResult.OK)
				{
					txtPastaInicial.Text = fbd1.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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

		private void Atalhos(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					AdicionaPaginaTbcConteudo(PaginaCFGValidacoes);
					tbcConteudo.SelectedTab = PaginaCFGValidacoes;
					break;

				case Keys.Escape:
					RemoverPaginaTbcConteudo(PaginaCFGValidacoes);
					break;

				case Keys.Enter:
					var operador = new Operador()
					{
						Codigo = 1,
						EhAdministrador = true,
						Nome = "ADMINISTRADOR"
					};

					Autentique(operador, txtCfgValidarSenha.Text.Trim());
					break;
			}
		}

		private void pnlConteudo_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			txtAtalhos.Select();
			txtAtalhos.Focus();
		}

		public void Autentique(Operador operador, string senhaInformada)
		{
			if (!ValidacaoDeSenhaOperador.SenhaInformadaEhValida(operador, senhaInformada))
			{
				MessageBox.Show("Senha Informa não e do Operador Administador!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCfgValidarSenha.Clear();
			}
			else
			{
				SessaoDoUsuario.Instancia.OperadorLogado = operador;
				RemoverPaginaTbcConteudo(PaginaCFGValidacoes);
				AdicionaPaginaTbcConteudo(PaginaCFGBusca);
				tbcConteudo.SelectedTab = PaginaCFGBusca;
			}
		}

		private void txtCfgValidarSenha_KeyDown(object sender, KeyEventArgs e)
		{
			Atalhos(e);
		}

		private void btnLocalizar_Click(object sender, EventArgs e)
		{
			if (txtPastaInicial.Text.Length == 0)
			{
				txtPastaInicial.Text = System.AppDomain.CurrentDomain.BaseDirectory;
			}

			AjusteBuscaDeAquivoPadrao();

			if (!string.IsNullOrEmpty(txtPastaInicial.Text) && cboPadraoArquivos.SelectedIndex != -1)
			{
				ProcuraArquivos(lstCaminhoCfg, txtPastaInicial.Text, cboPadraoArquivos.Text);
			}
			else
			{
				MessageBox.Show("Defina a pasta incial de busca e/ou o padrão de arquivos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void lbArquivos_DoubleClick(object sender, EventArgs e)
		{
			if (lstCaminhoCfg.SelectedIndex > -1)
			{
				var enderecoArquivo = lstCaminhoCfg.SelectedItem.ToString();

				int indice = enderecoArquivo.Contains("EmCatraca.")
							  ? lstCaminhoCfg.SelectedItem.ToString().IndexOf("EmCatraca.")
							  : lstCaminhoCfg.SelectedItem.ToString().IndexOf("emcatraca.");

				var nomeArquivo = lstCaminhoCfg.SelectedItem.ToString().Substring(indice);
				lbCfgValidarSenha.Text = $"Caminho do CFG: {enderecoArquivo}";

				if (nomeArquivo.Contains("Acesso") || nomeArquivo.Contains("acesso"))
				{
					MapeadorArquivoJson.CarreguerArquivoJsonParametros<RegistroAcesso>(EnumeradorTipoCFG.RegrasDeAcesso.Descricao, _paramentros);

					LimpaCFG();

					tbcAnaliseCFG.TabPages.Clear();
					btnLocalizar_Click(sender, e);

					FormataJson(nomeArquivo, PaginaCfgAcesso, listBoxAcesso);

				}
				else if (nomeArquivo.Contains("Servidor") || nomeArquivo.Contains("servidor"))
				{
					MapeadorArquivoJson.CarreguerArquivoJsonParametros<InformacaoConexao>(EnumeradorTipoCFG.InformacaoConexao.Descricao, _paramentros);

					LimpaCFG();

					tbcAnaliseCFG.TabPages.Clear();
					btnLocalizar_Click(sender, e);

					FormataJson(nomeArquivo, PaginaCfgServidor, listBoxServidor);
				}
				else if (nomeArquivo.Contains("Loader") || nomeArquivo.Contains("loader"))
				{
					MapeadorArquivoJson.CarreguerArquivoJsonParametros<CatracaLoader>(EnumeradorTipoCFG.RegrasDeAcesso.Descricao, _paramentros);

					LimpaCFG();

					tbcAnaliseCFG.TabPages.Clear();
					btnLocalizar_Click(sender, e);

					FormataJson(nomeArquivo, PaginaCfgLoader, listBoxLoader);
				}
				else if (nomeArquivo.Contains("Catracas") || nomeArquivo.Contains("catracas"))
				{
					MapeadorArquivoJson.CarreguerArquivoJsonParametros<Dispositivo>(EnumeradorTipoCFG.Dispositivo.Descricao, _paramentros);

					LimpaCFG();

					tbcAnaliseCFG.TabPages.Clear();
					btnLocalizar_Click(sender, e);

					FormataJson(nomeArquivo, PaginaCfgCatracas, listBoxCatracas);
				}
				else if (nomeArquivo.Contains("Liberacao") || nomeArquivo.Contains("liberacao"))
				{
					MapeadorArquivoJson.CarreguerArquivoJsonParametros<Liberacao>(EnumeradorTipoCFG.Liberacoes.Descricao, _paramentros);

					LimpaCFG();

					tbcAnaliseCFG.TabPages.Clear();
					btnLocalizar_Click(sender, e);

					FormataJson(nomeArquivo, PaginaCfgLiberacao, listBoxLiberacao);
				}
			}
		}

		private void FormataJson(string nomeArquivo, TabPage pagina, ListBox listBox)
		{
			AdicionaPaginaTbcConteudo(PaginaCFG);
			AdicionaPaginaTbAnaliseCFG(PaginaCFGBusca);
			AdicionaPaginaTbAnaliseCFG(pagina);

			var enderecoJson = $@"{System.AppDomain.CurrentDomain.BaseDirectory}Catraca\Log\{nomeArquivo}.log";
			var texto = string.Empty;
			if (File.Exists(enderecoJson))
			{
				string[] arquivos = File.ReadAllLines(enderecoJson);

				foreach (var letra in arquivos.SelectMany(json => json))
				{
					if (Equals(letra, '{'))
					{
						listBox.Items.Add($"  {letra}");
					}

					if (Equals(letra, ','))
					{
						listBox.Items.Add($"          {texto}");
						texto = string.Empty;
					}

					if (Equals(letra, '}'))
					{
						listBox.Items.Add($"  {letra}");
					}
				}
			}
			else
			{
				listBox.Items.Add("Arquivo não foi encontrado!");
				listBox.Items.Add(enderecoJson);
			}

			tbcConteudo.SelectedTab = PaginaCFG;
			tbcAnaliseCFG.SelectedTab = pagina;
		}

		private void LimpaCFG()
		{
			txtCfgValidarSenha.Clear();
			listBoxAcesso.Items.Clear();
			listBoxCatracas.Items.Clear();
			listBoxLiberacao.Items.Clear();
			listBoxLoader.Items.Clear();
			listBoxServidor.Items.Clear();

			RemoverPaginaTbcConteudo(PaginaCFG);
			RemoverPaginaTbcConteudo(PaginaCFGBusca);
			RemoverPaginaTbcConteudo(PaginaCFGValidacoes);
			RemoverPaginaTbcConteudo(PaginaCfgAcesso);
			RemoverPaginaTbcConteudo(PaginaCfgCatracas);
			RemoverPaginaTbcConteudo(PaginaCfgLiberacao);
			RemoverPaginaTbcConteudo(PaginaCfgLoader);
			RemoverPaginaTbcConteudo(PaginaCfgServidor);
		}
    }
}