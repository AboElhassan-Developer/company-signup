using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySignUpSystem.Services.DTOs
{
    public class UploadLogoDto
    {
        public IFormFile Logo { get; set; }
    }
}
