using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class PalestranteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string MiniCurriculo { get; set; }
        public string ImagemUrl { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [Phone(ErrorMessage = "O telefone não é válido!")]
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido!")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        public string Email { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }
    }
}