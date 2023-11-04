using AddressConsultation.Persistence.Models;
using AddressConsultation.Persistence.Repositories;
using AddressConsultation.Persistence.SqlServer.Infra;
using Dapper;
using SqlLibrary;

namespace AddressConsultation.Persistence.SqlServer.DataAccess
{
    public class UserRepository : IUserRepository<UserModel>
    {
        private readonly string _tableUser;
        private readonly string _columnUserId;
        private readonly string _columnUsername;
        private readonly string _columnPassword;
        private readonly string _columnEmail;
        private readonly string _columnRole;
        private readonly string _columnCreatedAt;
        private readonly string _columnLastUpdated;
        private readonly IDapperDbContext _dbContext;
        private readonly SqlQueryBuilder _queryBuilder;

        public UserRepository(IDapperDbContext dbContext)
        {
            _dbContext = dbContext;
            _queryBuilder = SqlQueryBuilderFactory.Create();
            _tableUser = "[User]";
            _columnUserId = "UserId";
            _columnUsername = "Username";
            _columnPassword = "Password";
            _columnEmail = "Email";
            _columnRole = "Role";
            _columnCreatedAt = "CreatedAt";
            _columnLastUpdated = "LastUpdated";
        }

        public async Task<UserModel> FindByUsernameAsync(string username)
        {
            _queryBuilder
                .Select("*")
                .From(_tableUser)
                .Where($"{_columnUsername} = @Username");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<UserModel>(sqlQuery, new { Username = username });
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            _queryBuilder
                .Select("*")
                .From(_tableUser)
                .Where($"{_columnEmail} = @Email");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<UserModel>(sqlQuery, new { Email = email });
        }

        public async Task InsertAsync(UserModel entity)
        {
            _queryBuilder
                .InsertInto(_tableUser, _columnUserId, _columnUsername, _columnPassword, _columnEmail, _columnRole, _columnCreatedAt, _columnLastUpdated)
                .Values("@UserId", "@Username", "@Password", "@Email", "@Role", "@CreatedAt", "@LastUpdated");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            await connection.ExecuteAsync(sqlQuery, entity);

        }
        public async Task UpdateAsync(UserModel entity, Guid userId)
        {
            _queryBuilder
                .Update(_tableUser)
                .Set($"{_columnUsername} = @Username", $"{_columnPassword} = @Password", $"{_columnEmail} = @Email", $"{_columnRole} = @Role", $"{_columnCreatedAt} = @CreatedAt", $"{_columnLastUpdated} = @LastUpdated")
                .Where($"{_columnUserId} = @UserId");

            string sqlQuery = _queryBuilder.Build();
            

            using var connection = _dbContext.GetConnection();
            await connection.ExecuteAsync(sqlQuery, new
            {
                Username = entity.Username,
                Password = entity.Password,
                Email = entity.Email,
                Role = entity.Role,
                CreatedAt = entity.CreatedAt,
                LastUpdated = entity.LastUpdated,
                UserId = userId
            });
        }

        public async Task DeleteAsync(Guid userId)
        {
            _queryBuilder
                .DeleteFrom(_tableUser)
                .Where($"{_columnUserId} = @UserId");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            await connection.ExecuteAsync(sqlQuery, new { UserId = userId });
        }
    }
}
