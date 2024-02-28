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
        public async Task<long> UpdateUserAsync(UserModel userModel)
        {
            try
            {
                var sql = @"UPDATE [dbo].[TblUser]
                           SET [UserTypeId] = @UserTypeId
                              ,[UserName] = @UserName
                              ,[Password] = @Password
                              ,[UserFullName] = @UserFullName
                              ,[ConnectionId] = @ConnectionId
                              ,[IsActive] = @IsActive
                            WHERE Id = @Id";
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
        public async Task<UserModel?> GetUserByIdAsync(long Id)
        {
            try
            {
                var sql = @"SELECT * FROM dbo.TblUser WHERE Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<UserModel>(sql, new { Id});
                    return result.FirstOrDefault();
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

        public async Task<long> SaveUserConnectionIdAsync(string ConnectionId, long UserId)
        {
            try
            {
                var sql = @"UPDATE SET [ConnectionId] = @ConnectionId [dbo].[TblUser] WHERE [Id] = @UserId";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, new { ConnectionId, UserId});
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<MenuModel?> GetMenuByIdAsync(long MenuId)
        {
            try
            {
                var sql = @"SELECT * FROM dbo.Menu
                            WHERE Id = @MenuId";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<MenuModel>(sql,new { MenuId });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> CreateMenuAsync(MenuModel menuModel)
        {
            try
            {
                menuModel.IsActive = true;
                var sql = @"INSERT INTO [dbo].[Menu]
                           ([MenuName]
                           ,[MenuUrl]
                           ,[IsActive])
                            VALUES
                           (@MenuName
                           ,@MenuUrl
                           ,@IsActive)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, menuModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> UpdateMenuAsync(MenuModel menuModel)
        {
            try
            {
                menuModel.IsActive = true;
                var sql = @"UPDATE [dbo].[Menu]
                            SET [MenuName] = @MenuName
                                ,[MenuUrl] = @MenuUrl
                                ,[IsActive] = @IsActive
                            WHERE Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, menuModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> CreateUserMenuPermissionAsync(MenuUserPermissionModel obj)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[UserMenuPermission]
                           ([MenuId]
                           ,[UserId]
                           ,[IsActive])
                            VALUES
                           (@MenuId
                           ,@UserId
                           ,@IsActive)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, obj);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> UpdateUserMenuPermissionAsync(MenuUserPermissionModel obj)
        {
            try
            {
                var sql = @"UPDATE [dbo].[UserMenuPermission]
                            SET [MenuId] = @MenuId
                                ,[UserId] = @UserId
                                ,[IsActive] = @IsActive
                             WHERE [Id] = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, obj);
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
