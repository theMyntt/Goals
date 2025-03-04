namespace Goals.API.DTOs.Request
{
    public record RegisterUserInputDTO(
        string Name,
        string Email,
        string Password);
}
