using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Data.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string ArabicName { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }

        public bool IsVerified { get; set; } = false;
        public string? OTP { get; set; }
        public string? PasswordHash { get; set; }
    }
}
