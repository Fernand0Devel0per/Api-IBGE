using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Persistence.Repositories
{
    public interface IAddressRepository<T> : IBaseRepository<T> where T : class
    {
        Task<T> FindByIBGECodeAsync(string ibgeCode);
        Task<IEnumerable<T>> FindByStateAsync(string state, int pageIndex, int pageSize);
        Task<IEnumerable<T>> FindByCityAsync(string cityName, int pageIndex, int pageSize);
    }
}
