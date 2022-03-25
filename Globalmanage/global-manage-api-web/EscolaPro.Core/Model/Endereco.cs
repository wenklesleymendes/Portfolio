using EscolaPro.Core.Interfaces;

namespace EscolaPro.Core.Model
{
    public class Endereco : BaseEntity, IIdentityEntity
    {
        private string rua;
        private string bairro;
        private string cidade;

        public int Id { get; set; }
        public string CEP { get; set; }
        public string Rua { get => rua?.ToTitleCase(); set => rua = value?.ToTitleCase(); }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get => bairro?.ToTitleCase(); set => bairro = value?.ToTitleCase(); }
        public string Cidade { get => cidade?.ToTitleCase(); set => cidade = value?.ToTitleCase(); }
        public string Estado { get; set; }
    }
}