namespace Goals.API.Exceptions
{
    public class ForbiddenException(string message) : HttpException(message, 403);
}
