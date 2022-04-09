using MdPaciente.Aplicacoes;
using MdPaciente.Dominio.Enums;
using MdPaciente.Dominio;
using MdPaciente.Dtos;
using System;
using System.Windows.Forms;
using ModelPrincipal.Enumeradores;

namespace MdPaciente.Visao.Telas.Exame
{
    public partial class frmCadastroExame : Form
    {
        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();

        private readonly Exames _exame = new Exames();

        private bool ModoEdicao = false;

        public frmCadastroExame(DtoConfiguracao dto)
        {
            InitializeComponent();
        }

        public frmCadastroExame(Exames exame, DtoConfiguracao DtoConfiguracao)
        {
            InitializeComponent();

            _dto = DtoConfiguracao;

            if (exame != null)
            {
                CarregaTelaComDadosExame(exame);
                ModoEdicao = true;
                _exame = exame;
            }
        }

        private void CarregaTelaComDadosExame(Exames exame)
        {
            
            

                txtNomePaciente.Text = exame.Nome;
                //txtIdadePaciente.Text = exame.Idade;
                dtpDataNascimento.Value = exame.DataNascimento;
                //cbSexoPaciente.SelectedIndex = exame.CodigoSexo.GetHashCode();
                //dtDataExame.Value = exame.Data;
                txtMedicoRequisitante.Text = exame.MedicoRequisitante;
                //cbTipoExame.SelectedIndex = exame.TipoExame.GetHashCode();
                txtMedico.Text = exame.Medico;
                txtMaFormacoes.Text = exame.MaFormacoes;
                txtMotivoExame.Text = exame.MotivoExame;
                txtEmailMedico.Text = exame.EmailMedico;
                txtTelefone.Text = exame.Telefone;
                txtSuspeitaDiagnostico.Text = exame.SuspeitaDiagnostico;
                txtTempoDor.Text = exame.TempoDor;
                //ckTempoDorAno.Checked = exame.TempoDorAno.Value;
                //ckTempoDorMes.Checked = exame.TempoDorMes.Value;
                //ckTempoDorDia.Checked = exame.TempoDorDia.Value;
                ckDorMomentoExame.Checked = exame.DorMomentoExame.Value;
                ckMenstruacaoAusente.Checked = exame.MenstruacaoAusente.Value;
                txtTempoMenstruacaoAusente.Text = exame.MenstruacaoAusenteTempo;
                ckMenstruacaoIrregular.Checked = exame.MenstruacaoIrregular.Value;
                ckMenstruacaoDolorosa.Checked = exame.MenstruacaoDolorosa.Value;
                ckDiabetes.Checked = exame.Diabetes.Value;
                ckArtriteReumatoide.Checked = exame.ArtriteReumatoide.Value;
                ckRaynaud.Checked = exame.Raynaud.Value;
                ckGota.Checked = exame.Gota.Value;
                ckLupus.Checked = exame.Lupus.Value;
                ckLitiaseBiliar.Checked = exame.LitiaseBiliar.Value;
                ckLitiaseRenal.Checked = exame.LitiaseRenal.Value;
                ckDislipidemia.Checked = exame.Dislipidemia.Value;
                ckSinusite.Checked = exame.Sinusite.Value;
                ckEnxaquecaFrontal.Checked = exame.EnxaquecaFrontal.Value;
                ckEnxaquecaTemporalDireita.Checked = exame.EnxaquecaTemporalDireita.Value;
                ckEnxaquecaTemporalEsquerda.Checked = exame.EnxaquecaTemporalEsquerda.Value;
                ckEnxaquecaNuca.Checked = exame.EnxaquecaNuca.Value;
                ckEnxaquecaTopo.Checked = exame.EnxaquecaTopo.Value;
                ckEnxaquecaAlternante.Checked = exame.EnxaquecaAlternante.Value;
                ckEnxaquecaTodaCabeca.Checked = exame.EnxaquecaTotal.Value;
                ckEnxaquecaTiara.Checked = exame.EnxaquecaTiara.Value;
                ckEnxaquecaPulsatil.Checked = exame.EnxaquecaPulsatil.Value;
                ckFotofobia.Checked = exame.Fotofobia.Value;
                ckFonofobia.Checked = exame.Fonofobia.Value;
                ckPerfumesOdores.Checked = exame.PerfumeOdores.Value;
                ckDormirMal.Checked = exame.DormirMal.Value;
                ckFome.Checked = exame.Fome.Value;
                ckDisturbioAlimentar.Checked = exame.Alimentar.Value;
                txtDescricaoDisturbioAlimentar.Text = exame.DescricaoAlimentar;
                ckOvariosPolicisticos.Checked = exame.OvarioPolicisticos.Value;
                ckDisplasiaMamaria.Checked = exame.DisplasiaMamaria.Value;
                ckEndometriose.Checked = exame.Endometriose.Value;
                ckVarizes.Checked = exame.Varizes.Value;
                ckAcne.Checked = exame.Acne.Value;
                ckCirurgiaVesiculaBiliar.Checked = exame.CPVesiculaBiliar.Value;
                ckHisterectomia.Checked = exame.CPHisterectomia.Value;
                ckCornetosNasaisSinusite.Checked = exame.CPCornetosNasaisSinusite.Value;
                ckCancerMamaDireita.Checked = exame.CPCancerMamaDireita.Value;
                ckCancerMamaEsquerda.Checked = exame.CPCancerMamaEsquerda.Value;
                ckCirurgiaVarizes.Checked = exame.CPVarizes.Value;
                ckProteseMamaria.Checked = exame.ProteseMamaria.Value;
                ckProteseMetalicaOssoCorpo.Checked = exame.ProteseMetalOssoCorpo.Value;
                ckTelaMarlex.Checked = exame.TelaMarlex.Value;
                ckAmputacaoMembro.Checked = exame.AmputacaoMembro.Value;
                txtAmputacaoMembro.Text = exame.AmputacaoMembroEspecificada;
                ckImplanteAnticoncepcional.Checked = exame.ImpAnticoncepcional.Value;
                ckDiuCobre.Checked = exame.DIUCobre.Value;
                ckDiuMirena.Checked = exame.DIUMirena.Value;
                dtDataCobre.Value = exame.DataCobre.Value;
                dtDataMirena.Value = exame.DataMirena.Value;
                ckInfiltracaoCorticoide.Checked = exame.InfiltracaoCorticoide.Value;
                txtLocalInfiltracaoCorticoide.Text = exame.LocalInfiltracao;
                txtAnticoncepcional.Text = exame.NomeAnticoncepcional;
                txtTempoUsoAnticoncepcionalAnos.Text = exame.TempoUsoAnticoncepcionalAnos;
                txtTempoUsoAnticoncepcionalMeses.Text = exame.TempoUsoAnticoncepcionalMeses;
                ckSinvastatinaSimilar.Checked = exame.Sinvastatina.Value;
                txtSinvastatinaSimilar.Text = exame.SimilarSinvastatina;
                txtTempoUsoSinvastatinaAnos.Text = exame.TempoUsoSinvastatinaAnos;
                txtTempoUsoSinvastatinaMeses.Text = exame.TempoUsoSinvastatinaMeses;
                ckOmeprazol.Checked = exame.Omeprazol.Value;
                ckCorticosteroide.Checked = exame.Corticosteroide.Value;
                ckAntiarritmico.Checked = exame.Antiarritmico.Value;
                ckHormonioTireoidiano.Checked = exame.HormonioTireoidiano.Value;
                ckAntidepressivo.Checked = exame.Antidepressivo.Value;
                ckAntihipertensivo.Checked = exame.Antihipertensivo.Value;
                ckDieta.Checked = exame.Dieta.Value;
                txtDescricaoDieta.Text = exame.DescricaoDieta;
                ckHiperproteica.Checked = exame.Hiperproteica.Value;
                ckHipocalorica.Checked = exame.Hipocalorica.Value;
                ckHerbalife.Checked = exame.Herbalife.Value;
                //ckStatusExame.Checked = exame.StatusExame.Value;


        }

        private void LimparFormulario()
        {
            //txtAnamnese.Clear();
            //txtLaudo.Clear();
            //txtTipoExame.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var exame = ObtenhaDadosTela();
                //MdPaciente.NovoExame(exame);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao cadastrar exame:\n" + ex.Message);
                return;
            }

            MessageBox.Show("Exame Cadastrado com sucesso!");


        }

        private void AtualizeExame()
        {
            try
            {
                var exameAtualizado = ObtenhaDadosTela();
                //exameAtualizado.Id = ExameAtual.Id;
               // MdPaciente.Atualize(exameAtualizado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao atualizar os dados do exame: \n" + ex.Message);
                return;
            }
            MessageBox.Show("Exame atualizado com sucesso!");
            ModoEdicao = false;
        }


        private void LimpeFormulario()
        {
            txtNomePaciente.Clear();
            txtIdadePaciente.Clear();
            dtpDataNascimento.Text = DateTime.Now.ToShortDateString();
            dtDataExame.Text = DateTime.Now.ToShortDateString();
            txtMedicoRequisitante.Clear();
            txtMedico.Clear();
            txtMaFormacoes.Clear();
            txtMotivoExame.Clear();
            txtEmailMedico.Clear();
            txtTelefone.Clear();
            txtSuspeitaDiagnostico.Clear();
            txtTempoDor.Clear();
            ckTempoDorAno.Checked = false;
            ckTempoDorMes.Checked = false;
            ckTempoDorDia.Checked = false;
            ckDorMomentoExame.Checked = false;
            ckMenstruacaoAusente.Checked = false;
            txtTempoMenstruacaoAusente.Clear();
            ckMenstruacaoIrregular.Checked = false;
            ckMenstruacaoDolorosa.Checked = false;
            ckDiabetes.Checked = false;
            ckArtriteReumatoide.Checked = false;
            ckRaynaud.Checked = false;
            ckGota.Checked = false;
            ckLupus.Checked = false;
            ckLitiaseBiliar.Checked = false;
            ckLitiaseRenal.Checked = false;
            ckDislipidemia.Checked = false;
            ckSinusite.Checked = false;
            ckEnxaquecaFrontal.Checked = false;
            ckEnxaquecaTemporalDireita.Checked = false;
            ckEnxaquecaTemporalEsquerda.Checked = false;
            ckEnxaquecaNuca.Checked = false;
            ckEnxaquecaTopo.Checked = false;
            ckEnxaquecaAlternante.Checked = false;
            ckEnxaquecaTodaCabeca.Checked = false;
            ckEnxaquecaTiara.Checked = false;
            ckEnxaquecaPulsatil.Checked = false;
            ckFotofobia.Checked = false;
            ckFonofobia.Checked = false;
            ckPerfumesOdores.Checked = false;
            ckDormirMal.Checked = false;
            ckFome.Checked = false;
            ckDisturbioAlimentar.Checked = false;
            txtDescricaoDisturbioAlimentar.Clear();
            ckOvariosPolicisticos.Checked = false;
            ckDisplasiaMamaria.Checked = false;
            ckEndometriose.Checked = false;
            ckVarizes.Checked = false;
            ckAcne.Checked = false;
            ckCirurgiaVesiculaBiliar.Checked = false;
            ckHisterectomia.Checked = false;
            ckCornetosNasaisSinusite.Checked = false;
            ckCancerMamaDireita.Checked = false;
            ckCancerMamaEsquerda.Checked = false;
            ckCirurgiaVarizes.Checked = false;
            ckProteseMamaria.Checked = false;
            ckProteseMetalicaOssoCorpo.Checked = false;
            ckTelaMarlex.Checked = false;
            ckAmputacaoMembro.Checked = false;
            txtAmputacaoMembro.Clear();
            ckImplanteAnticoncepcional.Checked = false;
            ckDiuCobre.Checked = false;
            ckDiuMirena.Checked = false;
            dtDataCobre.Text = DateTime.Now.ToShortDateString();
            dtDataMirena.Text = DateTime.Now.ToShortDateString();
            ckInfiltracaoCorticoide.Checked = false;
            txtLocalInfiltracaoCorticoide.Clear();
            txtAnticoncepcional.Clear();
            txtTempoUsoAnticoncepcionalAnos.Clear();
            txtTempoUsoAnticoncepcionalMeses.Clear();
            ckSinvastatinaSimilar.Checked = false;
            txtSinvastatinaSimilar.Clear();
            txtTempoUsoSinvastatinaAnos.Clear();
            txtTempoUsoSinvastatinaMeses.Clear();
            ckOmeprazol.Checked = false;
            ckCorticosteroide.Checked = false;
            ckAntiarritmico.Checked = false;
            ckHormonioTireoidiano.Checked = false;
            ckAntidepressivo.Checked = false;
            ckAntihipertensivo.Checked = false;
            ckDieta.Checked = false;
            txtDescricaoDieta.Clear();
            ckHiperproteica.Checked = false;
            ckHipocalorica.Checked = false;
            ckHerbalife.Checked = false;
            //ckStatusExame.Checked = false;

        }

        private Exames ObtenhaDadosTela()
        {
            var enums = new ProcessadorEnum();
            var sexo = enums.ObtenhaIndexSexo(cbSexoPaciente.SelectedIndex);
            //var tipoExame = enums.ObtenhaIndexTipoExame(cbTipoExame.SelectedIndex);

            Exames exame = new Exames()
            {
                
                Nome = txtNomePaciente.Text,
                //Idade = txtIdadePaciente.Text,
                DataNascimento = dtpDataNascimento.Value,
                //CodigoSexo = sexo,
                //Data = dtDataExame.Value,
                MedicoRequisitante = txtMedicoRequisitante.Text,
                //TipoExame = tipoExame,
                Medico = lbMedico.Text,
                MaFormacoes = txtMaFormacoes.Text,
                MotivoExame = txtMotivoExame.Text,
                EmailMedico = txtEmailMedico.Text,
                Telefone = txtTelefone.Text,
                SuspeitaDiagnostico = txtSuspeitaDiagnostico.Text,
                TempoDor = txtTempoDor.Text,
                //TempoDorAno = ckTempoDorAno.Checked,
                //TempoDorMes = ckTempoDorMes.Checked,
                //TempoDorDia = ckTempoDorDia.Checked,
                DorMomentoExame = ckDorMomentoExame.Checked,
                MenstruacaoAusente = ckMenstruacaoAusente.Checked,
                MenstruacaoAusenteTempo = txtTempoMenstruacaoAusente.Text,
                MenstruacaoIrregular = ckMenstruacaoIrregular.Checked,
                MenstruacaoDolorosa = ckMenstruacaoDolorosa.Checked,
                Diabetes = ckDiabetes.Checked,
                ArtriteReumatoide = ckArtriteReumatoide.Checked,
                Raynaud = ckRaynaud.Checked,
                Gota = ckGota.Checked,
                Lupus = ckLupus.Checked,
                LitiaseBiliar = ckLitiaseBiliar.Checked,
                LitiaseRenal = ckLitiaseRenal.Checked,
                Dislipidemia = ckDislipidemia.Checked,
                Sinusite = ckSinusite.Checked,
                EnxaquecaFrontal = ckEnxaquecaFrontal.Checked,
                EnxaquecaTemporalDireita = ckEnxaquecaTemporalDireita.Checked,
                EnxaquecaTemporalEsquerda = ckEnxaquecaTemporalEsquerda.Checked,
                EnxaquecaNuca = ckEnxaquecaNuca.Checked,
                EnxaquecaTopo = ckEnxaquecaTopo.Checked,
                EnxaquecaAlternante = ckEnxaquecaAlternante.Checked,
                EnxaquecaTotal = ckEnxaquecaTodaCabeca.Checked,
                EnxaquecaTiara = ckEnxaquecaTiara.Checked,
                EnxaquecaPulsatil = ckEnxaquecaPulsatil.Checked,
                Fotofobia = ckFotofobia.Checked,
                Fonofobia = ckFonofobia.Checked,
                PerfumeOdores = ckPerfumesOdores.Checked,
                DormirMal = ckDormirMal.Checked,
                Fome = ckFome.Checked,
                Alimentar = ckDisturbioAlimentar.Checked,
                DescricaoAlimentar = txtDescricaoDisturbioAlimentar.Text,
                OvarioPolicisticos = ckOvariosPolicisticos.Checked,
                DisplasiaMamaria = ckDisplasiaMamaria.Checked,
                Endometriose = ckEndometriose.Checked,
                Varizes = ckVarizes.Checked,
                Acne = ckAcne.Checked,
                CPVesiculaBiliar = ckCirurgiaVesiculaBiliar.Checked,
                CPHisterectomia = ckHisterectomia.Checked,
                CPCornetosNasaisSinusite = ckCornetosNasaisSinusite.Checked,
                CPCancerMamaDireita = ckCancerMamaDireita.Checked,
                CPCancerMamaEsquerda = ckCancerMamaEsquerda.Checked,
                CPVarizes = ckCirurgiaVarizes.Checked,
                ProteseMamaria = ckProteseMamaria.Checked,
                ProteseMetalOssoCorpo = ckProteseMetalicaOssoCorpo.Checked,
                TelaMarlex = ckTelaMarlex.Checked,
                AmputacaoMembro = ckAmputacaoMembro.Checked,
                AmputacaoMembroEspecificada = txtAmputacaoMembro.Text,
                ImpAnticoncepcional = ckImplanteAnticoncepcional.Checked,
                DIUCobre = ckDiuCobre.Checked,
                DIUMirena = ckDiuMirena.Checked,
                DataCobre = dtDataCobre.Value,
                DataMirena = dtDataMirena.Value,
                InfiltracaoCorticoide = ckInfiltracaoCorticoide.Checked,
                LocalInfiltracao = txtLocalInfiltracaoCorticoide.Text,
                NomeAnticoncepcional = txtAnticoncepcional.Text,
                TempoUsoAnticoncepcionalAnos = txtTempoUsoAnticoncepcionalAnos.Text,
                TempoUsoAnticoncepcionalMeses = txtTempoUsoAnticoncepcionalMeses.Text,
                Sinvastatina = ckSinvastatinaSimilar.Checked,
                SimilarSinvastatina = txtSinvastatinaSimilar.Text,
                TempoUsoSinvastatinaAnos = txtTempoUsoSinvastatinaAnos.Text,
                TempoUsoSinvastatinaMeses = txtTempoUsoSinvastatinaMeses.Text,
                Omeprazol = ckOmeprazol.Checked,
                Corticosteroide = ckCorticosteroide.Checked,
                Antiarritmico = ckAntiarritmico.Checked,
                HormonioTireoidiano = ckHormonioTireoidiano.Checked,
                Antidepressivo = ckAntidepressivo.Checked,
                Antihipertensivo = ckAntihipertensivo.Checked,
                Dieta = ckDieta.Checked,
                DescricaoDieta = txtDescricaoDieta.Text,
                Hiperproteica = ckHiperproteica.Checked,
                Hipocalorica = ckHipocalorica.Checked,
                Herbalife = ckHerbalife.Checked,
                //StatusExame = ckStatusExame.Checked
            };
            return exame;
        }
        #region Buttons
        private void btnExameSalvar_Click(object sender, EventArgs e)
        {
            if (ModoEdicao)
            {
                AtualizeExame();
            }
            else
            {
                //AdicioneExame();
            }

            //VoltarParaCard();
        }

        
        private void btnExameCancelar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
            this.Close();
        }
        #endregion

       
    }
}
