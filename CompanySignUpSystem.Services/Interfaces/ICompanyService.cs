using CompanySignUpSystem.Data.Entities;
using CompanySignUpSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company?> GetCompanyByEmailAsync(string email);
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(int id);
        Task<Company> SignUpAsync(CompanySignUpDto dto);
        Task<bool> ValidateOtpAsync(OtpValidationDto dto);
        Task<bool> SetPasswordAsync(SetPasswordDto dto);
        Task<LoginResultDto?> LoginAsync(LoginDto dto);

    }
}
