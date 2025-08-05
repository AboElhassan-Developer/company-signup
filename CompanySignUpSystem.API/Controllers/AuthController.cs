using CompanySignUpSystem.Repository.Interfaces;
using CompanySignUpSystem.Services.DTOs;
using CompanySignUpSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySignUpSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyService _companyService;

        public AuthController(ICompanyRepository companyRepository, ICompanyService companyService)
        {
            _companyRepository = companyRepository;
            _companyService = companyService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CompanySignUpDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ArabicName) || string.IsNullOrWhiteSpace(dto.EnglishName))
                return BadRequest("Company name (Arabic and English) is required");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required");

            try
            {
                var company = await _companyService.SignUpAsync(dto);

                return Ok(new
                {
                    Message = "Sign up succeeded. OTP sent to email.",
                    OTP = company.OTP,
                    CompanyId = company.Id
                });
            }
            catch (Exception ex)
            {
                return Conflict(new { Message = ex.Message });
            }
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromBody] OtpValidationDto dto)
        {
            if (dto.CompanyId <= 0 || string.IsNullOrWhiteSpace(dto.OTP))
                return BadRequest("CompanyId and OTP are required.");

            var company = await _companyRepository.GetByIdAsync(dto.CompanyId);
            if (company == null)
                return NotFound("Company not found.");

            if (company.OTP != dto.OTP)
                return BadRequest("Invalid OTP.");

            company.IsVerified = true;
            company.OTP = null;
            await _companyRepository.UpdateAsync(company);

            return Ok(new { Message = "OTP verified successfully. Company is now verified." });
        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto)
        {
            var result = await _companyService.SetPasswordAsync(dto);
            if (!result)
                return BadRequest("Password doesn't meet criteria or company not verified.");

            return Ok(new { message = "Password set successfully. You can now log in." });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and Password are required.");

            var result = await _companyService.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Invalid email, password, or company not verified.");

            return Ok(new
            {
                message = "Login successful",
                companyName = result.CompanyName,
                logoPath = result.LogoPath
            });
        }
    }
}