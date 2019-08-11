using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class LoteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo local deve ter entre 3 e 50 caracteres")]
        public string Nome { get; set; }
        [Range(2, 200000, ErrorMessage = "O preço deve ser entre R$2 e R$200.000")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public decimal Preco { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        [Range(2, 120000, ErrorMessage = "A {0} de pessoas deve ser entre 2 e 120.000!")]
        public int Quantidade { get; set; }
    }
}