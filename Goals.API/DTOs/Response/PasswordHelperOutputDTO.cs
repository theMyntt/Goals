namespace Goals.API.DTOs.Response
{
    public record PasswordHelperOutputDTO(string Salt, string Hash);
}
