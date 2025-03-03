using System.Security.Cryptography;
using System.Text;
using Goals.API.DTOs.Request;
using Goals.API.DTOs.Response;

namespace Goals.API.Helpers
{
    public class PasswordHelper
    {
        public static PasswordHelperOutputDTO Encrypt(string password)
        {
            using var hmac = new HMACSHA256();

            return new PasswordHelperOutputDTO(
                Salt: Convert.ToBase64String(hmac.Key),
                Hash: Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))));
        }

        public static bool Compare(string password, PasswordHelperInputDTO dto)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(dto.Salt));

            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

            return computedHash == dto.Hash;
        }
    }
}
