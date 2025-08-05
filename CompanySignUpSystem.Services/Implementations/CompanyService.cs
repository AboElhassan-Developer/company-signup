using CompanySignUpSystem.Data.Entities;
using CompanySignUpSystem.Repository.Interfaces;
using CompanySignUpSystem.Services.Interfaces;
using CompanySignUpSystem.Services.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CompanySignUpSystem.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmailService _emailService;
        public CompanyService(ICompanyRepository companyRepository, IEmailService emailService)
        {
            _companyRepository = companyRepository;
            _emailService = emailService;
        }
        public async Task<Company> SignUpAsync(CompanySignUpDto dto)
        {
         
            if (string.IsNullOrWhiteSpace(dto.ArabicName) || string.IsNullOrWhiteSpace(dto.EnglishName))
                throw new Exception("Arabic and English company names are required.");

            
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required.");

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(dto.Email, emailPattern))
                throw new Exception("Email is not valid.");

            var existing = await _companyRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email already in use.");

            
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                var phoneRegex = new Regex(@"^01[0125][0-9]{8}$");
                if (!phoneRegex.IsMatch(dto.PhoneNumber))
                    throw new Exception("Phone number is not valid. Must be a valid Egyptian mobile number.");
            }
            if (!string.IsNullOrWhiteSpace(dto.WebsiteUrl))
            {
                var isValidUrl = Uri.TryCreate(dto.WebsiteUrl, UriKind.Absolute, out var _)
                                 && (dto.WebsiteUrl.StartsWith("http://") || dto.WebsiteUrl.StartsWith("https://"));
                if (!isValidUrl)
                    throw new Exception("Website URL is not valid.");
            }
            if (!string.IsNullOrWhiteSpace(dto.LogoUrl))
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var ext = Path.GetExtension(dto.LogoUrl).ToLower();
                if (!allowedExtensions.Contains(ext))
                    throw new Exception("Logo URL must point to an image file (.jpg, .jpeg, .png, .gif).");
            }



            string otp = new Random().Next(100000, 999999).ToString();

            var company = new Company
            {
                ArabicName = dto.ArabicName,
                EnglishName = dto.EnglishName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                WebsiteUrl = dto.WebsiteUrl,
                LogoUrl = dto.LogoUrl,
                OTP = otp,
                IsVerified = false
            };

            var createdCompany = await _companyRepository.AddAsync(company);
            await _emailService.SendEmailAsync(dto.Email, "Your OTP Code", $"Your OTP is: {otp}");

            return createdCompany;

        }


        public async Task<bool> ValidateOtpAsync(OtpValidationDto dto)
        {
            var company = await _companyRepository.GetByIdAsync(dto.CompanyId);
            if (company == null || company.IsVerified)
                return false;

            if (company.OTP != dto.OTP)
                return false;

            company.IsVerified = true;
            company.OTP = null; 
            await _companyRepository.UpdateAsync(company);

            return true;
        }

        public async Task<bool> SetPasswordAsync(SetPasswordDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return false;

            if (!IsStrongPassword(dto.Password))
                return false;

            var company = await _companyRepository.GetByIdAsync(dto.CompanyId);
            if (company == null || !company.IsVerified)
                return false;

            company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _companyRepository.UpdateAsync(company);

            return true;
        }

        public async Task<LoginResultDto?> LoginAsync(LoginDto dto)
        {
            var company = await _companyRepository.GetByEmailAsync(dto.Email);
            if (company == null || !company.IsVerified)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, company.PasswordHash);
            if (!isPasswordValid)
                return null;

            return new LoginResultDto
            {
                CompanyName = company.EnglishName,
                LogoPath = company.LogoUrl ?? string.Empty
            };
        }



        private bool IsStrongPassword(string password)
        {
            if (password.Length < 6)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasDigit && hasSpecial;
        }


        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _companyRepository.GetByIdAsync(id);
        }

        public async Task<Company?> GetCompanyByEmailAsync(string email)
        {
            return await _companyRepository.GetByEmailAsync(email);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            return await _companyRepository.AddAsync(company);
        }


        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            return await _companyRepository.UpdateAsync(company);
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
             await _companyRepository.DeleteAsync(id);
            return true;
        }
    }
}

