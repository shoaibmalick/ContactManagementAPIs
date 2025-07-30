using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.DTO
{
    /// <summary>
    /// DTO for returning company information.
    /// </summary>
    public class CompanyDto
    {
        public int ID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }
}
