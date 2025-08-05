using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Services.DTOs
{
    public class LoginResultDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string LogoPath { get; set; } = string.Empty;
    }
}
