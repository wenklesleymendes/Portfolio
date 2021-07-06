using System.ComponentModel;

namespace Acesso.Dominio
{
    /// <summary>
    /// Enumerador Acesso Esperado.
    /// <list type="bullet">
    /// 
    /// <item>
    /// <term>EsperadoEntrada</term>
    /// <description>1- Acionar somente Entrada</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoSaida</term>
    /// <description>2 - Acionar somente Saida</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoNenhum</term>
    /// <description>3 - Acionar nada (mas led verde etc.)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoAguardarEntrada</term>
    /// <description>4 - Acionar somente Entrada e aguardar giro</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoAguardarSaida</term>
    /// <description>5 - Acionar somente Saída e aguardar giro</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoAguardarAmbas</term>
    /// <description>6 - Acionar Entrada e Saída e aguardar giro</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoProibido</term>
    /// <description>7 - Acionar Entrada e Saída e aguardar giro</description>
    /// </item>
    /// 
    /// <item>
    /// <term>EsperadoAguardarAmbas</term>
    /// <description>8 - Acionar Entrada e Saída e aguardar giro</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Esperado Qualquer</term>
    /// <description>9 - Acionar Entrada e Saída (mas não agu.giro)</description>
    /// </item>
    /// 
    /// </list>
    /// </summary>
    public enum AcessoEsperado
    {
        EsperadoEntrada,
        EsperadoSaida,
        EsperadoNenhum,
        EsperadoAguardarEntrada,
        EsperadoAguardarSaida,
        EsperadoAguardarAmbas,
        EsperadoProibido,
        EsperadoQualquer
    }

    /// <summary>
    /// Enumerador Sentido do Giro.
    /// <list type="bullet">
    /// 
    /// <item>
    /// <term>Indefinido</term>
    /// <description>Giro Indefinido</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Entrada</term>
    /// <description>Giro Entrada</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Saida</term>
    /// <description>Giro Saída</description>
    /// </item>
    /// 
    /// </list>
    /// </summary>
    public enum SentidoGiro
    {
        [Description("Indefinido")]
        Indefinido,
        [Description("Entrada")]
        Entrada,
        [Description("Saída")]
        Saida
    }

    /// <summary>
    /// Enumerador Tipo de Acesso.
    /// <list type="bullet">
    /// 
    /// <item>
    /// <term>Indefinido</term>
    /// <description>Acesso Indefinido</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Entrada</term>
    /// <description>Acesso pelo Teclado</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Cartao</term>
    /// <description>Acesso pelo Cartão</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Biometria</term>
    /// <description>Acesso pelo Biometria da pessoa</description>
    /// </item>
    /// 
    /// <item>
    /// <term>RFID</term>
    /// <description>Acesso pelo Cartão RFID</description>
    /// </item>
    /// 
    /// <item>
    /// <term>PID</term>
    /// <description>Acesso pelo dispositivo PID</description>
    /// </item>
    /// 
    /// </list>
    /// </summary>
    public enum TipoAcesso
    {
        Indefinido,
        Teclado,
        Cartao,
        Biometria,
        RFID,
        PID
    }

    /// <summary>
    /// Enumerador Status Liberacao Catraca.
    /// <list type="bullet">
    /// 
    /// <item>
    /// <term>Negado</term>
    /// <description>Status Acesso Negado</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Liberado</term>
    /// <description>Status Acesso Liberado</description>
    /// </item>
    /// 
    /// <item>
    /// <term>LiberadoComRestricoes</term>
    /// <description>Status Liberado com Restricões</description>
    /// </item>
    /// 
    /// <item>
    /// <term>MudancaEstado</term>
    /// <description>Status Mundado o Estado da Catraca</description>
    /// </item>
    /// 
    /// </list>
    /// </summary>
    public enum StatusLiberacaoCatraca
    {
        Negado,
        Liberado,
        LiberadoComRestricoes,
        MudancaEstado
    }


    /// <summary>
    /// Enumerador Tipo Pessoa.
    /// <list type="bullet">
    /// 
    /// <item>
    /// <term>Aluno</term>
    /// <description>Pessoa Aluno</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Professor</term>
    /// <description>Pessoa Profesor</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Colaborador</term>
    /// <description>Pessoa Colaborador</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Responsavel</term>
    /// <description>Pessoa Responsavel</description>
    /// </item>
    /// 
    /// <item>
    /// <term>AutorizadoBuscarAluno</term>
    /// <description>Pessoa Autorizado Buscar Aluno</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Nao Encontrada</term>
    /// <description>Pessoa Nao Encontrada</description>
    /// </item>
    /// 
    /// </list>
    /// </summary>
    public enum TipoPessoa
    {
        Aluno = 1,
        Professor = 2,
        Profissional = 3,
        Responsavel = 5,
        AutorizadoBuscarAluno = 6,
        NaoEncontrada = 99,
        Colaborador = 100
    }


    public enum FuncaoAcesso
    {
        ConfiguracaoDeAcesso = 1009006,
        MonitorDeAcesso = 1009001,
        EnvioDeUsuariosParaAcesso = 1009011,
        ExcecaoParaSairSozinho = 1009036,
        AcessoProfessoresColaboradores = 1009012,
        AcessoAluno = 1009007,
        ManutencaoDeRegistroDeAcesso = 1009007
    }

    public class Enumeradores
    {
        public static TipoPessoa ObtenhaTipoPessoa(int codigo)
        {
            if (codigo == 1) return TipoPessoa.Aluno;
            if (codigo == 2) return TipoPessoa.Professor;
            if (codigo == 3) return TipoPessoa.Profissional;
            if (codigo == 5) return TipoPessoa.Responsavel;
            if (codigo == 6) return TipoPessoa.AutorizadoBuscarAluno;

            return TipoPessoa.NaoEncontrada;
        }
    }
}