using Login_Service_with_JWT_Auth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login_Service_with_JWT_Auth.Auth
{
    public class JWTAuth
    {
        private readonly IConfiguration _configuration;

        public JWTAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetJWTToken(UserModel model)
        {
            try
            {
                //create claim based on the login user information
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("UserId",model.Id.ToString()),
                    new Claim("UserName",model.UserName),
                    new Claim("Password",model.Password),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: signIn
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
