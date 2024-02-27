using SuperShop.IService;
using SuperShop.IRepository;
using SuperShop.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperShop.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IUnitOfWorkRepository unitOfWorkRepository, IConfiguration configuration)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _configuration = configuration;
         }
        public async Task<KeyValuePair<UserModel, MessageHelperModel>> LogInUser(string UserName, string PassWord)
        {
            var user = await _unitOfWorkRepository.AuthenticationRepository.UserLogInAsync(UserName, PassWord);
            var msg = new MessageHelperModel();
            if (user == null)
            {
                msg.Message = "Invalid UserName";
            }
            else
            {
                if (user.Password == PassWord)
                {
                    msg.Message = "Welcome User";
                    msg.Token = GenerateToken(user);
                }
                else
                {
                    msg.Message = "Invalid Password";
                }
                user.Password = null;
            }
            return KeyValuePair.Create(user, msg);
        }
        public string GenerateToken(UserModel user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserFullName), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
