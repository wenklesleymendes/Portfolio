using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ControlePontoEletronico;
using System.ComponentModel.DataAnnotations;
using EscolaPro.Repository.Scripts;

namespace EscolaPro.Repository.Repository
{
    public class FuncionarioRepository : DomainRepository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<Funcionario> Atualizar(Funcionario funcionario)
        {
            if (funcionario.Contato != null)
            {
                if (funcionario.Contato.Id == 0)
                {
                    dbContext.Entry<Contato>(funcionario.Contato).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                    funcionario.ContatoId = funcionario.Contato.Id;
                }
                else
                {
                    dbContext.Entry<Contato>(funcionario.Contato).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }

            if (funcionario.Endereco != null)
            {
                if (funcionario.Endereco.Id == 0)
                {
                    dbContext.Entry<Endereco>(funcionario.Endereco).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                    funcionario.EnderecoId = funcionario.Endereco.Id;
                }
                else
                {
                    dbContext.Entry<Endereco>(funcionario.Endereco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }

            if (funcionario.DadosContratacao != null)
            {
                if (funcionario.DadosContratacao.Id == 0)
                {
                    dbContext.Entry<DadosContratacao>(funcionario.DadosContratacao).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                    funcionario.DadosContratacaoId = funcionario.DadosContratacao.Id;
                }
                else
                {
                    var dadosContratacaoOld = dbContext.Set<DadosContratacao>().AsNoTracking().Where(x => x.Id == funcionario.DadosContratacaoId).FirstOrDefault();

                    if (funcionario.DadosContratacao.TipoRegimeContratacao != dadosContratacaoOld.TipoRegimeContratacao)
                    {
                        funcionario.DadosContratacao.TipoRegimeContratacaoAnterior = dadosContratacaoOld.TipoRegimeContratacao;
                        funcionario.DadosContratacao.DataAlteracaoRegime = funcionario.DadosContratacao.DataAlteracaoRegime;
                    }
                    else
                    {
                        funcionario.DadosContratacao.TipoRegimeContratacaoAnterior = dadosContratacaoOld.TipoRegimeContratacao;
                    }


                    if (funcionario.DadosContratacao.DataAlteracaoRegime != dadosContratacaoOld.DataAlteracaoRegime)
                    {
                        funcionario.DadosContratacao.DataAlteracaoRegime = funcionario.DadosContratacao.DataAlteracaoRegime;
                    }

                    dbContext.Entry<DadosContratacao>(funcionario.DadosContratacao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }

            if (funcionario.AgenteIntegracao != null)
            {
                if (funcionario.AgenteIntegracao.Id == 0)
                {
                    dbContext.Entry<AgenteIntegracao>(funcionario.AgenteIntegracao).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                    funcionario.AgenteIntegracaoId = funcionario.AgenteIntegracao.Id;
                }
                else
                {
                    dbContext.Entry<AgenteIntegracao>(funcionario.AgenteIntegracao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }

            if (funcionario.JornadaTrabalho != null)
            {
                if (funcionario.JornadaTrabalho.Id == 0)
                {
                    dbContext.Entry<JornadaTrabalho>(funcionario.JornadaTrabalho).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                    funcionario.JornadaTrabalhoId = funcionario.JornadaTrabalho.Id;
                }
                else
                {
                    dbContext.Entry<JornadaTrabalho>(funcionario.JornadaTrabalho).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

            }

            if (funcionario.DadosBancario != null)
            {
                if (funcionario.DadosBancario.Id == 0)
                {
                    dbContext.Entry<DadosBancario>(funcionario.DadosBancario).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                    funcionario.DadosBancarioId = funcionario.DadosBancario.Id;
                }
                else
                {
                    dbContext.Entry<DadosBancario>(funcionario.DadosBancario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }

            await UpdateAsync(funcionario);

            IQueryable<Funcionario> query = await Task.FromResult(GenerateQuery((x => x.Id == funcionario.Id && !x.IsDelete), null));

            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<Funcionario>> BuscarListaPorUnidade(int idUnidade)
        {
            var funcionarioSalarioUnidade = dbContext.Set<SalarioUnidade>().Where(x => x.UnidadeId == idUnidade).ToList();

            List<Funcionario> funcionarios = new List<Funcionario>();

            foreach (var item in funcionarioSalarioUnidade)
            {
                var funcionario = await BuscarPorId(item.FuncionarioId);

                funcionarios.Add(funcionario);
            }

            return funcionarios;
        }

        public async Task<Funcionario> BuscarPorCPF(string cpf)
        {
            IQueryable<Funcionario> query = await Task.FromResult(GenerateQuery((x => x.CPF == cpf && !x.IsDelete), null));

            return query.FirstOrDefault();
        }

        public async Task<Funcionario> BuscarPorId(int idFuncionario)
        {
            try
            {
                IQueryable<Funcionario> query = await Task.FromResult(GenerateQuery((x => x.Id == idFuncionario && !x.IsDelete), null)
                    .Include(x => x.JornadaTrabalho)
                    .Include(x => x.MateriaProfessor)
                    .Include(x => x.SalarioUnidade)
                    .Include(x => x.AgenteIntegracao)
                    .Include(x => x.Contato)
                    .Include(x => x.Endereco)
                    .Include(x => x.DadosBancario)
                    .Include(x => x.DadosContratacao)
                    .Include(x => x.MateriaProfessor)
                    .Include(x => x.CursoProfessor)
                    .Include(x => x.Ferias));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Obsolete]
        public async Task<IEnumerable<Funcionario>> BuscarTodosPorFiltro(int? idUnidade, string nome, bool? ativo, string cpf, DateTime? dataInicioTerminoContrato, DateTime? dataFimTerminoContrato)
        {
            try
            {
                string sqlQuery = FuncionarioScript.FiltrarFuncionario(idUnidade, nome, ativo, cpf, dataInicioTerminoContrato, dataFimTerminoContrato);

                var query = dbSet.FromSql(sqlQuery).Select(x => new Funcionario
                {
                    Id = x.Id,
                    //Nome = x.Nome,
                    //AgenteIntegracaoId = x.AgenteIntegracaoId,
                    //AgenteIntegracao = x.AgenteIntegracao,
                    //Contato = x.Contato,
                    //ContatoId = x.ContatoId,
                    //CPF = x.CPF,
                    //CursoProfessor = x.CursoProfessor,
                    //DadosBancario = x.DadosBancario,
                    //DadosBancarioId = x.DadosBancarioId,
                    //DadosContratacao = x.DadosContratacao,
                    //DadosContratacaoId = x.DadosContratacaoId,
                    //DataNascimento = x.DataNascimento,
                    //Endereco = x.Endereco,
                    //EnderecoId = x.EnderecoId,
                    //JornadaTrabalho = x.JornadaTrabalho,
                    //JornadaTrabalhoId = x.JornadaTrabalhoId,
                    //MateriaProfessor = x.MateriaProfessor,
                    //MateriaProfessorId = x.MateriaProfessorId,
                    //SalarioUnidade = x.SalarioUnidade,
                    //RG = x.RG,
                    //IsActive = x.IsActive,
                    //IsDelete = x.IsDelete
                }).ToList();

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SalarioUnidade>> InserirSalarioUnidade(List<SalarioUnidade> salariosUnidade)
        {

            var salarioUnidadeExcluir = dbContext.Set<SalarioUnidade>();

            foreach (var item in salarioUnidadeExcluir.Where(x => x.FuncionarioId == salariosUnidade.FirstOrDefault().FuncionarioId))
            {
                dbContext.Entry<SalarioUnidade>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            await dbContext.SaveChangesAsync();

            foreach (var item in salariosUnidade.ToList())
            {
                dbContext.Entry<SalarioUnidade>(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            await dbContext.SaveChangesAsync();

            var retorno = dbContext.Set<SalarioUnidade>().Where(x => x.FuncionarioId == salariosUnidade.FirstOrDefault().FuncionarioId).ToList();

            return retorno;
        }

        public async Task<List<FuncionarioRobo>> ListaPIS()
        {
            IQueryable<Funcionario> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null));

            List<FuncionarioRobo> funcionarioRobo = new List<FuncionarioRobo>();

            foreach (var item in query)
            {
                if (item.Nome != "admin")
                {
                    var contrato = dbContext.Set<DadosContratacao>().Where(x => x.Id == item.DadosContratacaoId).FirstOrDefault();

                    funcionarioRobo.Add(new FuncionarioRobo
                    {
                        FuncionarioId = item.Id,
                        CPF = item.CPF,
                        NumeroPIS = contrato.NumeroPIS,
                        RegimeContratacao = contrato.TipoRegimeContratacao,
                        RegimeContratacaoAntigo = contrato.TipoRegimeContratacaoAnterior.HasValue ? contrato.TipoRegimeContratacaoAnterior : null,
                        DataRegimeAnterior = contrato.DataAlteracaoRegime
                    });
                }
            }


            return funcionarioRobo;
        }

        [Obsolete]
        public async Task<Funcionario> BuscarPorMateria(int materiaId)
        {
            try
            {

                string sqlQuery = FuncionarioScript.Filtrar(materiaId);

                var query = dbSet.FromSql(sqlQuery).Select(x => new Funcionario
                {
                    Id = x.Id,
                }).ToList();

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
