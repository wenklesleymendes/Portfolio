using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class UsuarioRepository : DomainRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Usuario> BuscarPorAlunoId(int alunoId)
        {
            try
            {
                var query = await Task.FromResult(GenerateQuery((x => x.AlunoId == alunoId), null)
                      .Include(x => x.Funcionario)
                      .Include(x => x.Unidade)
                      .Include(x => x.Departamento)
                      .Include(x => x.PerfilUsuario)
                      .Include(x => x.Aluno));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> BuscarPorId(int usuarioId)
        {
            var query = await Task.FromResult(GenerateQuery((x => x.Id == usuarioId), null)
                                  .Include(x => x.Funcionario)
                                  .Include(x => x.Unidade)
                                  .Include(x => x.Departamento)
                                  .Include(x => x.PerfilUsuario)
                                  .Include(x => x.Aluno).AsNoTracking());

            var usuario = query.SingleOrDefault();

            return usuario;
        }

        public async Task<Usuario> BuscarPorFuncionarioId(int funcionarioId)
        {
            var query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == funcionarioId), null)
                                  .Include(x => x.Funcionario)
                                  .Include(x => x.Unidade)
                                  .Include(x => x.Departamento)
                                  .Include(x => x.PerfilUsuario));

            var usuario = query.SingleOrDefault();

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> BuscarTodos(int idUsuarioLogado)
        {
            try
            {
                var usuarioLogado = await BuscarPorId(idUsuarioLogado);

                if (usuarioLogado.PerfilUsuario?.PerfilSistemaEnum == Core.Model.Enums.PerfilSistemaEnum.Administrador || usuarioLogado.UserName == "admin")
                {
                    var query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null)
                         .Include(x => x.Funcionario).ThenInclude(x => x.Contato)
                         .Include(x => x.Unidade)
                         .Include(x => x.Departamento)
                         .Include(x => x.PerfilUsuario)
                         .Include(x => x.Aluno).ThenInclude(x => x.Contato));

                    return query.OrderBy(x => x.IsAluno).ToList();
                }
                else
                {
                    var query = await Task.FromResult(GenerateQuery((x => !x.IsDelete && !x.IsAluno), null)
                          .Include(x => x.Funcionario).ThenInclude(x => x.Contato)
                          .Include(x => x.Unidade)
                          .Include(x => x.Departamento)
                          .Include(x => x.PerfilUsuario)
                          .Include(x => x.Aluno).ThenInclude(x => x.Contato));

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuarioAtendente()
        {
            try
            {
                var query = await Task.FromResult(GenerateQuery((x => x.PerfilUsuario.PerfilSistemaEnum == Core.Model.Enums.PerfilSistemaEnum.FinanceiroJunior ||
                                                                      x.PerfilUsuario.PerfilSistemaEnum == Core.Model.Enums.PerfilSistemaEnum.FinanceiroPleno ||
                                                                      x.PerfilUsuario.PerfilSistemaEnum == Core.Model.Enums.PerfilSistemaEnum.FinanceiroSenior), null)
                                                                 .Include(x => x.Funcionario)
                                                                 .Include(x => x.Unidade)
                                                                 .Include(x => x.Departamento)
                                                                 .Include(x => x.PerfilUsuario)
                                                                 .Include(x => x.Aluno).AsNoTracking());

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> BuscarUsuarioPorNome(string userName)
        {
            var query = await Task.FromResult(GenerateQuery((x => x.UserName == userName), null)
                                                                        .Include(x => x.Unidade)
                                                                        .Include(x => x.Funcionario)
                                                                        .Include(x => x.Departamento));

            return query.SingleOrDefault();
        }

         public async Task<IEnumerable<Usuario>> BuscaUsuarioPorUnidade(int? funcionarioId, int? departamentoId, int? unidadeId)
        {
            try
            {
                var usuarios = dbSet.Include(x => x.Unidade)
                 .Where(x => x.IsDelete == false &&
                             (unidadeId.HasValue ? x.UnidadeId == unidadeId.Value : true)
                             && x.IsAluno == false)
                 .Select(x => new Usuario
                 {
                     UserName = x.UserName,
                     Id = x.Id,
                     UnidadeId = x.UnidadeId,
                     IsActive = x.IsActive,
                 })
                 .Where(x => x.IsActive)
                 .Distinct()
                 .AsNoTracking()
                 .ToList();

                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Usuario>> FiltrarUsuario(int? funcionarioId, int? departamentoId, int? unidadeId)
        {
            try
            {
                var usuarios = dbSet.Include(x => x.Unidade)
                 .Include(x => x.Funcionario)
                 .Include(x => x.Departamento)
                 .Include(x => x.Aluno)
                 .Where(x => x.IsDelete == false &&
                             (unidadeId.HasValue ? x.UnidadeId == unidadeId.Value : true) &&
                             (departamentoId.HasValue ? x.DepartamentoId == departamentoId.Value : true) &&
                             (funcionarioId.HasValue ? x.FuncionarioId == funcionarioId.Value : true))
                 .Select(x => new Usuario
                 {
                     Id = x.Id,
                     UnidadeId = x.UnidadeId,
                     IsActive = x.IsActive
                 })
                 .Where(x => x.IsActive)
                 .Distinct()
                 .AsNoTracking()
                 .ToList();

                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> Login(string userName, string senha)
        {
            try
            {
                IQueryable<Usuario> query = await Task.FromResult(GenerateQuery((x => x.UserName.ToLower() == userName && x.Password.ToLower() == senha && x.IsActive), null)
                                                      .Include(x=> x.PerfilUsuario)
                                                      .Include(x => x.Aluno));

                return query.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUsuarioAdmin(int perfilId)
        {
            try
            {
                return dbContext.Set<Usuario>().AsNoTracking().Any(x => x.PerfilUsuarioId == perfilId);
            }
            catch
            {
                throw;
            }
        }
    }
}
