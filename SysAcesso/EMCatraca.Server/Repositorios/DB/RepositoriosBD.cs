using EMCatraca.Server.Interfaces;
using EscolarManager.Framework.Conexao;

namespace EMCatraca.Server.Repositorios.DB
{
    public class RepositoriosBD : IRepositorios
    {
        private readonly IDBHelper _dbhelper;

        public RepositoriosBD(IDBHelper dbhelper)
        {
            _dbhelper = dbhelper;
        }

        public IRepositorioAuditoria CrieRepositorioAuditoria()
        {
            return new RepositorioAuditoria(_dbhelper);
        }

        public IRepositorioOperador CrieRepositorioOperador()
        {
            return new RepositorioOperador(_dbhelper);
        }

        public IRepositorioAluno CrieRepositorioAluno()
        {
            return new RepositorioAluno(_dbhelper);
        }

        public IRepositorioAutorizadoBuscarAluno CrieRepositorioAutorizadoBuscarAluno()
        {
            return new RepositorioAutorizadoBuscarAluno(_dbhelper);
        }

        public IRepositorioColaborador CrieRepositorioColaborador()
        {
            return new RepositorioColaborador(_dbhelper);
        }

        public IRepositorioOcorrencias CrieRepositorioOcorrencias()
        {
            return new RepositorioOcorrencias(_dbhelper);
        }

        public IRepositorioProfessor CrieRepositorioProfessor()
        {
            return new RepositorioProfessor(_dbhelper);
        }

        public IRepositorioDeAcessoPessoa CrieRepositorioAcesso()
        {
            return new RepositorioAcessoPessoa(_dbhelper);
        }

        public IRepositorioResponsavel CrieRepositorioResponsavel()
        {
            return new RepositorioResponsavel(_dbhelper);
        }

        public IRepositorioAtributosAdicionais CrieRepositorioAtributosAdicionais()
        {
            return new RepositorioAtributosAdicionais(_dbhelper);
        }

        public IRepositorioSerieTurma CrieRepositorioSerieTurma()
        {
            return new RepositorioSerieTurma(_dbhelper);
        }
    }
}
