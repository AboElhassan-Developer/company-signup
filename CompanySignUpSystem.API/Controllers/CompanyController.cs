
using CompanySignUpSystem.Data.Entities;
using CompanySignUpSystem.Repository.Interfaces;
using CompanySignUpSystem.Services.DTOs;
using CompanySignUpSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySignUpSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyRepository.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Company company)
        {
            await _companyRepository.AddAsync(company);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Company updatedCompany)
        {
            var existing = await _companyRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.ArabicName = updatedCompany.ArabicName;
            existing.EnglishName = updatedCompany.EnglishName;
            existing.Email = updatedCompany.Email;
            existing.PhoneNumber = updatedCompany.PhoneNumber;
            existing.WebsiteUrl = updatedCompany.WebsiteUrl;
            existing.LogoUrl = updatedCompany.LogoUrl;
            existing.IsVerified = updatedCompany.IsVerified;
            existing.OTP = updatedCompany.OTP;
            existing.PasswordHash = updatedCompany.PasswordHash;

            await _companyRepository.UpdateAsync(existing);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _companyRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _companyRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
