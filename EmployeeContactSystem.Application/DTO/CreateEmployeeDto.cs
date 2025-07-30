using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.DTO
{
    /// <summary>
    /// DTO used to create or update employee data.
    /// </summary>
    public class CreateEmployeeDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public int CompanyID { get; set; }
    }
}
