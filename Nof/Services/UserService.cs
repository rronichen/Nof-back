using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nof.Helpers;
using Nof.Model;
using Nof.Models;
using Nof.Provider;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Nof.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        int RegisterUser(User user);
    }
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        private readonly IUserProvider _userProvider ;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext context, IUserProvider userProvider)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _userProvider = userProvider;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            
            var user =_context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }


        public User GetById(int id)
        {
            return _userProvider.GetUser(id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int RegisterUser(User user)
        {
             return  _userProvider.AddUser(user);
           
        }
    }
}

