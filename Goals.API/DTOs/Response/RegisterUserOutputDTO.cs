namespace Goals.API.DTOs.Response
{
    public record RegisterUserOutputDTO(
        string Message,
        string Token,
        int StatusCode);
}
