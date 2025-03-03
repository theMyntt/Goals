namespace Goals.API.Abstractions.Helpers
{
    public interface IJwtHelper
    {
        /// <summary>
        /// Wait name and email of user and give you a 7 days Jwt Token
        /// </summary>
        /// <param name="name">
        /// Name of User
        /// </param>
        /// <param name="email">
        /// Email of User
        /// </param>
        /// <returns>
        /// 7 days Jwt Token
        /// </returns>
        string GenerateToken(string name, string email);
    }
}
