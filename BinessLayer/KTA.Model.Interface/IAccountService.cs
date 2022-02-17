using KTA.Data.Entity;
using KTA.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface IAccountService : IService
    {
        Task<string> AddAsync(AccountDto dtoItem);
        Task<string> UpdateAsync(AccountDto dtoItem);
        Task<string> DeleteAsync(AccountDto dtoItem);
        
    }
}
