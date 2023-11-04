using AddressConsultation.Persistence.Models;
using AddressConsultation.Persistence.Repositories;
using AddressConsultation.Persistence.SqlServer.Infra;
using Dapper;
using SqlLibrary;

namespace AddressConsultation.Persistence.SqlServer.DataAccess
{
    public class AddressRepository : IAddressRepository<AddressModel>
    {
        private readonly string _tableIbge;
        private readonly string _columnId;
        private readonly string _columnCity;
        private readonly string _columnState;
        private readonly IDapperDbContext _dbContext;
        private readonly SqlQueryBuilder _queryBuilder;

        public AddressRepository(IDapperDbContext dbContext)
        {
            _dbContext = dbContext;
            _queryBuilder = SqlQueryBuilderFactory.Create();
            _tableIbge = "IBGE";
            _columnId = "Id";
            _columnCity = "City";
            _columnState = "State";

        }

        public async Task<IEnumerable<AddressModel>> FindByCityAsync(string cityName, int pageIndex, int pageSize)
        {
            _queryBuilder
                .Select("*")
                .From(_tableIbge)
                .Where($"{_columnCity} = @City")
                .OrderBy("Id")
                .Offset((pageIndex - 1) * pageSize)
                .FetchNext(pageSize);

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            return await connection.QueryAsync<AddressModel>(sqlQuery, new { City = cityName });
        }

        public async Task<AddressModel> FindByIBGECodeAsync(string ibgeCode)
        {
            _queryBuilder
                .Select("*")
                .From(_tableIbge)
                .Where($"{_columnId} = @Id");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<AddressModel>(sqlQuery, new { Id = ibgeCode });
        }

        public async Task<IEnumerable<AddressModel>> FindByStateAsync(string state, int pageIndex, int pageSize)
        {
            _queryBuilder
                .Select("*")
                .From(_tableIbge)
                .Where($"{_columnState} = @State")
                .OrderBy("City")
                .Offset((pageIndex - 1) * pageSize)
                .FetchNext(pageSize);

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            return await connection.QueryAsync<AddressModel>(sqlQuery, new { State = state });
        }

        public async Task InsertAsync(AddressModel entity)
        {
            _queryBuilder
                .InsertInto(_tableIbge, _columnId, _columnState, _columnCity)
                .Values("@Id", "@State", "@City");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            await connection.ExecuteAsync(sqlQuery, entity);
        }

        public async Task UpdateAsync(AddressModel entity)
        {
            _queryBuilder
                .Update(_tableIbge)
                .Set($"{_columnState} = @State", $"{_columnCity} = @City")
                .Where($"{_columnId} = @Id");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            await connection.ExecuteAsync(sqlQuery, entity);
        }

        public async Task DeleteAsync(string entityId)
        {
            _queryBuilder
                .DeleteFrom(_tableIbge)
                .Where($"{_columnId} = @Id");

            string sqlQuery = _queryBuilder.Build();

            using var connection = _dbContext.GetConnection();
            await connection.ExecuteAsync(sqlQuery, new { Id = entityId });
        }
    }
}
