using Dapper;
using Microsoft.Data.SqlClient;
using SuperShop.CustomException;
using SuperShop.IRepository;
using SuperShop.Model;

namespace SuperShop.Repository
{
    public class LogRepository: ILogRepository
    {
        private readonly IConfiguration _configuration;
        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<long> CreateLogAsync(LogModel logModel)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[tablLog]
                           ([TableId]
                           ,[TablePk]
                           ,[ActionBy]
                           ,[ActionDate]
                           ,[ActionChanges]
                           ,[JsonPayload]
                           ,[IsActive]
                           ,[ActionType]) OUTPUT INSERTED.Id
                           VALUES
                           (@TableId
                           ,@TablePK
                           ,@ActionBy
                           ,@ActionDate
                           ,@ActionChanges
                           ,@JsonPayload
                           ,@IsActive
                           ,@ActionType)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QuerySingleOrDefaultAsync<int>(sql, logModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new CustomizedException("Someting Went wrong. Please Contact with Admin",400);
            }
        }
        public async Task<LogModel> GetLogByIdAsync(GetLogModel logModel)
        {
            try
            {
                var sql = @"SELECT * FROM dbo.tablLog WHERE Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LogModel>(sql, logModel);
                    return result.FirstOrDefault() ?? new LogModel();
                }
            }
            catch (Exception ex)
            {
                throw new CustomizedException("Someting Went wrong. Please Contact with Admin",400);
            }
        }
        public async Task<List<LogModel>> GetAllLogAsync(GetLogModel logModel)
        {
            try
            {
                var sql = @"SELECT * FROM dbo.tablLog 
                            WHERE 
                            (@TableId = 0 or TableId = @TableId)
                            AND (@TablePk = 0 or TablePk = @TablePk)";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LogModel>(sql, logModel);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new CustomizedException("Someting Went wrong. Please Contact with Admin",400);
            }
        }
        public async Task<List<ItemTransactionLogModel>> GetItemLog(GetLogModel logModel)
        {
            try
            {
                var sql = @"SELECT l.ActionChanges,itmt.UnitOfAmount,itmt.UnitPrice,itmt.Total
                            FROM dbo.tablLog l 
                            JOIN dbo.ItemTransaction itmt ON itmt.id = l.tablepk
                            WHERE l.TableId = @TableId
	                              AND l.IsActive = 1 
	                              AND itmt.IsActive = 1 
	                              AND itmt.ItemId = @ItemId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<ItemTransactionLogModel>(sql, logModel);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new CustomizedException("Someting Went wrong. Please Contact with Admin",400);
            }
        }
    }
}
