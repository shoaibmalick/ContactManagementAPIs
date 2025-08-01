using AutoMapper;
using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Interfaces;
using EmployeeContactSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {

        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService, IMapper mapper, ILogger<CompaniesController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanies([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try 
            {
                _logger.LogInformation("Fetching all companies.");
                var companies = await _companyService.GetCompanies(search, page, pageSize);

                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch companies.");
                return StatusCode(500, "An unexpected error occurred while fetching companies.");
            }
        }
    }
}
