namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositoriosAPI : IRepositorios
    {
        public IRepositorioAuditoria CrieRepositorioAuditoria()
        {
            return new RepositorioAuditoriaAPI();
        }

        public IRepositorioOperador CrieRepositorioOperador()
        {
            return new RepositorioOperadorAPI();
        }

        public IRepositorioAluno CrieRepositorioAluno()
        {
            return new RepositorioAlunoAPI();
        }

        public IRepositorioAutorizadoBuscarAluno CrieRepositorioAutorizadoBuscarAluno()
        {
            return new RepositorioAutorizadoBuscarAlunoAPI();
        }

        public IRepositorioColaborador CrieRepositorioColaborador()
        {
            return new RepositorioColaboradorAPI();
        }

        public IRepositorioOcorrencias CrieRepositorioOcorrencias()
        {
            return new RepositorioOcorrenciasAPI();
        }

        public IRepositorioProfessor CrieRepositorioProfessor()
        {
            return new RepositorioProfessorAPI();
        }

        public IRepositorioDeAcessoPessoa CrieRepositorioAcesso()
        {
            return new RepositorioAcessoPessoaAPI();
        }

        public IRepositorioResponsavel CrieRepositorioResponsavel()
        {
            return new RepositorioResponsavelAPI();
        }

        public IRepositorioAtributosAdicionais CrieRepositorioAtributosAdicionais()
        {
            return new RepositorioAtributosAdicionaisAPI();
        }

        public IRepositorioSerieTurma CrieRepositorioSerieTurma()
        {
            return new RepositorioSerieTurmaAPI();
        }
    }
}
