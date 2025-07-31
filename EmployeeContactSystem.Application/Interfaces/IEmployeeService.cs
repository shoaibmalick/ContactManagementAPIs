using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeContactSystem.Application.DTO;

namespace EmployeeContactSystem.Application.Interfaces
{
    namespace EmployeeContactSystem.Application.Interfaces
    {
        /// <summary>
        /// Contract for employee-related business operations.
        /// </summary>
        public interface IEmployeeService
        {
            Task<IEnumerable<EmployeeDto>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10);
            Task<EmployeeDto> GetByIdAsync(int id);
            Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
            Task<EmployeeDto> UpdateAsync(int id, CreateEmployeeDto dto);
            Task<bool> DeleteAsync(int id);
        }
    }
}
