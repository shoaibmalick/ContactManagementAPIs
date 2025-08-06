using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Interfaces;
using EmployeeContactSystem.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ContactManagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] bool? status = null, int companyID = 0)
        {
            try { 
            var result = await _employeeService.GetAllAsync(search, page, pageSize, status, companyID);
            return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch employee list.");
                return StatusCode(500, "An unexpected error occurred while fetching employees.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try 
            { 
                var result = await _employeeService.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Employee ID {EmployeeId} not found.", id);
                    return NotFound($"Employee with ID {id} not found.");
                }

                _logger.LogInformation("Fetched employee ID {EmployeeId}.", id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch employee ID {EmployeeId}.", id);
                return StatusCode(500, "An unexpected error occurred while fetching the employee.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
            {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid employee model state.");
                    return BadRequest(ModelState);
                }

                var result = await _employeeService.CreateAsync(dto);
                _logger.LogInformation("Created employee ID {EmployeeId}.", result.ID);
                return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
            }

            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database conflict when creating employee.");
                return Conflict("Unable to create employee due to a database conflict.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating employee.");
                return StatusCode(500, "An unexpected error occurred while creating the employee.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model for updating employee ID {EmployeeId}.", id);
                    return BadRequest(ModelState);
                }

                //Checking if Employee Exist or not before updating
                bool exists = await _employeeService.ExistsAsync(id);
                if (!exists) return NotFound($"Employee with ID {id} not found.");

                var result = await _employeeService.UpdateAsync(id, dto);

                if (result == null)
                {
                    _logger.LogWarning("Employee ID {EmployeeId} not found for update.", id);
                    return NotFound($"Employee with ID {id} not found.");
                }
                _logger.LogInformation("Updated employee ID {EmployeeId}.", id);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while updating employee ID {EmployeeId}.", id);
                return Conflict("Unable to update employee due to a database conflict.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating employee ID {EmployeeId}.", id);
                return StatusCode(500, "An unexpected error occurred while updating the employee.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Checking if Employee Exist or not before deleting
                bool exists = await _employeeService.ExistsAsync(id);
                if (!exists) return NotFound($"Employee with ID {id} not found.");

                var success = await _employeeService.DeleteAsync(id);

                if (!success)
                {
                    _logger.LogWarning("Employee ID {EmployeeId} not found for deletion.", id);
                    return NotFound();
                }
                _logger.LogInformation("Deleted employee ID {EmployeeId}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting employee ID {EmployeeId}.", id);
                return StatusCode(500, "An unexpected error occurred while deleting the employee.");
            }
        }
    }
}
