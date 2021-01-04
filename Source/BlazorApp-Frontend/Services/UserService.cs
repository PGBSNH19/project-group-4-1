using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;

namespace BlazorApp_Frontend.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public HttpClient http { get; }
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginRequest request)
        {
            var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await http.PostJsonAsync<UserManagerResponse>(http.BaseAddress + "/api/auth/login", data);
            return response;
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
