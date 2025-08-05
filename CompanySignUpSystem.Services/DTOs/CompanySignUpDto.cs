using System.ComponentModel.DataAnnotations;

namespace CompanySignUpSystem.Services.DTOs
{
    public class CompanySignUpDto
    {
        [Required, MaxLength(100)]
        public string ArabicName { get; set; } 
        [Required, MaxLength(100)]
        public string EnglishName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [ RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Phone number must be an Egyptian number")]

        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }
    }
}
