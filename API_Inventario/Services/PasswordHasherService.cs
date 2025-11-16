using System.Security.Cryptography;
using System.Text;
using API_Inventario.Services.Interfaces;

namespace API_Inventario.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var inputPassword = HashPassword(password);
            return inputPassword == hashedPassword;
        }
    }
}
