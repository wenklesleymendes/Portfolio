using ModelPrincipal.Utilitarios;
using System.Collections.Generic;

namespace Repositorio.Persistencia.Configuracoes
{
    public interface IRepositorioConfiguracoes
    {
        void Crie(DtoConfigModulos configuracao);
        void Atualize(DtoConfigModulos configuracao);
        IEnumerable<DtoConfigModulos> ObtenhaTodos();
        void RemovaTodos();
    }
}
