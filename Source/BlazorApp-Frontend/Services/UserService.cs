using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BlazorApp_Frontend.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LoginAsync(User user)
        {
            try
            {
                var dbUser = await _userRepository.GetUserByEmail(user.Email);
                if (dbUser != null)
                {
                    string hashedPasswordClient = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: user.Password,
                        salt: dbUser.Salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                    if (dbUser.Password == hashedPasswordClient)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception exception)
            {
                throw new Exception($"Database Failure: {exception.Message}");
            }
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string GenerateHash(string password, byte[] salt)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashedPassword;
        }
    }
}
