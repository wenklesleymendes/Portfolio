using MdPaciente.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente.Dominio
{
    public class Exames
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public string MedicoRequisitante { get; set; }
        public string Medico { get; set; }
        public string EmailMedico { get; set; }
        public string Telefone { get; set; }
        public string MotivoExame { get; set; }
        public string SuspeitaDiagnostico { get; set; }
        public string TempoDor { get; set; }
        public string MaFormacoes { get; set; }
        public bool? DorMomentoExame { get; set; }
        public bool? MenstruacaoAusente { get; set; }
        public string MenstruacaoAusenteTempo { get; set; }
        public bool? MenstruacaoIrregular { get; set; }
        public bool? MenstruacaoDolorosa { get; set; }
        public bool? Diabetes { get; set; }
        public bool? ArtriteReumatoide { get; set; }
        public bool? Raynaud { get; set; }
        public bool? Gota { get; set; }
        public bool? Lupus { get; set; }
        public bool? LitiaseBiliar { get; set; }
        public bool? LitiaseRenal { get; set; }
        public bool? Dislipidemia { get; set; }
        public bool? Sinusite { get; set; }
        public bool? EnxaquecaFrontal { get; set; }
        public bool? EnxaquecaTemporalDireita { get; set; }
        public bool? EnxaquecaTemporalEsquerda { get; set; }
        public bool? EnxaquecaAlternante { get; set; }
        public bool? EnxaquecaNuca { get; set; }
        public bool? EnxaquecaTopo { get; set; }
        public bool? EnxaquecaTotal { get; set; }
        public bool? EnxaquecaTiara { get; set; }
        public bool? EnxaquecaPulsatil { get; set; }
        public bool? Fotofobia { get; set; }
        public bool? Fonofobia { get; set; }
        public bool? PerfumeOdores { get; set; }
        public bool? DormirMal { get; set; }
        public bool? Fome { get; set; }
        public bool? Alimentar { get; set; }
        public string DescricaoAlimentar { get; set; }
        public bool? OvarioPolicisticos { get; set; }
        public bool? DisplasiaMamaria { get; set; }
        public bool? Endometriose { get; set; }
        public bool? Varizes { get; set; }
        public bool? Acne { get; set; }
        public bool? CPVesiculaBiliar { get; set; }
        public bool? CPHisterectomia { get; set; }
        public bool? CPCornetosNasaisSinusite { get; set; }
        public bool? CPCancerMamaDireita { get; set; }
        public bool? CPCancerMamaEsquerda { get; set; }
        public bool? CPVarizes { get; set; }
        public bool? ProteseMamaria { get; set; }
        public bool? ProteseMetalOssoCorpo { get; set; }
        public bool? TelaMarlex { get; set; }
        public bool? AmputacaoMembro { get; set; }
        public string AmputacaoMembroEspecificada { get; set; }
        public bool? ImpAnticoncepcional { get; set; }
        //public DateTime? DataImpAnticoncepcional { get; set; }
        public bool? DIUCobre { get; set; }
        public bool? DIUMirena { get; set; }
        public DateTime? DataCobre { get; set; }
        public DateTime? DataMirena { get; set; }
        public bool? InfiltracaoCorticoide { get; set; }
        public string LocalInfiltracao { get; set; }
        public string NomeAnticoncepcional { get; set; }
        public string TempoUsoAnticoncepcionalAnos { get; set; }
        public string TempoUsoAnticoncepcionalMeses { get; set; }
        public bool? Sinvastatina { get; set; }
        public string SimilarSinvastatina { get; set; }
        public string TempoUsoSinvastatinaAnos { get; set; }
        public string TempoUsoSinvastatinaMeses { get; set; }
        public bool? Omeprazol { get; set; }
        public bool? Corticosteroide { get; set; }
        public bool? Antiarritmico { get; set; }
        public bool? HormonioTireoidiano { get; set; }
        public bool? Antidepressivo { get; set; }
        public bool? Antihipertensivo { get; set; }
        public bool? Dieta { get; set; }
        public string DescricaoDieta { get; set; }
        public bool? Hiperproteica { get; set; }
        public bool? Hipocalorica { get; set; }
        public bool? Herbalife { get; set; }
        public bool? StatusExame { get; set; }

    }
}
