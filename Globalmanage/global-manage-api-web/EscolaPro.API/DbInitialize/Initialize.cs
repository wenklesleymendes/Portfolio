using EscolaPro.Core.Extensions;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.DbInitialize
{
    public static class Initialize
    {
        public static async Task PerfilInitialize(IServiceProvider services)
        {
            int perfilId;

            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var perfilService = scope.ServiceProvider.GetRequiredService<IPerfilUsuarioService>();
            var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();

            if (await perfilService.ConsultarPerfilExistente(Core.Model.Enums.PerfilSistemaEnum.Administrador) == false)
            {
                var perfil = await perfilService.Inserir(new DtoPerfilUsuario()
                {
                    ConsultarAluno = true,
                    Relatorios = true,
                    CriacaoUsuario = true,
                    CriacaoPerfil = true,
                    ConfiguradorParametros = true,
                    TicketPainel = true,
                    TicketAdministracao = true,
                    Comunicacao = true,
                    CadastroFuncionario = true,
                    ControlePonto = true,
                    EscalaServico = true,
                    UploadPonto = true,
                    CadastroFornecedorCliente = true,
                    ContasAPagar = true,
                    Estoque = true,
                    FolhaPagamento = true,
                    CriarColegioAutorizado = true,
                    CriarAgendaProva = true,
                    ListaPassageiros = true,
                    HistoricoViagem = true,
                    Unidade = true,
                    CursoTurma = true,
                    PlanoPagamento = true,
                    PromocoesBolsaConvenio = true,
                    Solicitacao = true,
                    CriarMeta = true,
                    MetaPainel = true,
                    CriarComissoes = true,
                    CriarAulaOnline = true,
                    MinhasAulas = true,
                    IsActive = true,
                    VerTodasUnidades = true,
                    AcessoMenuAtendimento = true,
                    PerfilSistemaEnum = Core.Model.Enums.PerfilSistemaEnum.Administrador
                });

                perfilId = perfil.Id;
            }
            else
            {
                perfilId = perfilService.BuscaPerfilAdminId();
            }

            if (perfilId != 0 && !usuarioService.CheckUsuarioAdmin(perfilId))
            {
                await usuarioService.CriarUsuarioAdmin(new DtoUsuarioRequest()
                {
                    UserName = "admin",
                    Password = Criptografia.CreateMD5("admin"),
                    CPF = "0",
                    PerfilUsuarioId = perfilId,
                    IsActive = true
                });
            }
        }
    }
}
