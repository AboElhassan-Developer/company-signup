using CompanySignUpSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetByEmailAsync(string email);
        Task<Company> AddAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task DeleteAsync(int id);

    }
}
