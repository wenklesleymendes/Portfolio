using EMCatraca.Server.Interfaces;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositoriosAPI : IRepositorios
    {
        private readonly IAPIConexao _apiConexao;

        public RepositoriosAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public IRepositorioAuditoria CrieRepositorioAuditoria()
        {
            return new RepositorioAuditoriaAPI(_apiConexao);
        }

        public IRepositorioOperador CrieRepositorioOperador()
        {
            return new RepositorioOperadorAPI(_apiConexao);
        }

        public IRepositorioAluno CrieRepositorioAluno()
        {
            return new RepositorioAlunoAPI(_apiConexao);
        }

        public IRepositorioAutorizadoBuscarAluno CrieRepositorioAutorizadoBuscarAluno()
        {
            return new RepositorioAutorizadoBuscarAlunoAPI(_apiConexao);
        }

        public IRepositorioColaborador CrieRepositorioColaborador()
        {
            return new RepositorioColaboradorAPI(_apiConexao);
        }

        public IRepositorioOcorrencias CrieRepositorioOcorrencias()
        {
            return new RepositorioOcorrenciasAPI(_apiConexao);
        }

        public IRepositorioProfessor CrieRepositorioProfessor()
        {
            return new RepositorioProfessorAPI(_apiConexao);
        }

        public IRepositorioDeAcessoPessoa CrieRepositorioAcesso()
        {
            return new RepositorioAcessoPessoaAPI(_apiConexao);
        }

        public IRepositorioResponsavel CrieRepositorioResponsavel()
        {
            return new RepositorioResponsavelAPI(_apiConexao);
        }

        public IRepositorioAtributosAdicionais CrieRepositorioAtributosAdicionais()
        {
            return new RepositorioAtributosAdicionaisAPI(_apiConexao);
        }

        public IRepositorioSerieTurma CrieRepositorioSerieTurma()
        {
            return new RepositorioSerieTurmaAPI(_apiConexao);
        }
    }
}
