using Dapper;
using Microsoft.Data.SqlClient;
using SuperShop.IRepository;
using SuperShop.Model;

namespace SuperShop.Repository
{
    public class SuperShopRepository : ISuperShopRepository
    {
        private readonly IConfiguration _configuration;
        public SuperShopRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> CreateUserAsync(UserModel userModel)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[TblUser]
                           ([UserTypeId]
                           ,[UserName]
                           ,[Password]
                           ,[UserFullName]
                           ,[IsActive])
                            VALUES
                           (@UserTypeId
                           ,@UserName
                           ,@Password
                           ,@UserFullName
                           ,@ConnectionId
                           ,@IsActive)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, userModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> CreateLogAsync(LogModel logModel)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[Log]
                           ([TableId]
                           ,[ActionBy]
                           ,[ActionDate]
                           ,[ActionChanges]
                           ,[JsonPayload]
                           ,[IsActive]
                           ,[ActionType])
                           VALUES
                           (@TableId
                           ,@ActionBy
                           ,@ActionDate
                           ,@ActionChanges
                           ,@JsonPayload
                           ,@IsActive
                           ,@ActionType)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, logModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
