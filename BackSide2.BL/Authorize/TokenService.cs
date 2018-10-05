using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extensions;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackSide2.BL.authorize
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Person> _personService;

        public TokenService(
            IConfiguration configuration,
            IRepository<Person> personService)
        {
            _configuration = configuration;
            _personService = personService;
        }


        public async Task<LoggedDto> RegisterAsync(RegisterDto model)
        {
            var person =
                await (await _personService.GetAllAsync(d => d.Email == model.Email || d.UserName == model.Username))
                    .FirstOrDefaultAsync();

            if (person != null)
            {
                if (person.UserName == model.Username && person.Email == model.Email)
                    throw new TokenServiceException("Email and username already taken.");

                if (person.UserName == model.Username) throw new TokenServiceException("Username already taken.");

                if (person.Email == model.Email) throw new TokenServiceException("Email already taken.");
            }

            var systemUser = await (await _personService.GetAllAsync(d => d.UserName == "system"))
                .FirstOrDefaultAsync();
            if (systemUser == null)
                throw new TokenServiceException("SystemUserNotFound");

            var newUser = await _personService.InsertAsync(model.ToPerson(systemUser));

            return newUser.ToLoggedDto(GenerateJwtToken(newUser));
        }

        public async Task<LoggedDto> LoginAsync(
            LoginDto model
        )
        {
            var person =
                await (await _personService.GetAllAsync(d =>
                        d.Email == model.Email && d.Password == Hash.GetPassHash(model.Password)))
                    .FirstOrDefaultAsync();

            if (person != null)
                return person.ToLoggedDto(GenerateJwtToken(person));

            throw new TokenServiceException("Wrong email, or password.");
        }


        private string GenerateJwtToken(
            Person person
        )
        {
            return GenerateJwtToken(person.Email, person.Role.ToString(), person.UserName,
                person.Id);
        }

        private string GenerateJwtToken(
            string email,
            string role,
            string login,
            long id
        )
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.UniqueName, login),
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim("role", role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJwt;
        }
    }
}