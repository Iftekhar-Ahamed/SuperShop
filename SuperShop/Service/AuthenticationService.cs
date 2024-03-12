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
        public async Task<MessageHelperModel> LogInUser(string UserName, string PassWord)
        {
            var msg = new MessageHelperModel();
            var user = await _unitOfWorkRepository.AuthenticationRepository.UserLogInAsync(UserName, PassWord);
            
            if (user == null)
            {
                msg.Message = "Invalid UserName";
                msg.StatusCode = 401;
            }
            else
            {
                if (user.Password == PassWord)
                {
                    msg.Message = "Welcome User";
                    msg.Token = GenerateToken(user,"Access") + " "+ GenerateToken(user, "Refresh");
                    msg.data = user;
                    msg.StatusCode = 200;
                }
                else
                {
                    msg.Message = "Invalid Password";
                    msg.StatusCode = 401;
                }
                user.Password = null;
            }
            return msg;
        }
        public string GenerateToken(UserModel user,string type)
        {
            long lifetime = 0;
            if(type == "Access")
            {
                lifetime = 2;
            }
            else
            {
                lifetime = 10;
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserFullName), new Claim(ClaimTypes.NameIdentifier, (user.Id??0).ToString()) , new Claim("Type", type) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(lifetime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public MessageHelperModel GetNewAccessToken(UserModel userModel)
        {

            var res = GenerateToken(userModel, "Access");
            var msg = new MessageHelperModel();
            if (res != null)
            {
                msg.Token = res;
                msg.Message = "Sucessfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild";
                msg.StatusCode = 400;
            }
            return msg;
        }

    }
}
