using ModelPrincipal.Utilitarios;
using Repositorio.Persistencia.Configuracoes;
using System.Collections.Generic;

namespace Processos.Configuracoes
{
    public class ProcessoConfiguracao
    {
        private static readonly IRepositorioConfiguracoes Repositorio = new RepositorioConfiguracoes();

        public ProcessoConfiguracao()
        { }

        public void CrieConfiguracao(DtoConfigModulos configuracao)
        {
            Repositorio.Crie(configuracao);
        }

        public IEnumerable<DtoConfigModulos> ObtenhaConfiguracao()
        {
            return Repositorio.ObtenhaTodos();
        }

        public void AtualizePathModulos(DtoConfigModulos configuracao)
        {
            Repositorio.RemovaTodos();
            Repositorio.Crie(configuracao);
        }

        public void AtualizePathBD(DtoConfigModulos configuracao)
        {
            Repositorio.RemovaTodos();
            Repositorio.Crie(configuracao);
        }
    }
}
