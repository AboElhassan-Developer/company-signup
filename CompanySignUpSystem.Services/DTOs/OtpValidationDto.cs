using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Services.DTOs
{
    public class OtpValidationDto
    {
        public int CompanyId { get; set; }
        [Required, StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be 6 digits.")]

        public string OTP { get; set; }
    }
}
