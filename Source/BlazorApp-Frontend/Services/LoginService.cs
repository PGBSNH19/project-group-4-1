using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BlazorApp_Frontend.Services
{
    public class LoginService
    {
        private readonly UserRepository _userRepository;

        public LoginService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LoginAsync(User user)
        {
            //var dbUser = await _userRepository.GetUserByUsername(user.Username);
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var dbUser = new User
            {
                Username = "oskarmorell",
                Salt = salt,
                Password = "Hej1234"
            };

            if (dbUser != null)
            {
                string hashedPasswordDb = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: dbUser.Password,
                    salt: dbUser.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                string hashedPasswordClient = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Password,
                    salt: dbUser.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                if (hashedPasswordDb == hashedPasswordClient)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
