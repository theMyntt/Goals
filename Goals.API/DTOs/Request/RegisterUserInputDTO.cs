using System.ComponentModel.DataAnnotations;

namespace Goals.API.DTOs.Request
{
    public record RegisterUserInputDTO(
        [Required] string Name,
        [Required, EmailAddress] string Email,
        [Required, MinLength(6)] string Password);
}
