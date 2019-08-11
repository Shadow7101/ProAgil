using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class RedeSocialDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre 3 e 50 caracteres")]
        public string Nome { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre 3 e 50 caracteres")]
        [Url(ErrorMessage = "A URL informada não é válida!")]
        public string Url { get; set; }
    }
}