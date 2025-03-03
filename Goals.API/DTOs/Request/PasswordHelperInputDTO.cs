namespace Goals.API.DTOs.Request
{
    public record PasswordHelperInputDTO(string Salt, string Hash);
}
