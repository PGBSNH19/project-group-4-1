using API.Context;
using API.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserRepository : Repository, IUserRepository
    {
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        public UserRepository(NearbyProduceContext context) : base(context)
        { }
        public UserRepository(NearbyProduceContext context, UserManager<IdentityUser> userManager, IConfiguration configuration) : base(context)
        {
            _userManger = userManager;
            _configuration = configuration;
        }
       
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await GetUserByEmail(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = await CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim("UserName", user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                UserInfo = claims.ToDictionary(c => c.Type, c => c.Value),
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            try
            {
                var dbUser = await GetUserByEmail(user.Email);
                if (dbUser != null)
                {
                    string hashedPasswordClient = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
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

        public async Task<ICollection<User>> GetUsers()
        {
            IQueryable<User> query = _context.Users.Include(UserProduct => UserProduct.UserProducts);
            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            IQueryable<User> query = _context.Users.Where(x => x.UserID == id).Include(UserProduct => UserProduct.UserProducts).ThenInclude(Products => Products.product);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByName(string name)
        {
            IQueryable<User> query = _context.Users.Where(x => x.Username == name).Include(UserProduct => UserProduct.UserProducts);
            return await query.FirstOrDefaultAsync();

        }

        public async Task<User> GetUserByEmail(string email)
        {
            IQueryable<User> query = _context.Users.Where(x => x.Email == email).Include(UserProduct => UserProduct.UserProducts);
            return await query.FirstOrDefaultAsync();
        }
    }
}
