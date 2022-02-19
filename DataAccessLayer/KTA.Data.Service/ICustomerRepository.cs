using KTA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Service
{
    public interface ICustomerRepository : IRepositoryBase<CustomerEntity>
    {
        Task<CustomerEntity> GetSingleItemAsync(string custId);
    }
}
