using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using 

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
            var dbUser = await _userRepository.GetUserByUsername(user.Username);
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
