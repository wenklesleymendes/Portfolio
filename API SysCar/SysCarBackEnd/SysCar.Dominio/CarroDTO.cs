using System.ComponentModel.DataAnnotations;

namespace SysCar.Dominio
{
    public class CarroDTO
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "A Marca é de Preenchimento Obrigatório")]
        [StringLength(50, ErrorMessage = "Marca tem no mínimo 2 caracteres e no máximo 50", MinimumLength = 2)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "A Cor é de Preenchimento Obrigatório")]
        [StringLength(50, ErrorMessage = "Cor tem no mínimo 2 caracteres e no máximo 30", MinimumLength = 2)]
        public string Cor { get; set; }

        [Required(ErrorMessage = "A palca é de Preenchimento Obrigatório")]
        [StringLength(50, ErrorMessage = "Placa tem no mínimo 2 caracteres e no máximo 10", MinimumLength = 2)]
        public string Placa { get; set; }
    }
}