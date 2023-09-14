using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ControleUsuarioVO
{
    public class DtoUsuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DtoPerfilUsuario PerfilUsuario { get; set; }
        //public DtoFuncionario Funcionario { get; set; }
        public DtoFuncionarioResponse Funcionario { get; set; }
        public DtoCentroCusto Departamento { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
        public bool IsActive { get; set; }
        public DtoAluno Aluno { get; set; }
        public bool IsAluno { get; set; }
        public bool IsPrimeiroAcesso { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public bool IsMenuAtendimento
        {
            get
            {
                if (PerfilUsuario == null)
                {
                    return false;
                }
                return ValidarAtendente();
            }
        }


        private bool ValidarAtendente()
        {
            bool IsMenuAtendimento = false;
            if (PerfilUsuario != null
                && PerfilUsuario.AcessoMenuAtendimento
                && !PerfilUsuario.CadastroFornecedorCliente
                && !PerfilUsuario.CadastroFuncionario
                && !PerfilUsuario.Comunicacao
                && !PerfilUsuario.ConfiguradorParametros
                && !PerfilUsuario.ConsultarAluno
                && !PerfilUsuario.ContasAPagar
                && !PerfilUsuario.ControlePonto
                && !PerfilUsuario.CriacaoPerfil
                && !PerfilUsuario.CriacaoUsuario
                && !PerfilUsuario.CriarAgendaProva
                && !PerfilUsuario.CriarAulaOnline
                && !PerfilUsuario.CriarColegioAutorizado
                && !PerfilUsuario.CriarComissoes
                && !PerfilUsuario.CriarMeta
                && !PerfilUsuario.CursoTurma
                && !PerfilUsuario.EscalaServico
                && !PerfilUsuario.Estoque
                && !PerfilUsuario.FolhaPagamento
                && !PerfilUsuario.HistoricoViagem
                && !PerfilUsuario.IsentarMultaCancelamento
                && !PerfilUsuario.ListaPassageiros
                && !PerfilUsuario.MetaPainel
                && !PerfilUsuario.MinhasAulas
                && !PerfilUsuario.PlanoPagamento
                && !PerfilUsuario.PromocoesBolsaConvenio
                && !PerfilUsuario.Relatorios
                && !PerfilUsuario.Solicitacao
                && !PerfilUsuario.TicketAdministracao
                && !PerfilUsuario.TicketPainel
                && !PerfilUsuario.Unidade
                && !PerfilUsuario.UploadPonto
                && !PerfilUsuario.VerTodasUnidades)
                IsMenuAtendimento = true;

            return IsMenuAtendimento;
        }
    }
}
