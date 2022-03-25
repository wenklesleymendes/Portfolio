using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class ContasAPagarScript
    {
        public static string Filtrar(string cpfCnpj, string categoria, DateTime? inicioPeriodo, DateTime? fimPeriodo, int? idUnidade, Core.Model.Enums.TipoPagamentoEnum? tipoPagamento, Core.Model.Fornecedores.TipoPessoaEnum? tipoPessoa, Core.Model.MetasComissoes.StatusPagamentoEnum? statusPagamento)
        {
            string sqlQueryFiltro = @"select DISTINCT despesa.Id, despesa.DataEmissao from Despesa despesa
                                        inner join Unidade unidade
                                        on despesa.UnidadeId = unidade.Id ";


            if (tipoPessoa.HasValue || !string.IsNullOrEmpty(cpfCnpj))
            {
                sqlQueryFiltro = sqlQueryFiltro + @"inner join Fornecedor fornecedor 
                                                    on despesa.fornecedorId = fornecedor.Id ";
            }

            sqlQueryFiltro = sqlQueryFiltro + @"inner join Categoria categoria 
                                                on despesa.CategoriaId = categoria.Id
                                                inner join DespesaParcela despesaParcela
                                                on despesa.Id = despesaParcela.DespesaId
                                                where despesa.IsDelete = 0";

            if (inicioPeriodo.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and DespesaParcela.DataVencimento >= '{inicioPeriodo.Value.Date.ToString("yyyy-MM-dd")}'";
            }

            if (fimPeriodo.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and DespesaParcela.DataVencimento <= '{fimPeriodo.Value.Date.AddDays(1).ToString("yyyy-MM-dd")}'";
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and categoria.Descricao like '%{categoria}%'";
            }

            if (!string.IsNullOrEmpty(cpfCnpj))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and fornecedor.CpfCnpj = '{cpfCnpj}'";
            }

            if (idUnidade.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and despesa.UnidadeId = {idUnidade.Value}";
            }

            if (tipoPagamento.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and despesaParcela.TipoPagamento = {(int)tipoPagamento.Value}";
            }

            if (tipoPessoa.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and fornecedor.TipoPessoa = { (int)tipoPessoa.Value}";
            }

            if (statusPagamento.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and despesaParcela.StatusPagamento = { (int)statusPagamento.Value}";
            }

            return sqlQueryFiltro;
        }
    }
}
