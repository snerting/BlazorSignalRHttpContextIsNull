using Management.Ui.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Management.Ui.Services
{
    public interface IUserManagement
    {
        Task<IList<TenantViewDao>> GetTenantsAsync();
    }
}
