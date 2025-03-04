using System.ComponentModel.DataAnnotations;

namespace Goals.API.DTOs.Request
{
    public record LoginInputDTO(
        [Required, EmailAddress] string Email,
        [Required, MinLength(6)] string Password);
}
