using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Repository.Interfaces.Fornecedores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Fornecedores
{
    public class FornecedorRepository : DomainRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<Fornecedor> BuscarPorId(int idFuncionario)
        {
            IQueryable<Fornecedor> query = await Task.FromResult(GenerateQuery((x => x.Id == idFuncionario), null)
                .Include(x => x.Endereco)
                .Include(x => x.Contato)
                .Include(x => x.Categoria)
                .Include(x => x.DadosBancario));

            return query.FirstOrDefault();
        }

        public async Task<Fornecedor> Inserir(Fornecedor fornecedor)
        {
            try
            {
                if(fornecedor.Id == 0)
                {
                    return await AddAsync(fornecedor);
                }
                else
                {

                    if(fornecedor.Endereco != null)
                    {
                        if(fornecedor.Endereco.Id == 0)
                        {
                            dbContext.Entry<Endereco>(fornecedor.Endereco).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry<Endereco>(fornecedor.Endereco).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (fornecedor.Contato != null)
                    {
                        if (fornecedor.Contato.Id == 0)
                        {
                            dbContext.Entry<Contato>(fornecedor.Contato).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry<Contato>(fornecedor.Contato).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (fornecedor.Categoria != null)
                    {
                        if (fornecedor.Categoria.Id == 0)
                        {
                            dbContext.Entry<Categoria>(fornecedor.Categoria).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry<Categoria>(fornecedor.Categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (fornecedor.DadosBancario != null)
                    {
                        if (fornecedor.DadosBancario.Id == 0)
                        {
                            dbContext.Entry<DadosBancario>(fornecedor.DadosBancario).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry<DadosBancario>(fornecedor.DadosBancario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    await UpdateAsync(fornecedor);
                    return fornecedor;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
