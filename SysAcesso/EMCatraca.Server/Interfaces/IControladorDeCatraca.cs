using EMCatraca.Core.Services;

namespace EMCatraca.Server.Interfaces
{
    public interface IControladorDeCatraca
    {
        void Inicie(IServicoMonitorAcesso servicoMonitorAcesso);

        void Pare();
    }
}
