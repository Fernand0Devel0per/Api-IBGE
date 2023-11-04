using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Persistence.SqlServer.Infra
{
    public interface IDapperDbContext
    {
        IDbConnection GetConnection();
        Task ExecuteAsync(string sql, object parameters = null);
    }
}
