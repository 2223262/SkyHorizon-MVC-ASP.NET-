using System.ComponentModel.DataAnnotations;

namespace SkyHorizon_2223262.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de utilizador é obrigatório")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "A palavra-passe é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string Role { get; set; } = "User";

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = null!;
    }
}
