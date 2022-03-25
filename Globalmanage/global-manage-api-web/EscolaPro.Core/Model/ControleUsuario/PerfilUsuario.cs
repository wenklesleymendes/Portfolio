using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace EscolaPro.Core.Model
{
    public class PerfilUsuario : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        //public int NivelAcesso { get; set; }
        //public HorarioFuncionamento HorarioAcesso { get; set; }
        public PerfilSistemaEnum PerfilSistemaEnum { get; set; }

        // Alunos
        public bool CadastrarAluno { get; set; }
        public bool ConsultarAluno { get; set; }
        public bool IsentarMultaCancelamento { get; set; }

        // Relatórios
        public bool Relatorios { get; set; }

        // Portal Administrador
        public bool CriacaoUsuario { get; set; }
        public bool CriacaoPerfil { get; set; }
        public bool ConfiguradorParametros { get; set; }

        // Ticket
        public bool TicketPainel { get; set; }
        public bool TicketAdministracao { get; set; }

        // Comunicação
        public bool Comunicacao { get; set; }

        // Recursos Humanos
        public bool CadastroFuncionario { get; set; }
        public bool ControlePonto { get; set; }
        public bool EscalaServico { get; set; }
        public bool UploadPonto { get; set; }

        // Financeiro
        public bool CadastroFornecedorCliente { get; set; }
        public bool ContasAPagar { get; set; }
        public bool Estoque { get; set; }
        public bool FolhaPagamento { get; set; }

        // Provas
        public bool CriarColegioAutorizado { get; set; }
        public bool CriarAgendaProva { get; set; }
        public bool ListaPassageiros { get; set; }
        public bool HistoricoViagem { get; set; }

        // Gerenciador
        public bool Unidade { get; set; }
        public bool CursoTurma { get; set; }
        public bool PlanoPagamento { get; set; }
        public bool PromocoesBolsaConvenio { get; set; }
        public bool Solicitacao { get; set; }
        public bool ConteudoDigital { get; set; }

        // Metas e Comissões
        public bool CriarMeta { get; set; }
        public bool MetaPainel { get; set; }
        public bool CriarComissoes { get; set; }

        // Aulas On-line
        public bool CriarAulaOnline { get; set; }
        public bool MinhasAulas { get; set; }

        //public bool EjaEncceja { get; set; }

        public bool VerTodasUnidades { get; set; }

        //public string SemanaInicio { get; set; }
        //public string SemanaTermino { get; set; }
        //public string SabadoInicio { get; set; }
        //public string SabadoTermino { get; set; }

        public string SemanaInicio { get; set; }
        public string SemanaTermino { get; set; }
        public string SabadoInicio { get; set; }
        public string SabadoTermino { get; set; }

        // Menu Atendimento - Raul
        public bool AcessoMenuAtendimento { get; set; }
    }
}