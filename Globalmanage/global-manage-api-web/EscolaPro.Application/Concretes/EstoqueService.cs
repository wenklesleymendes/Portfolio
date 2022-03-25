using AutoMapper;
using EscolaPro.Core.Model.EstoqueProdutos;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.EstoqueProdutos;
using EscolaPro.Service.Dto.EstoqueVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IEstoqueHistoricoRepository _estoqueHistoricoRepository;
        private readonly IItemProdutoEstoqueRepository _itemProdutoEstoqueRepository;
        private readonly IMapper _mapper;

        public EstoqueService(
            IEstoqueRepository estoqueRepository,
            IUnidadeRepository unidadeRepository,
            IEstoqueHistoricoRepository estoqueHistoricoRepository,
            IItemProdutoEstoqueRepository itemProdutoEstoqueRepository,
            IMapper mapper)
        {
            _estoqueRepository = estoqueRepository;
            _estoqueHistoricoRepository = estoqueHistoricoRepository;
            _itemProdutoEstoqueRepository = itemProdutoEstoqueRepository;
            _unidadeRepository = unidadeRepository;
            _mapper = mapper;
        }

        public async Task<DtoItemProduto> AdicionarItem(DtoItemProduto dtoItemProduto)
        {
            try
            {
                if(dtoItemProduto.Id == 0)
                {
                    int saldoNegativo = 0;

                    var ultimoItemAdicionado = await _itemProdutoEstoqueRepository.BuscarEstoqueDisponivel(dtoItemProduto.ProdutoId);

                    if(ultimoItemAdicionado != null)
                    {
                        if(ultimoItemAdicionado.QuantidadeSaida > ultimoItemAdicionado.QuantidadeEntrada)
                        {
                            saldoNegativo = ultimoItemAdicionado.QuantidadeSaida - ultimoItemAdicionado.QuantidadeEntrada;
                        }

                        ultimoItemAdicionado.QuantidadeSaida = ultimoItemAdicionado.QuantidadeSaida - saldoNegativo;

                        await _itemProdutoEstoqueRepository.UpdateAsync(ultimoItemAdicionado);
                    }

                    dtoItemProduto.QuantidadeSaida = saldoNegativo;

                    var itemProduto = await _itemProdutoEstoqueRepository.AddAsync(_mapper.Map<ItemProduto>(dtoItemProduto));

                    var produto = await _estoqueRepository.GetByIdAsync(itemProduto.ProdutoId);

                    var unidade = await _unidadeRepository.GetByIdAsync(produto.UnidadeId);

                    HistoricoEstoque historicoEstoque = new HistoricoEstoque
                    {
                        Id = 0,
                        Descricao = $"Unidade {unidade.Nome}, entrada de {itemProduto.QuantidadeEntrada} apostilas.",
                        DataCadastro = DateTime.Now,
                        IdEstoque = itemProduto.ProdutoId,
                        TipoHistorico = TipoHistoricoEnum.Entrada
                    };

                    await _estoqueHistoricoRepository.AddAsync(historicoEstoque);
                 
                    return _mapper.Map<DtoItemProduto>(itemProduto);
                }
                else
                {
                    int saldoNegativo = 0;

                    var ultimoItemAdicionado = await _itemProdutoEstoqueRepository.BuscarEstoqueDisponivel(dtoItemProduto.ProdutoId);

                    if (ultimoItemAdicionado != null)
                    {
                        if (ultimoItemAdicionado.QuantidadeSaida > ultimoItemAdicionado.QuantidadeEntrada)
                        {
                            saldoNegativo = ultimoItemAdicionado.QuantidadeSaida - ultimoItemAdicionado.QuantidadeEntrada;
                        }

                        ultimoItemAdicionado.QuantidadeSaida = ultimoItemAdicionado.QuantidadeSaida - saldoNegativo;

                        await _itemProdutoEstoqueRepository.UpdateAsync(ultimoItemAdicionado);
                    }

                    dtoItemProduto.QuantidadeSaida = saldoNegativo;

                    var produto = await _estoqueRepository.GetByIdAsync(dtoItemProduto.ProdutoId);

                    var unidade = await _unidadeRepository.GetByIdAsync(produto.UnidadeId);

                    await _itemProdutoEstoqueRepository.UpdateAsync(_mapper.Map<ItemProduto>(dtoItemProduto));

                    var itemProduto = await _itemProdutoEstoqueRepository.GetByIdAsync(dtoItemProduto.Id);

                    HistoricoEstoque historicoEstoque = new HistoricoEstoque
                    {
                        Id = 0,
                        Descricao = $"Alteração - Unidade {unidade.Nome}, entrada de {itemProduto.QuantidadeEntrada} apostilas.",
                        DataCadastro = DateTime.Now,
                        IdEstoque = itemProduto.ProdutoId,
                        TipoHistorico = TipoHistoricoEnum.Entrada
                    };

                    await _estoqueHistoricoRepository.AddAsync(historicoEstoque);

                    return _mapper.Map<DtoItemProduto>(itemProduto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoHistoricoEstoque>> BuscarHistoricoPorEstoque(int idEstoque)
        {
            var historicoEstoque = await _estoqueHistoricoRepository.BuscarPorIdEstoque(idEstoque);

            return _mapper.Map<IEnumerable<DtoHistoricoEstoque>>(historicoEstoque.OrderByDescending(x => x.DataCadastro));
        }

        public async Task<IEnumerable<DtoItemProduto>> BuscarItemProdutoPorEstoque(int idProduto)
        {
            var itemProdutoLista = await _itemProdutoEstoqueRepository.BuscarItemProdutoPorProdutoId(idProduto);

            return _mapper.Map<IEnumerable<DtoItemProduto>>(itemProdutoLista);
        }

        public async Task<DtoProduto> BuscarPorId(int idProduto)
        {
            var Estoque = await _estoqueRepository.BuscarPorId(idProduto);
            return _mapper.Map<DtoProduto>(Estoque);
        }

        public async Task<IEnumerable<DtoGridEstoque>> BuscarTodos()
        {
            var produtosLista = await _estoqueRepository.GetAllAsync();

            List<DtoGridEstoque> dtoGridEstoquesLista = new List<DtoGridEstoque>();

            foreach (var item in produtosLista.Where(x => !x.IsDelete)) 
            {
                var produto = await _estoqueRepository.BuscarPorId(item.Id);

                var unidade = await _unidadeRepository.GetByIdAsync(produto.UnidadeId);

                var produtoRetorno = _mapper.Map<DtoProduto>(produto);

                var itemProdutoLoteDisponivel = await _itemProdutoEstoqueRepository.BuscarItemProdutoPorProdutoId(produto.Id);

                int quantidadeSaida = 0;  
                int quantidadeEntrada = 0;

                if (itemProdutoLoteDisponivel != null)
                {
                    quantidadeSaida = itemProdutoLoteDisponivel.Sum(x => x.QuantidadeSaida);
                    quantidadeEntrada = itemProdutoLoteDisponivel.Sum(x => x.QuantidadeEntrada);
                }

                dtoGridEstoquesLista.Add(new DtoGridEstoque
                {
                    Id = item.Id,
                    CodigoInterno = produto.CodigoInterno,
                    NomeProduto = produto.NomeProduto,
                    Unidade = unidade.Nome,
                    QuantidadeEstoque = quantidadeSaida > quantidadeEntrada ? 0 :  quantidadeEntrada - quantidadeSaida,
                    QuantidadeSaida = quantidadeSaida
                }); 
            }

            return dtoGridEstoquesLista;
        }

        public async Task<bool> DesativarOuAtivar(int idProduto)
        {
            var Estoque = await _estoqueRepository.GetByIdAsync(idProduto);
            Estoque.IsActive = Estoque.IsActive ? Estoque.IsActive = false : Estoque.IsActive = true;
            await _estoqueRepository.UpdateAsync(Estoque);
            return Estoque.IsActive;
        }

        public async Task<bool> Excluir(int idProduto)
        {
            var produto = await _estoqueRepository.GetByIdAsync(idProduto);
            produto.IsDelete = true;
            await _estoqueRepository.UpdateAsync(produto);
            return produto.IsDelete;
        }

        public async Task<bool> ExcluirItem(int idItemProduto)
        {
            var produto = await _itemProdutoEstoqueRepository.GetByIdAsync(idItemProduto);

            produto.IsDelete = true;

            await _itemProdutoEstoqueRepository.UpdateAsync(produto);

            int quantidadeRemovida = produto.QuantidadeEntrada - produto.QuantidadeSaida;

            HistoricoEstoque historicoEstoque = new HistoricoEstoque
            {
                Id = 0,
                Descricao = $"Estoque removido, quantidade de { quantidadeRemovida} apostilas.",
                DataCadastro = DateTime.Now,
                IdEstoque = produto.ProdutoId,
                TipoHistorico = TipoHistoricoEnum.Saida
            };

            await _estoqueHistoricoRepository.AddAsync(historicoEstoque);

            return produto.IsDelete;
        }

        public async Task<DtoProduto> Inserir(DtoProduto dtoProduto)
        {
            try
            {
                if (dtoProduto.Id == 0)
                {
                    var produto = await _estoqueRepository.AddAsync(_mapper.Map<Produto>(dtoProduto));


                    return _mapper.Map<DtoProduto>(produto);
                }
                else
                {
                    if(dtoProduto.Unidade != null)
                    {
                        dtoProduto.UnidadeId = dtoProduto.Unidade.Id;
                    }

                    await _estoqueRepository.UpdateAsync(_mapper.Map<Produto>(dtoProduto));

                    var retornoUpdate = await BuscarPorId(dtoProduto.Id);

                    return retornoUpdate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RetiradaApostila(int idProduto)
        {
            var itemProduto = await _itemProdutoEstoqueRepository.BuscarEstoqueDisponivel(idProduto);

            itemProduto.QuantidadeSaida  = itemProduto.QuantidadeSaida + 1;

            await _itemProdutoEstoqueRepository.UpdateAsync(itemProduto);

            var produto = await _estoqueRepository.GetByIdAsync(itemProduto.ProdutoId);

            var unidade = await _unidadeRepository.GetByIdAsync(produto.UnidadeId);

            Random random = new Random();

            int matricula = random.Next(1, 500);

            HistoricoEstoque historicoEstoque = new HistoricoEstoque
            {
                Id = 0,
                Descricao = $"Matrícula realizada Nº { matricula}, apostila grátis",
                DataCadastro = DateTime.Now,
                IdEstoque = itemProduto.ProdutoId,
                TipoHistorico = TipoHistoricoEnum.Saida
            };

            await _estoqueHistoricoRepository.AddAsync(historicoEstoque);
            
            return true;
        }
    }
}
