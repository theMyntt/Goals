namespace Goals.API.Exceptions
{
    public class NotFoundException(string message) : HttpException(message, 404);
}
