namespace Acesso.Interfaces
{
    public interface IRepositorios
    {
        IRepositorioDeAcessoPessoa CrieRepositorioAcesso();
        IRepositorioAluno CrieRepositorioAluno();
        IRepositorioAtributosAdicionais CrieRepositorioAtributosAdicionais();
        IRepositorioAuditoria CrieRepositorioAuditoria();
        IRepositorioAutorizadoBuscarAluno CrieRepositorioAutorizadoBuscarAluno();
        IRepositorioColaborador CrieRepositorioColaborador();
        IRepositorioOcorrencias CrieRepositorioOcorrencias();
        IRepositorioOperador CrieRepositorioOperador();
        IRepositorioProfessor CrieRepositorioProfessor();
        IRepositorioResponsavel CrieRepositorioResponsavel();
        IRepositorioSerieTurma CrieRepositorioSerieTurma();
    }
}