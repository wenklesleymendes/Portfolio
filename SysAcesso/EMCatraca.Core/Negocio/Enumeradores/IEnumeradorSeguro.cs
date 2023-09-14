namespace EMCatraca.Core.Negocio.Enumeradores
{
    public interface IEnumeradorSeguro<K>
    {
        K Codigo { get; }
        string Descricao { get; }
    }
}
