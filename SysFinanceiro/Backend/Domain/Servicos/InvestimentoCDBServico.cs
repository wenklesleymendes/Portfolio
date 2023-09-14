using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;

namespace Domain.Servicos
{
    public class InvestimentoCDBServico : IInvestimentoCDBServico
    {
        public DadosCDB CalculaCDB(DadosCDB dados)
        {
            Investimento investimento = new(dados.Valor, dados.QtdMeses);

            investimento.CalculaValorFinal();

            dados.ValorLiquido = investimento.ValorLiquido;
            dados.ValorBruto = investimento.VF;

            return dados;
        }
    }
}
