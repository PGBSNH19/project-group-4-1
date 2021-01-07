using AKSoftware.WebApi.Client;
using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public HttpClient http { get; }

        ServiceClient client = new ServiceClient();
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginRequest request)
        {
            var response = await client.PostAsync<UserManagerResponse>("https://nearbyproduceapitest.azurewebsites.net/api/v1.0/User/login", request);
            return response.Result;
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
