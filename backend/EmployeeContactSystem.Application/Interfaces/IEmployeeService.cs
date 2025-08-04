using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeContactSystem.Application.Interfaces
{
        /// <summary>
        /// Contract for employee-related business operations.
        /// </summary>
        public interface IEmployeeService
        {
            Task<PagedResult<EmployeeDto>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10);
            Task<EmployeeDto> GetByIdAsync(int id);
            Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
            Task<EmployeeDto> UpdateAsync(int id, UpdateEmployeeDto dto);
            Task<bool> DeleteAsync(int id);
            Task<bool> ExistsAsync(int id);
        }
}

