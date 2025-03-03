namespace Goals.API.DTOs.Response
{
    public record LoginOutputDTO(
        string Message,
        string Token,
        int StatusCode);
}
