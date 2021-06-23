using System.ComponentModel.DataAnnotations;

namespace SysCar.Dominio
{
    public class MotoristaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A Nome é de Preenchimento Obrigatório")]
        [StringLength(50, ErrorMessage = "Nome tem no mínimo 2 caracteres e no máximo 50", MinimumLength = 2)]
        public string Nome { get; set; }
    }
}