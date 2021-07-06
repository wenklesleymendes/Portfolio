namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositoriosBD : IRepositorios
    {
        public IRepositorioAuditoria CrieRepositorioAuditoria()
        {
            return new RepositorioAuditoria();
        }

        public IRepositorioOperador CrieRepositorioOperador()
        {
            return new RepositorioOperador();
        }

        public IRepositorioAluno CrieRepositorioAluno()
        {
            return new RepositorioAluno();
        }

        public IRepositorioAutorizadoBuscarAluno CrieRepositorioAutorizadoBuscarAluno()
        {
            return new RepositorioAutorizadoBuscarAluno();
        }

        public IRepositorioColaborador CrieRepositorioColaborador()
        {
            return new RepositorioColaborador();
        }

        public IRepositorioOcorrencias CrieRepositorioOcorrencias()
        {
            return new RepositorioOcorrencias();
        }

        public IRepositorioProfessor CrieRepositorioProfessor()
        {
            return new RepositorioProfessor();
        }

        public IRepositorioDeAcessoPessoa CrieRepositorioAcesso()
        {
            return new RepositorioAcessoPessoa();
        }

        public IRepositorioResponsavel CrieRepositorioResponsavel()
        {
            return new RepositorioResponsavel();
        }

        public IRepositorioAtributosAdicionais CrieRepositorioAtributosAdicionais()
        {
            return new RepositorioAtributosAdicionais();
        }

        public IRepositorioSerieTurma CrieRepositorioSerieTurma()
        {
            return new RepositorioSerieTurma();
        }
    }
}
