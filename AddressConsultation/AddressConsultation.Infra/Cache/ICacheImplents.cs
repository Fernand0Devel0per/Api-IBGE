using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Infra.Cache
{
    public interface ICacheImplents<T>
    {
        string Prefix { get; set; }
        Task<T> Get(string key);
        Task Set(string key, T value);

    }
}
