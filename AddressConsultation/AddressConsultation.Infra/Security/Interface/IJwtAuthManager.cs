using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Infra.Security.Interface
{
    public interface IJwtAuthManager
    {
        string GenerateToken(UserWithRole user);
    }
}
