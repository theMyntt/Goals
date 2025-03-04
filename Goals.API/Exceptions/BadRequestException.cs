namespace Goals.API.Exceptions
{
    public class BadRequestException(string message) : HttpException(message, 400);
}
