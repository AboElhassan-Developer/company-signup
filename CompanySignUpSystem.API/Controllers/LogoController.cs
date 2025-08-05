using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CompanySignUpSystem.Services.DTOs;
namespace CompanySignUpSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public LogoController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadLogo([FromForm] UploadLogoDto dto)
        {
            var logo = dto.Logo;

            if (logo == null || logo.Length == 0)
                return BadRequest("No file uploaded.");

            if (logo.Length > 2 * 1024 * 1024)
                return BadRequest("Maximum allowed file size is 2MB.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(logo.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("Only image files are allowed.");

            var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsPath = Path.Combine(rootPath, "Uploads", "Logos");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await logo.CopyToAsync(stream);
            }

            var logoUrl = $"{Request.Scheme}://{Request.Host}/Uploads/Logos/{uniqueFileName}";

            return Ok(new { logoUrl });
        }


    }
}