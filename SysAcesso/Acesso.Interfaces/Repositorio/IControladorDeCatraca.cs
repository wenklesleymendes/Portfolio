using Acesso.Servicos;

namespace Acesso.Interfaces
{
    public interface IControladorDeCatraca
    {
        void Inicie(IServicoMonitorAcesso servicoMonitorAcesso);

        void Pare();
    }
}
