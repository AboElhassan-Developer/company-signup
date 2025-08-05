using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Services.DTOs
{
    public class SetPasswordDto
    {
        public int CompanyId { get; set; }
        [Required, MinLength(8), MaxLength(32)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Password must contain at least one uppercase, one digit, and one special character.")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
