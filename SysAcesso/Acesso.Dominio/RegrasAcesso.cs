using System;
using System.Collections.Generic;

namespace Acesso.Dominio
{
    /// <summary>
    /// Regras do acesso para customizar no projeto.
    /// </summary>
    [Serializable]
    public class RegrasAcesso
    {

        /// <remarks>
        /// <para>Os Colaboradores podem usar teclado</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string ColaboradoresPodemDigitar { get; set; }

        /// <remarks>
        /// <para>Aluno sem matrícula bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoAlunoSemMatricula { get; set; }

        /// <remarks>
        /// <para>Professor inativo bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoProfessorInativo { get; set; }

        /// <remarks>
        /// <para>Colaborador inativo bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoColaboradorInativo { get; set; }

        /// <remarks>
        /// <para>Responsável de alunos sem matrícula bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoResponsavelSemMatricula { get; set; }

        /// <remarks>
        /// <para>Autorizado a busca aluno sem matrícula bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoAutorizadoSemMatricula { get; set; }

        /// <remarks>
        /// <para>luno com ocorrências bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoAlunoComOcorrencias { get; set; }

        /// <remarks>
        /// <para>Professor com ocorrências bloqueia o acesso</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoProfessorComOcorrencias { get; set; }

        /// <remarks>
        /// <para>Colaborador com ocorrências bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoColaboradorComOcorrencias { get; set; }

        /// <remarks>
        /// <para>Aluno inadimplentes bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoAlunoInadimplente { get; set; }

        /// <remarks>
        /// <para>Aluno com pendencias de documentos bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoAlunoComPendenciaDocumentos { get; set; }

        /// <remarks>
        /// <para>Aluno com pendencias de material bloqueia o acesso.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string BloquearAcessoAlunoComPendenciaMaterial { get; set; }

        /// <remarks>
        /// <para>Aluno pode sair sozinho bloqueia o acesso .</para>
        /// <para>Codigo =<see langword="Codigo Atributo"/> </para>
        /// </remarks>
        public int AtributoPodeSairSozinho { get; set; }

        /// <remarks>
        /// <para>Atributo  de bloqueio.</para>
        /// <para>Codigo =<see langword="Codigo do atributo"/> </para>
        /// </remarks>
        public int AtributoBloqueado { get; set; }

        /// <remarks>
        /// <para>Aluno mensagem para o bloqueio de acesso.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemAlunoComOcorrencias { get; set; }

        /// <remarks>
        /// <para>Professor mensagem para o bloqueio de acesso.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemProfessorComOcorrencias { get; set; }

        /// <remarks>
        /// <para>Colaborador messangens do bloqueio de acesso com ocorrências.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemColaboradorComOcorrencias { get; set; }

        /// <remarks>
        /// <para>Aluno messangens do bloqueio de acesso com inadiplencias.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemAlunoComInadimplenciaDeDuplicatas { get; set; }

        /// <remarks>
        /// <para>Aluno messangens do bloqueio de acesso com inadiplencias forma de pagamento cheques.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemAlunoComInadimplenciaDeCheques { get; set; }

        /// <remarks>
        /// <para>Aluno messangens do bloqueio de acesso com pendencias de documentos.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemAlunoComPendenciaDeDocumentos { get; set; }

        /// <remarks>
        /// <para>Aluno messangens do bloqueio de acesso com pendencias de materiais.</para>
        /// <para>Mensagem=<see langword="Texto"/> </para>
        /// </remarks>
        public string MensagemAlunoComPendenciaDeMateriais { get; set; }

        /// <remarks>
        /// <para>Aluno tempo minimo para novo acesso bloqueio de acesso.</para>
        /// <para>Segundos=<see langword="10"/> </para>
        /// </remarks>
        public int TempoMinimoParaNovoAcessoSegundos { get; set; }

        /// <remarks>
        /// <para>Aluno tempo que será liberado acesso fora do periodo configurado.</para>
        /// <para>Segundos=<see langword="30"/> </para>
        /// </remarks>
        public int TempoParaAcessoLiberadoSegundos { get; set; }

        /// <remarks>
        /// <para>Professor podera libera o aluno.</para>
        /// <para>Valor padrão =<see langword="NAO"/> </para>
        /// </remarks>
        public string ProfessoresPodemLiberarAcessoAluno { get; set; }

        /// <remarks>
        /// <para>Calaborador pode liberar acesso do aluno.</para>
        /// <para>Valor padrão =<see langword="NAO"/> </para>
        /// </remarks>
        public string ColaboradoresPodeLiberarAcessoAluno { get; set; }

        /// <remarks>
        /// <para>Existe formulário de pessoa que podem liberar Aluno.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string FormularioLiberaAcessoAluno { get; set; }

        /// <remarks>
        /// <para>Responsaveis podem liberar Aluno.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string ResponsaveisPodemLiberarAcessoAluno { get; set; }

        /// <remarks>
        /// <para>Autorizado a busca podem liberar Aluno.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string AutorizadosPodemLiberamAcessoAluno { get; set; }

        /// <remarks>
        /// <para>Aluno esta liberado a digitar no teclado da catraca.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string AlunosPodemDigitar { get; set; }

        /// <remarks>
        /// <para>Professor esta liberado a digitar no teclado da catraca.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string ProfessoresPodemDigitar { get; set; }

        /// <remarks>
        /// <para>Colaborador esta liberado a digitar no teclado da catraca.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>>
        public string ProfissionaisPodemDigitar { get; set; }

        /// <remarks>
        /// <para>Responsavel esta liberado a digitar no teclado da catraca.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string ResponsaveisPodemDigitar { get; set; }

        /// <remarks>
        /// <para>Autorizado busca esta liberado a digitar no teclado da catraca.</para>
        /// <para>Valor padrão =<see langword="Nao"/> </para>
        /// </remarks>
        public string AutorizadosPodemDigitar { get; set; }

        /// <remarks>
        /// <para>Intervalos para acesso unico por giro e turno.</para>
        /// </remarks>
        public string IntervalosParaAcessoUnico { get; set; }

        public List<DiaSemanaIntervalosAcesso> IntervalosParaAcesso { get; set; }

        public RegrasAcesso Cria()
        {
            var acesso = new RegrasAcesso
            {
                BloquearAcessoAlunoSemMatricula = "SIM",
                BloquearAcessoProfessorInativo = "SIM",
                BloquearAcessoColaboradorInativo = "SIM",
                BloquearAcessoAutorizadoSemMatricula = "SIM",
                BloquearAcessoResponsavelSemMatricula = "SIM",
                IntervalosParaAcesso = new List<DiaSemanaIntervalosAcesso>()
            };

            for (int i = 0; i <= 6; i++)
            {
                acesso.IntervalosParaAcesso.Add(new DiaSemanaIntervalosAcesso() { DiaSemana = i, Intervalo = new List<Horarios>() });
            }

            return acesso;
        }
    }

	[Serializable]
    public class DiaSemanaIntervalosAcesso
    {
        public int DiaSemana { get; set; }
        public List<Horarios> Intervalo { get; set; }
    }

    [Serializable]
    public class Horarios
    {
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string Tipo { get; set; }
    }
}