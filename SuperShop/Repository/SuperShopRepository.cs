﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        public async Task<AllUserInformationViewModel?> GetUserInformationByIdAsync(long Id)
        {
            try
            {
                var sql = @"SELECT u.[Id]
                                  ,u.[UserTypeId]
                                  ,u.[UserName]
                                  ,u.[Password]
                                  ,u.[UserFullName]
                                  ,u.[ConnectionId]
                                  ,t.[UserTypeName]
                                  ,u.[IsActive]
                            FROM dbo.TblUser u
                            JOIN [dbo].[UserType] t ON u.UserTypeId = t.Id
                            WHERE 
                                u.Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<AllUserInformationViewModel>(sql, new { Id });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> DeleteUserById(long Id)
        {
            try
            {
                var sql = @"DELETE FROM [dbo].[TblUser] WHERE Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, new { Id });
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AllUserInformationViewModel>> GetAllUserAsync(GetDataConfigModel getDataConfigModel)
        {
            try
            {
                long t = 0;
                long.TryParse(getDataConfigModel.SearchTerm, out t);
                getDataConfigModel.NumSearchTerm = t;


                var sql = @"SELECT u.[Id]
                                  ,u.[UserTypeId]
                                  ,u.[UserName]
                                  ,u.[Password]
                                  ,u.[UserFullName]
                                  ,u.[ConnectionId]
                                  ,t.[UserTypeName]
                                  ,u.[IsActive]
                            FROM dbo.TblUser u
                            JOIN [dbo].[UserType] t ON u.UserTypeId = t.Id
                            WHERE 
                                (ISNULL(@IsActive,1) = 1 or @IsActive = u.IsActive)
                                and (u.Id = @NumSearchTerm or u.UserFullName LIKE '%' + @SearchTerm + '%')";
                if (getDataConfigModel.OrderBy != null && getDataConfigModel.OrderColumn != null)
                {
                    var allowedColumns = new[] { "UserFullName", "UserName", "Id", "UserTypeId" }; 
                    if (allowedColumns.Contains(getDataConfigModel.OrderColumn, StringComparer.OrdinalIgnoreCase))
                    {
                        sql += $" ORDER BY {getDataConfigModel.OrderColumn} {getDataConfigModel.OrderBy}";
                    }
                }
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<AllUserInformationViewModel>(sql, getDataConfigModel);
                    return result.ToList();
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
        public async Task<int> CreateItemTransactionTypeAsync(ItemTransactionTypeModel obj)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[ItemTransactionType]
                           ([TransactionName]
                           ,[IsActive])
                            VALUES
                           (@TransactionName
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
        public async Task<int> UpdateItemTransactionTypeAsync(ItemTransactionTypeModel obj)
        {
            try
            {
                var sql = @"UPDATE [dbo].[ItemTransactionType]
                            SET [TransactionName] = @TransactionName
                                ,[IsActive] = @IsActive
                            WHERE Id = @Id";
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
        public async Task<ItemTransactionTypeModel?> GetItemTransactionTypeAsync(long Id,bool? IsActive)
        {
            try
            {
                var sql = @"SELECT * FROM dbo.ItemTransactionType WHERE Id = @Id and (ISNULL(@IsActive,1)=1 or IsActive = @IsActive)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<ItemTransactionTypeModel>(sql, new { Id, IsActive});
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<ItemTransactionTypeModel>?> GetAllItemTransactionTypeAsync()
        {
            try
            {
                var sql = @"SELECT * FROM dbo.ItemTransactionType WHERE IsActive = 1";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<ItemTransactionTypeModel>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<MenuModel>> GetMenuPermissionByUserIdAsync(long UserId)
        {
            try
            {
                var sql = @"SELECT * FROM [dbo].[UserMenuPermission] mp
                            JOIN [dbo].[Menu] m ON m.Id = mp.MenuId
                            WHERE mp.UserId = @UserId and mp.IsActive = 1 and m.IsActive = 1";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<MenuModel>(sql,new {UserId });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetAllMenuPermissionModel>> GetAllMenuPermissionAsync(GetDataConfigModel getDataConfigModel)
        {
            try
            {
                long t = 0;
                long.TryParse(getDataConfigModel.SearchTerm, out t);
                getDataConfigModel.NumSearchTerm = t;

                var sql = @"SELECT mp.id,u.id as UserId,u.UserFullName,m.id as MenuId,m.MenuName,mp.IsActive 
                            FROM [dbo].[UserMenuPermission] mp
                            JOIN [dbo].[Menu] m ON m.Id = mp.MenuId
                            JOIN [dbo].[tblUser] u ON u.Id = mp.UserId
                            WHERE  
                            m.IsActive = 1 
                            and u.IsActive = 1
                            and (ISNULL(@IsActive,1) = 1 or @IsActive = mp.IsActive)
                            and (u.Id = @NumSearchTerm or m.Id = @NumSearchTerm or u.UserFullName LIKE '%' + @SearchTerm + '%')";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<GetAllMenuPermissionModel>(sql, getDataConfigModel);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GetAllMenuPermissionModel> GetMenuPermissionByIdAsync(long MenuPermissionId)
        {
            try
            {

                var sql = @"SELECT mp.id,u.id as UserId,u.UserFullName,m.id as MenuId,m.MenuName,mp.IsActive 
                            FROM [dbo].[UserMenuPermission] mp
                            JOIN [dbo].[Menu] m ON m.Id = mp.MenuId
                            JOIN [dbo].[tblUser] u ON u.Id = mp.UserId
                            WHERE  
                            m.IsActive = 1 
                            and u.IsActive = 1
                            and mp.Id = @MenuPermissionId ";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<GetAllMenuPermissionModel>(sql, new { MenuPermissionId });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<MenuModel>> GetAllMenusAsync(GetDataConfigModel getDataConfigModel)
        {
            try
            {
                long t = 0;
                long.TryParse(getDataConfigModel.SearchTerm, out t);
                getDataConfigModel.NumSearchTerm = t;

                var sql = @"SELECT * FROM [dbo].[Menu]
                            WHERE 
                                (ISNULL(@IsActive,1) = 1 or @IsActive = IsActive)
                                and (Id = @NumSearchTerm or MenuName LIKE '%' + @SearchTerm + '%')";

                if (getDataConfigModel.OrderBy != null && getDataConfigModel.OrderColumn != null)
                {
                    var allowedColumns = new[] { "MenuName", "Id"}; 
                    if (allowedColumns.Contains(getDataConfigModel.OrderColumn, StringComparer.OrdinalIgnoreCase))
                    {
                        sql += $" ORDER BY {getDataConfigModel.OrderColumn} {getDataConfigModel.OrderBy}";
                    }
                };

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<MenuModel>(sql,getDataConfigModel);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<UserTypeModel>> GetUserTypeAsync()
        {
            try
            {
                var sql = @"SELECT [Id]
                                  ,[UserTypeName]
                                  ,[IsActive]
                            FROM [dbo].[UserType]
                            WHERE IsActive = 1";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<UserTypeModel>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> DeleteMenuById(long Id)
        {
            try
            {
                var sql = @"DELETE FROM [dbo].[Menu] WHERE Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, new { Id });
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
