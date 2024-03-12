using Dapper;
using Microsoft.Data.SqlClient;
using SuperShop.CustomException;
using SuperShop.IRepository;
using SuperShop.Model;

namespace SuperShop.Repository
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        public AuthenticationRepository(IConfiguration configuration) {
            _configuration = configuration;
        }
        public async Task<UserModel?> UserLogInAsync(string UserName, string PassWord)
        {
            try
            {
                var sql = "SELECT * FROM dbo.TblUser WHERE UserName = @UserName";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<UserModel>(sql, new { UserName });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomizedException("Someting Went wrong. Please Contact with Admin",400);
            }
        }
    }
}
