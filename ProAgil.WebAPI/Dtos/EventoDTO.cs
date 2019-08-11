using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo local deve ter entre 3 e 100 caracteres")]
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "O campo tema é de preenchimnto obrigatório!")]
        public string Tema { get; set; }
        [Range(2, 120000, ErrorMessage = "A quantidade de pessoas deve ser entre 2 e 120.000!")]
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }
        [Phone(ErrorMessage = "O telefone não é válido!")]
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido!")]
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}