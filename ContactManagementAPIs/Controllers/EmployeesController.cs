using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Interfaces;
using EmployeeContactSystem.Application.Interfaces.EmployeeContactSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _employeeService.GetAllAsync(search, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            var result = await _employeeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDto dto)
        {
            var result = await _employeeService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _employeeService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
